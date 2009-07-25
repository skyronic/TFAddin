// 
// TaskForceMain.cs
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
namespace MonoDevelop.TaskForce
{
	public delegate void TaskChangedDelegate (ActiveTaskChangedEventArgs args);


	/// <summary>
	/// Singleton class to access all the main classes
	/// </summary>
	public sealed class TaskForceMain
	{
		/// <summary>
		/// Allocate ourselves as the constructor is private
		/// </summary>
		static readonly TaskForceMain instance = new TaskForceMain ();

		/// <summary>
		/// 
		/// </summary>
		public static TaskForceMain Instance {
			/// <summary>
			/// TODO: thread-safety locking
			/// </summary>
			get { return instance; }
		}


		public TaskData ActiveTask {
			get;
			set;
		}

		public bool IsTaskActive {
			get;
			set;
		}

		// Event fired when a task has been activated [post]
		public event TaskChangedDelegate TaskActivated;

		// event fired when a task is deactivated [post]
		public event TaskChangedDelegate TaskDeactivated;

		LogUtil log;

		/// <summary>
		/// Activate a task. If a task is currently active it's deactivated
		/// </summary>
		/// <param name="_task">
		/// A <see cref="TaskData"/>
		/// </param>
		public void ActivateTask (TaskData _task)
		{
			log.DEBUG ("Activating task: " + _task.Label);
			// Is a task running right now
			if (IsTaskActive) {
				// Deactivate it
				DeactivateCurrentTask ();
			}

			// previous task didn't close properly
			if (IsTaskActive) {
				throw new ApplicationException ("Task didn't get deactivated");
			}

			ActiveTask = _task;
			IsTaskActive = true;

			// Execute the activated task hook
			ActiveTaskChangedEventArgs args = new ActiveTaskChangedEventArgs ();

			// Why are we even doing this?
			args.CurrentTask = ActiveTask;
			
			// Call the task activation hook which informs the context.
			ActiveTask.OnTaskActivated();

			TaskActivated (args);

		}

		public void DeactivateCurrentTask ()
		{
			if (ActiveTask != null) {
				if (IsTaskActive) {
					log.DEBUG ("Deactivating task: " + ActiveTask.Label);
					ActiveTaskChangedEventArgs args = new ActiveTaskChangedEventArgs ();
					
					// Call the task deactivated hook
					ActiveTask.OnTaskDeactivated();
					
					args.CurrentTask = ActiveTask;

					ActiveTask = null;
					IsTaskActive = false;

					TaskDeactivated (args);
				}
			}
		}

		private TaskForceMain ()
		{
			IsTaskActive = false;
			log = new LogUtil ("TaskForceMain");
		}
	}


	[Serializable()]
	public sealed class ActiveTaskChangedEventArgs : EventArgs
	{

		public TaskData CurrentTask {
			get;
			set;
		}

		public ActiveTaskChangedEventArgs ()
		{

		}
	}
}
