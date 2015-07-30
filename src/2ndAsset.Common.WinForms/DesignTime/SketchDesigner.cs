/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	public partial class SketchDesigner : UserControl
	{
		#region Constructors/Destructors

		public SketchDesigner()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private SketchShape currentShape;
		private string currentShapeTypeName;
		private Sketch sketch;

		#endregion

		#region Properties/Indexers/Events

		private SketchShape CurrentShape
		{
			get
			{
				return this.currentShape;
			}
			set
			{
				this.currentShape = value;
			}
		}

		private string CurrentShapeTypeName
		{
			get
			{
				return this.currentShapeTypeName;
			}
			set
			{
				this.currentShapeTypeName = value;
			}
		}

		private Sketch Sketch
		{
			get
			{
				return this.sketch;
			}
			set
			{
				this.sketch = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ctxMnuSpecificDelete_Click(object sender, EventArgs e)
		{
			SketchShape shapeUnderCursor;

			if ((object)this.Sketch == null)
				return;

			// hit test sketch
			shapeUnderCursor = this.Sketch.GetShapeAtPoint(this.pnlCanvas.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

			if ((object)shapeUnderCursor != null)
			{
				this.CurrentShape = shapeUnderCursor;
				this.pnlCanvas.Refresh();

				this.Sketch.Shapes.Remove(shapeUnderCursor);
				this.pnlCanvas.Refresh();
			}
		}

		private void ctxMnuSpecificProps_Click(object sender, EventArgs e)
		{
			SketchShape shapeUnderCursor;

			if ((object)this.Sketch == null)
				return;

			// hit test sketch
			shapeUnderCursor = this.Sketch.GetShapeAtPoint(this.pnlCanvas.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

			if ((object)shapeUnderCursor != null)
			{
				this.CurrentShape = shapeUnderCursor;
				this.pnlCanvas.Refresh();

				using (PropertyForm frmProperty = new PropertyForm(shapeUnderCursor))
				{
					frmProperty.PropertyUpdate += new EventHandler(this.f_PropertyUpdate);
					frmProperty.ShowDialog(this.ParentForm);
					frmProperty.PropertyUpdate -= new EventHandler(this.f_PropertyUpdate);
				}
			}
		}

		private void f_PropertyUpdate(object sender, EventArgs e)
		{
			this.pnlCanvas.Refresh();
		}

		public void InitializeSketching()
		{
			Image image;
			ToolBarButton tbBtnShape;
			Type shapeType;
			XmlObjectDesignTimeBehaviorAttribute xmlObjectDesignTimeBehaviorAttribute;

			tbBtnShape = new ToolBarButton();
			tbBtnShape.ToolTipText = "Arrow";
			tbBtnShape.Tag = null;
			tbBtnShape.ImageIndex =
				this.ilMain.Images.Add(Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchArrow.bmp")), Color.Magenta);
			tbBtnShape.Pushed = true;

			this.tbarTools.Buttons.Add(tbBtnShape);

			foreach (string shapeKey in SketchFactory.ShapeKeys)
			{
				tbBtnShape = new ToolBarButton();

				shapeType = SketchFactory.GetShapeType(shapeKey);

				if ((object)shapeType == null)
					throw new InvalidOperationException();

				xmlObjectDesignTimeBehaviorAttribute = ReflectionFascade.Instance.GetOneAttribute<XmlObjectDesignTimeBehaviorAttribute>(shapeType);

				if ((object)xmlObjectDesignTimeBehaviorAttribute == null)
					continue;

				if (!xmlObjectDesignTimeBehaviorAttribute.ShowInToolbox)
					continue;

				tbBtnShape.ToolTipText = xmlObjectDesignTimeBehaviorAttribute.Description;
				tbBtnShape.Tag = shapeKey;

				image = xmlObjectDesignTimeBehaviorAttribute.GetToolboxImage();

				if ((object)image != null)
					tbBtnShape.ImageIndex = this.ilMain.Images.Add(image, Color.Magenta);
				else
					tbBtnShape.ImageIndex = 4;

				this.tbarTools.Buttons.Add(tbBtnShape);
			}

			this.SetSketch(null);
		}

		private void mnuCtxGenericClear_Click(object sender, EventArgs e)
		{
			if ((object)this.Sketch == null)
				return;

			if (MessageBox.Show(this.ParentForm, "Are you sure you want to clear the sketch?",
				"Sketch Designer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			this.Sketch.Shapes.Clear();
			this.pnlCanvas.Refresh();
		}

		private void pnlCanvas_Click(object sender, EventArgs e)
		{
		}

		private void pnlCanvas_DoubleClick(object sender, EventArgs e)
		{
			SketchShape shapeUnderCursor;

			if ((object)this.Sketch == null)
				return;

			if ((object)this.CurrentShapeTypeName == null)
			{
				// hit test sketch
				shapeUnderCursor = this.Sketch.GetShapeAtPoint(this.pnlCanvas.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

				if ((object)shapeUnderCursor != null)
				{
					this.CurrentShape = shapeUnderCursor;
					this.pnlCanvas.Refresh();

					using (PropertyForm frmProperty = new PropertyForm(shapeUnderCursor))
					{
						frmProperty.PropertyUpdate += new EventHandler(this.f_PropertyUpdate);
						frmProperty.ShowDialog(this.ParentForm);
						frmProperty.PropertyUpdate -= new EventHandler(this.f_PropertyUpdate);
					}
				}
			}
		}

		private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
		{
			SketchShape shapeUnderCursor;

			if ((object)this.Sketch == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if ((object)this.CurrentShapeTypeName == null)
				{
					// hit test sketch
					shapeUnderCursor = this.Sketch.GetShapeAtPoint(this.pnlCanvas.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

					if ((object)shapeUnderCursor != null)
					{
						this.CurrentShape = shapeUnderCursor;
						this.pnlCanvas.Refresh();
					}
					else
					{
						this.CurrentShape = null;
						this.pnlCanvas.Refresh();
					}
				}
				else
				{
					this.CurrentShape = null;
					this.pnlCanvas.Refresh();

					if ((object)this.CurrentShape != null)
						return;

					this.CurrentShape = SketchFactory.CreateShape(this.CurrentShapeTypeName);

					if ((object)this.CurrentShape == null)
					{
						Cursor.Current = Cursors.No;
						return;
					}

					this.CurrentShape.BeginSketch(new Point(e.X, e.Y));
					this.pnlCanvas.Refresh();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if ((object)this.CurrentShapeTypeName == null)
				{
					// hit test sketch
					shapeUnderCursor = this.Sketch.GetShapeAtPoint(this.pnlCanvas.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

					if ((object)shapeUnderCursor != null)
					{
						this.CurrentShape = shapeUnderCursor;
						this.pnlCanvas.Refresh();

						this.ctxMnuSpecific.Show(this, new Point(e.X, e.Y));
					}
					else
					{
						this.CurrentShape = null;
						this.pnlCanvas.Refresh();

						this.ctxMnuGeneric.Show(this, new Point(e.X, e.Y));
					}
				}
				else
				{
					this.CurrentShape = null;
					this.pnlCanvas.Refresh();
				}
			}
		}

		private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if ((object)this.Sketch == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if ((object)this.CurrentShapeTypeName == null)
				{
					this.CurrentShape = null;
					this.pnlCanvas.Refresh();
				}
				else
				{
					//this.currentShape = null;
					//this.pnlCanvas.Refresh();

					if ((object)this.CurrentShape == null)
						return;

					this.CurrentShape.ContinueSketch(new Point(e.X, e.Y));
					this.pnlCanvas.Refresh();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				this.CurrentShape = null;
				this.pnlCanvas.Refresh();
			}
		}

		private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
		{
			if ((object)this.Sketch == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if ((object)this.CurrentShapeTypeName == null)
				{
					this.CurrentShape = null;
					this.pnlCanvas.Refresh();
				}
				else
				{
					//this.currentShape = null;
					//this.pnlCanvas.Refresh();

					if ((object)this.CurrentShape == null)
						return;

					this.CurrentShape.EndSketch(new Point(e.X, e.Y));
					this.Sketch.Shapes.Add(this.CurrentShape);
					this.CurrentShape = null;

					this.pnlCanvas.Refresh();
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				this.CurrentShape = null;
				this.pnlCanvas.Refresh();
			}
		}

		private void pnlCanvas_Paint(object sender, PaintEventArgs e)
		{
			if ((object)this.Sketch == null)
				return;

			// draw sketch:
			this.Sketch.RenderToSurface(e.Graphics);

			if ((object)this.CurrentShapeTypeName == null)
			{
				// highlight selected
				if ((object)this.CurrentShape != null)
					SketchUtilities.DrawFocusRect(e.Graphics, this.CurrentShape.GetBounds());
			}
			else
			{
				// draw current:
				if ((object)this.CurrentShape != null)
					this.CurrentShape.Render(e.Graphics);
			}
		}

		public void SetSketch(Sketch sketch)
		{
			this.Sketch = sketch;

			foreach (ToolBarButton tbBtn in this.tbarTools.Buttons)
				tbBtn.Enabled = (object)this.Sketch != null;

			this.pnlCanvas.Enabled = (object)this.Sketch != null;

			this.pnlCanvas.Refresh();
		}

		private void SketchDesigner_Load(object sender, EventArgs e)
		{
			// do nothing
		}

		private void tbarTools_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			ToolBarButton tbBtnOldShape = null;

			if ((object)this.Sketch == null)
				return;

			foreach (ToolBarButton tbBtnShape in this.tbarTools.Buttons)
			{
				if (tbBtnShape.Pushed)
				{
					tbBtnOldShape = tbBtnShape;
					break;
				}
			}

			if ((object)tbBtnOldShape == null)
				tbBtnOldShape = e.Button;

			tbBtnOldShape.Pushed = false;
			e.Button.Pushed = true;

			this.CurrentShapeTypeName = (string)e.Button.Tag;
		}

		#endregion
	}
}