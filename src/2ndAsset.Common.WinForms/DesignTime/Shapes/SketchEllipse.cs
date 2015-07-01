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
	[XmlObjectDesignTimeBehavior("Ellipse", true, typeof(SketchEllipse), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchEllipse.bmp")]
	public class SketchEllipse : SketchShape
	{
		#region Constructors/Destructors

		public SketchEllipse()
		{
		}

		public SketchEllipse(Rectangle bounds)
		{
			this.bounds = bounds;
		}

		#endregion

		#region Fields/Constants

		private Color backgroundColor = Color.Transparent;
		private Point beginPoint;
		private Rectangle bounds = Rectangle.Empty;
		private Color foregroundColor = Color.Black;
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

		public override void BeginSketch(Point beginPoint)
		{
			this.BeginPoint = beginPoint;
			this.Bounds = new Rectangle(beginPoint.X, beginPoint.Y, 0, 0);
		}

		public override void ContinueSketch(Point currentPoint)
		{
			this.Bounds = SketchUtilities.GetRectangleFromPoints(this.beginPoint, currentPoint);
		}

		public override void EndSketch(Point endPoint)
		{
			this.Bounds = SketchUtilities.GetRectangleFromPoints(this.beginPoint, endPoint);
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
				gp.AddEllipse(this.Bounds);

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
				using (Brush backgroundBrush = new SolidBrush(this.BackgroundColor))
				{
					surface.FillEllipse(backgroundBrush, this.Bounds);

					using (Brush foregroundBrush = new SolidBrush(this.ForegroundColor))
					{
						using (Pen pen = new Pen(foregroundBrush, this.Thickness))
							surface.DrawEllipse(pen, this.Bounds);
					}
				}
			}
		}

		#endregion
	}
}