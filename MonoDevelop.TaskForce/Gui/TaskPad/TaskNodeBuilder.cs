// 
// TaskNodeBuilder.cs
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
//------------------------------------------------------------------------------


using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.Components.Commands;



namespace MonoDevelop.TaskForce.Gui.TaskPad
{
    public class TaskNodeBuilder : TaskPadNodeBuilder
    {
		public TaskNodeBuilder () : base()
		{
			log = new LogUtil ("TaskNodeBuilder");
		}

        public override Type CommandHandlerType
        {
            get
            {
                return typeof(TaskNodeCommandHandler);
            }
        }

        public override Type NodeDataType
        {
            get
            {
                return typeof(TaskData);
            }
        }

        public override void BuildChildNodes (ITreeBuilder treeBuilder, object dataObject)
        {
			base.BuildChildNodes (treeBuilder, dataObject);
			
			if (dataObject is TaskData)
			{			

				TaskData taskData = dataObject as TaskData;
				
				foreach (object child in taskData.children)
				{
					treeBuilder.AddChild (child);
				}
			}
        }


        public override void BuildNode (ITreeBuilder treeBuilder, object dataObject, ref string label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
        {
			// convert the data object to a nodedata
			if (dataObject is TaskData)
			{
				TaskData taskData = dataObject as TaskData;
				
				label = taskData.Label;
				
				// TODO: Change the silly icons
				icon = Context.GetIcon(Gtk.Stock.Apply);
				closedIcon = Context.GetIcon(Gtk.Stock.Clear);
			}
			
        }

		public override void OnNodeDataChanged (NodeData source, NodeDataChangedEventArgs args)
		{
			if (source is TaskData)
			{
				// get the tree builder
				ITreeBuilder treeBuilder = Context.GetTreeBuilder (source);
				treeBuilder.UpdateAll ();
			}
		}

        public override void Dispose ()
        {
            base.Dispose();
        }



        protected override void Initialize()
        {
            base.Initialize();
        }

        public override void DataTypeComparison(object dataObject)
        {
            throw new System.NotImplementedException();
        }

        public override string GetNodeName(ITreeNavigator thisNode, object dataObject)
        {
			if(dataObject is NodeData)
			{
				return (dataObject as NodeData).Label;
			}
			return "";
        }
		

    }

    public class TaskNodeCommandHandler : TaskPadNodeCommandHandler
    {
		public TaskNodeCommandHandler()
		{
			
		}
		
		[CommandHandler(ContextMenuCommands.EditTask)]
		public void EditTask()
		{
			// get the current node
			TaskData self = this.CurrentNode.DataItem as TaskData;
			
			// access the provider from the parent's provider object
			if(self.parent is ProviderData) // TODO: Make this more robust if needed when categories come in
			{
				ProviderData selfProvider = self.parent as ProviderData;
				
				// Call the "Edit task" function on the appropriate provider
				selfProvider.provider.EditTask(self);
			}
		}
		
		[CommandHandler(ContextMenuCommands.ViewTask)]
		public void ViewTask()
		{
			// get the current node
			TaskData self = this.CurrentNode.DataItem as TaskData;
			
			// access the provider from the parent's provider object
			if(self.parent is ProviderData) // TODO: Make this more robust if needed when categories come in
			{
				ProviderData selfProvider = self.parent as ProviderData;
				
				// Call the "Edit task" function on the appropriate provider
				selfProvider.provider.ViewTask(self);
			}	
		}
		
		[CommandHandler(ContextMenuCommands.ActivateTask)]
		public void ActivateTask()
		{
			// Get the current task
			TaskData self = this.CurrentNode.DataItem as TaskData;
			
			if(self!=null)
			{
				TaskForceMain tfMain = TaskForceMain.Instance;
				tfMain.ActivateTask(self);
			}
		}
		
		[CommandHandler(ContextMenuCommands.DeactivateTask)]
		public void DeactivateTask()
		{
			TaskData self = this.CurrentNode.DataItem as TaskData;
			if(self!=null)
			{
				TaskForceMain tfMain = TaskForceMain.Instance;
				if(tfMain.ActiveTask == self)
				{
					tfMain.DeactivateCurrentTask();
				}
			}
		}		
		
		public override void ActivateItem ()
		{
			// just jump into the edit task menu
			EditTask();
		}
		
		[CommandHandler(ContextMenuCommands.DeleteTask)]
		public void DeleteTask()
		{
			TaskData self = this.CurrentNode.DataItem as TaskData;
			if(self!=null)
			{
				// TODO: handle if it isn't a providerdata as well
				if(self.parent is ProviderData)					
				{
					ProviderData providerNode = self.parent as ProviderData;
					// Remove from the children list
					providerNode.children.Remove(self);
					self.Dispose();
					self = null;
					
					// Force the treeview to update
					providerNode.TriggerUpdate();
					
					// update on disk:
					TaskForceMain.Instance.StartTFStoreUpdate();					
				}
			}
		}
		public override void OnNodeChange()
		{
			
		}
		

    }

}

