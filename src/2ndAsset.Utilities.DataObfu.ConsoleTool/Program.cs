/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
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
						@"-sourcefile:DB_to_DB_Example.json"
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
			obfuscationConfiguration = ObfuscationMixIn.FromJsonFile<ObfuscationConfiguration>(sourceFilePath);

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
					destinationAdapter.PushData(obfuscationConfiguration.TableConfiguration, sourceDataEnumerable);
				}
			}
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