/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TextMetal.Middleware.Common.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class ShufflingObfuscationStrategy : ObfuscationStrategy
	{
		#region Constructors/Destructors

		public ShufflingObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetShuffle(long randomSeed, object value)
		{
			Random random;
			Type valueType;
			string _value;

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			random = new Random((int)randomSeed);
			var fidelityMap = ImplNormalize(ref _value);
			_value = new string(_value.ToCharArray().OrderBy(s => random.Next(int.MaxValue)).ToArray());
			ImplDenormalize(fidelityMap, ref _value);

			return _value;
		}

		private static void ImplDenormalize(Dictionary<int, char> fidelityMap, ref string value)
		{
			StringBuilder sb;
			char ch;
			int offset = 0;

			if ((object)fidelityMap == null)
				throw new ArgumentNullException("fidelityMap");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(value))
				return;

			sb = new StringBuilder(value);

			for (int index = 0; index < value.Length; index++)
			{
				if (fidelityMap.TryGetValue(index, out ch))
				{
					sb.Insert(index, ch);
					offset++;
				}
			}

			value = sb.ToString();
		}

		private static Dictionary<int, char> ImplNormalize(ref string value)
		{
			StringBuilder sb;
			Dictionary<int, char> fidelityMap;

			fidelityMap = new Dictionary<int, char>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(value))
			{
				value = value;
				return fidelityMap;
			}

			sb = new StringBuilder();

			// 212-555-1234 => 2125551212 => 1945687302 => 194-568-7302
			for (int index = 0; index < value.Length; index++)
			{
				if (char.IsLetterOrDigit(value[index]))
					sb.Append(value[index]);
				else
					fidelityMap.Add(index, value[index]);
			}

			value = sb.ToString();
			return fidelityMap;
		}

		protected override object CoreGetObfuscatedValue(long signHash, long valueHash, int? extentValue, IMetaColumn metaColumn, object columnValue)
		{
			object value;
			long randomSeed;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			randomSeed = valueHash;

			value = GetShuffle(randomSeed, columnValue);

			return value;
		}

		#endregion
	}
}