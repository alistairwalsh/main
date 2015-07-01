/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	public interface IExecutableApplicationFascade : IDisposable
	{
		#region Properties/Indexers/Events

		AssemblyInformationFascade AssemblyInformationFascade
		{
			get;
		}

		bool HookUnhandledExceptionEvents
		{
			get;
		}

		#endregion

		#region Methods/Operators

		int EntryPoint(string[] args);

		void ShowNestedExceptionsAndThrowBrickAtProcess(Exception e);

		#endregion
	}
}