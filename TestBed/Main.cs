using System;
using MonoDevelop.TaskForce.LocalProvider;
using MonoDevelop.TaskForce.Utilities;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using MonoDevelop.TaskForce.LocalProvider.CoreData;

namespace TestBed
{
	class MainClass
	{
		public static LogUtil log;
		
		
		public static void Main(string[] args)
		{
			// Create a stub task with comments
			TaskCore stub = new TaskCore();
			stub.Title = "Mr. Stub2";
			stub.Description = "Hello! I am Mr. stub2";
			stub.CreateDate = DateTime.Now;
			stub.DueDate = DateTime.Now + TimeSpan.FromDays(4);
			
			Comment c = new Comment();
			c.postDate = DateTime.Now + TimeSpan.FromDays(1);
			c.subject = "Fixed it for you2";
			c.content = "Hey, the bug is fixed now2!";
			
			stub.AddComment(c);
			
			
			DBHelper.Initialize();
			//DBHelper.AddTask(stub);
			DBHelper.GetAllTasks();
		}
	}
}