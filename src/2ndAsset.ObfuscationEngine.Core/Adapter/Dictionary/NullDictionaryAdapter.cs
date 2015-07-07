/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public class NullDictionaryAdapter : DictionaryAdapter<AdapterSpecificConfiguration>, INullAdapter
	{
		#region Constructors/Destructors

		public NullDictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)surrogateId == null)
				throw new ArgumentNullException("surrogateId");

			return null;
		}

		protected override void CoreInitialize()
		{
		}

		protected override void CorePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}