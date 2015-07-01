/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace _2ndAsset.Common.WinForms.DesignTime.Shapes
{
	[Serializable]
	[XmlObjectDesignTimeBehavior("Point Set", true, typeof(SketchPointSet), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchPointSet.bmp")]
	public class SketchPointSet : SketchShape
	{
		#region Constructors/Destructors

		public SketchPointSet()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<SketchPoint> points = new List<SketchPoint>();
		private Color foregroundColor = Color.Black;
		private int thickness = 2;

		#endregion

		#region Properties/Indexers/Events

		[XmlArray(ElementName = "SketchPoints", Order = 0)]
		[XmlArrayItem(ElementName = "SketchPoint")]
		public List<SketchPoint> Points
		{
			get
			{
				return this.points;
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
		public int Thickness
		{
			get
			{
				return this.thickness;
			}

			set
			{
				this.thickness = value;

				foreach (SketchPoint point in this.Points)
					point.Thickness = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override void BeginSketch(Point beginPoint)
		{
			this.Points.Add(new SketchPoint(beginPoint));
		}

		public override void ContinueSketch(Point currentPoint)
		{
			this.Points.Add(new SketchPoint(currentPoint));
		}

		public override void EndSketch(Point endPoint)
		{
			this.Points.Add(new SketchPoint(endPoint));
		}

		public override Rectangle GetBounds()
		{
			return Rectangle.Empty;
		}

		public override bool IsAtPoint(Point location)
		{
			foreach (SketchPoint point in this.Points)
			{
				if (point.IsAtPoint(location))
					return true;
			}
			return false;
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
				foreach (SketchPoint point in this.Points)
				{
					point.ForegroundColor = this.ForegroundColor;
					point.Thickness = this.Thickness;
					point.Render(surface);
				}
			}
		}

		#endregion
	}
}