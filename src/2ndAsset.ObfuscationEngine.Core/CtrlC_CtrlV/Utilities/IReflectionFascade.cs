/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	/// <summary>
	/// Provides easier reflection facilities.
	/// </summary>
	public interface IReflectionFascade
	{
		#region Methods/Operators

		/// <summary>
		/// Gets all custom attributes of the specified type. If no custom attributes of the specified type are defined, then null is returned.
		/// </summary>
		/// <typeparam name="TAttribute"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </typeparam>
		/// <param name="target"> The target object. </param>
		/// <returns> The custom attributes array or null. </returns>
		TAttribute[] GetAllAttributes<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute;

		/// <summary>
		/// Returns the concatenation of error messages from an exception object. All inner exceptions and collected exceptions (public properties implementing IEnumerable&lt;Exception&gt;) are returned.
		/// </summary>
		/// <param name="exception"> The root exception to get errors. </param>
		/// <param name="indent"> The indent level count. </param>
		/// <returns> A string concatenation of error messages delimited by newlines. </returns>
		string GetErrors(Exception exception, int indent);

		/// <summary>
		/// Attempts to get the property type for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to get the type for. </param>
		/// <param name="propertyType"> An output run-time type of the logical property or null if the logical property lookup failed. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		bool GetLogicalPropertyType(object targetInstance, string propertyName, out Type propertyType);

		/// <summary>
		/// Attempts to get the property value for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to get the value for. </param>
		/// <param name="propertyValue"> An output run-time value of the logical property or null if the logical property lookup failed. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		bool GetLogicalPropertyValue(object targetInstance, string propertyName, out object propertyValue);

		/// <summary>
		/// Used to obtain the least-derived public, instance property of a given name.
		/// </summary>
		/// <param name="propertyType"> The property type to interogate. </param>
		/// <param name="propertyName"> The property name to lookup. </param>
		/// <returns> A PropertyInfo for the least-derived public, instance property by the given name or null if none were found. </returns>
		PropertyInfo GetLowestProperty(Type propertyType, string propertyName);

		/// <summary>
		/// Get the single custom attribute of the attribute specified type. If more than one custom attribute exists for the requested type, an InvalidOperationException is thrown. If no custom attributes of the specified type are defined, then null is returned.
		/// </summary>
		/// <typeparam name="TAttribute"> The custom attribute type. </typeparam>
		/// <param name="target"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </param>
		/// <returns> The single custom attribute or null if none are defined. </returns>
		TAttribute GetOneAttribute<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute;

		/// <summary>
		/// Asserts that the custom attribute is not defined on the target. If more than zero custom attributes exist for the requested type, an InvalidOperationException is thrown.
		/// </summary>
		/// <typeparam name="TAttribute"> The custom attribute type. </typeparam>
		/// <param name="target"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </param>
		void GetZeroAttributes<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute;

		/// <summary>
		/// Transforms a nullable type to its underlying non-nullable equivalent.
		/// Simply returns an existing reference type
		/// </summary>
		/// <param name="conversionType"> The nullable run-time type to transform. </param>
		/// <returns> The non-nullbale run-time type. </returns>
		Type MakeNonNullableType(Type conversionType);

		/// <summary>
		/// Transforms a nullable type to its underlying non-nullable equivalent.
		/// </summary>
		/// <param name="conversionType"> The nullable run-time type to transform. </param>
		/// <returns> The non-nullbale run-time type. </returns>
		Type MakeNullableType(Type conversionType);

		/// <summary>
		/// Attempts to set the property value for a logical property (CLR, associative, etc.). This overload assume stayHard=false and makeSoft=true semantics.
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to set the value for. </param>
		/// <param name="propertyValue"> The value of the logical property to set or null. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		bool SetLogicalPropertyValue(object targetInstance, string propertyName, object propertyValue);

		/// <summary>
		/// Attempts to set the property value for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to set the value for. </param>
		/// <param name="propertyValue"> The value of the logical property to set or null. </param>
		/// <param name="stayHard"> Force only 'hard' object semantics and not use associative lookup (i.e. the target instance must be a real CLR object). </param>
		/// <param name="makeSoft"> Allow making 'soft' object semantics (i.e. the target instance could be an associative object). </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not; lookup respects the 'stayHard' and 'makeSoft' flags. </returns>
		bool SetLogicalPropertyValue(object targetInstance, string propertyName, object propertyValue, bool stayHard, bool makeSoft);

		#endregion
	}
}