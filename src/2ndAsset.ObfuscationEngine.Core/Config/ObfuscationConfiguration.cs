/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Strategy;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class ObfuscationConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public ObfuscationConfiguration()
		{
			this.dictionaryConfigurations = new ConfigurationObjectCollection<DictionaryConfiguration>(this);
		}

		#endregion

		#region Fields/Constants

		private static readonly Version currentConfigurationVersion = new Version(1, 0, 0, 0);
		private static readonly Version currentEngineVersion = new Version(1, 0, 0, 0);
		private readonly ConfigurationObjectCollection<DictionaryConfiguration> dictionaryConfigurations;
		private Version configurationVersion;
		private AdapterConfiguration destinationAdapterConfiguration;
		private Version engineVersion;
		private HashConfiguration hashConfiguration;
		private string performanceCriticalStrategyAqtn;
		private AdapterConfiguration sourceAdapterConfiguration;
		private TableConfiguration tableConfiguration;

		#endregion

		#region Properties/Indexers/Events

		public static Version CurrentConfigurationVersion
		{
			get
			{
				return currentConfigurationVersion;
			}
		}

		public static Version CurrentEngineVersion
		{
			get
			{
				return currentEngineVersion;
			}
		}

		public ConfigurationObjectCollection<DictionaryConfiguration> DictionaryConfigurations
		{
			get
			{
				return this.dictionaryConfigurations;
			}
		}

		public Version ConfigurationVersion
		{
			get
			{
				return this.configurationVersion;
			}
			set
			{
				this.configurationVersion = value;
			}
		}

		public AdapterConfiguration DestinationAdapterConfiguration
		{
			get
			{
				return this.destinationAdapterConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.destinationAdapterConfiguration, value);
				this.destinationAdapterConfiguration = value;
			}
		}

		public Version EngineVersion
		{
			get
			{
				return this.engineVersion;
			}
			set
			{
				this.engineVersion = value;
			}
		}

		public HashConfiguration HashConfiguration
		{
			get
			{
				return this.hashConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.hashConfiguration, value);
				this.hashConfiguration = value;
			}
		}

		[JsonIgnore]
		public new ObfuscationConfiguration Parent
		{
			get
			{
				return this;
			}
			set
			{
			}
		}

		public string PerformanceCriticalStrategyAqtn
		{
			get
			{
				return this.performanceCriticalStrategyAqtn;
			}
			set
			{
				this.performanceCriticalStrategyAqtn = value;
			}
		}

		public AdapterConfiguration SourceAdapterConfiguration
		{
			get
			{
				return this.sourceAdapterConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.sourceAdapterConfiguration, value);
				this.sourceAdapterConfiguration = value;
			}
		}

		public TableConfiguration TableConfiguration
		{
			get
			{
				return this.tableConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.tableConfiguration, value);
				this.tableConfiguration = value;
			}
		}

		#endregion

		#region Methods/Operators

		public IPerformanceCriticalStrategy GetPerformanceCriticalStrategyInstance()
		{
			IPerformanceCriticalStrategy instance;
			Type type;

			type = this.GetPerformanceCriticalStrategyType();

			if ((object)type == null)
				return null;

			instance = (IPerformanceCriticalStrategy)Activator.CreateInstance(type);

			return instance;
		}

		public Type GetPerformanceCriticalStrategyType()
		{
			Type type;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.PerformanceCriticalStrategyAqtn))
				return null;

			type = Type.GetType(this.PerformanceCriticalStrategyAqtn, false);

			return type;
		}

		public override IEnumerable<Message> Validate()
		{
			List<Message> messages;
			int index;
			Type type;
			IPerformanceCriticalStrategy performanceCriticalStrategy;
			const string SRC_CONTEXT = "Source";
			const string DST_CONTEXT = "Destination";

			messages = new List<Message>();

			if ((object)this.ConfigurationVersion == null ||
				this.ConfigurationVersion != CurrentConfigurationVersion)
				messages.Add(NewError("Configuration version is invalid."));

			if ((object)this.EngineVersion == null ||
				this.EngineVersion != CurrentEngineVersion)
				messages.Add(NewError("Engine version is invalid."));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.PerformanceCriticalStrategyAqtn))
				new object(); //messages.Add(NewError(string.Format("Performance critical strategy AQTN is required.")));
			else
			{
				type = this.GetPerformanceCriticalStrategyType();

				if ((object)type == null)
					messages.Add(NewError(string.Format("Performance critical strategy failed to load type from AQTN.")));
				else if (typeof(IObfuscationStrategy).IsAssignableFrom(type))
				{
					performanceCriticalStrategy = this.GetPerformanceCriticalStrategyInstance();

					if ((object)performanceCriticalStrategy == null)
						messages.Add(NewError(string.Format("Performance critical strategy failed to instatiate type from AQTN.")));
				}
				else
					messages.Add(NewError(string.Format("Performance critical strategy loaded an unrecognized type via AQTN.")));
			}

			if ((object)this.TableConfiguration == null)
				messages.Add(NewError("Table configuration is required."));
			else
				messages.AddRange(this.TableConfiguration.Validate());

			if ((object)this.SourceAdapterConfiguration == null)
				messages.Add(NewError("Source adapter configuration is required."));
			else
				messages.AddRange(this.SourceAdapterConfiguration.Validate(SRC_CONTEXT));

			if ((object)this.DestinationAdapterConfiguration == null)
				messages.Add(NewError("Destination adapter configuration is required."));
			else
				messages.AddRange(this.DestinationAdapterConfiguration.Validate(DST_CONTEXT));

			if ((object)this.HashConfiguration == null)
				messages.Add(NewError("Hash configuration is required."));
			else
				messages.AddRange(this.HashConfiguration.Validate());

			// check for duplicate dictionaries
			var dictionaryIdSums = this.DictionaryConfigurations.GroupBy(d => d.DictionaryId)
				.Select(dl => new
							{
								DictionaryId = dl.First().DictionaryId,
								Count = dl.Count()
							}).Where(dl => dl.Count > 1);

			if (dictionaryIdSums.Any())
				messages.AddRange(dictionaryIdSums.Select(d => NewError(string.Format("Duplicate dictionary configuration found: '{0}'.", d.DictionaryId))).ToArray());

			index = 0;
			foreach (DictionaryConfiguration dictionaryConfiguration in this.DictionaryConfigurations)
				messages.AddRange(dictionaryConfiguration.Validate(index++));

			return messages;
		}

		#endregion
	}
}