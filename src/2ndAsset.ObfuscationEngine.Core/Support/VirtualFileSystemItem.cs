/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public sealed class VirtualFileSystemItem
	{
		#region Constructors/Destructors

		public VirtualFileSystemItem(VirtualFileSystemItemType itemType, string itemName, string itemPath)
		{
			this.itemType = itemType;
			this.itemName = itemName;
			this.itemPath = itemPath;
		}

		#endregion

		#region Fields/Constants

		private readonly string itemName;
		private readonly string itemPath;
		private readonly VirtualFileSystemItemType itemType;

		#endregion

		#region Properties/Indexers/Events

		public string ItemName
		{
			get
			{
				return this.itemName;
			}
		}

		public string ItemPath
		{
			get
			{
				return this.itemPath;
			}
		}

		public VirtualFileSystemItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		#endregion
	}
}