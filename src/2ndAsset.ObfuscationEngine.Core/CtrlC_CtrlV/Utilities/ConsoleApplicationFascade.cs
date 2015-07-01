/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities
{
	public abstract class ConsoleApplicationFascade : ExecutableApplicationFascade
	{
		#region Constructors/Destructors

		protected ConsoleApplicationFascade()
		{
		}

		#endregion

		#region Methods/Operators

		protected override sealed void DisplayArgumentErrorMessage(IEnumerable<Message> argumentMessages)
		{
			ConsoleColor oldConsoleColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;

			if ((object)argumentMessages != null)
			{
				Console.WriteLine();
				foreach (Message argumentMessage in argumentMessages)
					Console.WriteLine(argumentMessage.Description);
			}

			Console.ForegroundColor = oldConsoleColor;
		}

		protected override sealed void DisplayArgumentMapMessage(IDictionary<string, ArgumentSpec> argumentMap)
		{
			ConsoleColor oldConsoleColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Magenta;

			var requiredArgumentTokens = argumentMap.Select(m => (!m.Value.Required ? "[" : string.Empty) + string.Format("-{0}:value{1}", m.Key, !m.Value.Bounded ? "(s)" : string.Empty) + (!m.Value.Required ? "]" : string.Empty));

			if ((object)requiredArgumentTokens != null)
			{
				Console.WriteLine();
				Console.WriteLine(string.Format("USAGE: {0} ", Assembly.GetEntryAssembly().ManifestModule.Name) + string.Join(" ", requiredArgumentTokens));
			}

			Console.ForegroundColor = oldConsoleColor;
		}

		protected override sealed void DisplayFailureMessage(Exception exception)
		{
			ConsoleColor oldConsoleColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine();
			Console.WriteLine((object)exception != null ? this.ReflectionFascade.GetErrors(exception, 0) : "<unknown>");
			Console.ForegroundColor = oldConsoleColor;

			Console.WriteLine();
			Console.WriteLine("The operation failed to complete.");
		}

		protected override sealed void DisplayRawArgumentsMessage(IEnumerable<string> arguments)
		{
			ConsoleColor oldConsoleColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;

			if ((object)arguments != null)
			{
				Console.WriteLine();
				Console.WriteLine("RAW CMDLN: {0}", Environment.CommandLine);
				Console.WriteLine();
				Console.WriteLine("RAW ARGS:");

				int index = 0;
				foreach (string argument in arguments)
					Console.WriteLine("[{0}] => {1}", index++, argument);
			}

			Console.ForegroundColor = oldConsoleColor;
		}

		protected override sealed void DisplaySuccessMessage(TimeSpan duration)
		{
			Console.WriteLine();
			Console.WriteLine("Operation completed successfully; duration: '{0}'.", duration);
		}

		#endregion
	}
}