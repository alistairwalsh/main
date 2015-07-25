/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.DelimitedTextAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public class DelimitedTextDictionaryAdapter : DictionaryAdapter<DelimitedTextAdapterConfiguration>, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextDictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			object value;
			int index;

			HeaderSpec[] headerSpecs;
			IEnumerable<IDictionary<string, object>> records;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)surrogateId == null)
				throw new ArgumentNullException("surrogateId");

			if ((object)this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextSpec"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextFilePath"));

			using (RecordTextReader delimitedTextReader = new DelimitedTextReader(new StreamReader(File.Open(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath, FileMode.Open, FileAccess.Read, FileShare.None)), this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec))
			{
				index = surrogateId.ChangeType<int>() - 1;

				headerSpecs = delimitedTextReader.ReadHeaderSpecs().ToArray();
				records = delimitedTextReader.ReadRecords();

				var record = records.ElementAtOrDefault(index);

				if ((object)record == null)
					value = null;
				else
					value = record[headerSpecs[index].HeaderName];
			}

			return value;
		}

		protected override void CoreInitialize()
		{
		}

		protected override void CorePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			HeaderSpec[] headerSpecs;
			IEnumerable<IDictionary<string, object>> records;
			IDictionary<long, object> dictionaryCache;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			if ((object)this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextSpec"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextFilePath"));

			using (RecordTextReader delimitedTextReader = new DelimitedTextReader(new StreamReader(File.Open(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath, FileMode.Open, FileAccess.Read, FileShare.None)), this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec))
			{
				dictionaryCache = new Dictionary<long, object>();

				headerSpecs = delimitedTextReader.ReadHeaderSpecs().ToArray();
				records = delimitedTextReader.ReadRecords();

				foreach (IDictionary<string, object> record in records)
				{
					object[] values = record.Values.ToArray();
					long id = values[0].ChangeType<long>();
					object value = values[1].ChangeType<string>();

					dictionaryCache.Add(id, value);
				}

				substitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}