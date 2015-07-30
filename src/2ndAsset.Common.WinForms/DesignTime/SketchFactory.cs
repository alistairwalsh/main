/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using _2ndAsset.Common.WinForms.DesignTime.Shapes;

namespace _2ndAsset.Common.WinForms.DesignTime
{
	public static class SketchFactory
	{
		#region Constructors/Destructors

		static SketchFactory()
		{
			shapes.Add("SketchPoint", typeof(SketchPoint));
			shapes.Add("SketchPointSet", typeof(SketchPointSet));
			shapes.Add("SketchLine", typeof(SketchLine));
			shapes.Add("SketchRectangle", typeof(SketchRectangle));
			shapes.Add("SketchEllipse", typeof(SketchEllipse));
			shapes.Add("SketchText", typeof(SketchText));
			shapes.Add("SketchImage", typeof(SketchImage));
		}

		#endregion

		#region Fields/Constants

		private static readonly Type[] knownTypes = new Type[]
													{
														//typeof(SketchShape),
														//typeof(SketchPoint),
														//typeof(SketchPointSet),
														//typeof(SketchLine),
														//typeof(SketchRectangle),
														//typeof(SketchEllipse),
														//typeof(SketchText),
														//typeof(SketchImage),
													};

		private static readonly IDictionary<string, Type> shapes = new Dictionary<string, Type>();

		#endregion

		#region Properties/Indexers/Events

		public static Type[] KnownTypes
		{
			get
			{
				return knownTypes;
			}
		}

		public static string[] ShapeKeys
		{
			get
			{
				return shapes.Keys.ToArray();
			}
		}

		#endregion

		#region Methods/Operators

		public static SketchShape CreateShape(string shapeKey)
		{
			Type shapeType;

			if ((object)shapeKey == null)
				throw new ArgumentNullException("shapeKey");

			if (!shapes.TryGetValue(shapeKey, out shapeType))
				return new SketchUnknown(shapeKey);

			return (SketchShape)Activator.CreateInstance(shapeType);
		}

		public static string GetShapeKey(Type shapeType)
		{
			string shapeKey;

			if ((object)shapeType == null)
				throw new ArgumentNullException("shapeType");

			shapeKey = shapes.Where(x => x.Value == shapeType).Select(x => x.Key).SingleOrDefault();

			return shapeKey;
		}

		public static Type GetShapeType(string shapeKey)
		{
			Type shapeType;

			if ((object)shapeKey == null)
				throw new ArgumentNullException("shapeKey");

			if (!shapes.TryGetValue(shapeKey, out shapeType))
				return null;

			return shapeType;
		}

		#endregion
	}
}