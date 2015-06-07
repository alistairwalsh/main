/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public abstract class DictionaryAdapter : Adapter, IDictionaryAdapter
	{
		#region Constructors/Destructors

		protected DictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetAlternativeValueFromId(DictionaryConfiguration configuration, object id);

		public object GetAlternativeValueFromId(DictionaryConfiguration configuration, object id)
		{
			return this.CoreGetAlternativeValueFromId(configuration, id);
		}

		#endregion
	}
}