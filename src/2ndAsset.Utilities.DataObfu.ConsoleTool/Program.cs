/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.Utilities.DataObfu.ConsoleTool
{
	/// <summary>
	/// Entry point class for the application.
	/// </summary>
	internal class Program : ConsoleApplicationFascade
	{
		#region Fields/Constants

		private const string CMDLN_TOKEN_SOURCEFILE = "sourcefile";

		#endregion

		#region Methods/Operators

		/// <summary>
		/// The entry point method for this application.
		/// </summary>
		/// <param name="args"> The command line arguments passed from the executing environment. </param>
		/// <returns> The resulting exit code. </returns>
		[STAThread]
		public static int Main(string[] args)
		{
			args = new[]
					{
						@"-sourcefile:DB_to_DB_Example.json",
						@"-sourcefile2:DTF_to_DTF_Example.json"
					};

			using (Program program = new Program())
				return program.EntryPoint(args);
		}

		private void Execute(string sourceFilePath)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;
			IEnumerable<Message> messages;

			sourceFilePath = Path.GetFullPath(sourceFilePath);
			obfuscationConfiguration = OxymoronEngine.FromJsonFile<ObfuscationConfiguration>(sourceFilePath);

			messages = obfuscationConfiguration.Validate();

			if (messages.Any())
				throw new ApplicationException(string.Format("Obfuscation configuration validation failed:\r\n{0}", string.Join("\r\n", messages.Select(m => m.Description).ToArray())));

			using (ISourceAdapter sourceAdapter = (ISourceAdapter)Activator.CreateInstance(obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType()))
			{
				sourceAdapter.Initialize(obfuscationConfiguration);

				using (IDestinationAdapter destinationAdapter = (IDestinationAdapter)Activator.CreateInstance(obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterType()))
				{
					destinationAdapter.Initialize(obfuscationConfiguration);
					destinationAdapter.UpstreamMetadata = sourceAdapter.UpstreamMetadata;

					sourceDataEnumerable = sourceAdapter.PullData(obfuscationConfiguration.TableConfiguration);
					sourceDataEnumerable = WrapConsoleRecordCounter(sourceDataEnumerable, (x, y, z) => Console.WriteLine("{0} {1} {2}", x, y, z));
					destinationAdapter.PushData(obfuscationConfiguration.TableConfiguration, sourceDataEnumerable);
				}
			}
		}

		private static IEnumerable<IDictionary<string, object>> WrapConsoleRecordCounter(IEnumerable<IDictionary<string, object>> records, Action<long, bool, double> recordProcessCallback)
		{
			long recordCount = 0;
			DateTime startUtc;

			startUtc = DateTime.UtcNow;

			if ((object)records == null)
				throw new ArgumentNullException("records");

			foreach (IDictionary<string, object> record in records)
			{
				recordCount++;

				if ((recordCount % 1000) == 0)
				{
					//Thread.Sleep(250);
					if ((object)recordProcessCallback != null)
						recordProcessCallback(recordCount, false, (DateTime.UtcNow - startUtc).TotalSeconds);
				}

				yield return record;
			}

			if ((object)recordProcessCallback != null)
				recordProcessCallback(recordCount, true, (DateTime.UtcNow - startUtc).TotalSeconds);
		}

		protected override IDictionary<string, ArgumentSpec> GetArgumentMap()
		{
			IDictionary<string, ArgumentSpec> argumentMap;

			argumentMap = new Dictionary<string, ArgumentSpec>();
			argumentMap.Add(CMDLN_TOKEN_SOURCEFILE, new ArgumentSpec<string>(true, true));

			return argumentMap;
		}

		protected override int OnStartup(string[] args, IDictionary<string, IList<object>> arguments)
		{
			string sourceFilePath;

			if ((object)args == null)
				throw new ArgumentNullException("args");

			if ((object)arguments == null)
				throw new ArgumentNullException("arguments");

			sourceFilePath = (string)arguments[CMDLN_TOKEN_SOURCEFILE].Single();

			this.Execute(sourceFilePath);

			return 0;
		}

		#endregion
	}
}