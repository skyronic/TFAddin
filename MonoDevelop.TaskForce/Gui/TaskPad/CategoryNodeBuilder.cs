// 
// CategoryNodeBuilder.cs
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
using MonoDevelop.Ide.Commands;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Components.Commands;
using MonoDevelop.TaskForce.Providers;
using Mono.Addins;
using MonoDevelop.TaskForce.Utilities;


namespace MonoDevelop.TaskForce.Gui.TaskPad
{
	
	
	public class CategoryNodeBuilder : TaskPadNodeBuilder
	{
		LogUtil log;
		public CategoryNodeBuilder()
		{
			log = new LogUtil("CategoryNodeBuilder");
			log.SetHash(this);
			
			
		}
		
		
		public override Type CommandHandlerType
        {
            get
            {
                return typeof(CategoryNodeCommandHandler);
            }
        }

        public override Type NodeDataType
        {
            get
            {
                return typeof(CategoryData);
            }
        }

        public override void BuildChildNodes (ITreeBuilder treeBuilder, object dataObject)
        {
			base.BuildChildNodes (treeBuilder, dataObject);
			
			if (dataObject is CategoryData)
			{			log.DEBUG("Building child nodes");

				CategoryData categoryData = dataObject as CategoryData;
				
				foreach (object child in categoryData.children)
				{
					treeBuilder.AddChild (child);
				}
			}
        }


        public override void BuildNode (ITreeBuilder treeBuilder, object dataObject, ref string label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
        {
			// convert the data object to a nodedata
			if (dataObject is CategoryData)
			{log.DEBUG("Building a node");
				CategoryData categoryData = dataObject as CategoryData;
				
				label = categoryData.Label;
				
				// TODO: Change the silly icons
				icon = Context.GetIcon(Gtk.Stock.Apply);
				closedIcon = Context.GetIcon(Gtk.Stock.Clear);
			}
			
			
			
        }

		public override void OnNodeDataChanged (NodeData source, NodeDataChangedEventArgs args)
		{
			if (source is CategoryData)
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
	
	public class CategoryNodeCommandHandler : TaskPadNodeCommandHandler
	{
		public override void OnNodeChange ()
		{
			throw new System.NotImplementedException ();
		}
		
		[CommandHandler(ContextMenuCommands.NewCategory)]
		public void OnNewCategoryContextMenu ()
		{
			if(this.CurrentNode.DataItem is CategoryData)
			{
				CategoryData self = this.CurrentNode.DataItem as CategoryData;
				
				// call the new category view
				self.provider.AddChildCategory(self);
			}
		}
	}
}
