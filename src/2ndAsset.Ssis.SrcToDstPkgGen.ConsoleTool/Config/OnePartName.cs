/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class OnePartName
	{
		#region Constructors/Destructors

		public OnePartName()
		{
		}

		#endregion

		#region Fields/Constants

		private string objectName;
		private ObjectType objectType;

		#endregion

		#region Properties/Indexers/Events

		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
			set
			{
				this.objectName = value;
			}
		}

		public ObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
			set
			{
				this.objectType = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override string ToString()
		{
			return string.Format("[{0}]", this.ObjectName);
		}

		#endregion
	}
}