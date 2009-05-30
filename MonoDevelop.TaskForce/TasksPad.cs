
using System;
using MonoDevelop.Core.Gui;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.Projects;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.TaskForce.TaskData;
namespace MonoDevelop.TaskForce
{
	
	
	public class TasksPad : SolutionPad
	{
		LogUtil log;
		
		public TasksPad()
		{
			log = new LogUtil("TasksPadMain");
			log.DEBUG("Initialized");
			IdeApp.Workspace.WorkspaceItemOpened += IdeAppWorkspaceWorkspaceItemOpened;
		}

		void IdeAppWorkspaceWorkspaceItemOpened (object sender, WorkspaceItemEventArgs e)
		{
			log.DEBUG("Workspace item opened");
			treeView.AddChild("Hi there!");
			treeView.AddChild(new IntTaskData(6));
		}
		
		public override void Initialize (NodeBuilder[] builders, TreePadOption[] options, string contextMenuPath)
		{
			log.DEBUG("Initialize called");
			base.Initialize (builders, options, contextMenuPath);
			
			treeView.AddChild(new IntTaskData(5));
			
		}
	}
}
