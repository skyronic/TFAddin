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
using MonoDevelop.Core.Serialization;
using System.IO;
using MonoDevelop.TaskForce.Gui.TaskPad;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using MonoDevelop.Core;
using System.Collections.Generic;
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
		
		public ExtensibleTreeView TreeView
		{get;
			set;
		}
		
		public TFStore Store
		{
			get;
			set;
		}

		public TaskData ActiveTask {
			get;
			set;
		}

		public bool IsTaskActive {
			get;
			set;
		}
		
		public TaskSolutionPad TaskPad
		{
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
			log.DEBUG ("Activating task: " + _task.Label);

			// Call the task activation hook which informs the context.
			ActiveTask.OnTaskActivated();

			// TaskActivated (args);TODO: Why am I getting an exception here?

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

					//TaskDeactivated (args);
				}
			}
		}
		
		public void TempAddNewProvider(string serializedString)
		{
			log.INFO("The serialized string is - " + serializedString);
			
			// deserialize the string into a ProviderData			
			object o1 = Util.DeserializeString(serializedString, typeof(ProviderData));
			
			ProviderData tempProv = o1 as ProviderData;
			tempProv.SerializeData();
			log.INFO("The freshly serialized data is:" + tempProv.serializedString);
			
			tempProv.provider.ConstructBasicProvider(tempProv);
			
			// add the provider data node to the treeview, and pray it works
			this.TreeView.AddChild(tempProv);
		}

		private TaskForceMain ()
		{
			IsTaskActive = false;
			log = new LogUtil ("TaskForceMain");
		}
		
		
		
		
		/// <summary>
		/// The main function that will be executed on the startup of the application
		/// 
		/// Teh Entry point
		/// </summary>
		public void Main()
		{
			log.INFO("TaskForce started");
			
			// Subscribe to the solution opened event.
			IdeApp.Workspace.SolutionLoaded += OnSolutionLoaded;
			IdeApp.Workspace.SolutionUnloaded += OnSolutionUnloaded;
			
			this.RegisterAllTypes();
		}

		void OnSolutionUnloaded (object sender, SolutionEventArgs e)
		{
			
		}
		
		
		// We need both to be available for the population to take place.
		bool padAvailable = false;
		bool solutionAvailable = false;
		
		
		/// <summary>
		/// This function is called twice - once when the solution is loaded and once when the taskpad is loaded
		/// this way, it works only on the second time. The user might not have the pad enabled for it to work.
		/// </summary>
		public void PopulateGui()
		{
			if(padAvailable && solutionAvailable)
			{
			string targetFileName = activeSolution.BaseDirectory.Combine(".taskforce/taskforce.xml").FullPath;
			
			if(File.Exists(targetFileName))
			{
				log.INFO("Deserializing TFStore from file");
				// Read from the file
				Store = Util.DeserializeString(File.ReadAllText(targetFileName), typeof(TFStore)) as TFStore;
				if(Store == null)
				{
					log.ERROR("unable to deserialize taskforce store");
					return;
				}
				Store.TargetFile = targetFileName;
				Store.TreeView = this.TreeView;
				
				Store.PostDeserializeHook();
			}
			else
			{
				// Create the directory				
				FileService.CreateDirectory(activeSolution.BaseDirectory.Combine(".taskforce").FullPath);
				Store = new TFStore();
				
				Store.TargetFile = targetFileName;
				Store.TreeView = this.TreeView;
				Store.CreateNewLocalProvider(activeSolution);
				
			}
			}
		}
		
		Solution activeSolution;

		void OnSolutionLoaded (object sender, SolutionEventArgs e)
		{
			activeSolution = e.Solution;
			
			// set the bit and call the PopulateGui method
			solutionAvailable = true;
			PopulateGui();			
		}
		
		public void OnTaskPadLoaded()
		{
			padAvailable = true;
			PopulateGui();
		}
		
		
		/// <summary>
		/// Registers all the types with the serialization engine
		/// </summary>
		public void RegisterAllTypes()
		{
			// Register all the provider types with the data context of the serializer
			ProviderData.RegisterProviderTypes();
			
			// make a list of all the types here
			List<Type> typeList = new List<Type>();
			
			typeList.Add(typeof(TaskData));
			typeList.Add(typeof(TFStore));
			typeList.Add(typeof(NodeData));
			typeList.Add(typeof(ProviderData));
			typeList.Add(typeof(MonoDevelop.TaskForce.Gui.Components.CommentData));
			// typeList.Add(typeof(TaskData));
			
			foreach(Type t in typeList)
			{
				Util.context.IncludeType(t);
			}
		}
		
		public void StartTFStoreUpdate()
		{
			Store.UpdateFile();
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
