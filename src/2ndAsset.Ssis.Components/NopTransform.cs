/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Diagnostics;

using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace _2ndAsset.Ssis.Components
{
#if DEBUG
	[DtsPipelineComponent(DisplayName = NopConstants.COMPONENT_NAME,
		Description = NopConstants.COMPONENT_DESCRIPTION,
		UITypeName = NopConstants.COMPONENT_UI_AQTN,
		ComponentType = ComponentType.Transform,
		CurrentVersion = NopConstants.COMPONENT_CURRENT_VERSION,
		IconResource = NopConstants.COMPONENT_ICON_RESOURCE_NAME)]
	public class NopTransform : PipelineComponent
	{
		#region Constructors/Destructors

		public NopTransform()
		{
		}

		#endregion

		#region Methods/Operators

		public override void Initialize()
		{
			this.TryLaunchDebugger(true);

			base.Initialize();
		}

		public override void PerformUpgrade(int pipelineVersion)
		{
			if (pipelineVersion < NopConstants.COMPONENT_CURRENT_VERSION)
				this.SetComponentVersion(pipelineVersion);
		}

		public override void ProvideComponentProperties()
		{
			IDTSInput100 dtsInput100;
			IDTSOutput100 dtsOutput100;
			IDTSOutput100 dtsErrorOutput100;
			IDTSCustomProperty100 dtsCustomProperty100;
			IDTSRuntimeConnection100 dtsRuntimeConnection100;

			// set component information
			this.ComponentMetaData.Name = NopConstants.COMPONENT_NAME;
			this.ComponentMetaData.Description = NopConstants.COMPONENT_DESCRIPTION;
			this.ComponentMetaData.ContactInfo = NopConstants.COMPONENT_EMAIL;
			this.ComponentMetaData.Version = NopConstants.COMPONENT_CURRENT_VERSION;

			// reset the component
			this.RemoveAllInputsOutputsAndCustomProperties();
			this.ComponentMetaData.RuntimeConnectionCollection.RemoveAll();

			this.ComponentMetaData.UsesDispositions = true;

			// add an input object
			dtsInput100 = this.ComponentMetaData.InputCollection.New();
			dtsInput100.Name = NopConstants.COMPONENT_INPUT_DEFAULT_NAME;
			//dtsInput100.HasSideEffects = true;
			dtsInput100.ErrorRowDisposition = DTSRowDisposition.RD_NotUsed;

			// add an output object
			dtsOutput100 = this.ComponentMetaData.OutputCollection.New();
			dtsOutput100.Name = NopConstants.COMPONENT_OUTPUT_DEFAULT_NAME;
			dtsOutput100.IsErrorOut = false;
			dtsOutput100.SynchronousInputID = dtsInput100.ID; // synchronous transform

			// add an error object
			dtsErrorOutput100 = this.ComponentMetaData.OutputCollection.New();
			dtsErrorOutput100.Name = NopConstants.COMPONENT_OUTPUT_ERROR_NAME;
			dtsErrorOutput100.IsErrorOut = true;
			dtsErrorOutput100.SynchronousInputID = dtsInput100.ID; // synchronous transform

			// add custom properties to component etc.
			dtsCustomProperty100 = this.ComponentMetaData.CustomPropertyCollection.New();
			dtsCustomProperty100.Name = NopConstants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH;
			dtsCustomProperty100.Description = NopConstants.COMPONENT_PROP_DESC_DEBUGGER_LAUNCH;
			dtsCustomProperty100.TypeConverter = typeof(bool).AssemblyQualifiedName;
			//dtsCustomProperty100.UITypeEditor = null;
			dtsCustomProperty100.Value = false;
			dtsCustomProperty100.EncryptionRequired = false;

			// add connections
			dtsRuntimeConnection100 = this.ComponentMetaData.RuntimeConnectionCollection.New();
			dtsRuntimeConnection100.Name = NopConstants.COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY;
			dtsRuntimeConnection100.Description = NopConstants.COMPONENT_RUNTIMECONNECTION_DESC_DICTIONARY;
		}

		public override void ReinitializeMetaData()
		{
			if (!this.ComponentMetaData.AreInputColumnsValid)
				this.ComponentMetaData.RemoveInvalidInputColumns();

			base.ReinitializeMetaData();
		}

		private void SetComponentVersion(int pipelineVersion)
		{
			this.ComponentMetaData.Version = NopConstants.COMPONENT_CURRENT_VERSION;
		}

		private void TryLaunchDebugger(bool force)
		{
#if DEBUG
			if (force)
				Debugger.Launch();
#endif
		}

		#endregion
	}
#endif
}