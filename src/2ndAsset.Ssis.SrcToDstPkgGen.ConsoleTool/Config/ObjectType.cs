/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
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