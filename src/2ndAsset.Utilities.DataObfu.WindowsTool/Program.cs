/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Forms;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool
{
	/// <summary>
	/// Entry point class for the application.
	/// </summary>
	internal class Program : WindowsApplicationFascade<MainForm, SplashForm>
	{
		#region Constructors/Destructors

		public Program()
		{
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// The entry point method for this application.
		/// </summary>
		/// <param name="args"> The command line arguments passed from the executing environment. </param>
		/// <returns> The resulting exit code. </returns>
		[STAThread]
		public static int Main(string[] args)
		{
			using (Program program = new Program())
				return program.EntryPoint(args);
		}

		protected override IDictionary<string, ArgumentSpec> GetArgumentMap()
		{
			IDictionary<string, ArgumentSpec> argumentMap;

			argumentMap = new Dictionary<string, ArgumentSpec>();
			/*argumentMap.Add("test", new ArgumentSlot()
			{
				Required = true,
				Bounded = true
			});*/

			return argumentMap;
		}

		#endregion
	}
}