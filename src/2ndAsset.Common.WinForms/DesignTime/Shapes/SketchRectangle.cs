/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace _2ndAsset.Common.WinForms.DesignTime.Shapes
{
	[Serializable]
	[XmlObjectDesignTimeBehavior("Rectangle", true, typeof(SketchRectangle), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchRectangle.bmp")]
	public class SketchRectangle : SketchShape
	{
		#region Constructors/Destructors

		public SketchRectangle()
		{
		}

		public SketchRectangle(Rectangle bounds)
		{
			this.bounds = bounds;
		}

		#endregion

		#region Fields/Constants

		private Color backgroundColor = Color.Transparent;
		private Point beginPoint;
		private Rectangle bounds;
		private Color foregroundColor = Color.Black;
		private int radius = 1;
		private float thickness = 1;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute("background-color")]
		public string _BackgroundColor
		{
			get
			{
				return ColorTranslator.ToHtml(this.BackgroundColor);
			}

			set
			{
				this.BackgroundColor = ColorTranslator.FromHtml(value);
			}
		}

		[XmlAttribute("bounds")]
		public string _Bounds
		{
			get
			{
				return new RectangleConverter().ConvertToString(this.Bounds);
			}

			set
			{
				this.Bounds = (Rectangle)(new RectangleConverter().ConvertFromString(value) ?? Rectangle.Empty);
			}
		}

		[XmlAttribute("foreground-color")]
		public string _ForegroundColor
		{
			get
			{
				return ColorTranslator.ToHtml(this.ForegroundColor);
			}

			set
			{
				this.ForegroundColor = ColorTranslator.FromHtml(value);
			}
		}

		[XmlIgnore]
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}

			set
			{
				this.backgroundColor = value;
			}
		}

		protected Point BeginPoint
		{
			get
			{
				return this.beginPoint;
			}
			set
			{
				this.beginPoint = value;
			}
		}

		[XmlIgnore]
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}

			set
			{
				this.bounds = value;
			}
		}

		[XmlIgnore]
		public Color ForegroundColor
		{
			get
			{
				return this.foregroundColor;
			}

			set
			{
				this.foregroundColor = value;
			}
		}

		[XmlAttribute("radius")]
		public int Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = value;
			}
		}

		[XmlAttribute("thickness")]
		public float Thickness
		{
			get
			{
				return this.thickness;
			}

			set
			{
				this.thickness = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ApplyRoundedCornerRectangle(GraphicsPath path)
		{
			if ((object)path == null)
				throw new ArgumentNullException("path");

			// rounded corner rectangle
			int local0 = this.bounds.X + this.bounds.Width - this.radius * 2;
			int local1 = this.bounds.Y + this.bounds.Height - this.radius * 2;
			int local2 = this.bounds.X + this.radius;
			int local3 = this.radius * 2;

			path.AddLine(local2, this.bounds.Y, local0, this.bounds.Y);
			path.AddArc(local0, this.bounds.Y, local3, local3, 270, 90);
			path.AddLine(this.bounds.X + this.bounds.Width, this.bounds.Y + this.radius, this.bounds.X + this.bounds.Width, local1);
			path.AddArc(local0, local1, local3, local3, 0, 90);
			path.AddLine(local0, this.bounds.Y + this.bounds.Height, local2, this.bounds.Y + this.bounds.Height);
			path.AddArc(this.bounds.X, local1, local3, local3, 90, 90);
			path.AddLine(this.bounds.X, local1, this.bounds.X, this.bounds.Y + this.radius);
			path.AddArc(this.bounds.X, this.bounds.Y, local3, local3, 180, 90);
		}

		public override void BeginSketch(Point beginPoint)
		{
			this.BeginPoint = beginPoint;
			this.Bounds = new Rectangle(beginPoint.X, beginPoint.Y, 0, 0);
		}

		public override void ContinueSketch(Point currentPoint)
		{
			this.Bounds = SketchUtilities.GetRectangleFromPoints(this.BeginPoint, currentPoint);
		}

		public override void EndSketch(Point endPoint)
		{
			this.Bounds = SketchUtilities.GetRectangleFromPoints(this.BeginPoint, endPoint);
			this.BeginPoint = Point.Empty;
		}

		public override Rectangle GetBounds()
		{
			return this.Bounds;
		}

		public override bool IsAtPoint(Point location)
		{
			bool retval;

			using (GraphicsPath gp = new GraphicsPath())
			{
				if (this.Radius > 0)
				{
					this.ApplyRoundedCornerRectangle(gp);
					gp.CloseFigure();
				}
				else
					gp.AddRectangle(this.Bounds);

				using (Brush brush = new SolidBrush(this.ForegroundColor))
				{
					using (Pen pen = new Pen(brush, this.Thickness))
					{
						gp.Widen(pen);
						retval = gp.IsVisible(location);
					}
				}
			}

			return retval;
		}

		public override bool IsInBounds(Rectangle bounds)
		{
			throw new NotImplementedException();
		}

		public override void Render(Graphics surface)
		{
			if ((object)surface == null)
				throw new ArgumentNullException("surface");

			if (this.Visible)
			{
				using (GraphicsPath gp = new GraphicsPath())
				{
					if (this.Radius > 0)
					{
						this.ApplyRoundedCornerRectangle(gp);
						gp.CloseFigure();
					}

					using (Brush backgroundBrush = new SolidBrush(this.BackgroundColor))
					{
						if (this.Radius > 0)
							surface.FillPath(backgroundBrush, gp);
						else
							surface.FillRectangle(backgroundBrush, this.Bounds);

						using (Brush foregroundBrush = new SolidBrush(this.ForegroundColor))
						{
							using (Pen pen = new Pen(foregroundBrush, this.Thickness))
							{
								if (this.Radius > 0)
									surface.DrawPath(pen, gp);
								else
									surface.DrawRectangle(pen, this.Bounds);
							}
						}
					}
				}
			}
		}

		#endregion
	}
}