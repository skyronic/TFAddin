// 
// ProviderFrontend.cs
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
using MonoDevelop.TaskForce.Providers;
using MonoDevelop.Ide.Gui;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.LocalProvider.TaskWidgets;
using MonoDevelop.TaskForce.LocalProvider.CoreData;
using System.Collections;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.LocalProvider.Gui;
using System.Collections.Generic;

namespace MonoDevelop.TaskForce.LocalProvider
{
	
	
	public class ProviderFrontend : IProvider
	{
		protected LogUtil log;
		protected ProviderData providerNode;

		#region IProvider implementation
		public string DoSomething ()
		{
			return "Hello there!";
		}
		#endregion
		
		/// <summary>
		/// Shows the new task GUI and updates the providernode object when finished
		/// </summary>
		/// <param name="providerNode">
		/// A <see cref="ProviderData"/>
		/// </param>
		public void NewTask(ProviderData _providerNode)
		{
			NewTaskView newTab = new NewTaskView(providerNode);
			
			IdeApp.Workbench.OpenDocument(newTab, true);
		}
		
		public void EditTask(TaskData target)
		{
			
		}
		
		public void ViewTask(TaskData target)
		{
			
		}
		
		/// <summary>
		/// Initializes the provider and populates the node
		/// </summary>
		/// <param name="providerNode">
		/// A <see cref="ProviderData"/>
		/// </param>
		public void InitializeProvider(ProviderData _providerNode)
		{
			providerNode = _providerNode;
			
			log = new LogUtil("LOCAL PROVIDER");
			
			if(providerNode == null)
			{
				log.ERROR("Provider is wrong!");
			}
			// initialize the database
			DBHelper.Initialize();
			
			// set any required details about the provider
			providerNode.Label = "Local Provider";
			
			// get an arraylist of all the taskcores
			List<TaskCore> tasks = DBHelper.GetAllTasks(); // get all the coredata
			log.INFO("Tasks - Count - " + tasks.Count.ToString());
			foreach(TaskCore core in tasks)
			{
				log.INFO("The core object is: " + core);
				// create a new task node
				TaskData taskNode = new TaskData();
				
				// set the core data object
				taskNode.CoreDataObject = core;
				
				// set the label and icon
				taskNode.Label = core.Title;
				
				// update the tree without updating gui				
				providerNode.AddChildSilent(taskNode);
				
			}
			
			// trigger changes in the gui
			providerNode.TriggerUpdate();
		}
		
		/// <summary>
		/// Creates a new category and appends it to the provider node builder
		/// YOU HAVE TO HAVE CALLED IntializeProvider() Before this
		/// </summary>
		public void CreateNewCategory()
		{
			// URGENT: Change the name of newcategory into something else
			NewCategory n = new NewCategory(providerNode);
			log.INFO("Creating a new category");
			n.ShowAll();
		}
	
		public ProviderFrontend()
		{
			
		}
		
		/// <summary>
		/// Used to nest categories.
		/// 
		/// TODO: Migrate createnewcategory to use this function instead
		/// </summary>
		/// <param name="parent">
		/// A <see cref="NodeData"/>
		/// </param>
		public void AddChildCategory(NodeData parent)
		{
			NewCategory n = new NewCategory(parent);
			log.INFO("Making new category with parent - " + parent.Label);
			n.ShowAll();
		}
		
		
		
	}
}
