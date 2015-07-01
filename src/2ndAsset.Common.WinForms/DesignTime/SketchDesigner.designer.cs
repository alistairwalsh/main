using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	public partial class SketchDesigner
	{
		#region Fields/Constants

		private IContainer components;
		private ContextMenu ctxMnuGeneric;
		private ContextMenu ctxMnuSpecific;
		private MenuItem ctxMnuSpecificDelete;
		private MenuItem ctxMnuSpecificProps;
		private ImageList ilMain;
		private MenuItem mnuCtxGenericClear;
		private Controls.BaseGdiPaintSurface pnlCanvas;
		private ToolBar tbarTools;

		#endregion

		#region Methods/Operators

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
					this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new Container();
			ResourceManager resources = new ResourceManager(typeof(SketchDesigner));
			this.pnlCanvas = new Controls.BaseGdiPaintSurface();
			this.ilMain = new ImageList(this.components);
			this.tbarTools = new ToolBar();
			this.ctxMnuGeneric = new ContextMenu();
			this.mnuCtxGenericClear = new MenuItem();
			this.ctxMnuSpecific = new ContextMenu();
			this.ctxMnuSpecificDelete = new MenuItem();
			this.ctxMnuSpecificProps = new MenuItem();
			this.SuspendLayout();
			// 
			// pnlCanvas
			// 
			this.pnlCanvas.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
													  | AnchorStyles.Left)
													 | AnchorStyles.Right)));
			this.pnlCanvas.BackColor = Color.White;
			this.pnlCanvas.Cursor = Cursors.Default;
			this.pnlCanvas.Location = new Point(0, 0);
			this.pnlCanvas.Name = "pnlCanvas";
			this.pnlCanvas.Size = new Size(376, 216);
			this.pnlCanvas.TabIndex = 2;
			this.pnlCanvas.Click += new EventHandler(this.pnlCanvas_Click);
			this.pnlCanvas.MouseUp += new MouseEventHandler(this.pnlCanvas_MouseUp);
			this.pnlCanvas.Paint += new PaintEventHandler(this.pnlCanvas_Paint);
			this.pnlCanvas.DoubleClick += new EventHandler(this.pnlCanvas_DoubleClick);
			this.pnlCanvas.MouseMove += new MouseEventHandler(this.pnlCanvas_MouseMove);
			this.pnlCanvas.MouseDown += new MouseEventHandler(this.pnlCanvas_MouseDown);
			// 
			// ilMain
			// 
			this.ilMain.ColorDepth = ColorDepth.Depth32Bit;
			this.ilMain.ImageSize = new Size(16, 16);
			this.ilMain.ImageStream = ((ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
			this.ilMain.TransparentColor = Color.Magenta;
			// 
			// tbarTools
			// 
			this.tbarTools.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
													 | AnchorStyles.Right)));
			this.tbarTools.Dock = DockStyle.None;
			this.tbarTools.DropDownArrows = true;
			this.tbarTools.ImageList = this.ilMain;
			this.tbarTools.Location = new Point(0, 216);
			this.tbarTools.Name = "tbarTools";
			this.tbarTools.ShowToolTips = true;
			this.tbarTools.Size = new Size(376, 28);
			this.tbarTools.TabIndex = 5;
			this.tbarTools.TextAlign = ToolBarTextAlign.Right;
			this.tbarTools.Wrappable = false;
			this.tbarTools.ButtonClick += new ToolBarButtonClickEventHandler(this.tbarTools_ButtonClick);
			// 
			// ctxMnuGeneric
			// 
			this.ctxMnuGeneric.MenuItems.AddRange(new MenuItem[]
			                                      {
			                                      	this.mnuCtxGenericClear
			                                      });
			// 
			// mnuCtxGenericClear
			// 
			this.mnuCtxGenericClear.Index = 0;
			this.mnuCtxGenericClear.Text = "Clear";
			this.mnuCtxGenericClear.Click += new EventHandler(this.mnuCtxGenericClear_Click);
			// 
			// ctxMnuSpecific
			// 
			this.ctxMnuSpecific.MenuItems.AddRange(new MenuItem[]
			                                       {
			                                       	this.ctxMnuSpecificDelete,
			                                       	this.ctxMnuSpecificProps
			                                       });
			// 
			// ctxMnuSpecificDelete
			// 
			this.ctxMnuSpecificDelete.Index = 0;
			this.ctxMnuSpecificDelete.Text = "Delete";
			this.ctxMnuSpecificDelete.Click += new EventHandler(this.ctxMnuSpecificDelete_Click);
			// 
			// ctxMnuSpecificProps
			// 
			this.ctxMnuSpecificProps.Index = 1;
			this.ctxMnuSpecificProps.Text = "Properties...";
			this.ctxMnuSpecificProps.Click += new EventHandler(this.ctxMnuSpecificProps_Click);
			// 
			// SketchDesigner
			// 
			this.Controls.Add(this.tbarTools);
			this.Controls.Add(this.pnlCanvas);
			this.Name = "SketchDesigner";
			this.Size = new Size(376, 248);
			this.Load += new EventHandler(this.SketchDesigner_Load);
			this.ResumeLayout(false);
		}

		#endregion
	}
}
