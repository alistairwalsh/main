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
	[XmlObjectDesignTimeBehavior("Point", true, typeof(SketchPoint), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchPoint.bmp")]
	public class SketchPoint : SketchShape
	{
		#region Constructors/Destructors

		public SketchPoint()
		{
		}

		public SketchPoint(Point locationPoint)
		{
			this.locationPoint = locationPoint;
		}

		#endregion

		#region Fields/Constants

		private Color foregroundColor = Color.Black;
		protected Point locationPoint = Point.Empty;
		private int thickness = 2;

		#endregion

		#region Properties/Indexers/Events

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

		[XmlAttribute("location-point")]
		public string _LocationPoint
		{
			get
			{
				return new PointConverter().ConvertToString(this.LocationPoint);
			}

			set
			{
				this.LocationPoint = (Point)(new PointConverter().ConvertFromString(value) ?? Point.Empty);
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
		public Point LocationPoint
		{
			get
			{
				return this.locationPoint;
			}

			set
			{
				this.locationPoint = value;
			}
		}

		[XmlAttribute("thickness")]
		public int Thickness
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
			this.LocationPoint = beginPoint;
		}

		public override void ContinueSketch(Point currentPoint)
		{
			this.LocationPoint = currentPoint;
		}

		public override void EndSketch(Point endPoint)
		{
			this.LocationPoint = endPoint;
		}

		public override Rectangle GetBounds()
		{
			return new Rectangle(this.LocationPoint.X, this.LocationPoint.Y, this.LocationPoint.X + this.Thickness, this.LocationPoint.Y + this.Thickness);
		}

		public override bool IsAtPoint(Point location)
		{
			bool retval;

			using (GraphicsPath gp = new GraphicsPath())
			{
				gp.AddRectangle(new Rectangle(this.LocationPoint, new Size(this.Thickness, this.Thickness)));

				retval = gp.IsVisible(location);
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
				using (Bitmap bitmap = new Bitmap(this.Thickness, this.Thickness))
				{
					for (int px = 0; px < this.Thickness; px++)
					{
						for (int py = 0; py < this.Thickness; py++)
							bitmap.SetPixel(px, py, this.ForegroundColor);
					}

					surface.DrawImageUnscaled(bitmap, this.LocationPoint);
				}
			}
		}

		#endregion
	}
}