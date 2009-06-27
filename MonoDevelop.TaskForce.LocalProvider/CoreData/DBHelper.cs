// 
// DBHelper.cs
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


using System;
using System.Data;
using Mono.Data;
using Mono.Data.Sqlite;
using MonoDevelop.TaskForce.Utilities;
using System.Collections;
using System.Collections.Generic;
using MonoDevelop.TaskForce.Gui.Components;

namespace MonoDevelop.TaskForce.LocalProvider.CoreData
{
	
	public class DBHelper
	{
		public static void LogQuery(SqliteCommand cmd)
		{
			log.INFO("SQL Query:" + cmd.CommandText);
			cmd.ExecuteNonQuery();
		}
		public static SqliteConnection conn;
		public static bool initialized = false;
		private static LogUtil log;
		public const string DateFormat = "yyyy-MM-dd";
		
		/// <summary>
		/// Creates the tables. Called only once
		/// </summary>
		public static void CreateTables()
		{
			CheckInitialize();
			SqliteCommand cmd = new SqliteCommand(conn);
			cmd.CommandText = "CREATE TABLE Tasks(TaskID integer PRIMARY KEY AUTOINCREMENT , Name varchar (100), Priority integer ,Description varchar (5000), CreateDate datetime, DueDate datetime, Depends integer)" ;
			LogQuery(cmd);
			cmd.CommandText = "CREATE TABLE Comments(CommentID integer primary key, TaskId integer, Author varchar(100), Subject varchar(100), Message varchar(5000), PostDate datetime)";
			LogQuery(cmd);
		}
		/// <summary>
		/// Checks if the database is intialized and initializes if it isn't already.
		/// </summary>
		public static void CheckInitialize()
		{
			if(!initialized)
			{
				Initialize();
			}
		}
		
		/// <summary>
		/// Adds a task and all the comments into the database
		/// </summary>
		/// <param name="input">
		/// A <see cref="TaskCore"/> - the task to be added
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/> - the task ID
		/// </returns>
		public static int AddTask(TaskCore input)
		{
			SqliteCommand cmd = new SqliteCommand(conn);
			cmd.CommandText = String.Format("INSERT INTO Tasks (Name, Priority, Description, CreateDate, DueDate, Depends) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', {5})", input.Title, input.Priority, input.Description, input.CreateDate.ToString(DateFormat), input.DueDate.ToString(DateFormat), input.Depends.ToString());			
			LogQuery(cmd);
			
			// execute a query to find the new taskID of the newly created task
			cmd.CommandText = String.Format("Select TaskID FROM Tasks WHERE(Name=\"{0}\");", input.Title);
			SqliteDataReader cursor = cmd.ExecuteReader();
			
			int TaskID = -1;
			if(cursor.Read())
			{
				// This HAS to execute once because theoretically
				// there is only one row in the database that will be selected
				TaskID = cursor.GetInt32(cursor.GetOrdinal("TaskID"));
			}
			
			foreach(CommentData c in input.Comments)
			{
				c.TaskId = TaskID;
				SqliteCommand cmd1 = new SqliteCommand(conn);
				cmd1.CommandText = String.Format("INSERT INTO Comments(TaskId, Subject, Author, Message, PostDate) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", c.TaskId, c.Title, c.Author, c.Content, c.PostDate.ToString(DateFormat));
				LogQuery(cmd1);
			}
			return 0; //TODO: Return taskid
		}
		
