using System;
using MonoDevelop.TaskForce.LocalProvider;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.Data;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;
using MonoDevelop.TaskForce.LocalProvider.CoreData;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

using MonoDevelop.Core;
using MonoDevelop.Projects;
using MonoDevelop.Projects.Policies;
using MonoDevelop.Core.Serialization;

namespace TestBed
{
	interface IScaryData
	{
		void TearDown();
	}
	
	class SomeRandomClass : IScaryData
	{
		public SomeRandomClass()
		{
			
		}
		public void TearDown()
		{
			Core = "Boo!";
			x = new CategoryData();
			x.Label = "WTF";
		}
		
		[ItemProperty]
		NodeData x;
		
		[ItemProperty]
		public string Core;
	}
	
	class DataToken
	{
		[ItemProperty]
		public int Data;
		
		[ItemProperty]
		public IScaryData scaryData;
		
		public List<DataToken> children = new List<DataToken>();
		
		public DataToken parent;
		
		
		public DataToken()
		{
			scaryData = new SomeRandomClass();
			scaryData.TearDown();
		}
		
		public DataToken(int _data)
		{
			Data = _data;
			for(int i=0;i < Data;i++)
			{
				DataToken d = new DataToken(i);
				d.parent = this;
				children.Add(d);
			}
		}
		
	
	}
	
	class SerializationTest
	{
		[ItemProperty]
		public string Name
		{
			get;
			set;
		}
		
		//[ItemProperty("simpsons1")]
		//public List<DataToken> rows = new List<DataToken>();
		
		[ItemProperty]
		public DataToken rootNode = new DataToken(3);
		public void populate()
		{
			
		}
		public SerializationTest()
		{
			
		}
	}
	class MainClass
	{
		public static LogUtil log;
		
		public static void CheckDatabaseThing()
		{
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
			DBHelper.AddTask(stub);
			DBHelper.GetAllTasks();			
		}
		
		
		public static void TestSerialization()
		{
			SerializationTest blah = new SerializationTest();
			blah.populate();
			DataContext c = new DataContext();
			c.IncludeType(blah.GetType());
			blah.Name = "Hello there!";
			
			XmlDataSerializer ser = new XmlDataSerializer(c);
			XmlTextWriter xtw = new XmlTextWriter(Console.Out);
			
			ser.Serialize(xtw,blah);
		}
		
		public static void Main(string[] args)
		{
			TestSerialization();
		}
	}
}