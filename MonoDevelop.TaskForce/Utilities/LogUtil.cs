// 
// LogUtil.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2009 Anirudh Sanjeev
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4918
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace MonoDevelop.TaskForce.Utilities
{
	/// <summary>
	/// Logging utility
	/// </summary>
	public class LogUtil
	{
		
		/// <summary>
		/// The name of the logging module
		/// </summary>
		public string moduleName;
		public string hashCode;
		public LogUtil()
		{
			this.moduleName = "Untitled";
		}
		
		/// <summary>
		/// Constructor with custom name
		/// </summary>
		/// <param name="_moduleName">
		/// A <see cref="String"/> which is the name of the module.
		/// </param>
		public LogUtil(String _moduleName)
		{
			this.moduleName = _moduleName;
			this.hashCode = "";
		}
		protected string GetPrefix()
		{
			if(hashCode != "")
				return String.Format("[TaskForce][{0}][#{1}#]",moduleName, hashCode);
			
			return String.Format("[TaskForce][{0}]",moduleName);
			
		}
		
		public void SetHash(object o)
		{
			hashCode =  o.GetHashCode().ToString();
		}
		
		/// <summary>
		/// Write a Debug message
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>. Log message
		/// </param>
		public void DEBUG(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("[DEBUG]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
		
		/// <summary>
		/// Write an Info message
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>. Log message
		/// </param>
		public void INFO(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("[INFO]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
		
		
		/// <summary>
		/// Write a standard log message
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>. Log message
		/// </param>
		public void LOG(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("[LOG]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
		
		
		/// <summary>
		/// Write a warning message.
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>. Log message.
		/// </param>
		public void WARN(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine("[WARN]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
		
		
		/// <summary>
		/// Write an error message
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>. Log Message
		/// </param>
		public void ERROR(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[ERROR]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
		
		/// <summary>
		/// Ignore the logs, but keep them in the source code
		/// </summary>
		/// <param name="message">
		/// A <see cref="String"/>
		/// </param>
		public void NULL(String message)
		{
			
		}
		
	}
}

