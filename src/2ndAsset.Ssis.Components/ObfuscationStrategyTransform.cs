/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;

using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

using x__ComponentMetadataWrapper = _2ndAsset.Ssis.Components.__ComponentMetadataWrapper;

namespace _2ndAsset.Ssis.Components
{
	[DtsPipelineComponent(DisplayName = Constants.COMPONENT_NAME,
		Description = Constants.COMPONENT_DESCRIPTION,
		UITypeName = Constants.COMPONENT_UI_AQTN,
		ComponentType = ComponentType.Transform,
		CurrentVersion = Constants.COMPONENT_CURRENT_VERSION,
		IconResource = Constants.COMPONENT_ICON_RESOURCE_NAME)]
	public class ObfuscationStrategyTransform : PipelineComponent, IOxymoronHost
	{
		#region Constructors/Destructors

		public ObfuscationStrategyTransform()
		{
		}

		#endregion

		#region Fields/Constants

		private const int E_FAIL = unchecked((int)0x80004005);
		private readonly List<IDictionaryAdapter> dictionaryAdapters = new DisposableList<IDictionaryAdapter>();
		private readonly IDictionary<DictionaryConfiguration, IDictionaryAdapter> dictionaryConfigurationToAdapterMappings = new Dictionary<DictionaryConfiguration, IDictionaryAdapter>();
		private readonly IList<ColumnInfo> inputColumnInfos = new List<ColumnInfo>();

		private readonly DataType[] validPipelineComponentsDataTypes = new DataType[]
																		{
																			DataType.DT_WSTR,
																			DataType.DT_BYTES,
																			DataType.DT_DBTIMESTAMP,
																			DataType.DT_DBTIMESTAMPOFFSET,
																			DataType.DT_DBTIME2,
																			DataType.DT_NUMERIC,
																			DataType.DT_GUID,
																			DataType.DT_I1,
																			DataType.DT_I2,
																			DataType.DT_I4,
																			DataType.DT_I8,
																			DataType.DT_BOOL,
																			DataType.DT_R4,
																			DataType.DT_R8,
																			DataType.DT_UI1,
																			DataType.DT_UI2,
																			DataType.DT_UI4,
																			DataType.DT_UI8
																		};

		private x__ComponentMetadataWrapper componentMetadataWrapper;
		private IDbConnection dictionaryDbConnection;
		private bool disposed;
		private IOxymoronEngine oxymoronEngine;

		#endregion

		#region Properties/Indexers/Events

		private List<IDictionaryAdapter> DictionaryAdapters
		{
			get
			{
				return this.dictionaryAdapters;
			}
		}

		private IDictionary<DictionaryConfiguration, IDictionaryAdapter> DictionaryConfigurationToAdapterMappings
		{
			get
			{
				return this.dictionaryConfigurationToAdapterMappings;
			}
		}

		private IList<ColumnInfo> InputColumnInfos
		{
			get
			{
				return this.inputColumnInfos;
			}
		}

		private x__ComponentMetadataWrapper ComponentMetadataWrapper
		{
			get
			{
				return this.componentMetadataWrapper;
			}
			set
			{
				this.componentMetadataWrapper = value;
			}
		}

		private IDbConnection DictionaryDbConnection
		{
			get
			{
				return this.dictionaryDbConnection;
			}
			set
			{
				this.dictionaryDbConnection = value;
			}
		}

		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
			private set
			{
				this.disposed = value;
			}
		}

		private IOxymoronEngine OxymoronEngine
		{
			get
			{
				return this.oxymoronEngine;
			}
			set
			{
				this.oxymoronEngine = value;
			}
		}

		#endregion

		#region Methods/Operators

		private static Type InferClrTypeForSsisDataType(DataType dataType)
		{
			if (dataType == DataType.DT_IMAGE ||
				dataType == DataType.DT_NTEXT ||
				dataType == DataType.DT_TEXT)
				return typeof(Object); // LONG DATA TYPES ARE NOT SUPPORTED
			else
				return BufferTypeToDataRecordType(dataType);
		}

