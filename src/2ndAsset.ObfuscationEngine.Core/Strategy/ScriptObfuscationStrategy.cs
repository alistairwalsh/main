/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.CSharp;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

using ZZZ = System.Func<object, object>;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns an alternate value that is a script function of the original value.
	/// DATA TYPE: any
	/// </summary>
	public sealed class ScriptObfuscationStrategy : ObfuscationStrategy<ScriptObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public ScriptObfuscationStrategy()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly IDictionary<string, ZZZ> csharpCodeCache = new Dictionary<string, ZZZ>();

		#endregion

		#region Properties/Indexers/Events

		private static IDictionary<string, ZZZ> CSharpCodeCache
		{
			get
			{
				return csharpCodeCache;
			}
		}

		#endregion

		#region Methods/Operators

		public static object CompileCSharpCode(object columnValue, string scriptSource)
		{
			CompilerResults cr;
			CompilerParameters cp;
			ZZZ callback;

			if (!CSharpCodeCache.TryGetValue(scriptSource, out callback))
			{
				using (CSharpCodeProvider provider = new CSharpCodeProvider())
				{
					cp = new CompilerParameters();

					cp.ReferencedAssemblies.Add("System.dll");
					cp.GenerateExecutable = false;
					//cp.OutputAssembly = null;
					cp.GenerateInMemory = true;

					cr = provider.CompileAssemblyFromSource(cp, scriptSource);

					if (cr.Errors.Cast<CompilerError>().Any())
						throw new InvalidOperationException(string.Format("{0}", string.Join(",", cr.Errors.Cast<CompilerError>().Select(e => e.ErrorText).ToArray())));

					Type type = cr.CompiledAssembly.GetType("Script", false);
					MethodInfo method = type.GetMethod("GetValue", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(object) }, null);

					callback = (o) => method.Invoke(null, new object[] { o });

					CSharpCodeCache.Add(scriptSource, callback);
				}
			}

			if ((object)callback == null)
				throw new InvalidOperationException(string.Format("callback"));

			return callback(columnValue);
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<ScriptObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			return CompileCSharpCode(columnValue, columnConfiguration.ObfuscationStrategySpecificConfiguration.SourceCode);
		}

		#endregion
	}
}