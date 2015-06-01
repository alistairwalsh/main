/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

using TextMetal.Middleware.Common.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public class ImpersonationScope : IDisposable
	{
		#region Constructors/Destructors

		public ImpersonationScope(string userName, string domainName, string password, LogonType logonType, LogonProvider logonProvider)
		{
			IntPtr logonToken = IntPtr.Zero;
			IntPtr logonTokenDuplicate = IntPtr.Zero;

			Console.WriteLine("Enter impersonation scope");

			if ((object)userName == null)
				throw new ArgumentNullException("userName");

			if ((object)domainName == null)
				throw new ArgumentNullException("domainName");

			//if ((object)password == null)
			//	throw new ArgumentNullException("password");

			if (DataTypeFascade.Instance.IsWhiteSpace(userName))
				throw new ArgumentOutOfRangeException("userName");

			if (DataTypeFascade.Instance.IsWhiteSpace(domainName))
				throw new ArgumentOutOfRangeException("domainName");

			//if (DataType.IsWhiteSpace(password))
			//	throw new ArgumentOutOfRangeException("password");

			try
			{
				Console.WriteLine("Incoming identity: " + WindowsIdentity.GetCurrent().Name);

				this.processWindowsImpersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);

				Console.WriteLine("Process-impersonated identity: " + WindowsIdentity.GetCurrent().Name);

				//if (Win32NativeMethods.RevertToSelf())
				{
					if (LogonUser(userName, domainName, password, (int)logonType, (int)logonProvider, ref logonToken) != 0)
					{
						if (DuplicateToken(logonToken, (int)ImpersonationLevel.SecurityImpersonation, ref logonTokenDuplicate) != 0)
						{
							this.impersonatedWindowsIdentity = new WindowsIdentity(logonTokenDuplicate);
							this.threadWindowsImpersonationContext = this.ImpersonatedWindowsIdentity.Impersonate();

							Console.WriteLine("Thread-impersonated identity: " + WindowsIdentity.GetCurrent().Name);
						}
						else
							throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					else
						throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			catch
			{
				this.Dispose();
				throw;
			}
			finally
			{
				if (logonToken != IntPtr.Zero)
				{
					CloseHandle(logonToken);
					logonToken = IntPtr.Zero;
				}

				if (logonTokenDuplicate != IntPtr.Zero)
				{
					CloseHandle(logonTokenDuplicate);
					logonTokenDuplicate = IntPtr.Zero;
				}

				Console.WriteLine("Leave constructor");
			}
		}

		#endregion

		#region Fields/Constants

		private readonly WindowsIdentity impersonatedWindowsIdentity;
		private readonly WindowsImpersonationContext processWindowsImpersonationContext;
		private readonly WindowsImpersonationContext threadWindowsImpersonationContext;
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		private WindowsIdentity ImpersonatedWindowsIdentity
		{
			get
			{
				return this.impersonatedWindowsIdentity;
			}
		}

		private WindowsImpersonationContext ProcessWindowsImpersonationContext
		{
			get
			{
				return this.processWindowsImpersonationContext;
			}
		}

		private WindowsImpersonationContext ThreadWindowsImpersonationContext
		{
			get
			{
				return this.threadWindowsImpersonationContext;
			}
		}

		private bool Disposed
		{
			get
			{
				return this.disposed;
			}
			set
			{
				this.disposed = value;
			}
		}

		#endregion

		#region Methods/Operators

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool RevertToSelf();

		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				Console.WriteLine("Thread-impersonated identity: " + WindowsIdentity.GetCurrent().Name);

				if ((object)this.ThreadWindowsImpersonationContext != null)
				{
					this.ThreadWindowsImpersonationContext.Undo();
					this.ThreadWindowsImpersonationContext.Dispose();
				}

				if ((object)this.ImpersonatedWindowsIdentity != null)
					this.ImpersonatedWindowsIdentity.Dispose();

				Console.WriteLine("Process-impersonated identity: " + WindowsIdentity.GetCurrent().Name);

				if ((object)this.ProcessWindowsImpersonationContext != null)
				{
					this.ProcessWindowsImpersonationContext.Undo();
					this.ProcessWindowsImpersonationContext.Dispose();
				}

				Console.WriteLine("Outgoing identity: " + WindowsIdentity.GetCurrent().Name);
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
				Console.WriteLine("Leave impersonation scope");
			}
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public enum ImpersonationLevel
		{
			SecurityAnonymous = 0,
			SecurityIdentification = 1,
			SecurityImpersonation = 2,
			SecurityDelegation = 3
		}

		public enum LogonProvider
		{
			LOGON32_PROVIDER_DEFAULT = 0,
			LOGON32_PROVIDER_WINNT35 = 1,
			LOGON32_PROVIDER_WINNT40 = 2,
			LOGON32_PROVIDER_WINNT50 = 3
		}

		public enum LogonType
		{
			LOGON32_LOGON_INTERACTIVE = 2,
			LOGON32_LOGON_NETWORK = 3,
			LOGON32_LOGON_BATCH = 4,
			LOGON32_LOGON_SERVICE = 5,
			LOGON32_LOGON_UNLOCK = 7,
			LOGON32_LOGON_NETWORK_CLEARTEXT = 8, // Win2K or higher
			LOGON32_LOGON_NEW_CREDENTIALS = 9 // Win2K or higher 
		}

		#endregion
	}
}