using System;
using MonoDevelop.TaskForce.LocalProvider;
using MonoDevelop.TaskForce.Utilities;
using System.Data.SQLite;


namespace TestBed
{
	class MainClass
	{
		public static LogUtil log;
		public static SQLiteConnection conn;
		public static void CreateDB()
		{
		SQLiteConnection Conn = new SQLiteConnection();
Conn.ConnectionString = "Data Source=tasks.db;New=True;Compress=True;Synchronous=Off";
Conn.Open();
SQLiteCommand Cmd = new SQLiteCommand();
Cmd = Conn.CreateCommand();
Cmd.CommandText = "CREATE TABLE Tasks(TaskID integer primary key , Name varchar (100), Priority integer ,Description varchar (500), CreateDate datetime, DueDate datetime)" ;
Cmd.ExecuteNonQuery();
Conn.Close();
		}
		
		public static void AddTask(string name, int priority, string desc, DateTime createDate, DateTime dueDate)
		{
			SQLiteCommand Cmd = new SQLiteCommand(conn);
			
			Cmd.CommandText = String.Format("INSERT INTO Tasks (Name, Priority, Description, CreateDate, DueDate) VALUES ('{0}', {1}, '{2}', '{3}', '{4}')", name, priority, desc, createDate.ToShortDateString(), dueDate.ToShortDateString());
			log.INFO("Creating query with " + Cmd.CommandText);
			Cmd.ExecuteNonQuery();
		}
		
		
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			ProviderFrontend x = new ProviderFrontend();
			log = new LogUtil("Main");
			
			conn = new SQLiteConnection();
			conn.ConnectionString = "Data Source=tasks.db;Synchronous=Off";
			conn.Open();
			AddTask("Take over world", 42, "Something evil goes here", DateTime.Today, DateTime.Now + TimeSpan.FromDays(4));
			
			
			conn.Close();
		}
	}
}