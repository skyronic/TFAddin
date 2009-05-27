
using System;
using MonoDevelop.Ide.Gui.Components;
namespace MonoDevelop.TaskForce.TaskPad
{
	
	
	public class TempNodeBuilder : TypeNodeBuilder
	{
		
		public TempNodeBuilder()
		{
		}
		public override void BuildNode (MonoDevelop.Ide.Gui.Components.ITreeBuilder treeBuilder, object dataObject, ref string label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
		{
			base.BuildNode (treeBuilder, dataObject, ref label, ref icon, ref closedIcon);
			
			label = "Hello there!";
			
		}
		public override void BuildChildNodes (MonoDevelop.Ide.Gui.Components.ITreeBuilder treeBuilder, object dataObject)
		{
			base.BuildChildNodes (treeBuilder, dataObject);
			treeBuilder.AddChild("child Noode");
		}
		public override string GetNodeName (MonoDevelop.Ide.Gui.Components.ITreeNavigator thisNode, object dataObject)
		{
			return "My Name:)";
		}
		public override Type NodeDataType {
			get {
				return typeof(string);
			}
		}


		
	}
}
