
using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.TaskForce.TaskData;
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
			log.DEBUG("Building a node");
			
			ITaskData taskData = (ITaskData)dataObject;
			if(taskData.GetTaskType() == TaskType.IntTask)
			{
				log.DEBUG("Found an integer task type");
				IntTaskData intTaskData = (IntTaskData)taskData;
				
				// Set the label based on the current data
				label = "Node: " + intTaskData.data.ToString();
				
			}
			
		}
		public override bool HasChildNodes (MonoDevelop.Ide.Gui.Components.ITreeBuilder builder, object dataObject)
		{
			
			log.DEBUG("Checking for child nodes");
			
			ITaskData taskData = (ITaskData)dataObject;
			if(taskData.GetTaskType() == TaskType.IntTask)
			{
				log.DEBUG("Found an integer task type");
				IntTaskData intTaskData = (IntTaskData)taskData;
				
				// return true if there are any children
				return(intTaskData.children.Count != 0);
				
			}
			return false;
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
	}
}
