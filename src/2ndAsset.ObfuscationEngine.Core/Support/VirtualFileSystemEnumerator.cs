/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public sealed class VirtualFileSystemEnumerator
	{
		#region Constructors/Destructors

		public VirtualFileSystemEnumerator()
		{
		}

		#endregion

		#region Methods/Operators

		public IEnumerable<VirtualFileSystemItem> EnumerateVirtualItems(string directoryPath, bool enableRecursion)
		{
			IEnumerable<string> directoryNames;
			IEnumerable<string> fileNames;

			if ((object)directoryPath == null)
				throw new ArgumentNullException("directoryPath");

			directoryPath = Path.GetFullPath(directoryPath);

			if (File.Exists(directoryPath))
				throw new DirectoryNotFoundException(directoryPath);

			if (!Directory.Exists(directoryPath))
				throw new DirectoryNotFoundException(directoryPath);

			directoryNames = Directory.EnumerateDirectories(directoryPath);

			foreach (string directoryName in directoryNames)
			{
				string tempDirectoryPath = Path.Combine(directoryPath, directoryName);
				yield return new VirtualFileSystemItem(VirtualFileSystemItemType.Directory, directoryName, tempDirectoryPath);

				if (enableRecursion)
				{
					var items = this.EnumerateVirtualItems(tempDirectoryPath, true);

					foreach (var item in items)
						yield return item;
				}
			}

			fileNames = Directory.EnumerateFiles(directoryPath);

			foreach (string fileName in fileNames)
				yield return new VirtualFileSystemItem(VirtualFileSystemItemType.File, fileName, Path.Combine(directoryPath, fileName));
		}

		#endregion
	}
}