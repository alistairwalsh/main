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

		public long? GetHash(long? multiplier, long? size, long? seed, object value)
		{
			const long DEFAULT_HASH = -1L;
			long hashCode;
			byte[] buffer;
			Type valueType;
			string _value;

			if ((object)multiplier == null)
				return null;

			if ((object)size == null)
				return null;

			if ((object)seed == null)
				return null;

			if (size == 0L)
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

			hashCode = (long)seed;
			for (int index = 0; index < buffer.Length; index++)
				hashCode = ((long)multiplier * hashCode + buffer[index]) % uint.MaxValue;

			if (hashCode > int.MaxValue)
				hashCode = hashCode - uint.MaxValue;

			if (hashCode < 0)
				hashCode = hashCode + int.MaxValue;

			hashCode = (hashCode % (long)size);

			return (int)hashCode;
		}

		#endregion
	}
}