		private static void SetBufferValue(PipelineBuffer outputBuffer, int outputBufferIndex, object value, DataType dataType)
		{
			if ((object)outputBuffer == null)
				throw new ArgumentNullException("outputBuffer");

			if ((object)value == null)
				outputBuffer.SetNull(outputBufferIndex);
			else
			{
				switch (dataType)
				{
					case DataType.DT_BOOL:
						outputBuffer.SetBoolean(outputBufferIndex, Convert.ToBoolean(value));
						break;
					case DataType.DT_BYTES:
						outputBuffer.SetBytes(outputBufferIndex, (byte[])value);
						break;
					case DataType.DT_CY:
						outputBuffer.SetDecimal(outputBufferIndex, Convert.ToDecimal(value));
						break;
					case DataType.DT_DATE:
						outputBuffer.SetDateTime(outputBufferIndex, Convert.ToDateTime(value));
						break;
					case DataType.DT_DBDATE:
						outputBuffer[outputBufferIndex] = (DateTime)value;
						break;
					case DataType.DT_DBTIME:
						// it is not possible to populate DT_DBTIME columns from managed code in SSIS 2005
						break;
					case DataType.DT_DBTIMESTAMP:
						outputBuffer.SetDateTime(outputBufferIndex, Convert.ToDateTime(value));
						break;
					case DataType.DT_DECIMAL:
						outputBuffer.SetDecimal(outputBufferIndex, Convert.ToDecimal(value));
						break;
					case DataType.DT_FILETIME:
						outputBuffer[outputBufferIndex] = value;
						break;
					case DataType.DT_GUID:
						outputBuffer.SetGuid(outputBufferIndex, (Guid)value);
						break;
					case DataType.DT_I1:
						outputBuffer.SetSByte(outputBufferIndex, Convert.ToSByte(value));
						break;
					case DataType.DT_I2:
						outputBuffer.SetInt16(outputBufferIndex, Convert.ToInt16(value));
						break;
					case DataType.DT_I4:
						outputBuffer.SetInt32(outputBufferIndex, Convert.ToInt32(value));
						break;
					case DataType.DT_I8:
						outputBuffer.SetInt64(outputBufferIndex, Convert.ToInt64(value));
						break;
					case DataType.DT_IMAGE:
						// we have to treat blob columns a little differently
						BlobColumn colDT_IMAGE = (BlobColumn)value;
						if (colDT_IMAGE.IsNull)
							outputBuffer.SetNull(outputBufferIndex);
						else
							outputBuffer.AddBlobData(outputBufferIndex, colDT_IMAGE.GetBlobData(0, (int)colDT_IMAGE.Length));
						break;
					case DataType.DT_NTEXT:
						// we have to treat blob columns a little differently
						BlobColumn colDT_NTEXT = (BlobColumn)value;
						if (colDT_NTEXT.IsNull)
							outputBuffer.SetNull(outputBufferIndex);
						else
							outputBuffer.AddBlobData(outputBufferIndex, colDT_NTEXT.GetBlobData(0, (int)colDT_NTEXT.Length));
						break;
					case DataType.DT_NULL:
						outputBuffer.SetNull(outputBufferIndex);
						break;
					case DataType.DT_NUMERIC:
						outputBuffer.SetDecimal(outputBufferIndex, Convert.ToDecimal(value));
						break;
					case DataType.DT_R4:
						outputBuffer.SetSingle(outputBufferIndex, Convert.ToSingle(value));
						break;
					case DataType.DT_R8:
						outputBuffer.SetDouble(outputBufferIndex, Convert.ToDouble(value));
						break;
					case DataType.DT_STR:
						outputBuffer.SetString(outputBufferIndex, value.ToString());
						break;
					case DataType.DT_TEXT:
						// we have to treat blob columns a little differently
						BlobColumn colDT_TEXT = (BlobColumn)value;
						if (colDT_TEXT.IsNull)
							outputBuffer.SetNull(outputBufferIndex);
						else
							outputBuffer.AddBlobData(outputBufferIndex, colDT_TEXT.GetBlobData(0, (int)colDT_TEXT.Length));
						break;
					case DataType.DT_UI1:
						outputBuffer.SetByte(outputBufferIndex, Convert.ToByte(value));
						break;
					case DataType.DT_UI2:
						outputBuffer.SetUInt16(outputBufferIndex, Convert.ToUInt16(value));
						break;
					case DataType.DT_UI4:
						outputBuffer.SetUInt32(outputBufferIndex, Convert.ToUInt32(value));
						break;
					case DataType.DT_UI8:
						outputBuffer.SetUInt64(outputBufferIndex, Convert.ToUInt64(value));
						break;
					case DataType.DT_WSTR:
						outputBuffer.SetString(outputBufferIndex, value.ToString());
						break;
					default:
						throw new InvalidOperationException(string.Format("Ah snap."));
				}
			}
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="transaction"> </param>
		public override void AcquireConnections(object transaction)
		{
			IDbConnection dbConnection;

			base.AcquireConnections(transaction);

			if (this.TryGetDictionaryDbConnection(true, out dbConnection))
				this.DictionaryDbConnection = dbConnection;
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		public override void Cleanup()
		{
			this.CoreDispose(true);

			base.Cleanup();
		}

		protected virtual void CoreDispose(bool disposing)
		{
			if (disposing)
				this.ComponentMetadataWrapper = null;
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iID"> </param>
		/// <param name="iExternalMetadataColumnID"> </param>
		public override void DeleteExternalMetadataColumn(int iID, int iExternalMetadataColumnID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: delete external metadata columns.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: delete external metadata columns."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		public override void DeleteInput(int inputID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: delete input.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: delete input."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		public override void DeleteOutput(int outputID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: delete output.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: delete output."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		/// <param name="outputColumnID"> </param>
		public override void DeleteOutputColumn(int outputID, int outputColumnID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: delete output columns.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: delete output columns."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iErrorCode"> </param>
		/// <returns> </returns>
		public override string DescribeRedirectedErrorCode(int iErrorCode)
		{
			// do nothing
			object obj = new object();
			return base.DescribeRedirectedErrorCode(iErrorCode);
		}

		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.CoreDispose(true);
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// (OK)
		/// </summary>
		/// <param name="blockedInputID"> </param>
		/// <returns> </returns>
		public override Collection<int> GetDependentInputs(int blockedInputID)
		{
			// do nothing
			object obj = new object();
			return base.GetDependentInputs(blockedInputID);
		}

		private IUnitOfWork GetDictionaryUnitOfWork()
		{
			IDbConnection dbConnection = null;

			// same connection used for all dictionaries
			return UnitOfWork.From(this.DictionaryDbConnection, null);
		}

		public object GetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			return this.DictionaryConfigurationToAdapterMappings[dictionaryConfiguration].GetAlternativeValueFromId(dictionaryConfiguration, metaColumn, surrogateId);
		}

		/// <summary>
		/// (OK)
		/// </summary>
		public override void Initialize()
		{
			this.ComponentMetadataWrapper = new x__ComponentMetadataWrapper(this.ComponentMetaData, this.GetDictionaryUnitOfWork);

			this.TryLaunchDebugger();
			base.Initialize();
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iID"> </param>
		/// <param name="iExternalMetadataColumnIndex"> </param>
		/// <param name="strName"> </param>
		/// <param name="strDescription"> </param>
		/// <returns> </returns>
		public override IDTSExternalMetadataColumn100 InsertExternalMetadataColumnAt(int iID, int iExternalMetadataColumnIndex, string strName, string strDescription)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: insert external metadata column.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: insert external metadata column."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="insertPlacement"> </param>
		/// <param name="inputID"> </param>
		/// <returns> </returns>
		public override IDTSInput100 InsertInput(DTSInsertPlacement insertPlacement, int inputID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: insert input.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: insert input."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="insertPlacement"> </param>
		/// <param name="outputID"> </param>
		/// <returns> </returns>
		public override IDTSOutput100 InsertOutput(DTSInsertPlacement insertPlacement, int outputID)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: insert output.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: insert output."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		/// <param name="outputColumnIndex"> </param>
		/// <param name="name"> </param>
		/// <param name="description"> </param>
		/// <returns> </returns>
		public override IDTSOutputColumn100 InsertOutputColumnAt(int outputID, int outputColumnIndex, string name, string description)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: insert output column.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: insert output column."));
		}

		/// <summary>
		/// (OK)
		/// </summary>
		/// <param name="inputIDs"> </param>
		/// <param name="canProcess"> </param>
		public override void IsInputReady(int[] inputIDs, ref bool[] canProcess)
		{
			// do nothing
			object obj = new object();
			base.IsInputReady(inputIDs, ref canProcess);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iInputID"> </param>
		/// <param name="iInputColumnID"> </param>
		/// <param name="iExternalMetadataColumnID"> </param>
		/// <returns> </returns>
		public override IDTSExternalMetadataColumn100 MapInputColumn(int iInputID, int iInputColumnID, int iExternalMetadataColumnID)
		{
			// do nothing
			object obj = new object();
			return base.MapInputColumn(iInputID, iInputColumnID, iExternalMetadataColumnID);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iOutputID"> </param>
		/// <param name="iOutputColumnID"> </param>
		/// <param name="iExternalMetadataColumnID"> </param>
		/// <param name="bMatch"> </param>
		/// <returns> </returns>
		public override IDTSExternalMetadataColumn100 MapOutputColumn(int iOutputID, int iOutputColumnID, int iExternalMetadataColumnID, bool bMatch)
		{
			// do nothing
			object obj = new object();
			return base.MapOutputColumn(iOutputID, iOutputColumnID, iExternalMetadataColumnID, bMatch);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		/// <param name="inputColumnID"> </param>
		public override void OnDeletingInputColumn(int inputID, int inputColumnID)
		{
			// do nothing
			object obj = new object();
			base.OnDeletingInputColumn(inputID, inputColumnID);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		public override void OnInputPathAttached(int inputID)
		{
			base.OnInputPathAttached(inputID);

			// TODO: move this to wrapper
			IDTSVirtualInput100 dtsVirtualInput100;

			// wipe all input columns from all inputs, reset usage on virtual input columns
			for (int i = 0; i < this.ComponentMetaData.InputCollection.Count; i++)
			{
				this.ComponentMetaData.InputCollection[i].InputColumnCollection.RemoveAll();

				dtsVirtualInput100 = this.ComponentMetaData.InputCollection[i].GetVirtualInput();

				foreach (IDTSVirtualInputColumn100 dtsVirtualInputColumn100 in dtsVirtualInput100.VirtualInputColumnCollection)
					dtsVirtualInput100.SetUsageType(dtsVirtualInputColumn100.LineageID, DTSUsageType.UT_READONLY);
			}

			// we have new upstream metadata, lets create some input column properties
			this.ComponentMetadataWrapper.CreateInputColumnProperties();
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		public override void OnInputPathDetached(int inputID)
		{
			// do nothing
			object obj = new object();
			base.OnInputPathDetached(inputID);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		public override void OnOutputPathAttached(int outputID)
		{
			// do nothing
			object obj = new object();
			base.OnOutputPathAttached(outputID);
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		/// <param name="pipelineVersion"> </param>
		public override void PerformUpgrade(int pipelineVersion)
		{
			if (pipelineVersion < Constants.COMPONENT_CURRENT_VERSION)
				this.SetComponentVersion(pipelineVersion);
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		public override void PostExecute()
		{
			this.OxymoronEngine.Dispose();
			this.OxymoronEngine = null;

			this.DictionaryConfigurationToAdapterMappings.Clear();
			this.InputColumnInfos.Clear();

			base.PostExecute();
		}

		/// <summary>
		/// IDTSRuntimeComponent100
		/// </summary>
		public override void PreExecute()
		{
			ObfuscationConfiguration obfuscationConfiguration;

			IDTSInput100 dtsInput100;
			ColumnInfo columnInfo;

			this.TryLaunchDebugger();

			obfuscationConfiguration = this.ComponentMetadataWrapper.GetObfuscationConfiguration();

			this.OxymoronEngine = new OxymoronEngine(this, obfuscationConfiguration);

			foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
			{
				IDictionaryAdapter dictionaryAdapter;

				dictionaryAdapter = new DtsDictionaryAdapter();
				this.DictionaryAdapters.Add(dictionaryAdapter);
				dictionaryAdapter.Initialize(obfuscationConfiguration);

				dictionaryAdapter.InitializePreloadCache(dictionaryConfiguration, this.OxymoronEngine.SubstitutionCacheRoot);

				this.DictionaryConfigurationToAdapterMappings.Add(dictionaryConfiguration, dictionaryAdapter);
			}

			// interogate input columns and stash away for later use
			dtsInput100 = this.ComponentMetaData.InputCollection[0];

			foreach (IDTSInputColumn100 dtsInputColumn100 in dtsInput100.InputColumnCollection)
			{
				columnInfo = new ColumnInfo();
				columnInfo.name = dtsInputColumn100.Name;
				columnInfo.bufferColumnIndex = this.BufferManager.FindColumnByLineageID(dtsInput100.Buffer, dtsInputColumn100.LineageID);
				columnInfo.columnDisposition = dtsInputColumn100.ErrorRowDisposition;
				columnInfo.lineageId = dtsInputColumn100.LineageID;

				columnInfo.type = dtsInputColumn100.DataType;
				columnInfo.length = dtsInputColumn100.Length;
				columnInfo.precision = dtsInputColumn100.Precision;
				columnInfo.scale = dtsInputColumn100.Scale;
				columnInfo.codepage = dtsInputColumn100.CodePage;

				this.InputColumnInfos.Add(columnInfo);
			}
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		public override void PrepareForExecute()
		{
			// do nothing
			object obj = new object();
			base.PrepareForExecute();
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputs"> </param>
		/// <param name="outputIDs"> </param>
		/// <param name="buffers"> </param>
		public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
		{
			// do nothing
			object obj = new object();
			base.PrimeOutput(outputs, outputIDs, buffers);
		}

		/// <summary>
		/// IDTSRuntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		/// <param name="buffer"> </param>
		public override void ProcessInput(int inputID, PipelineBuffer buffer)
		{
			ColumnInfo columnInfo;
			string columnName;
			Type columnType;
			object columnValue, obfuColumnValue;
			int rowIndex = 0;

			IMetaColumn metaColumn;

			if (!buffer.EndOfRowset)
			{
				while (buffer.NextRow())
				{
					for (int columnIndex = 0; columnIndex < buffer.ColumnCount; columnIndex++)
					{
						if (columnIndex >= this.InputColumnInfos.Count)
							continue;

						columnInfo = this.InputColumnInfos[columnIndex];

						columnName = columnInfo.name;
						columnType = InferClrTypeForSsisDataType(columnInfo.type);
						columnValue = buffer[columnInfo.bufferColumnIndex];

						metaColumn = new MetaColumn()
									{
										ColumnIndex = columnIndex,
										ColumnName = columnName,
										ColumnType = columnType,
										ColumnIsNullable = null,
										TableIndex = 0,
										TagContext = null
									};

						obfuColumnValue = this.OxymoronEngine.GetObfuscatedValue(metaColumn, columnValue);

						SetBufferValue(buffer, columnInfo.bufferColumnIndex, obfuColumnValue, columnInfo.type);

						//if (!buffer.IsNull(columnInfo.bufferColumnIndex))
						//buffer[columnInfo.bufferColumnIndex] = columnValue;
					}

					//buffer.DirectRow(ComponentMetaData.OutputCollection[Constants.OUTPUT_NAME].ID);

					rowIndex++;
				}
			}

			//buffer.SetEndOfRowset();
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		public override void ProvideComponentProperties()
		{
			// TODO: add the rest of this to the wrappers

			IDTSInput100 dtsInput100;
			IDTSOutput100 dtsOutput100;
			IDTSOutput100 dtsErrorOutput100;
			IDTSRuntimeConnection100 dtsRuntimeConnection100;

			// set component information
			this.ComponentMetaData.Name = Constants.COMPONENT_NAME;
			this.ComponentMetaData.Description = Constants.COMPONENT_DESCRIPTION;
			this.ComponentMetaData.ContactInfo = Constants.COMPONENT_EMAIL;
			this.ComponentMetaData.Version = Constants.COMPONENT_CURRENT_VERSION;

			// reset the component
			this.RemoveAllInputsOutputsAndCustomProperties();
			this.ComponentMetaData.RuntimeConnectionCollection.RemoveAll();

			this.ComponentMetaData.UsesDispositions = true;

			// add an input object
			dtsInput100 = this.ComponentMetaData.InputCollection.New();
			dtsInput100.Name = Constants.COMPONENT_INPUT_DEFAULT_NAME;
			//dtsInput100.HasSideEffects = true;
			dtsInput100.ErrorRowDisposition = DTSRowDisposition.RD_NotUsed;

			// add an output object
			dtsOutput100 = this.ComponentMetaData.OutputCollection.New();
			dtsOutput100.Name = Constants.COMPONENT_OUTPUT_DEFAULT_NAME;
			dtsOutput100.IsErrorOut = false;
			dtsOutput100.SynchronousInputID = dtsInput100.ID; // synchronous transform

			// add an error object
			dtsErrorOutput100 = this.ComponentMetaData.OutputCollection.New();
			dtsErrorOutput100.Name = Constants.COMPONENT_OUTPUT_ERROR_NAME;
			dtsErrorOutput100.IsErrorOut = true;
			dtsErrorOutput100.SynchronousInputID = dtsInput100.ID; // synchronous transform

			// add custom properties to component etc.
			this.ComponentMetadataWrapper.CreateProperties();

			// add connections
			dtsRuntimeConnection100 = this.ComponentMetaData.RuntimeConnectionCollection.New();
			dtsRuntimeConnection100.Name = Constants.COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY;
			dtsRuntimeConnection100.Description = Constants.COMPONENT_RUNTIMECONNECTION_DESC_DICTIONARY;
		}

		/// <summary>
		/// (OK)
		/// </summary>
		public override void RegisterEvents()
		{
			// do nothing
			object obj = new object();
			base.RegisterEvents();
		}

		/// <summary>
		/// (OK)
		/// </summary>
		public override void RegisterLogEntries()
		{
			// do nothing
			object obj = new object();
			base.RegisterLogEntries();
		}

		/// <summary>
		/// IDTSDesigntimeComponent100
		/// </summary>
		public override void ReinitializeMetaData()
		{
			if (!this.ComponentMetaData.AreInputColumnsValid)
				this.ComponentMetaData.RemoveInvalidInputColumns();

			base.ReinitializeMetaData();
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		public override void ReleaseConnections()
		{
			base.ReleaseConnections();

			if ((object)this.DictionaryDbConnection != null)
			{
				this.DictionaryDbConnection.Dispose();
				this.DictionaryDbConnection = null;
			}
		}

		/// <summary>
		/// IDTSDesigntimeComponent100
		/// </summary>
		/// <param name="propertyName"> </param>
		/// <param name="propertyValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetComponentProperty(string propertyName, object propertyValue)
		{
			// do nothing
			object obj = new object();
			return base.SetComponentProperty(propertyName, propertyValue);
		}

		/// <summary>
		/// (OK)
		/// </summary>
		/// <param name="pipelineVersion"> </param>
		private void SetComponentVersion(int pipelineVersion)
		{
			this.ComponentMetaData.Version = Constants.COMPONENT_CURRENT_VERSION;
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iID"> </param>
		/// <param name="iExternalMetadataColumnID"> </param>
		/// <param name="eDataType"> </param>
		/// <param name="iLength"> </param>
		/// <param name="iPrecision"> </param>
		/// <param name="iScale"> </param>
		/// <param name="iCodePage"> </param>
		public override void SetExternalMetadataColumnDataTypeProperties(int iID, int iExternalMetadataColumnID, DataType eDataType, int iLength, int iPrecision, int iScale, int iCodePage)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set external metadata type properties.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set external metadata type properties."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iID"> </param>
		/// <param name="iExternalMetadataColumnID"> </param>
		/// <param name="strPropertyName"> </param>
		/// <param name="oValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetExternalMetadataColumnProperty(int iID, int iExternalMetadataColumnID, string strPropertyName, object oValue)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set external metadata column property.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set external metadata column property."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		/// <param name="inputColumnID"> </param>
		/// <param name="propertyName"> </param>
		/// <param name="propertyValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetInputColumnProperty(int inputID, int inputColumnID, string propertyName, object propertyValue)
		{
			// do nothing
			object obj = new object();
			return base.SetInputColumnProperty(inputID, inputColumnID, propertyName, propertyValue);
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		/// <param name="propertyName"> </param>
		/// <param name="propertyValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetInputProperty(int inputID, string propertyName, object propertyValue)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set input property.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set input property."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="iOutputID"> </param>
		/// <param name="iOutputColumnID"> </param>
		/// <param name="eDataType"> </param>
		/// <param name="iLength"> </param>
		/// <param name="iPrecision"> </param>
		/// <param name="iScale"> </param>
		/// <param name="iCodePage"> </param>
		public override void SetOutputColumnDataTypeProperties(int iOutputID, int iOutputColumnID, DataType eDataType, int iLength, int iPrecision, int iScale, int iCodePage)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set output column data type properties.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set output column data type properties."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		/// <param name="outputColumnID"> </param>
		/// <param name="propertyName"> </param>
		/// <param name="propertyValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetOutputColumnProperty(int outputID, int outputColumnID, string propertyName, object propertyValue)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set output column property.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set output column property."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="outputID"> </param>
		/// <param name="propertyName"> </param>
		/// <param name="propertyValue"> </param>
		/// <returns> </returns>
		public override IDTSCustomProperty100 SetOutputProperty(int outputID, string propertyName, object propertyValue)
		{
			bool cancelled;
			this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Unsupported: set output property.", string.Empty, 0, out cancelled);
			throw new NotSupportedException(string.Format("Unsupported: set output property."));
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <param name="inputID"> </param>
		/// <param name="virtualInput"> </param>
		/// <param name="lineageID"> </param>
		/// <param name="usageType"> </param>
		/// <returns> </returns>
		public override IDTSInputColumn100 SetUsageType(int inputID, IDTSVirtualInput100 virtualInput, int lineageID, DTSUsageType usageType)
		{
			var retval = base.SetUsageType(inputID, virtualInput, lineageID, usageType);

			// something likely changed, lets update
			this.ComponentMetadataWrapper.CreateInputColumnProperties();

			return retval;
		}

		/// <summary>
		/// (OK)
		/// </summary>
		/// <returns> </returns>
		private bool TryGetDictionaryDbConnection(bool open, out IDbConnection dbConnection)
		{
			IDTSRuntimeConnection100 dtsRuntimeConnection100;
			IDTSConnectionManagerDatabaseParameters100 dtsConnectionManagerDatabaseParameters100;
			ConnectionManager connectionManager;

			dbConnection = null;
			dtsRuntimeConnection100 = this.ComponentMetaData.RuntimeConnectionCollection[Constants.COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY];

			if ((object)dtsRuntimeConnection100.ConnectionManager != null)
			{
				connectionManager = DtsConvert.GetWrapper(dtsRuntimeConnection100.ConnectionManager);

				if ((object)(dtsConnectionManagerDatabaseParameters100 = connectionManager.InnerObject as IDTSConnectionManagerDatabaseParameters100) == null)
					return false;

				if (open)
					dbConnection = (IDbConnection)dtsConnectionManagerDatabaseParameters100.GetConnectionForSchema();
			}

			return true;
		}

		/// <summary>
		/// (OK)
		/// </summary>
		private void TryLaunchDebugger()
		{
			this.TryLaunchDebugger(false);
		}

		/// <summary>
		/// (OK)
		/// </summary>
		/// <param name="force"> </param>
		private void TryLaunchDebugger(bool force)
		{
#if DEBUG
			if (((object)this.ComponentMetadataWrapper != null &&
				this.ComponentMetadataWrapper.DebuggerLaunch) || force)
				Debugger.Launch();
#endif
		}

		/// <summary>
		/// IDTSDesigntimeComponent100 (OK)
		/// </summary>
		/// <returns> </returns>
		public override DTSValidationStatus Validate()
		{
			const string ERROR_INVALID_USAGE_TYPE = "Invalid usage type for column '{0}'";
			const string ERROR_INVALID_DATA_TYPE = "Invalid data type for column '{0}'";
			const string ERROR_INVALID_DICTIONARY_CONNECTION_MANAGER = "No dictionary connection manager was specified '{0}'";
			const string ERROR_INVALID_INPUT = "No input '{0}'";
			const string ERROR_INVALID_OUTPUT = "No output '{0}'";
			const string ERROR_INVALID_INPUT_COLUMNS = "No input columns for input '{0}'";
			const string ERROR_INVALID_OUTPUT_COLUMNS = "No output columns for output '{0}'";

			bool cancel;
			IEnumerable<Message> messages;

			IDTSInput100 dtsInput100;
			IDTSOutput100 dtsOutput100;
			IDTSRuntimeConnection100 dtsRuntimeConnection100;

			ObfuscationConfiguration obfuscationConfiguration;

			// input
			if (!this.ComponentMetaData.AreInputColumnsValid)
				return DTSValidationStatus.VS_NEEDSNEWMETADATA;

			dtsInput100 = this.ComponentMetaData.InputCollection[Constants.COMPONENT_INPUT_DEFAULT_NAME];

			if (!dtsInput100.IsAttached)
			{
				this.ComponentMetaData.FireError(E_FAIL, dtsInput100.IdentificationString, string.Format(ERROR_INVALID_INPUT, dtsInput100.Name), string.Empty, 0, out cancel);
				return DTSValidationStatus.VS_ISBROKEN;
			}

			if (dtsInput100.InputColumnCollection.Count <= 0)
			{
				this.ComponentMetaData.FireError(E_FAIL, dtsInput100.IdentificationString, string.Format(ERROR_INVALID_INPUT_COLUMNS, dtsInput100.Name), string.Empty, 0, out cancel);
				return DTSValidationStatus.VS_ISBROKEN;
			}

			// input columns
			foreach (IDTSInputColumn100 dtsInputColumn100 in dtsInput100.InputColumnCollection)
			{
				/*if (dtsInputColumn100.UsageType != DTSUsageType.UT_READWRITE)
				{
					this.ComponentMetaData.FireError(0, dtsInputColumn100.IdentificationString, string.Format(ERROR_INVALID_USAGE_TYPE, dtsInputColumn100.Name), string.Empty, 0, out cancel);
					return DTSValidationStatus.VS_ISBROKEN;
				}*/

				if (!this.validPipelineComponentsDataTypes.Contains(dtsInputColumn100.DataType))
				{
					this.ComponentMetaData.FireError(E_FAIL, dtsInputColumn100.IdentificationString, string.Format(ERROR_INVALID_DATA_TYPE, dtsInputColumn100.Name), string.Empty, 0, out cancel);
					return DTSValidationStatus.VS_ISBROKEN;
				}
			}

			// output
			dtsOutput100 = this.ComponentMetaData.OutputCollection[Constants.COMPONENT_OUTPUT_DEFAULT_NAME];

			if (!dtsOutput100.IsAttached)
			{
				//this.ComponentMetaData.FireError(E_FAIL, dtsOutput100.IdentificationString, string.Format(ERROR_INVALID_OUTPUT, dtsInput100.Name), string.Empty, 0, out cancel);
				//return DTSValidationStatus.VS_ISBROKEN;
			}

			if (dtsOutput100.OutputColumnCollection.Count <= 0)
			{
				//this.ComponentMetaData.FireError(E_FAIL, dtsOutput100.IdentificationString, string.Format(ERROR_INVALID_OUTPUT, dtsOutput100.Name), string.Empty, 0, out cancel);
				//return DTSValidationStatus.VS_ISBROKEN;
			}

			// error output
			dtsOutput100 = this.ComponentMetaData.OutputCollection[Constants.COMPONENT_OUTPUT_ERROR_NAME];
			if (dtsOutput100.OutputColumnCollection.Count <= 0)
			{
				this.ComponentMetaData.FireError(E_FAIL, dtsOutput100.IdentificationString, string.Format(ERROR_INVALID_OUTPUT_COLUMNS, dtsOutput100.Name), string.Empty, 0, out cancel);
				return DTSValidationStatus.VS_ISBROKEN;
			}

			// runtime connections
			dtsRuntimeConnection100 = this.ComponentMetaData.RuntimeConnectionCollection[Constants.COMPONENT_RUNTIMECONNECTION_NAME_DICTIONARY];
			if ((object)dtsRuntimeConnection100.ConnectionManager == null)
			{
				this.ComponentMetaData.FireError(E_FAIL, dtsRuntimeConnection100.IdentificationString, string.Format(ERROR_INVALID_DICTIONARY_CONNECTION_MANAGER, dtsRuntimeConnection100.Name), string.Empty, 0, out cancel);
				return DTSValidationStatus.VS_ISBROKEN;
			}
			else
			{
				IDbConnection dbConnection = null;

				if (!this.TryGetDictionaryDbConnection(false, out dbConnection))
				{
					this.ComponentMetaData.FireError(E_FAIL, dtsRuntimeConnection100.IdentificationString, string.Format(ERROR_INVALID_DICTIONARY_CONNECTION_MANAGER, dtsRuntimeConnection100.Name), string.Empty, 0, out cancel);
					return DTSValidationStatus.VS_ISBROKEN;
				}
			}

			// obfuscation specific validation
			obfuscationConfiguration = this.ComponentMetadataWrapper.GetObfuscationConfiguration();

			if ((object)obfuscationConfiguration == null)
			{
				this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, "Obfuscation configuration has not been set.", string.Empty, 0, out cancel);

				return DTSValidationStatus.VS_ISBROKEN;
			}

			messages = obfuscationConfiguration.Validate();

			if (messages.Any())
			{
				foreach (var message in messages)
					this.ComponentMetaData.FireError(E_FAIL, this.ComponentMetaData.Name, message.Description, string.Empty, 0, out cancel);

				return DTSValidationStatus.VS_ISBROKEN;
			}

			return base.Validate();
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private struct ColumnInfo
		{
			#region Fields/Constants

			public int bufferColumnIndex;
			public int codepage;
			public DTSRowDisposition columnDisposition;
			public int length;
			public int lineageId;
			public string name;
			public int precision;
			public int scale;
			public DataType type;

			#endregion
		}

		#endregion
	}
}