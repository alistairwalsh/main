/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public interface IAdapter : IDisposable
	{
		#region Methods/Operators

		void Initialize(ObfuscationConfiguration obfuscationConfiguration);

		void Terminate();

		#endregion
	}
}