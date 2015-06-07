/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public interface IHashingStrategy
	{
		#region Methods/Operators

		long? GetHash(long? hashMultiplier, long? hashBucketSize, long? hashSeed, object value);

		#endregion
	}
}