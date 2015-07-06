/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class DictionaryConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public DictionaryConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private AdapterConfiguration dictionaryAdapterConfiguration;
		private string dictionaryId;
		private bool preloadEnabled;
		private long? recordCount;

		#endregion

		#region Properties/Indexers/Events

		public AdapterConfiguration DictionaryAdapterConfiguration
		{
			get
			{
				return this.dictionaryAdapterConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.dictionaryAdapterConfiguration, value);
				this.dictionaryAdapterConfiguration = value;
			}
		}

		public string DictionaryId
		{
			get
			{
				return this.dictionaryId;
			}
			set
			{
				this.dictionaryId = value;
			}
		}

		[JsonIgnore]
		public new ObfuscationConfiguration Parent
		{
			get
			{
				return (ObfuscationConfiguration)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		public bool PreloadEnabled
		{
			get
			{
				return this.preloadEnabled;
			}
			set
			{
				this.preloadEnabled = value;
			}
		}

		public long? RecordCount
		{
			get
			{
				return this.recordCount;
			}
			set
			{
				this.recordCount = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override sealed IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public virtual IEnumerable<Message> Validate(int? index)
		{
			List<Message> messages;
			const string CONTEXT = "Dictionary";

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.DictionaryId))
				messages.Add(NewError(string.Format("Dictionary[{0}] ID is required.", index)));

			if ((object)this.DictionaryAdapterConfiguration == null)
				messages.Add(NewError("Dictionary adapter configuration is required."));
			else
				messages.AddRange(this.DictionaryAdapterConfiguration.Validate(CONTEXT));

			return messages;
		}

		#endregion
	}
}