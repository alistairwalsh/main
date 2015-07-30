/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

using Microsoft.SqlServer.Dts.Pipeline.Design;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

namespace _2ndAsset.Ssis.Components.UI
{
	public class NopTransformUI : IDtsComponentUI
	{
		#region Constructors/Destructors

		public NopTransformUI()
		{
		}

		#endregion

		#region Fields/Constants

		private IServiceProvider serviceProvider;

		#endregion

		#region Properties/Indexers/Events

		private IServiceProvider ServiceProvider
		{
			get
			{
				return this.serviceProvider;
			}
			set
			{
				this.serviceProvider = value;
			}
		}

		#endregion

		#region Methods/Operators

		public void Delete(IWin32Window parentWindow)
		{
			// do nothing
		}

		public bool Edit(IWin32Window parentWindow, Variables variables, Connections connections)
		{
			DialogResult dialogResult;

			try
			{
				MessageBox.Show(parentWindow, "The custom user interface is under construction in the release." + Environment.NewLine + "Please use the advanced editor for now.", NopConstants.COMPONENT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show(parentWindow, ex.ToString(), NopConstants.COMPONENT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return false;
		}

		public void Help(IWin32Window parentWindow)
		{
			throw new NotSupportedException(string.Format("Not supported."));
		}

		public void Initialize(IDTSComponentMetaData100 dtsComponentMetadata, IServiceProvider serviceProvider)
		{
			this.ServiceProvider = serviceProvider;
		}

		public void New(IWin32Window parentWindow)
		{
			// do nothing
		}

		#endregion
	}
}