/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Windows.Forms;

using Microsoft.SqlServer.Dts.Pipeline.Design;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.Ssis.Components.UI
{
	public class ObfuscationStrategyTransformUI : IDtsComponentUI
	{
		#region Constructors/Destructors

		public ObfuscationStrategyTransformUI()
		{
		}

		#endregion

		#region Fields/Constants

		private __ComponentMetadataWrapper componentMetadataWrapper;
		private IDTSComponentMetaData100 dtsComponentMetaData100;
		private IServiceProvider serviceProvider;

		#endregion

		#region Properties/Indexers/Events

		private __ComponentMetadataWrapper ComponentMetadataWrapper
		{
			get
			{
				return this.componentMetadataWrapper;
			}
			set
			{
				this.componentMetadataWrapper = value;
			}
		}

		private IDTSComponentMetaData100 DtsComponentMetaData100
		{
			get
			{
				return this.dtsComponentMetaData100;
			}
			set
			{
				this.dtsComponentMetaData100 = value;
			}
		}

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
				MessageBox.Show(parentWindow, "The custom user interface is under construction in the release." + Environment.NewLine + "Please use the advanced editor for now.", Constants.COMPONENT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);

				/*using (ObfuConfMainForm form = new ObfuConfMainForm())
				{
					form.ObfuscationConfiguration = this.ComponentMetadataWrapper.GetTableConfiguration().Parent as ObfuscationConfiguration;
					dialogResult = form.ShowDialog(parentWindow);

					if (dialogResult == DialogResult.OK)
						return true;
				}*/
			}
			catch (Exception ex)
			{
				MessageBox.Show(parentWindow, ex.ToString(), Constants.COMPONENT_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return false;
		}

		private IUnitOfWork GetDictionaryUnitOfWork()
		{
			return null;
		}

		public void Help(IWin32Window parentWindow)
		{
			throw new NotSupportedException(string.Format("Not supported."));
		}

		public void Initialize(IDTSComponentMetaData100 dtsComponentMetadata, IServiceProvider serviceProvider)
		{
			this.DtsComponentMetaData100 = dtsComponentMetadata;
			this.ServiceProvider = serviceProvider;
			this.ComponentMetadataWrapper = new __ComponentMetadataWrapper(this.DtsComponentMetaData100, this.GetDictionaryUnitOfWork);
		}

		public void New(IWin32Window parentWindow)
		{
			// do nothing
		}

		#endregion
	}
}