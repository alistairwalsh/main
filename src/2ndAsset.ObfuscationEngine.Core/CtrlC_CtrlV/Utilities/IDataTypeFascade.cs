/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	/// <summary>
	/// Provides easier easier facilities for core data type functionality such as validation and parsing.
	/// </summary>
	public interface IDataTypeFascade
	{
		#region Methods/Operators

		/// <summary>
		/// Performs a run-time type change on a given value.
		/// </summary>
		/// <typeparam name="T"> The type to change value to. </typeparam>
		/// <param name="value"> The value to change type. </param>
		/// <returns> A value changed to the given type. </returns>
		T ChangeType<T>(object value);

		/// <summary>
		/// Performs a run-time type change on a given value.
		/// </summary>
		/// <param name="value"> The value to change type. </param>
		/// <param name="conversionType"> The type to change value to. </param>
		/// <returns> A value changed to the given type. </returns>
		object ChangeType(object value, Type conversionType);

		/// <summary>
		/// Obtains the default value for a given type using reflection.
		/// </summary>
		/// <param name="targetType"> The target type. </param>
		/// <returns> The default value for the target type. </returns>
		object DefaultValue(Type targetType);

		/// <summary>
		/// Determines if a string value is null or zero length.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is null or zero length. </returns>
		bool IsNullOrEmpty(string value);

		/// <summary>
		/// Determines if a string value is null, zero length, or only contains white space.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is null, zero length, or only contains white space. </returns>
		bool IsNullOrWhiteSpace(string value);

		/// <summary>
		/// Determines if a string value is a valid email address.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is a valid email address. </returns>
		bool IsValidEmailAddress(string value);

		/// <summary>
		/// Determines if a string value is zero length or only contains white space.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is zero length or only contains white space. </returns>
		bool IsWhiteSpace(string value);

		/// <summary>
		/// Checks whether two object instances are equal using the Object.Equals() method. Value coercion is performed.
		/// </summary>
		/// <param name="objA"> An object instance or null. </param>
		/// <param name="objB"> Another object instance or null. </param>
		/// <returns> A value indicating whether the two object instances are equal. </returns>
		bool ObjectsEqualValueSemantics(object objA, object objB);

		/// <summary>
		/// Returns a string that represents the specified type with the format specification. If the value is null, then the default value of a zero length string is returned.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		string SafeToString<TValue>(TValue value);

		/// <summary>
		/// Returns a string that represents the specified type with the format specification. If the value is null, then the default value of a zero length string is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		string SafeToString<TValue>(TValue value, string format);

		/// <summary>
		/// Returns a string that represents the specified type with the format specification. If the value is null, then the default value is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <param name="default"> The default value to return if the value is null. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		string SafeToString<TValue>(TValue value, string format, string @default);

		/// <summary>
		/// Returns a string that represents the specified type with the format specification. If the value is null, then the default value is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <param name="default"> The default value to return if the value is null. </param>
		/// <param name="dofvisnow"> Use default value if the formatted value is null or whotespace. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		string SafeToString<TValue>(TValue value, string format, string @default, bool dofvisnow);

		/// <summary>
		/// Converts the specified string representation to its valueType equivalent and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="valueType"> The type to convert the string value. </param>
		/// <param name="value"> A string containing a valueType to convert. </param>
		/// <param name="result"> When this method returns, contains the valueType value equivalent contained in value, if the conversion succeeded, or null if the conversion failed. The conversion fails if the value parameter is null, is an empty string, or does not contain a valid string representation of an valueType. This parameter is passed uninitialized. </param>
		/// <returns> A boolean value of true if the value was converted successfully; otherwise, false. </returns>
		bool TryParse(Type valueType, string value, out object result);

		/// <summary>
		/// Converts the specified string representation to its TValue equivalent and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <typeparam name="TValue"> The type to parse the string value. </typeparam>
		/// <param name="value"> A string containing a TValue to convert. </param>
		/// <param name="result"> When this method returns, contains the TValue value equivalent contained in value, if the conversion succeeded, or default(TValue) if the conversion failed. The conversion fails if the value parameter is null, is an empty string, or does not contain a valid string representation of a TValue. This parameter is passed uninitialized. </param>
		/// <returns> A boolean value of true if the value was converted successfully; otherwise, false. </returns>
		bool TryParse<TValue>(string value, out TValue result);

		#endregion
	}
}