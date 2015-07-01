/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	public static class ExtensionMethods
	{
		#region Methods/Operators

		public static T ChangeType<T>(this object value)
		{
			return DataTypeFascade.Instance.ChangeType<T>(value);
		}

		public static object ChangeType(this object value, Type conversionType)
		{
			return DataTypeFascade.Instance.ChangeType(value, conversionType);
		}

		public static string SafeToString<TValue>(this TValue value)
		{
			return DataTypeFascade.Instance.SafeToString<TValue>(value);
		}

		public static string SafeToString<TValue>(this TValue value, string format)
		{
			return DataTypeFascade.Instance.SafeToString<TValue>(value, format);
		}

		public static string SafeToString<TValue>(this TValue value, string format, string @default)
		{
			return DataTypeFascade.Instance.SafeToString<TValue>(value, format, @default);
		}

		public static string SafeToString<TValue>(this TValue value, string format, string @default, bool dofvisnow)
		{
			return DataTypeFascade.Instance.SafeToString<TValue>(value, format, @default, dofvisnow);
		}

		#endregion
	}
}