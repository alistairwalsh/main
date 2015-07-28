/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Threading.Tasks;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public abstract class WrappedTextReader : TextReader
	{
		#region Constructors/Destructors

		protected WrappedTextReader(TextReader innerTextReader)
		{
			if ((object)innerTextReader == null)
				throw new ArgumentNullException("innerTextReader");

			this.innerTextReader = innerTextReader;
		}

		#endregion

		#region Fields/Constants

		private readonly TextReader innerTextReader;

		#endregion

		#region Properties/Indexers/Events

		protected TextReader InnerTextReader
		{
			get
			{
				return this.innerTextReader;
			}
		}

		#endregion

		#region Methods/Operators

		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize((object)this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.InnerTextReader.Dispose();

			base.Dispose(disposing);
		}

		public override int Peek()
		{
			return this.InnerTextReader.Peek();
		}

		public override int Read()
		{
			return this.InnerTextReader.Read();
		}

		public override int Read(char[] buffer, int index, int count)
		{
			return this.InnerTextReader.Read(buffer, index, count);
		}

		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			return this.InnerTextReader.ReadAsync(buffer, index, count);
		}

		public override int ReadBlock(char[] buffer, int index, int count)
		{
			return this.InnerTextReader.ReadBlock(buffer, index, count);
		}

		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			return this.InnerTextReader.ReadBlockAsync(buffer, index, count);
		}

		public override string ReadLine()
		{
			return this.InnerTextReader.ReadLine();
		}

		public override Task<string> ReadLineAsync()
		{
			return this.InnerTextReader.ReadLineAsync();
		}

		public override string ReadToEnd()
		{
			return this.InnerTextReader.ReadToEnd();
		}

		public override Task<string> ReadToEndAsync()
		{
			return this.InnerTextReader.ReadToEndAsync();
		}

		#endregion
	}
}