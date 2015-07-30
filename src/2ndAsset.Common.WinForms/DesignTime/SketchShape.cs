/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	[Serializable]
	public abstract class SketchShape
	{
		#region Constructors/Destructors

		protected SketchShape()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;
		private bool visible = true;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name ?? string.Empty;
			}

			set
			{
				this.name = (value ?? string.Empty).Trim();
			}
		}

		[XmlAttribute("visible")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}

			set
			{
				this.visible = value;
			}
		}

		#endregion

		#region Methods/Operators

		public abstract void BeginSketch(Point beginPoint);

		public abstract void ContinueSketch(Point currentPoint);

		public abstract void EndSketch(Point endPoint);

		public abstract Rectangle GetBounds();

		public Size GetExtent()
		{
			Rectangle bounds;
			Size extent;
			bounds = this.GetBounds();
			extent = SketchUtilities.GetExtent(bounds);
			return extent;
		}

		public abstract bool IsAtPoint(Point location);

		public abstract bool IsInBounds(Rectangle bounds);

		public abstract void Render(Graphics surface);

		#endregion
	}
}