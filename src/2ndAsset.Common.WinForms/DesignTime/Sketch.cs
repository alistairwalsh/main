/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Serialization;

using _2ndAsset.Common.WinForms.DesignTime.Shapes;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	[Serializable]
	[XmlRoot(ElementName = "Sketch", Namespace = "http://candoitfor.com/sketch-model/v1")]
	public class Sketch
	{
		#region Constructors/Destructors

		public Sketch()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<SketchShape> shapes = new List<SketchShape>();

		#endregion

		#region Properties/Indexers/Events

		[XmlArray(ElementName = "Shapes", Order = 0)]
		[XmlArrayItem(typeof(SketchPoint))]
		[XmlArrayItem(typeof(SketchPointSet))]
		[XmlArrayItem(typeof(SketchLine))]
		[XmlArrayItem(typeof(SketchRectangle))]
		[XmlArrayItem(typeof(SketchEllipse))]
		[XmlArrayItem(typeof(SketchText))]
		[XmlArrayItem(typeof(SketchImage))]
		public List<SketchShape> Shapes
		{
			get
			{
				return this.shapes;
			}
		}

		#endregion

		#region Methods/Operators

		public static byte[] GetImageBytesFromSketch(Sketch sketch)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				SaveSketchToImageStream(sketch, stream);
				stream.Seek(0, SeekOrigin.Begin);

				return stream.GetBuffer();
			}
		}

		public static Sketch GetSketchFromXmlData(string xmlData)
		{
			StringReader stringReader;
			Sketch sketch;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(xmlData))
				sketch = new Sketch();
			else
			{
				stringReader = new StringReader(xmlData);
				sketch = LoadFrom(stringReader);
			}

			return sketch;
		}

		public static string GetXmlDataFromSketch(Sketch sketch)
		{
			string xmlData;
			StringWriter stringWriter;

			if ((object)sketch == null)
				return null;

			stringWriter = new StringWriter();

			SaveTo(sketch, stringWriter);

			xmlData = stringWriter.ToString();

			return xmlData;
		}

		public static Sketch LoadFrom(TextReader textReader)
		{
			Sketch sketch;
			XmlSerializer xmlSerializer;

			if ((object)textReader == null)
				throw new ArgumentNullException("textReader");

			xmlSerializer = new XmlSerializer(typeof(Sketch), SketchFactory.KnownTypes);

			sketch = (Sketch)xmlSerializer.Deserialize(textReader);
			return sketch;
		}

		public static Sketch LoadFromFile(string filePath)
		{
			Sketch sketch;

			if ((object)filePath == null)
				throw new ArgumentNullException("filePath");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(filePath))
				throw new ArgumentOutOfRangeException("filePath");

			if (!File.Exists(filePath))
				return null;

			using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (StreamReader streamReader = new StreamReader(stream))
					sketch = LoadFrom(streamReader);
			}

			return sketch;
		}

		public static void SaveSketchToImageFile(Sketch sketch, string filePath)
		{
			if ((object)filePath == null)
				throw new ArgumentNullException("filePath");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(filePath))
				throw new ArgumentOutOfRangeException("filePath");

			using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
				SaveSketchToImageStream(sketch, stream);
		}

		public static void SaveSketchToImageStream(Sketch sketch, Stream stream)
		{
			int width = 0, height = 0;
			Size extent;

			if ((object)sketch == null)
				throw new ArgumentNullException("sketch");

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			foreach (SketchShape shape in sketch.Shapes)
			{
				extent = shape.GetExtent();

				width = Math.Max(width, extent.Width);
				height = Math.Max(height, extent.Height);
			}

			width = Math.Max(width, 1);
			height = Math.Max(height, 1);

			using (Image image = new Bitmap(width, height))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					sketch.RenderToSurface(graphics);
					image.Save(stream, ImageFormat.Png);
				}
			}
		}

		public static void SaveTo(Sketch sketch, TextWriter textWriter)
		{
			XmlSerializer xmlSerializer;

			if ((object)sketch == null)
				throw new ArgumentNullException("sketch");

			if ((object)textWriter == null)
				throw new ArgumentNullException("textWriter");

			xmlSerializer = new XmlSerializer(typeof(Sketch), SketchFactory.KnownTypes);

			xmlSerializer.Serialize(textWriter, sketch);
		}

		public static void SaveToFile(Sketch sketch, string filePath)
		{
			if ((object)sketch == null)
				throw new ArgumentNullException("sketch");

			if ((object)filePath == null)
				throw new ArgumentNullException("filePath");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(filePath))
				throw new ArgumentOutOfRangeException("filePath");

			using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
					SaveTo(sketch, streamWriter);
			}
		}

		public SketchShape GetShapeAtPoint(Point location)
		{
			Rectangle bounds;
			SketchShape shape;

			for (int i = this.Shapes.Count - 1; i >= 0; i--)
			{
				shape = this.Shapes[i];
				bounds = shape.GetBounds();

				if (bounds.Contains(location))
					return shape;
			}

			return null;
		}

		public void RenderToSurface(Graphics surface)
		{
			foreach (SketchShape shape in this.Shapes)
				shape.Render(surface);
		}

		#endregion
	}
}