		/// <summary>
		/// Gets the taskcore object from the sqlite cursor
		/// </summary>
		/// <param name="cursor">
		/// A <see cref="SqliteDataReader"/>. This won't be traversed, must be 
		/// called each time the cursor moves ahead
		/// </param>
		/// <returns>
		/// A <see cref="TaskCore"/> - allocated and re-created
		/// Returns null if reading failed
		/// </returns>
		public static TaskCore GetTaskCoreFromCursor(SqliteDataReader cursor)
		{
			// this assumes a single task exists
			
			// the task to be extracted from the cursor
			TaskCore task = new TaskCore();
			
			try{
			task.Id = cursor.GetInt32(cursor.GetOrdinal("TaskId"));
			task.Depends = cursor.GetInt32(cursor.GetOrdinal("Depends"));
			task.Title = cursor.GetString(cursor.GetOrdinal("Name"));
			task.Description = cursor.GetString(cursor.GetOrdinal("Description"));
			task.Priority = cursor.GetInt32(cursor.GetOrdinal("Priority"));
				task.DueDate = cursor.GetDateTime(cursor.GetOrdinal("DueDate"));
				task.CreateDate = cursor.GetDateTime(cursor.GetOrdinal("CreateDate"));
			}
			catch
			{
				log.ERROR("Reading from cursor failed");
				return null;
			}
			
			// now, extract the comments.
			SqliteCommand cmd = new SqliteCommand(conn);
			
			cmd.CommandText = String.Format("SELECT * FROM Comments WHERE (TaskId = {0});", task.Id);
			
			SqliteDataReader commentCursor = cmd.ExecuteReader();
			
			while(commentCursor.Read())
			{
				CommentData comment = new CommentData();
				
				try
				{
				comment.Id = cursor.GetInt32(cursor.GetOrdinal("CommentID"));
				comment.TaskId = cursor.GetInt32(cursor.GetOrdinal("TaskId"));
				comment.Title = cursor.GetString(cursor.GetOrdinal("Subject"));
				comment.Author = cursor.GetString(cursor.GetOrdinal("Author"));
				comment.Content = cursor.GetString(cursor.GetOrdinal("Message"));
				comment.PostDate = cursor.GetDateTime(cursor.GetOrdinal("PostDate"));
				}
				catch
				{
					log.ERROR("Something went wrong while retrieving comment");
					return null;
				}
				task.Comments.Add(comment);
			}
			log.DEBUG("Extracted task as : " + task.ToString());
			return task;
		}
		
		/// <summary>
		/// Traverses a sqlitedatareader to return an arryalist of tasks
		/// </summary>
		/// <param name="cursor">
		/// A <see cref="SqliteDataReader"/>. Will be traversed.
		/// the query should've been executed
		/// </param>
		/// <returns>
		/// A <see cref="ArrayList"/> containing the resultant tasks
		/// </returns>
		public static List<TaskCore> GetTasksFromCursor(SqliteDataReader cursor)
		{
			List<TaskCore> tasks = new List<TaskCore>();
			while(cursor.Read())
			{
				TaskCore task = GetTaskCoreFromCursor(cursor);
				if(task!=null)
				{
					log.INFO("Task added");
					tasks.Add(task);
				}
			}
			
			return tasks;
		}
		
		
		/// <summary>
		/// The basic query which will fetch all the tasks from the database
		/// 
		/// this serves more like an example. 
		/// </summary>
		/// <returns>
		/// A <see cref="ArrayList"/> containing all the resultant tasks
		/// </returns>
		public static List<TaskCore> GetAllTasks()
		{
			SqliteCommand cmd = new SqliteCommand(conn);
			cmd.CommandText = "SELECT * FROM Tasks";
			SqliteDataReader reader = cmd.ExecuteReader();
			
			return GetTasksFromCursor(reader);
		}
		
		public static void Initialize()
		{
			initialized = true;
			conn = new SqliteConnection();
			log = new LogUtil("DBHelper");
			
			if(System.IO.File.Exists("tasks.db"))
			{
				conn.ConnectionString = "Data Source=tasks.db;Synchronous=Off";
				log.INFO("Opening connection");
				conn.Open();
			}
			else
			{
				log.WARN("Creating new database");
				conn.ConnectionString = "Data Source=tasks.db;New=True;Synchronous=Off";
				conn.Open();
				CreateTables();
			}
		}
		
	}
}
