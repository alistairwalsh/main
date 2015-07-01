/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	/// <summary>
	/// The set of database members supported by this framework.
	/// </summary>
	public enum MemberType
	{
		Unknown = 0,

		Column,

		Parameter
	}
}