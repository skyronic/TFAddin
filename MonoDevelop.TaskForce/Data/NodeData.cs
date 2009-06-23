// 
// NodeDataSkeleton.cs
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
using MonoDevelop.TaskForce.Utilities;
using System.Collections;
using System.Text;
using MonoDevelop.TaskForce.Providers;

namespace MonoDevelop.TaskForce.Data
{
	public delegate void NodeDataChangedHandler(NodeData source, NodeDataChangedEventArgs args);
	
	public abstract class NodeData
	{
		
		// The provider object (moved from providerdata to here)
		public IProvider provider;
		
		public string temp;
		
		public NodeData()
		{
			children = new ArrayList();
			data = new Hashtable();
			parent = null;
		}
		
		string label;
		public string Label
		{
			get
			{
				return label;
			}
			set
			{
				label = value;
			}
		}		
		
		public abstract NodeType nodeType
		{
			get;
		}
		
		Gdk.Pixbuf open_icon;
		public Gdk.Pixbuf OpenIcon
		{
			get
			{
				return open_icon;
			}
			set
			{
				open_icon = value;
			}
		}
		
		Gdk.Pixbuf closed_icon;
		public Gdk.Pixbuf ClosedIcon
		{
			get
			{
				return closed_icon;
			}
			set
			{
				closed_icon = value;
			}
		}
		
		public NodeData parent;
		public ArrayList children;
		protected LogUtil log;
		public Hashtable data; // Contains all the data
		
		public abstract bool CanMakeChild(NodeType childType);
		public abstract bool CanMakeChild(NodeData childData);
		
		public virtual void AddChild(NodeData childData)
		{
			log.DEBUG("AddChild");
			// Check if this node can have this as a child
			if(CanMakeChild(childData))
			{
				// First, remove the child from it's parent list
				NodeData oldParent = childData.parent;
				
				if(oldParent != null)
					childData.parent.children.Remove(childData);
				
				// set the child data's parent to the current object
				childData.parent = this;
				
				// add the child data as a child
				this.children.Add(childData);
				
				// Inform both classes about the updates
				this.TriggerUpdate();
				
				if(oldParent!=null)
					oldParent.TriggerUpdate();
				
				log.DEBUG("Added a new child:" + childData.ToString());
			}
		}
		
		/// <summary>
		/// Adds a child and does not update GUI. to be used when heavy
		/// tree activity is taking place 
		/// 
		/// YOU HAVE TO CALL TriggerUpdate after finishing tree opeartions
		/// </summary>
		/// <param name="childData">
		/// A <see cref="NodeData"/>
		/// </param>
		public virtual void AddChildSilent(NodeData childData)
		{
			log.DEBUG("AddChild");
			// Check if this node can have this as a child
			if(CanMakeChild(childData))
			{
				// First, remove the child from it's parent list
				NodeData oldParent = childData.parent;
				
				if(oldParent != null)
					childData.parent.children.Remove(childData);
				
				// set the child data's parent to the current object
				childData.parent = this;
				
				// add the child data as a child
				this.children.Add(childData);
				
				// Inform both classes about the updates
				this.TriggerUpdate();
				
			}
		}
		
		/// <summary>
		/// Triggered when a node is changed
		/// </summary>
		public event NodeDataChangedHandler NodeDataChanged;
		
		public virtual void TriggerUpdate()
		{
			// Todo: Add args somewhere?
			
			NodeDataChangedEventArgs args = new NodeDataChangedEventArgs();			
			NodeDataChanged(this, args);
		}
		
		public virtual void SerializeData()
		{
			//StringBuilder resultString;
		}
	}
	
	public class NodeDataChangedEventArgs
	{
		public Hashtable argData;
		public NodeDataChangedEventArgs()
		{
			
		}
	}
}
