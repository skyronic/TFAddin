
using System;
using MonoDevelop.Core.Gui;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.Projects;
using MonoDevelop.TaskForce.Utilities;
namespace MonoDevelop.TaskForce
{
	
	
	public class TasksPad : TreeViewPad
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
			this.treeView.AddChild("Hi there!");
		}
		
		public override void Initialize (NodeBuilder[] builders, TreePadOption[] options, string contextMenuPath)
		{
			log.DEBUG("Initialize called");
			base.Initialize (builders, options, contextMenuPath);
			
			foreach (WorkspaceItem it in IdeApp.Workspace.Items)
				treeView.AddChild (it);
		}
	}
}
