/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "")]
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