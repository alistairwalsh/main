/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class TwoPartName : OnePartName
	{
		#region Constructors/Destructors

		public TwoPartName()
		{
		}

		#endregion

		#region Fields/Constants

		private string schemaName;

		#endregion

		#region Properties/Indexers/Events

		public string SchemaName
		{
			get
			{
				return this.schemaName;
			}
			set
			{
				this.schemaName = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override string ToString()
		{
			return string.Format("[{0}].[{1}]", this.SchemaName, this.ObjectName);
		}

		#endregion
	}
}