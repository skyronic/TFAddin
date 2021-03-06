// 
// ContextData.cs
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
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.Ide.Gui;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MonoDevelop.Core.Serialization;


namespace MonoDevelop.TaskForce.Context
{

	/// <summary>
	/// Contains the data of the context which is attached to a task
	/// 
	/// Currently supports:
	/// 1. Open documents and line numbers
	/// 
	/// TODO: Should this data type be task-agnostic?
	/// </summary>
	public class ContextData
	{
		private TaskData parentTask;
		private LogUtil log;

		[ItemProperty()]
		private DocumentStore documentStore;

		[ItemProperty()]
		public List<TaskSession> Sessions {
			get;
			set;
		}

		private TaskSession activeSession;

		public void TaskActivated ()
		{
			activeSession = new TaskSession ();
			documentStore.RestoreMemento ();
		}

		public void TaskDeactivated ()
		{
			documentStore.CaptureMemento ();
			activeSession.EndTime = DateTime.Now;
			Sessions.Add (activeSession);

			// move the item to a new session
			activeSession = new TaskSession ();

			// Store the task information by triggering an update
			TaskForceMain.Instance.StartTFStoreUpdate ();
		}


		public void Initialize (TaskData _taskData)
		{
			parentTask = _taskData;

			// any more intialization
		}
		public ContextData ()
		{
			log = new LogUtil ("ContextData");
			log.SetHash (this);

			documentStore = new DocumentStore ();
			Sessions = new List<TaskSession> ();

		}
	}
}
