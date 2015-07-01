/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	/// <summary>
	/// Provides static helper and/or extension methods for core data type functionality such as validation and parsing.
	/// </summary>
	public sealed partial class DataTypeFascade
	{
		#region Methods/Operators

		private static bool IsValidEnum(Type enumType, string value)
		{
			try
			{
				Enum.Parse(enumType, value, true);
			}
			catch (ArgumentNullException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}

			return true;
		}

		private static bool IsValidGuid(string value)
		{
			try
			{
				new Guid(value);
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (FormatException)
			{
				return false;
			}

			return true;
		}

		private static object ParseEnum(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, true);
		}

		private static Guid ParseGuid(string value)
		{
			return new Guid(value);
		}

		#endregion
	}
}