/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Microsoft.SqlServer.Dts.Runtime;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool
{
	public class ConsoleEvents : IDTSEvents
	{
		#region Constructors/Destructors

		public ConsoleEvents()
		{
		}

		#endregion

		#region Methods/Operators

		public void OnBreakpointHit(IDTSBreakpointSite breakpointSite, BreakpointTarget breakpointTarget)
		{
		}

		public void OnCustomEvent(TaskHost taskHost, string eventName, string eventText, ref object[] arguments, string subComponent, ref bool fireAgain)
		{
		}

		public bool OnError(DtsObject source, int errorCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError)
		{
			Console.WriteLine("[Error] {0}: {1}", subComponent, description);
			return true;
		}

		public void OnExecutionStatusChanged(Executable exec, DTSExecStatus newStatus, ref bool fireAgain)
		{
		}

		public void OnInformation(DtsObject source, int informationCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError, ref bool fireAgain)
		{
		}

		public void OnPostExecute(Executable exec, ref bool fireAgain)
		{
		}

		public void OnPostValidate(Executable exec, ref bool fireAgain)
		{
		}

		public void OnPreExecute(Executable exec, ref bool fireAgain)
		{
		}

		public void OnPreValidate(Executable exec, ref bool fireAgain)
		{
		}

		public void OnProgress(TaskHost taskHost, string progressDescription, int percentComplete, int progressCountLow, int progressCountHigh, string subComponent, ref bool fireAgain)
		{
			Console.WriteLine("[Progress] {0}: {1} = {2}", subComponent, progressDescription, percentComplete);
		}

		public bool OnQueryCancel()
		{
			return true;
		}

		public void OnTaskFailed(TaskHost taskHost)
		{
		}

		public void OnVariableValueChanged(DtsContainer DtsContainer, Variable variable, ref bool fireAgain)
		{
		}

		public void OnWarning(DtsObject source, int warningCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError)
		{
			Console.WriteLine("[Warning] {0}: {1}", subComponent, description);
		}

		#endregion
	}
}