/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.AdoNetAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public class AdoNetDictionaryAdapter : AdoNetDictionaryAdapter<AdoNetAdapterConfiguration>
	{
	}
}