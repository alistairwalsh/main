/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

using Newtonsoft.Json;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Serialization
{
	public class JsonSerializationStrategy : IBinarySerializationStrategy, ITextSerializationStrategy
	{
		#region Constructors/Destructors

		public JsonSerializationStrategy()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly JsonSerializationStrategy instance = new JsonSerializationStrategy();

		#endregion

		#region Properties/Indexers/Events

		public static JsonSerializationStrategy Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Deserializes an object from the specified byte array value.
		/// </summary>
		/// <param name="value"> The byte array value to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromBytes(byte[] value, Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Deserializes an object from the specified byte array value. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="value"> The byte array value to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromBytes<TObject>(byte[] value)
		{
			TObject obj;
			Type targetType;

			targetType = typeof(TObject);

			obj = (TObject)this.GetObjectFromBytes(value, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified input file.
		/// </summary>
		/// <param name="inputFilePath"> The input file path to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromFile(string inputFilePath, Type targetType)
		{
			object obj;

			if ((object)inputFilePath == null)
				throw new ArgumentNullException("inputFilePath");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			using (Stream stream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				obj = this.GetObjectFromStream(stream, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified input file. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="inputFilePath"> The input file path to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromFile<TObject>(string inputFilePath)
		{
			TObject obj;
			Type targetType;

			if ((object)inputFilePath == null)
				throw new ArgumentNullException("inputFilePath");

			targetType = typeof(TObject);
			obj = (TObject)this.GetObjectFromFile(inputFilePath, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified text reader.
		/// </summary>
		/// <param name="textReader"> The text reader to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromReader(TextReader textReader, Type targetType)
		{
			JsonSerializer serializer;
			object obj;

			if ((object)textReader == null)
				throw new ArgumentNullException("textReader");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			serializer = JsonSerializer.Create(new JsonSerializerSettings()
												{
													Formatting = Formatting.Indented,
													TypeNameHandling = TypeNameHandling.None,
													ReferenceLoopHandling = ReferenceLoopHandling.Ignore
												});

			using (JsonReader jsonReader = new JsonTextReader(textReader))
				obj = (object)serializer.Deserialize(jsonReader, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified text reader. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="textReader"> The text reader to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromReader<TObject>(TextReader textReader)
		{
			TObject obj;
			Type targetType;

			if ((object)textReader == null)
				throw new ArgumentNullException("textReader");

			targetType = typeof(TObject);
			obj = (TObject)this.GetObjectFromReader(textReader, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified binary reader.
		/// </summary>
		/// <param name="binaryReader"> The binary reader to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromReader(BinaryReader binaryReader, Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Deserializes an object from the specified binary reader. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="binaryReader"> The binary reader to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromReader<TObject>(BinaryReader binaryReader)
		{
			TObject obj;
			Type targetType;

			targetType = typeof(TObject);

			obj = (TObject)this.GetObjectFromReader(binaryReader, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified readable stream.
		/// </summary>
		/// <param name="stream"> The readable stream to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromStream(Stream stream, Type targetType)
		{
			JsonSerializer serializer;
			object obj;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			serializer = JsonSerializer.Create(new JsonSerializerSettings()
												{
													Formatting = Formatting.Indented,
													TypeNameHandling = TypeNameHandling.None,
													ReferenceLoopHandling = ReferenceLoopHandling.Ignore
												});

			using (StreamReader streamReader = new StreamReader(stream))
			{
				using (JsonReader jsonReader = new JsonTextReader(streamReader))
					obj = (object)serializer.Deserialize(jsonReader, targetType);
			}

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified readable stream. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="stream"> The readable stream to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromStream<TObject>(Stream stream)
		{
			TObject obj;
			Type targetType;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			targetType = typeof(TObject);
			obj = (TObject)this.GetObjectFromStream(stream, targetType);

			return obj;
		}

		/// <summary>
		/// Deserializes an object from the specified string value.
		/// </summary>
		/// <param name="value"> The string value to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
		public object GetObjectFromString(string value, Type targetType)
		{
			object obj;
			StringReader stringReader;

			using (stringReader = new StringReader(value))
			{
				obj = this.GetObjectFromReader(stringReader, targetType);
				return obj;
			}
		}

		/// <summary>
		/// Deserializes an object from the specified text value. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="value"> The string value to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
		public TObject GetObjectFromString<TObject>(string value)
		{
			TObject obj;
			Type targetType;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			targetType = typeof(TObject);
			obj = (TObject)this.GetObjectFromString(value, targetType);

			return obj;
		}

		/// <summary>
		/// Serializes an object to a byte array value.
		/// </summary>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		/// <returns> A byte array representation of the object graph. </returns>
		public byte[] SetObjectToBytes(Type targetType, object obj)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Serializes an object to a byte array value. This is the generic overload.
		/// </summary>
		/// <param name="obj"> The object graph to serialize. </param>
		/// <returns> A byte array representation of the object graph. </returns>
		public byte[] SetObjectToBytes<TObject>(TObject obj)
		{
			byte[] bytes;
			Type targetType;

			targetType = typeof(TObject);

			bytes = this.SetObjectToBytes(targetType, obj);

			return bytes;
		}

		/// <summary>
		/// Serializes an object to the specified output file.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the object graph to serialize. </typeparam>
		/// <param name="outputFilePath"> The output file path to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToFile<TObject>(string outputFilePath, TObject obj)
		{
			Type targetType;

			if ((object)outputFilePath == null)
				throw new ArgumentNullException("outputFilePath");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			targetType = obj.GetType();

			this.SetObjectToFile(outputFilePath, targetType, (object)obj);
		}

		/// <summary>
		/// Serializes an object to the specified output file.
		/// </summary>
		/// <param name="outputFilePath"> The output file path to serialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToFile(string outputFilePath, Type targetType, object obj)
		{
			if ((object)outputFilePath == null)
				throw new ArgumentNullException("outputFilePath");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			using (Stream stream = File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
				this.SetObjectToStream(stream, targetType, obj);
		}

		/// <summary>
		/// Serializes an object to the specified writable stream.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the object graph to serialize. </typeparam>
		/// <param name="stream"> The writable stream to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToStream<TObject>(Stream stream, TObject obj)
		{
			Type targetType;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			targetType = obj.GetType();

			this.SetObjectToStream(stream, targetType, (object)obj);
		}

		/// <summary>
		/// Serializes an object to the specified writable stream.
		/// </summary>
		/// <param name="stream"> The writable stream to serialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToStream(Stream stream, Type targetType, object obj)
		{
			JsonSerializer serializer;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			serializer = JsonSerializer.Create(new JsonSerializerSettings()
												{
													Formatting = Formatting.Indented,
													TypeNameHandling = TypeNameHandling.None,
													ReferenceLoopHandling = ReferenceLoopHandling.Ignore
												});

			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
					serializer.Serialize(jsonWriter, obj, targetType);
			}
		}

		/// <summary>
		/// Serializes an object to a string value.
		/// </summary>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		/// <returns> A string representation of the object graph. </returns>
		public string SetObjectToString(Type targetType, object obj)
		{
			StringWriter stringWriter;

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			using (stringWriter = new StringWriter())
			{
				this.SetObjectToWriter(stringWriter, obj);
				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// Serializes an object to a string value. This is the generic overload.
		/// </summary>
		/// <param name="obj"> The object graph to serialize. </param>
		/// <returns> A string representation of the object graph. </returns>
		public string SetObjectToString<TObject>(TObject obj)
		{
			Type targetType;

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			targetType = obj.GetType();

			return this.SetObjectToString(targetType, (object)obj);
		}

		/// <summary>
		/// Serializes an object to the specified text writer.
		/// </summary>
		/// <param name="textWriter"> The text writer to serialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToWriter(TextWriter textWriter, Type targetType, object obj)
		{
			JsonSerializer serializer;

			if ((object)textWriter == null)
				throw new ArgumentNullException("stream");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			serializer = JsonSerializer.Create(new JsonSerializerSettings()
												{
													Formatting = Formatting.Indented,
													TypeNameHandling = TypeNameHandling.None,
													ReferenceLoopHandling = ReferenceLoopHandling.Ignore
												});

			using (JsonWriter jsonWriter = new JsonTextWriter(textWriter))
				serializer.Serialize(jsonWriter, obj, targetType);
		}

		/// <summary>
		/// Serializes an object to the specified text writer. This is the generic overload.
		/// </summary>
		/// <param name="textWriter"> The text writer to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToWriter<TObject>(TextWriter textWriter, TObject obj)
		{
			Type targetType;

			if ((object)textWriter == null)
				throw new ArgumentNullException("textWriter");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			targetType = obj.GetType();

			this.SetObjectToWriter(textWriter, targetType, (object)obj);
		}

		/// <summary>
		/// Serializes an object to the specified binary writer. This is the generic overload.
		/// </summary>
		/// <param name="binaryWriter"> The binary writer to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToWriter<TObject>(BinaryWriter binaryWriter, TObject obj)
		{
			Type targetType;

			targetType = typeof(TObject);

			this.SetObjectToWriter(binaryWriter, targetType, obj);
		}

		/// <summary>
		/// Serializes an object to the specified binary writer.
		/// </summary>
		/// <param name="binaryWriter"> The binary writer to serialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the object graph to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public void SetObjectToWriter(BinaryWriter binaryWriter, Type targetType, object obj)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}