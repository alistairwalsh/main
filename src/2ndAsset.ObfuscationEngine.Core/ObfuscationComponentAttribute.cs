/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public enum ObfuscationComponentType
	{
		Unknown = 0,

		SourceAdapter,

		DictionaryAdapter,

		DestinationAdapter
	}

	public class ObfuscationComponentAttribute : Attribute
	{
		#region Constructors/Destructors

		public ObfuscationComponentAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private ObfuscationComponentType componentType;
		private int currentVersion;
		private string description;
		private string displayName;
		private string iconResource;

		#endregion

		#region Properties/Indexers/Events

		public ObfuscationComponentType ComponentType
		{
			get
			{
				return this.componentType;
			}
			set
			{
				this.componentType = value;
			}
		}

		public int CurrentVersion
		{
			get
			{
				return this.currentVersion;
			}
			set
			{
				this.currentVersion = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		public string IconResource
		{
			get
			{
				return this.iconResource;
			}
			set
			{
				this.iconResource = value;
			}
		}

		#endregion
	}
}