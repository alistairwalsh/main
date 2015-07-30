/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using Solder.Framework.Utilities;

using Message = Solder.Framework.Message;

namespace _2ndAsset.Common.WinForms
{
	public abstract class WindowsApplicationFascade<TMainForm> : ExecutableApplicationFascade
		where TMainForm : Form, new()
	{
		#region Constructors/Destructors

		protected WindowsApplicationFascade()
		{
		}

		#endregion

		#region Methods/Operators

		protected override sealed void DisplayArgumentErrorMessage(IEnumerable<Message> argumentMessages)
		{
			/*if ((object)argumentMessages != null)
			{
				using (MessageForm messageForm = new MessageForm()
												{
													CoreText = this.AssemblyInformationFascade.Product,
													Message = string.Empty,
													Messages = argumentMessages,
													StartPosition = FormStartPosition.CenterScreen
												})
					messageForm.ShowDialog(null);
			}*/
		}

		protected override sealed void DisplayArgumentMapMessage(IDictionary<string, ArgumentSpec> argumentMap)
		{
			string message;

			var requiredArgumentTokens = argumentMap.Select(m => (!m.Value.Required ? "[" : string.Empty) + string.Format("-{0}:value{1}", m.Key, !m.Value.Bounded ? "(s)" : string.Empty) + (!m.Value.Required ? "]" : string.Empty));

			if ((object)requiredArgumentTokens != null)
			{
				message = string.Format("USAGE: {0} ", Assembly.GetEntryAssembly().ManifestModule.Name) + string.Join(" ", requiredArgumentTokens) + Environment.NewLine;
				MessageBox.Show(message, this.AssemblyInformationFascade.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		protected override sealed void DisplayFailureMessage(Exception exception)
		{
			MessageBox.Show("A fatal error occured:" + Environment.NewLine + ((object)exception != null ? this.ReflectionFascade.GetErrors(exception, 0) : "<unknown>") + Environment.NewLine + "The application will now terminate.", this.AssemblyInformationFascade.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected override sealed void DisplayRawArgumentsMessage(IEnumerable<string> arguments)
		{
		}

		protected override sealed void DisplaySuccessMessage(TimeSpan duration)
		{
			//MessageBox.Show(string.Format("Operation completed successfully; duration: '{0}'.", duration), this.AssemblyInformationFascade.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			this.ShowNestedExceptionsAndThrowBrickAtProcess(new ApplicationException("OnApplicationThreadException", e.Exception));
		}

		protected virtual void OnRunApplication()
		{
			Application.Run(new TMainForm());
		}

		protected override sealed int OnStartup(string[] args, IDictionary<string, IList<object>> arguments)
		{
			if (this.HookUnhandledExceptionEvents)
				Application.ThreadException += this.OnApplicationThreadException;

			Control.CheckForIllegalCrossThreadCalls = true;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			this.OnRunApplication();

			return 0;
		}

		#endregion
	}
}