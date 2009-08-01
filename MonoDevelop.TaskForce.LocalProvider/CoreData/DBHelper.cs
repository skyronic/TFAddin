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
	
	[Serializable]
	public sealed class DBHelperException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DBHelperException"/> class
		/// </summary>
		public DBHelperException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DBHelperException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public DBHelperException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DBHelperException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public DBHelperException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DBHelperException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		public DBHelperException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
	public class DBHelper
	{
		public static void LogQuery (SqliteCommand cmd)
		{
			log.INFO ("SQL Query:" + cmd.CommandText);
			cmd.ExecuteNonQuery ();
		}
		public static SqliteConnection conn;
		public static bool initialized = false;
		private static LogUtil log;
		public const string DateFormat = "yyyy-MM-dd";

		/// <summary>
		/// Creates the tables. Called only once
		/// </summary>
		public static void CreateTables ()
		{
			CheckInitialize ();
			SqliteCommand cmd = new SqliteCommand (conn);
			cmd.CommandText = "CREATE TABLE Tasks(TaskID integer PRIMARY KEY AUTOINCREMENT , Name varchar (100), Priority integer ,Description varchar (5000), CreateDate datetime, DueDate datetime, Depends integer)";
			LogQuery (cmd);
			cmd.CommandText = "CREATE TABLE Comments(CommentID integer primary key, TaskId integer, Author varchar(100), Subject varchar(100), Message varchar(5000), PostDate datetime)";
			LogQuery (cmd);
		}
		/// <summary>
		/// Checks if the database is intialized and initializes if it isn't already.
		/// </summary>
		public static void CheckInitialize ()
		{
			if (!initialized) {
				Initialize ();
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
		public static int AddTask (TaskCore input)
		{
			SqliteCommand cmd = new SqliteCommand (conn);
			cmd.CommandText = String.Format ("INSERT INTO Tasks (Name, Priority, Description, CreateDate, DueDate, Depends) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', {5})", input.Title, input.Priority, input.Description, input.CreateDate.ToString (DateFormat), input.DueDate.ToString (DateFormat), input.Depends.ToString ());
			LogQuery (cmd);

			// execute a query to find the new taskID of the newly created task
			cmd.CommandText = String.Format ("Select TaskID FROM Tasks WHERE(Name=\"{0}\");", input.Title);
			SqliteDataReader cursor = cmd.ExecuteReader ();

			int TaskID = -1;
			if (cursor.Read ()) {
				// This HAS to execute once because theoretically
				// there is only one row in the database that will be selected
				TaskID = cursor.GetInt32 (cursor.GetOrdinal ("TaskID"));
			}

			foreach (CommentData c in input.Comments) {
				c.TaskId = TaskID;
				SqliteCommand cmd1 = new SqliteCommand (conn);
				cmd1.CommandText = String.Format ("INSERT INTO Comments(TaskId, Subject, Author, Message, PostDate) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", c.TaskId, c.Title, c.Author, c.Content, c.PostDate.ToString (DateFormat));
				LogQuery (cmd1);
			}
			return 0;
			//TODO: Return taskid
		}
		
		/// <summary>
		/// Updates the task data
		/// 
		/// NOTE: This does NOT update the comments too.
		/// you have to call DBHelper.AddComment() for that.
		/// </summary>
		/// <param name="input">
		/// A <see cref="TaskCore"/>
		/// </param>
		public static void UpdateTask(TaskCore input)
		{
			// We're assuming that the task has the Task ID valid
			SqliteCommand cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE Tasks SET Name = @name, Priority = @priority, Description = @description, CreateDate = @createdate, DueDate = @duedate, Depends = @depends WHERE TaskID = @taskid";
			cmd.Parameters.AddWithValue("@name", input.Title);
			cmd.Parameters.AddWithValue("@priority", input.Priority);
			cmd.Parameters.AddWithValue("@description", input.Description);
			cmd.Parameters.AddWithValue("@createdate", input.CreateDate.ToString(DateFormat));
			cmd.Parameters.AddWithValue("@duedate", input.DueDate.ToString(DateFormat));
			cmd.Parameters.AddWithValue("@depends", input.Depends);
			cmd.Parameters.AddWithValue("@taskid", input.Id);
			
			
			LogQuery(cmd);
		}

		/// <summary>
		/// Adds a new comment to the database. Does not affect the task
		/// </summary>
		/// <param name="task">
		/// A <see cref="TaskCore"/>
		/// </param>
		/// <param name="comment">
		/// A <see cref="CommentData"/>
		/// </param>
		public static void AddComment (TaskCore task, CommentData comment)
		{
			
			SqliteCommand cmd = conn.CreateCommand ();
			cmd.CommandText = "INSERT INTO Comments(TaskId, Subject, Author, Message, PostDate) VALUES (@taskid, @subject, @author, @message, @postdate);";
			cmd.Parameters.AddWithValue ("@taskid", task.Id.ToString());
			cmd.Parameters.AddWithValue ("@subject", comment.Title);
			cmd.Parameters.AddWithValue ("@author", comment.Author);
			cmd.Parameters.AddWithValue ("@message", comment.Content);
			cmd.Parameters.AddWithValue ("@postdate", comment.PostDate.ToString(DateFormat));
			
			LogQuery(cmd);
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
		public static TaskCore GetTaskCoreFromCursor (SqliteDataReader cursor)
		{
			// this assumes a single task exists

			// the task to be extracted from the cursor
			TaskCore task = new TaskCore ();

			try {
				task.Id = cursor.GetInt32 (cursor.GetOrdinal ("TaskId"));
				task.Depends = cursor.GetInt32 (cursor.GetOrdinal ("Depends"));
				task.Title = cursor.GetString (cursor.GetOrdinal ("Name"));
				task.Description = cursor.GetString (cursor.GetOrdinal ("Description"));
				task.Priority = cursor.GetInt32 (cursor.GetOrdinal ("Priority"));
				task.DueDate = cursor.GetDateTime (cursor.GetOrdinal ("DueDate"));
				task.CreateDate = cursor.GetDateTime (cursor.GetOrdinal ("CreateDate"));
			} catch {
				log.ERROR ("Reading from cursor failed");
				return null;
			}

			// now, extract the comments.
			SqliteCommand cmd = new SqliteCommand (conn);

			cmd.CommandText = String.Format ("SELECT * FROM Comments WHERE (TaskId = {0});", task.Id);
			log.WARN ("Trying to read comments with query - " + cmd.CommandText);

			SqliteDataReader commentCursor = cmd.ExecuteReader ();

			while (commentCursor.Read ()) {
				CommentData comment = new CommentData ();

				try {
					comment.Id = commentCursor.GetInt32 (commentCursor.GetOrdinal ("CommentID"));
					comment.TaskId = commentCursor.GetInt32 (commentCursor.GetOrdinal ("TaskId"));
					comment.Title = commentCursor.GetString (commentCursor.GetOrdinal ("Subject"));
					comment.Author = commentCursor.GetString (commentCursor.GetOrdinal ("Author"));
					comment.Content = commentCursor.GetString (commentCursor.GetOrdinal ("Message"));
					comment.PostDate = commentCursor.GetDateTime (commentCursor.GetOrdinal ("PostDate"));
					log.DEBUG ("Extracted comment - " + comment.ToString ());
				} catch {
					log.ERROR ("Something went wrong while retrieving comment");
					return null;
				}
				task.Comments.Add (comment);
			}
			log.DEBUG ("Extracted task as : " + task.ToString ());
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
		public static List<TaskCore> GetTasksFromCursor (SqliteDataReader cursor)
		{
			List<TaskCore> tasks = new List<TaskCore> ();
			while (cursor.Read ()) {
				TaskCore task = GetTaskCoreFromCursor (cursor);
				if (task != null) {
					log.INFO ("Task added");
					tasks.Add (task);
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
		public static List<TaskCore> GetAllTasks ()
		{
			SqliteCommand cmd = new SqliteCommand (conn);
			cmd.CommandText = "SELECT * FROM Tasks";
			SqliteDataReader reader = cmd.ExecuteReader ();

			return GetTasksFromCursor (reader);
		}

		public static void Initialize ()
		{
			initialized = true;
			conn = new SqliteConnection ();
			log = new LogUtil ("DBHelper");

			if (System.IO.File.Exists ("tasks.db")) {
				conn.ConnectionString = "Data Source=tasks.db;Synchronous=Off";
				log.INFO ("Opening connection");
				conn.Open ();
			} else {
				log.WARN ("Creating new database");
				conn.ConnectionString = "Data Source=tasks.db;New=True;Synchronous=Off";
				conn.Open ();
				CreateTables ();
			}
		}

	}
}
