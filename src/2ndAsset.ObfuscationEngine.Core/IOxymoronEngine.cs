﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public interface IOxymoronEngine : IDisposable
	{
		#region Methods/Operators

		object GetObfuscatedValue(IMetaColumn metaColumn, object columnValue);

		#endregion
	}
}