/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Ssis.Components.UI.Forms;

namespace _2ndAsset.Ssis.Components.UI.Test.WindowsTool
{
	internal class Program : WindowsApplicationFascade<ObfuConfMainForm, Form>
	{
		#region Fields/Constants

		private static readonly Program instance = new Program();

		#endregion

		#region Properties/Indexers/Events

		public static Program Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			using (Instance)
				return Instance.EntryPoint(args);
		}

		protected override IDictionary<string, ArgumentSpec> GetArgumentMap()
		{
			return null;
		}

		#endregion
	}
}