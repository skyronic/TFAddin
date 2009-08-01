// 
// ProviderNodeBuilder.cs
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
	
	
	public class ProviderNodeBuilder : TaskPadNodeBuilder
	{
		
		public ProviderNodeBuilder() : base()
		{
			log = new MonoDevelop.TaskForce.Utilities.LogUtil("ProviderNodeBuilder");
		}
		
        public override Type CommandHandlerType
        {
            get
            {
                return typeof(ProviderNodeCommandHandler);
            }
        }

        public override Type NodeDataType
        {
            get
            {
                return typeof(ProviderData);
            }
        }

        public override void BuildChildNodes (ITreeBuilder treeBuilder, object dataObject)
        {
			log.DEBUG("building provider's child nodes");
			base.BuildChildNodes (treeBuilder, dataObject);
			if (dataObject is ProviderData)
			{
				ProviderData providerData = dataObject as ProviderData;
				
				foreach (object child in providerData.children)
				{
					treeBuilder.AddChild (child);
				}
			}
        }


        public override void BuildNode (ITreeBuilder treeBuilder, object dataObject, ref string _label, ref Gdk.Pixbuf icon, ref Gdk.Pixbuf closedIcon)
        {
			// convert the data object to a nodedata
			if (dataObject is ProviderData)
			{
				log.DEBUG("Building provider node");
				log.SetHash(dataObject);
				ProviderData providerData = dataObject as ProviderData;
				
				_label = providerData.Label;
				// TODO: Change the silly icons
				icon = Context.GetIcon(Gtk.Stock.Cdrom);
				
				
				closedIcon = Context.GetIcon(Gtk.Stock.Close);
			}
			
        }

		public override void OnNodeDataChanged (MonoDevelop.TaskForce.Data.NodeData source, MonoDevelop.TaskForce.Data.NodeDataChangedEventArgs args)
		{
			if (source is ProviderData)
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
	
	public class ProviderNodeCommandHandler : TaskPadNodeCommandHandler
	{
		public ProviderNodeCommandHandler()
		{
			
		}
		
		[CommandHandler(ContextMenuCommands.AddTask)]
		public void OnAddTaskContextMenu ()			
		{
			if(this.CurrentNode.DataItem is ProviderData) // always be sure
			{
				ProviderData self = this.CurrentNode.DataItem as ProviderData;				
				
				self.provider.NewTask(self);				
			}			
		}
		
		[CommandHandler(ContextMenuCommands.NewCategory)]
		public void OnNewCategoryContextMenu ()
		{
			LogUtil log = new LogUtil("ProviderNodeCommandHandler");
			log.DEBUG("Creating a new category");
			if(this.CurrentNode.DataItem is ProviderData)
			{
				ProviderData self = this.CurrentNode.DataItem as ProviderData;
				
				// call the new category view
				self.provider.CreateNewCategory();
			}
		}
		[CommandHandler(ContextMenuCommands.Trigger1)]
		public void OnTrigger1Clicked()
		{
			
		}
		
		public override void OnNodeChange ()
		{
			
		}

	}
}
