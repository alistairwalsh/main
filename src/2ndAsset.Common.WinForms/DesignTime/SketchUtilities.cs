/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	public static class SketchUtilities
	{
		#region Methods/Operators

		public static Color DeserializeColor(string color)
		{
			return ColorTranslator.FromHtml(color);
		}

		[DllImport("user32.dll")]
		private static extern bool DrawFocusRect(IntPtr hDC, ref RECT lprc);

		public static void DrawFocusRect(Graphics g, Rectangle rect)
		{
			IntPtr hdc = IntPtr.Zero;
			RECT r;

			try
			{
				r = RECT.FromRectangle(rect);
				hdc = g.GetHdc();
				DrawFocusRect(hdc, ref r);
			}
			finally
			{
				if (hdc != IntPtr.Zero)
				{
					g.ReleaseHdc(hdc);
					hdc = IntPtr.Zero;
				}
			}
		}

		/// <summary>
		/// Calculates the distance between two points.
		/// </summary>
		/// <param name="from"> The from point. </param>
		/// <param name="to"> The to point. </param>
		/// <returns> The distance. </returns>
		public static double GetDistanceBetweenPointFs(PointF from, PointF to)
		{
			double d = 0.0;

			d = Math.Sqrt(Math.Pow(to.X - from.X, 2.0) + Math.Pow(to.Y - from.Y, 2.0));

			return d;
		}

		/// <summary>
		/// Calculates the distance between two points.
		/// </summary>
		/// <param name="from"> The from point. </param>
		/// <param name="to"> The to point. </param>
		/// <returns> The distance. </returns>
		public static double GetDistanceBetweenPoints(Point from, Point to)
		{
			double d = 0.0;

			d = Math.Sqrt(Math.Pow(to.X - from.X, 2.0) + Math.Pow(to.Y - from.Y, 2.0));

			return d;
		}

		public static Size GetExtent(Rectangle bounds)
		{
			Size extent;
			extent = new Size(bounds.X + bounds.Width, bounds.Y + bounds.Height);
			return extent;
		}

		/// <summary>
		/// Calculates a bounding rectangle given two points.
		/// </summary>
		/// <param name="from"> The from point. </param>
		/// <param name="to"> The to point. </param>
		/// <returns> The bounding rectangle. </returns>
		public static RectangleF GetRectangleFFromPointFs(PointF from, PointF to)
		{
			RectangleF rect;

			if (to.X == from.X && to.Y == from.Y)
				rect = RectangleF.FromLTRB(from.X, from.Y, to.X, to.Y);
			else if (to.X < from.X && to.Y < from.Y)
				rect = RectangleF.FromLTRB(to.X, to.Y, from.X, from.Y);
			else if (to.X < from.X && to.Y > from.Y)
				rect = RectangleF.FromLTRB(to.X, from.Y, from.X, to.Y);
			else if (to.X > from.X && to.Y < from.Y)
				rect = RectangleF.FromLTRB(from.X, to.Y, to.X, from.Y);
			else if (to.X > from.X && to.Y > from.Y)
				rect = RectangleF.FromLTRB(from.X, from.Y, to.X, to.Y);
			else
				rect = RectangleF.Empty;

			return rect;
		}

		/// <summary>
		/// Calculates a bounding rectangle given two points.
		/// </summary>
		/// <param name="from"> The from point. </param>
		/// <param name="to"> The to point. </param>
		/// <returns> The bounding rectangle. </returns>
		public static Rectangle GetRectangleFromPoints(Point from, Point to)
		{
			Rectangle rect;

			if (to.X == from.X && to.Y == from.Y)
				rect = Rectangle.FromLTRB(from.X, from.Y, to.X, to.Y);
			else if (to.X < from.X && to.Y < from.Y)
				rect = Rectangle.FromLTRB(to.X, to.Y, from.X, from.Y);
			else if (to.X < from.X && to.Y > from.Y)
				rect = Rectangle.FromLTRB(to.X, from.Y, from.X, to.Y);
			else if (to.X > from.X && to.Y < from.Y)
				rect = Rectangle.FromLTRB(from.X, to.Y, to.X, from.Y);
			else if (to.X > from.X && to.Y > from.Y)
				rect = Rectangle.FromLTRB(from.X, from.Y, to.X, to.Y);
			else
				rect = Rectangle.Empty;

			return rect;
		}

		public static string SerializeColor(Color color)
		{
			return ColorTranslator.ToHtml(color);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			#region Fields/Constants

			public int Bottom;
			public int Left;
			public int Right;
			public int Top;

			#endregion

			#region Methods/Operators

			public static RECT FromRectangle(Rectangle rect)
			{
				RECT r;
				r.Left = rect.Left;
				r.Top = rect.Top;
				r.Right = rect.Right;
				r.Bottom = rect.Bottom;

				return r;
			}

			public Rectangle ToRectangle()
			{
				return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
			}

			#endregion
		}

		#endregion
	}
}