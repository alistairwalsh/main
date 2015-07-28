/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace _2ndAsset.Ssis.Components
{
	public enum MappingDisposition : sbyte
	{
		UnknownToPackageConfig = -1,
		NotMappedInPackageConfig = 0,
		IsMappedInPackageConfig = 1,
	}
}