/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting.Tool
{
	public sealed class ToolHost : OxymoronHost, IToolHost
	{
		#region Constructors/Destructors

		public ToolHost()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<DictionaryConfiguration, IDictionaryAdapter> dictionaryConfigurationToAdapterMappings = new Dictionary<DictionaryConfiguration, IDictionaryAdapter>();

		#endregion

		#region Properties/Indexers/Events

		private IDictionary<DictionaryConfiguration, IDictionaryAdapter> DictionaryConfigurationToAdapterMappings
		{
			get
			{
				return this.dictionaryConfigurationToAdapterMappings;
			}
		}

		#endregion

		#region Methods/Operators

		private static IEnumerable<IDictionary<string, object>> WrapRecordCounter(IEnumerable<IDictionary<string, object>> records, Action<long, bool, double> recordProcessCallback)
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

		protected override object CoreGetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			return this.DictionaryConfigurationToAdapterMappings[dictionaryConfiguration].GetAlternativeValueFromId(dictionaryConfiguration, metaColumn, surrogateId);
		}

		public void Host(ObfuscationConfiguration obfuscationConfiguration, Action<long, bool, double> statusCallback)
		{
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;
			IEnumerable<Message> messages;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			messages = obfuscationConfiguration.Validate();

			if (messages.Any())
				throw new ApplicationException(string.Format("Obfuscation configuration validation failed:\r\n{0}", string.Join("\r\n", messages.Select(m => m.Description).ToArray())));

			using (IOxymoronEngine oxymoronEngine = new OxymoronEngine(this, obfuscationConfiguration))
			{
				using (DisposableList<IDictionaryAdapter> dictionaryAdapters = new DisposableList<IDictionaryAdapter>())
				{
					foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
					{
						IDictionaryAdapter dictionaryAdapter;

						dictionaryAdapter = dictionaryConfiguration.DictionaryAdapterConfiguration.GetAdapterInstance<IDictionaryAdapter>();
						dictionaryAdapters.Add(dictionaryAdapter);
						dictionaryAdapter.Initialize(dictionaryConfiguration.DictionaryAdapterConfiguration);

						dictionaryAdapter.InitializePreloadCache(dictionaryConfiguration, oxymoronEngine.SubstitutionCacheRoot);

						this.DictionaryConfigurationToAdapterMappings.Add(dictionaryConfiguration, dictionaryAdapter);
					}

					using (ISourceAdapter sourceAdapter = obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterInstance<ISourceAdapter>())
					{
						sourceAdapter.Initialize(obfuscationConfiguration.SourceAdapterConfiguration);

						using (IDestinationAdapter destinationAdapter = obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterInstance<IDestinationAdapter>())
						{
							destinationAdapter.Initialize(obfuscationConfiguration.DestinationAdapterConfiguration);
							destinationAdapter.UpstreamMetadata = sourceAdapter.UpstreamMetadata;

							sourceDataEnumerable = sourceAdapter.PullData(obfuscationConfiguration.TableConfiguration);
							sourceDataEnumerable = oxymoronEngine.GetObfuscatedValues(sourceDataEnumerable);

							if ((object)statusCallback != null)
								sourceDataEnumerable = WrapRecordCounter(sourceDataEnumerable, statusCallback);

							destinationAdapter.PushData(obfuscationConfiguration.TableConfiguration, sourceDataEnumerable);
						}
					}
				}
			}
		}

		public void Host(string sourceFilePath)
		{
			ObfuscationConfiguration obfuscationConfiguration;

			sourceFilePath = Path.GetFullPath(sourceFilePath);
			obfuscationConfiguration = OxymoronEngine.FromJsonFile<ObfuscationConfiguration>(sourceFilePath);

			this.Host(obfuscationConfiguration, (x, y, z) => Console.WriteLine("{0} {1} {2}", x, y, z));
		}

		public bool TryGetUpstreamMetadata(ObfuscationConfiguration obfuscationConfiguration, out IEnumerable<IMetaColumn> metaColumns)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			metaColumns = null;

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
				(object)obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType() != null)
			{
				using (ISourceAdapter sourceAdapter = obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterInstance<ISourceAdapter>())
				{
					sourceAdapter.Initialize(obfuscationConfiguration.SourceAdapterConfiguration);
					metaColumns = sourceAdapter.UpstreamMetadata;
				}
			}

			return true;
		}

		#endregion
	}
}