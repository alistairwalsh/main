﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Solder.Context;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting.Tool
{
	public sealed class ToolHost : IToolHost
	{
		#region Constructors/Destructors

		public ToolHost()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly string TOOL_HOST_CURRENT_KEY = typeof(ToolHost).GUID.SafeToString();
		private readonly IDictionary<DictionaryConfiguration, IDictionaryAdapter> dictionaryConfigurationToAdapterMappings = new Dictionary<DictionaryConfiguration, IDictionaryAdapter>();
		private readonly IDictionary<string, IDictionary<long, object>> substitutionCacheRoot = new Dictionary<string, IDictionary<long, object>>();

		#endregion

		#region Properties/Indexers/Events

		public static IToolHost Current
		{
			get
			{
				return DefaultContextualStorageFactory.Instance.GetContextualStorage().GetValue<IToolHost>(TOOL_HOST_CURRENT_KEY);
			}
			set
			{
				DefaultContextualStorageFactory.Instance.GetContextualStorage().SetValue<IToolHost>(TOOL_HOST_CURRENT_KEY, value);
			}
		}

		public IDictionary<DictionaryConfiguration, IDictionaryAdapter> DictionaryConfigurationToAdapterMappings
		{
			get
			{
				return this.dictionaryConfigurationToAdapterMappings;
			}
		}

		public IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get
			{
				return this.substitutionCacheRoot;
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

		public void Dispose()
		{
			this.SubstitutionCacheRoot.Clear();
			this.DictionaryConfigurationToAdapterMappings.Clear();
		}

		public void Host(string sourceFilePath)
		{
			ObfuscationConfiguration obfuscationConfiguration;

			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;
			IEnumerable<Message> messages;

			sourceFilePath = Path.GetFullPath(sourceFilePath);
			obfuscationConfiguration = OxymoronEngine.FromJsonFile<ObfuscationConfiguration>(sourceFilePath);

			messages = obfuscationConfiguration.Validate();

			if (messages.Any())
				throw new ApplicationException(string.Format("Obfuscation configuration validation failed:\r\n{0}", string.Join("\r\n", messages.Select(m => m.Description).ToArray())));

			using (DisposableList<IDictionaryAdapter> dictionaryAdapters = new DisposableList<IDictionaryAdapter>())
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
				{
					IDictionaryAdapter dictionaryAdapter = (IDictionaryAdapter)Activator.CreateInstance(dictionaryConfiguration.DictionaryAdapterConfiguration.GetAdapterType());

					dictionaryAdapters.Add(dictionaryAdapter);
					dictionaryAdapter.Initialize(obfuscationConfiguration);

					if (dictionaryConfiguration.PreloadEnabled)
						dictionaryAdapter.InitializePreloadCache(dictionaryConfiguration, this.SubstitutionCacheRoot);

					this.DictionaryConfigurationToAdapterMappings.Add(dictionaryConfiguration, dictionaryAdapter);
				}

				using (ISourceAdapter sourceAdapter = (ISourceAdapter)Activator.CreateInstance(obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType()))
				{
					sourceAdapter.Initialize(obfuscationConfiguration);

					using (IDestinationAdapter destinationAdapter = (IDestinationAdapter)Activator.CreateInstance(obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterType()))
					{
						destinationAdapter.Initialize(obfuscationConfiguration);
						destinationAdapter.UpstreamMetadata = sourceAdapter.UpstreamMetadata;

						sourceDataEnumerable = sourceAdapter.PullData(obfuscationConfiguration.TableConfiguration);
						sourceDataEnumerable = WrapRecordCounter(sourceDataEnumerable, (x, y, z) => Console.WriteLine("{0} {1} {2}", x, y, z));
						destinationAdapter.PushData(obfuscationConfiguration.TableConfiguration, sourceDataEnumerable);
					}
				}
			}
		}

		#endregion
	}
}