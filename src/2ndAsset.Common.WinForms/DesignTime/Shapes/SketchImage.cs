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
	[XmlObjectDesignTimeBehavior("Image", true, typeof(SketchImage), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchImage.bmp")]
	public class SketchImage : SketchShape
	{
		#region Constructors/Destructors

		public SketchImage()
		{
		}

		public SketchImage(Point locationPoint, Image image)
		{
			this.locationPoint = locationPoint;
			this.image = image;
		}

		#endregion

		#region Fields/Constants

		private Image image;
		protected Point locationPoint;

		#endregion

		#region Properties/Indexers/Events

		[XmlElement("Data")]
		public string _Image
		{
			get
			{
				// TODO
				return null;
			}
			set
			{
				// TODO
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
		public Image Image
		{
			get
			{
				return this.image;
			}

			set
			{
				this.image = value;
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
			return new Rectangle(this.LocationPoint, (object)this.Image != null ? this.Image.Size : Size.Empty);
		}

		public override bool IsAtPoint(Point location)
		{
			bool retval;

			if ((object)this.Image == null)
				return false;

			using (GraphicsPath gp = new GraphicsPath())
			{
				gp.AddRectangle(new Rectangle(this.LocationPoint, this.image.Size));
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
				if ((object)this.Image != null)
					surface.DrawImage(this.Image, this.LocationPoint);
			}
		}

		#endregion
	}
}