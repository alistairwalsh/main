/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public interface IObfuscationMixIn : IDisposable
	{
		#region Methods/Operators

		object GetObfuscatedValue(int columnIndex, string columnName, Type columnType, object columnValue);

		#endregion
	}
}