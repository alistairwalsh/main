/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;
using System.Linq;

using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Serialization;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;

namespace _2ndAsset.Ssis.Components
{
	public sealed class DictionaryMetadataWrapper
	{
		#region Constructors/Destructors

		public DictionaryMetadataWrapper()
		{
		}

		#endregion

		#region Fields/Constants

		private string commandText;
		private string dictionaryId;
		private long recordCount;

		#endregion

		#region Properties/Indexers/Events

		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
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

		public long RecordCount
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

		public static IEnumerable<DictionaryMetadataWrapper> FromJson(string jsonData)
		{
			IEnumerable<DictionaryMetadataWrapper> configurations;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(jsonData))
				configurations = null;
			else
				configurations = new JsonSerializationStrategy().GetObjectFromString<List<DictionaryMetadataWrapper>>(jsonData);

			return configurations;
		}

		public static string ToJson(IEnumerable<DictionaryMetadataWrapper> dictionaryMetadataWrappers)
		{
			string jsonData;

			if ((object)dictionaryMetadataWrappers == null)
				jsonData = null;
			else
				jsonData = new JsonSerializationStrategy().SetObjectToString<List<DictionaryMetadataWrapper>>(dictionaryMetadataWrappers.ToList());

			return jsonData;
		}

		#endregion
	}
}