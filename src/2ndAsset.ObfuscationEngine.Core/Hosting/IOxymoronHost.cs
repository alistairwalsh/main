/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting
{
	public interface IOxymoronHost : IDisposable
	{
		#region Methods/Operators

		object GetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

		#endregion
	}
}