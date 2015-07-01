/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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