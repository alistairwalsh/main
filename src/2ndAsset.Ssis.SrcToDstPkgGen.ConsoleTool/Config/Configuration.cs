/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Commercial software distribution. May contain open source.
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Serialization;

namespace _2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool.Config
{
	public class Configuration
	{
		#region Constructors/Destructors

		public Configuration()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly FourPartName dictionary = new FourPartName();
		private readonly List<DataTransfer> objects = new List<DataTransfer>();
		private bool? truncateDestination;
		private bool? validateExternalMetadata;

		#endregion

		#region Properties/Indexers/Events

		public FourPartName Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		public List<DataTransfer> Objects
		{
			get
			{
				return this.objects;
			}
		}

		public bool? TruncateDestination
		{
			get
			{
				return this.truncateDestination;
			}
			set
			{
				this.truncateDestination = value;
			}
		}

		public bool? ValidateExternalMetadata
		{
			get
			{
				return this.validateExternalMetadata;
			}
			set
			{
				this.validateExternalMetadata = value;
			}
		}

		#endregion

		#region Methods/Operators

		public static Configuration FromJsonFile(string jsonFile)
		{
			Configuration configuration;

			configuration = new JsonSerializationStrategy().GetObjectFromFile<Configuration>(jsonFile);

			return configuration;
		}

		public static void ToJsonFile(Configuration configuration, string jsonFile)
		{
			new JsonSerializationStrategy().SetObjectToFile<Configuration>(jsonFile, configuration);
		}

		#endregion
	}
}