/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Text;

using TextMetal.Middleware.Common.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class DefaultHashingStrategy : IHashingStrategy
	{
		#region Constructors/Destructors

		private DefaultHashingStrategy()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly DefaultHashingStrategy instance = new DefaultHashingStrategy();

		#endregion

		#region Properties/Indexers/Events

		public static DefaultHashingStrategy Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		public long? GetHash(long? hashMultiplier, long? hashBucketSize, long? hashSeed, object value)
		{
			const long DEFAULT_HASH = -1L;
			long hashCode;
			byte[] buffer;
			Type valueType;
			string _value;

			if ((object)hashMultiplier == null)
				return null;

			if ((object)hashBucketSize == null)
				return null;

			if ((object)hashSeed == null)
				return null;

			if (hashBucketSize == 0L)
				return null; // prevent DIV0

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return DEFAULT_HASH;

			_value = _value.Trim();

			buffer = Encoding.UTF8.GetBytes(_value);

			hashCode = (long)hashSeed;
			for (int index = 0; index < buffer.Length; index++)
				hashCode = ((long)hashMultiplier * hashCode + buffer[index]) % uint.MaxValue;

			if (hashCode > int.MaxValue)
				hashCode = hashCode - uint.MaxValue;

			if (hashCode < 0)
				hashCode = hashCode + int.MaxValue;

			hashCode = (hashCode % (long)hashBucketSize);

			return (int)hashCode;
		}

		#endregion
	}
}