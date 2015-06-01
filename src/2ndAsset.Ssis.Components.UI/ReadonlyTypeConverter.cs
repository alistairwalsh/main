/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;

namespace _2ndAsset.Ssis.Components.UI
{
	public class ReadonlyTypeConverter : TypeConverter
	{
		#region Constructors/Destructors

		public ReadonlyTypeConverter()
		{
		}

		#endregion

		#region Methods/Operators

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return false;
		}

		#endregion
	}
}