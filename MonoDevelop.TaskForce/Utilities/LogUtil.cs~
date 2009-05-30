
using System;

namespace MonoDevelop.TaskForce.Utilities
{
	public class LogUtil
	{
		public string moduleName;
		
		public LogUtil()
		{
			this.moduleName = "Untitled";
		}
		
		public LogUtil(String _moduleName)
		{
			this.moduleName = _moduleName;
		}
		protected string GetPrefix()
		{
			return String.Format("[TaskForce][{0}]",moduleName);
		}
		
		public void DEBUG(String message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[DEBUG]" + GetPrefix() + message);
			Console.ForegroundColor = currentColor;
		}
	}
}
