/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.IO;

namespace _2ndAsset.Common.WinForms.DesignTime.Shapes
{
	[Serializable]
	[XmlObjectDesignTimeBehavior("Unknown Shape", false, typeof(SketchUnknown), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchUnknown.bmp")]
	public class SketchUnknown : SketchShape
	{
		#region Constructors/Destructors

		public SketchUnknown(string shapeData)
		{
			this.shapeData = shapeData;
		}

		#endregion

		#region Fields/Constants

		private string shapeData;

		#endregion

		#region Methods/Operators

		public override void BeginSketch(Point beginPoint)
		{
		}

		public override void ContinueSketch(Point currentPoint)
		{
		}

		public override void EndSketch(Point endPoint)
		{
		}

		public override Rectangle GetBounds()
		{
			return Rectangle.Empty;
		}

		public override bool IsAtPoint(Point location)
		{
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
				using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchUnknown.bmp"))
				{
					if ((object)stream == null)
						return;

					using (Image image = Image.FromStream(stream))
					{
						if (image is Bitmap)
							((Bitmap)image).MakeTransparent(Color.Magenta);

						surface.DrawImage(image, Point.Empty);
					}
				}
			}
		}

		#endregion
	}
}