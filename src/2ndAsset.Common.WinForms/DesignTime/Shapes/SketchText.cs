/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace _2ndAsset.Common.WinForms.DesignTime.Shapes
{
	[Serializable]
	[XmlObjectDesignTimeBehavior("Text", true, typeof(SketchText), "TextMetal.Common.WinForms.DesignTime.Shapes.Resources.SketchText.bmp")]
	public class SketchText : SketchRectangle
	{
		#region Constructors/Destructors

		public SketchText()
		{
		}

		public SketchText(Rectangle bounds, string text)
			: base(bounds)
		{
			this.text = text ?? string.Empty;
		}

		#endregion

		#region Fields/Constants

		private string fontFamily = "Courier New";
		private float fontSize = 10;
		private FontStyle fontStyle = FontStyle.Regular;
		private StringAlignment stringAlignment = StringAlignment.Near;
		private StringFormatFlags stringFormatFlags = 0;
		private StringTrimming stringTrimming = StringTrimming.None;
		private string text = "";
		private Color textColor = Color.Black;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute("text-color")]
		public string _TextColor
		{
			get
			{
				return ColorTranslator.ToHtml(this.TextColor);
			}

			set
			{
				this.TextColor = ColorTranslator.FromHtml(value);
			}
		}

		[XmlAttribute("font-family")]
		public string FontFamily
		{
			get
			{
				return this.fontFamily;
			}
			set
			{
				this.fontFamily = value;
			}
		}

		[XmlAttribute("font-size")]
		public float FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		[XmlAttribute("font-style")]
		public FontStyle FontStyle
		{
			get
			{
				return this.fontStyle;
			}
			set
			{
				this.fontStyle = value;
			}
		}

		[XmlAttribute("string-alignment")]
		public StringAlignment StringAlignment
		{
			get
			{
				return this.stringAlignment;
			}

			set
			{
				this.stringAlignment = value;
			}
		}

		[XmlAttribute("string-format-flags")]
		public StringFormatFlags StringFormatFlags
		{
			get
			{
				return this.stringFormatFlags;
			}

			set
			{
				this.stringFormatFlags = value;
			}
		}

		[XmlAttribute("string-trimming")]
		public StringTrimming StringTrimming
		{
			get
			{
				return this.stringTrimming;
			}

			set
			{
				this.stringTrimming = value;
			}
		}

		[XmlAttribute("text")]
		public string Text
		{
			get
			{
				return this.text ?? string.Empty;
			}

			set
			{
				this.text = (value ?? string.Empty).Trim();
			}
		}

		[XmlIgnore]
		public Color TextColor
		{
			get
			{
				return this.textColor;
			}

			set
			{
				this.textColor = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override void Render(Graphics surface)
		{
			if ((object)surface == null)
				throw new ArgumentNullException("surface");

			if (this.Visible)
			{
				base.Render(surface);

				using (Brush textBrush = new SolidBrush(this.TextColor))
				{
					using (StringFormat format = new StringFormat())
					{
						format.Alignment = this.StringAlignment;
						format.FormatFlags = this.StringFormatFlags;
						format.Trimming = this.StringTrimming;

						using (Font font = new Font(this.FontFamily, this.FontSize, this.FontStyle))
							surface.DrawString(this.Text, font, textBrush, this.Bounds, format);
					}
				}
			}
		}

		#endregion
	}
}