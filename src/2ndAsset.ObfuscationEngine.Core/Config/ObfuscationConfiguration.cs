/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using TextMetal.Middleware.Common;

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

		private readonly ConfigurationObjectCollection<DictionaryConfiguration> dictionaryConfigurations;
		private string configurationNamespace;
		private AdapterConfiguration destinationAdapterConfiguration;
		private HashConfiguration hashConfiguration;
		private AdapterConfiguration sourceAdapterConfiguration;
		private TableConfiguration tableConfiguration;

		#endregion

		#region Properties/Indexers/Events

		public ConfigurationObjectCollection<DictionaryConfiguration> DictionaryConfigurations
		{
			get
			{
				return this.dictionaryConfigurations;
			}
		}

		public string ConfigurationNamespace
		{
			get
			{
				return this.configurationNamespace;
			}
			set
			{
				this.configurationNamespace = value;
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

		public override IEnumerable<Message> Validate()
		{
			List<Message> messages;
			int index;

			messages = new List<Message>();

			if ((object)this.TableConfiguration == null)
				messages.Add(NewError("Table configuration is required."));
			else
				messages.AddRange(this.TableConfiguration.Validate());

			if ((object)this.SourceAdapterConfiguration == null)
				messages.Add(NewError("Source adapter configuration is required."));
			else
				messages.AddRange(this.SourceAdapterConfiguration.Validate());

			if ((object)this.DestinationAdapterConfiguration == null)
				messages.Add(NewError("Destination adapter configuration is required."));
			else
				messages.AddRange(this.DestinationAdapterConfiguration.Validate());

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