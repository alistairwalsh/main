/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

using System;

using Microsoft.SqlServer.Dts.Runtime;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool
{
	public sealed class ConsoleComponentEventHandler : IDTSComponentEvents
	{
		#region Methods/Operators

		public void FireBreakpointHit(BreakpointTarget breakpointTarget)
		{
		}

		public void FireCustomEvent(string eventName, string eventText, ref object[] arguments, string subComponent, ref bool fireAgain)
		{
		}

		public bool FireError(int errorCode, string subComponent, string description, string helpFile, int helpContext)
		{
			Console.WriteLine("[Error] {0}: {1}", subComponent, description);
			return true;
		}

		public void FireInformation(int informationCode, string subComponent, string description, string helpFile, int helpContext, ref bool fireAgain)
		{
		}

		public void FireProgress(string progressDescription, int percentComplete, int progressCountLow, int progressCountHigh, string subComponent, ref bool fireAgain)
		{
		}

		public bool FireQueryCancel()
		{
			return false;
		}

		public void FireWarning(int warningCode, string subComponent, string description, string helpFile, int helpContext)
		{
			Console.WriteLine("[Warning] {0}: {1}", subComponent, description);
		}

		#endregion
	}
}