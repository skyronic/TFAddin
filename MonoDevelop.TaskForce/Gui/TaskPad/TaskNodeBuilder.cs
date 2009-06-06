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

// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4918
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.TaskForce.Data;
using MonoDevelop.TaskForce.Utilities;


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
			{			log.DEBUG("Building child nodes");

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
			{log.DEBUG("Building a node");
				TaskData taskData = dataObject as TaskData;
				
				label = taskData.data["label"] as String;
				
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
				return (dataObject as NodeData).data["name"] as string;
			}
			return "";
        }
		

    }

    public class TaskNodeCommandHandler : TaskPadNodeCommandHandler
    {
		public TaskNodeCommandHandler()
		{
		}
		
		public override void OnNodeChange ()
		{
			throw new System.NotImplementedException ();
		}

    }

}

