/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class AdapterSpecificConfigurationAttribute : Attribute
	{
		#region Constructors/Destructors

		public AdapterSpecificConfigurationAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private string specificConfigurationAqtn;
		private string userControlAqtn;

		#endregion

		#region Properties/Indexers/Events

		public string SpecificConfigurationAqtn
		{
			get
			{
				return this.specificConfigurationAqtn;
			}
			set
			{
				this.specificConfigurationAqtn = value;
			}
		}

		public string UserControlAqtn
		{
			get
			{
				return this.userControlAqtn;
			}
			set
			{
				this.userControlAqtn = value;
			}
		}

		#endregion

		#region Methods/Operators

		public Type GetSpecificConfigurationType()
		{
			Type specificConfigurationAqtn;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.SpecificConfigurationAqtn))
				return null;

			specificConfigurationAqtn = Type.GetType(this.SpecificConfigurationAqtn, false);

			return specificConfigurationAqtn;
		}

		public Type GetUserControlType()
		{
			Type userControlType;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.UserControlAqtn))
				return null;

			userControlType = Type.GetType(this.UserControlAqtn, false);

			return userControlType;
		}

		#endregion
	}
}