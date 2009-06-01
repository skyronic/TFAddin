
using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.TaskForce.TaskData;
using MonoDevelop.TaskForce.Utilities;

namespace MonoDevelop.TaskForce.TaskPad
{
	
	
	public class IntegerNodeBuilder : TypeNodeBuilder
	{
		MonoDevelop.TaskForce.Utilities.LogUtil log;
		public IntegerNodeBuilder()
		{
			log = new MonoDevelop.TaskForce.Utilities.LogUtil("IntNodeBuilder");
			log.DEBUG("Initialized IntNodeBuilder");
		}
		public override void BuildNode (MonoDevelop.Ide.Gui.Components.ITreeBuilder treeBuilder, object dataObject, ref string label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
		{
			base.BuildNode (treeBuilder, dataObject, ref label, ref icon, ref closedIcon);
			
			ITaskData taskData = (ITaskData)dataObject;
			if(taskData.GetTaskType() == TaskType.IntTask)
			{
				IntTaskData intTaskData = (IntTaskData)taskData;
				
				// Set the label based on the current data
				label = "Node: " + intTaskData.data.ToString();
				
				intTaskData.TaskDataChanged += new TaskDataChangedHandler(HandleNodeChange);
			}
			
		}
		
		public void HandleNodeChange(object sender)
		{
			
			log.DEBUG("Handling a node change");
			ITreeBuilder treeBuilder = this.Context.GetTreeBuilder(sender);
			
			treeBuilder.Update();
			treeBuilder.UpdateChildren();
			
		}
		public override bool HasChildNodes (MonoDevelop.Ide.Gui.Components.ITreeBuilder builder, object dataObject)
		{
			
			
			ITaskData taskData = (ITaskData)dataObject;
			if(taskData.GetTaskType() == TaskType.IntTask)
			{
				IntTaskData intTaskData = (IntTaskData)taskData;
				
				// return true if there are any children
				return(intTaskData.children.Count != 0);
				
			}
			return false;
		}		
		
		public override void OnNodeAdded (object dataObject)
		{
			// try a conversioni
			IntTaskData dropObj = dataObject as IntTaskData;
			log.DEBUG("Node has been Added. Data:" + dropObj.data.ToString());
			
		}
		
		public override void OnNodeRemoved (object dataObject)
		{
			IntTaskData removeObj = dataObject as IntTaskData;
			log.DEBUG("Node has been removed. Data: " + removeObj.data.ToString());
		}
		
		public override object GetParentObject (object dataObject)
		{
			return (dataObject as IntTaskData).parent;
		}
		
		


		
		public override void BuildChildNodes (MonoDevelop.Ide.Gui.Components.ITreeBuilder treeBuilder, object dataObject)
		{
			base.BuildChildNodes (treeBuilder, dataObject);
			
			ITaskData taskData = (ITaskData)dataObject;
			if(taskData.GetTaskType() == TaskType.IntTask)
			{
				IntTaskData intTaskData = (IntTaskData)taskData;
				foreach(object child in intTaskData.children)
				{
					treeBuilder.AddChild(child);
				}
			}
			
		}
		public override string GetNodeName (MonoDevelop.Ide.Gui.Components.ITreeNavigator thisNode, object dataObject)
		{
			return "My Name:)";
		}
		
		public override void GetNodeAttributes (MonoDevelop.Ide.Gui.Components.ITreeNavigator parentNode, object dataObject, ref MonoDevelop.Ide.Gui.Components.NodeAttributes attributes)
		{
			base.GetNodeAttributes (parentNode, dataObject, ref attributes);
		}

		public override Type NodeDataType {
			get {
				return typeof(ITaskData);
			}
		}
		
		public override Type CommandHandlerType {
			get {
				return typeof(IntegerNodeCommandHandler);
			}
		}

	}
	
	public class IntegerNodeCommandHandler : NodeCommandHandler
	{
		LogUtil log;
		public IntegerNodeCommandHandler()
		{
			log = new LogUtil("IntegerNodeCommandHandler");
		}
		
		public override DragOperation CanDragNode ()
		{
			return DragOperation.Move;
		}
		
		public override bool CanDropNode (object dataObject, MonoDevelop.Ide.Gui.Components.DragOperation operation)
		{
			IntTaskData self = this.CurrentNode.DataItem as IntTaskData;
			// assume that the object is an IntTaskData
			IntTaskData newChild = (IntTaskData)dataObject;
			
			// makes log very verbose, disabled
			//log.DEBUG("CurrentNODE is: " + self.data.ToString());
			//log.DEBUG("DraggedNODE is: " + newChild.data.ToString());			
			
			if(((ITaskData)dataObject).GetTaskType() == TaskType.IntTask)
			{
				return true;				
			}
			return false;
		}
		
		public override void OnNodeDrop (object dataObjects, MonoDevelop.Ide.Gui.Components.DragOperation operation)
		{
			
			IntTaskData self = this.CurrentNode.DataItem as IntTaskData;
			// assume that the object is an IntTaskData
			IntTaskData newChild = dataObjects as IntTaskData;
			log.DEBUG("The current data is: " + self.data.ToString());
			log.DEBUG("Dropping node with data: " + newChild.data.ToString());
			// add this to the list of my children

			// tell the parent to remove this child.
			// not a root node
			IntTaskData oldParent = null;
			if(newChild.parent != null)
			{
				newChild.parent.children.Remove(newChild);
				// tell the parent to fire the changed event
				oldParent = newChild.parent;
			}
			log.DEBUG("Parent currently is:" + newChild.parent.ToString());
			newChild.parent = self;
			log.DEBUG("Parent now is:" + newChild.parent.ToString());
			self.children.Add(newChild);
			
			log.DEBUG("Triggering change in current node");
			self.TriggerChange();
			
			if(oldParent != null)
			{
				log.DEBUG("Triggering change in dropped node");
				oldParent.TriggerChange();
			}
			
			
			
			
			base.OnNodeDrop(dataObjects, operation);
			
		}
		
		public override void RenameItem (string newName)
		{
			//base.RenameItem (newName);
		}

		public override void OnItemSelected ()
		{
			IntTaskData self = this.CurrentNode.DataItem as IntTaskData;
			log.DEBUG( self.data.ToString() + " Selected!");
		}
		
		public override void ActivateItem ()
		{
			IntTaskData myData = this.CurrentNode.DataItem as IntTaskData;
			TaskViewContent taskView = new TaskViewContent();
			taskView.SetTaskIntData(myData.data);
			
			MonoDevelop.Ide.Gui.IdeApp.Workbench.OpenDocument(taskView, true);
		}


		
		public override bool CanDeleteItem ()
		{
			return true;
		}
		
		public override void DeleteItem ()
		{
			// unparent
			IntTaskData self = this.CurrentNode.DataItem as IntTaskData;
			self.parent.children.Remove(self);
			
			// dispose?			
		}


	}
}
