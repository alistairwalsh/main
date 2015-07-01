/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	/// <summary>
	/// The set of database objects supported by this framework.
	/// </summary>
	public enum ObjectType
	{
		Unknown = 0,

		Table,

		View,

		ProcedureRequest,

		ProcedureResult,

		ProcedureResponse
	}
}