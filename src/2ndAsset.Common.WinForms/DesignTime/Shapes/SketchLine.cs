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
	[XmlObjectDesignTimeBehavior("Line", true, typeof(SketchLine), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchLine.bmp")]
	public class SketchLine : SketchShape
	{
		#region Constructors/Destructors

		public SketchLine()
		{
		}

		public SketchLine(Point fromPoint, Point toPoint)
		{
			this.fromPoint = fromPoint;
			this.toPoint = toPoint;
		}

		#endregion

		#region Fields/Constants

		private Color foregroundColor = Color.Black;
		private Point fromPoint = Point.Empty;
		private float thickness = 1;
		private Point toPoint = Point.Empty;

		#endregion

		#region Properties/Indexers/Events

		public double Distance
		{
			get
			{
				return SketchUtilities.GetDistanceBetweenPoints(this.FromPoint, this.ToPoint);
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

		[XmlAttribute("from-point")]
		public string _FromPoint
		{
			get
			{
				return new PointConverter().ConvertToString(this.FromPoint);
			}

			set
			{
				this.FromPoint = (Point)(new PointConverter().ConvertFromString(value) ?? Point.Empty);
			}
		}

		[XmlAttribute("to-point")]
		public string _ToPoint
		{
			get
			{
				return new PointConverter().ConvertToString(this.ToPoint);
			}

			set
			{
				this.ToPoint = (Point)(new PointConverter().ConvertFromString(value) ?? Point.Empty);
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

		[XmlIgnore]
		public Point FromPoint
		{
			get
			{
				return this.fromPoint;
			}

			set
			{
				this.fromPoint = value;
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

		[XmlIgnore]
		public Point ToPoint
		{
			get
			{
				return this.toPoint;
			}

			set
			{
				this.toPoint = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override void BeginSketch(Point beginPoint)
		{
			this.FromPoint = beginPoint;
			this.ToPoint = beginPoint;
		}

		public override void ContinueSketch(Point currentPoint)
		{
			this.ToPoint = currentPoint;
		}

		public override void EndSketch(Point endPoint)
		{
			this.ToPoint = endPoint;
		}

		public override Rectangle GetBounds()
		{
			return SketchUtilities.GetRectangleFromPoints(this.FromPoint, this.ToPoint);
		}

		public override bool IsAtPoint(Point location)
		{
			bool retval;

			using (GraphicsPath gp = new GraphicsPath())
			{
				gp.AddLine(this.FromPoint, this.ToPoint);

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
				using (Brush foregroundBrush = new SolidBrush(this.ForegroundColor))
				{
					using (Pen pen = new Pen(foregroundBrush, this.Thickness))
						surface.DrawLine(pen, this.FromPoint, this.ToPoint);

					using (Font font = new Font("Courier New", 8f))
						surface.DrawString(this.Distance.ToString("n"), font, Brushes.Red, this.FromPoint);
				}
			}
		}

		#endregion
	}
}