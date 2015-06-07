/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public sealed class DelimitedTextDictionaryAdapter : DictionaryAdapter, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextDictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration configuration, object id)
		{
			throw new NotImplementedException();
		}

		protected override void CoreInitialize(ObfuscationConfiguration configuration)
		{
			throw new NotImplementedException();
		}

		protected override void CoreTerminate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}