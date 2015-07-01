/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

using System;
using System.Collections.Generic;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class DataTransfer
	{
		#region Constructors/Destructors

		public DataTransfer()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly FourPartName destination = new FourPartName();
		private readonly List<string> excludeMemberNames = new List<string>();
		private readonly FourPartName source = new FourPartName();

		#endregion

		#region Properties/Indexers/Events

		public FourPartName Destination
		{
			get
			{
				return this.destination;
			}
		}

		public List<string> ExcludeMemberNames
		{
			get
			{
				return this.excludeMemberNames;
			}
		}

		public FourPartName Source
		{
			get
			{
				return this.source;
			}
		}

		#endregion
	}
}