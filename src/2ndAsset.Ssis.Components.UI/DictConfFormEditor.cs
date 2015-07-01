/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace _2ndAsset.Ssis.Components.UI
{
	public class DictConfFormEditor : UITypeEditor
	{
		#region Constructors/Destructors

		public DictConfFormEditor()
		{
		}

		#endregion

		#region Methods/Operators

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService formsEditorService;

			if ((object)context == null)
				throw new ArgumentNullException("context");

			if ((object)provider == null)
				throw new ArgumentNullException("provider");

			formsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			if ((object)formsEditorService != null)
			{
				//using (DictConfMainForm dictConfMainForm = new DictConfMainForm())
				//{
				//	dictConfMainForm.StartPosition = FormStartPosition.CenterParent;
				//	dictConfMainForm.EditValue = (string)value;

				//	if (formsEditorService.ShowDialog(dictConfMainForm) == DialogResult.OK)
				//		return (string)dictConfMainForm.EditValue;
				//}
			}

			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		#endregion
	}
}