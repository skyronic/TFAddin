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
using System.Collections.Generic;
using MonoDevelop.Core.Serialization;
using System.Xml;
using System.IO;

namespace MonoDevelop.TaskForce.Data
{
	public delegate void NodeDataChangedHandler (NodeData source, NodeDataChangedEventArgs args);

	public abstract class NodeData : IDisposable
	{

		// The provider object (moved from providerdata to here)
		[ItemProperty()]
		public IProvider provider;

		public NodeData ()
		{
			children = new List<NodeData> ();
			data = new Hashtable ();
			parent = null;
			log = new LogUtil ("NodeData");
		}

		[ItemProperty()]
		public string Label {
			get;
			set;
		}


		[ItemProperty()]
		public ICoreData CoreDataObject {
			get;
			set;
		}

		public string CoreDataSerializationString {
			get;
			set;
		}

		public abstract NodeType nodeType {
			get;
		}

		public Gdk.Pixbuf OpenIcon {
			get;
			set;
		}

		Gdk.Pixbuf ClosedIcon {
			get;
			set;
		}

		public NodeData parent;

		public string serializedString;

		// The children of the current node.
		[ItemProperty()]
		public List<NodeData> children;


		protected LogUtil log;
		public Hashtable data;
		// Contains all the data
		public abstract bool CanMakeChild (NodeType childType);
		public abstract bool CanMakeChild (NodeData childData);

		public virtual void AddChild (NodeData childData)
		{
			// Check if this node can have this as a child
			if (CanMakeChild (childData)) {
				// First, remove the child from it's parent list
				NodeData oldParent = childData.parent;

				if (oldParent != null)
					childData.parent.children.Remove (childData);

				// set the child data's parent to the current object
				childData.parent = this;

				// add the child data as a child
				this.children.Add (childData);

				// Inform both classes about the updates
				this.TriggerUpdate ();

				if (oldParent != null)
					oldParent.TriggerUpdate ();

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
		public virtual void AddChildSilent (NodeData childData)
		{
			// Check if this node can have this as a child
			if (CanMakeChild (childData)) {
				// First, remove the child from it's parent list
				NodeData oldParent = childData.parent;

				if (oldParent != null)
					childData.parent.children.Remove (childData);

				// set the child data's parent to the current object
				childData.parent = this;

				// add the child data as a child
				this.children.Add (childData);

				// Inform both classes about the updates
				//this.TriggerUpdate();

			}
		}

		/// <summary>
		/// Triggered when a node is changed
		/// </summary>
		public event NodeDataChangedHandler NodeDataChanged;

		public virtual void TriggerUpdate ()
		{
			// Todo: Add args somewhere?

			NodeDataChangedEventArgs args = new NodeDataChangedEventArgs ();
			NodeDataChanged (this, args);
		}

		public virtual void SerializeData ()
		{
			PreSerializeHook ();

			// Serialize the current object
			serializedString = Util.SerializeObjectToString (this);

		}


		/// <summary>
		/// this does any generic NodeData post serialization 
		/// </summary>
		public virtual void PostSerializeHook ()
		{

		}

		public virtual void PostDeserializeHook ()
		{
			// Re-enable anything that needs to be done in a constructor normally
			log = new LogUtil ("NodeData");

			// Deserialize the CoreDataString into the CoreDataObject
			//CoreDataObject.DeSerialize(CoreDataSerializationString);

			// Free up the memory contained in the CoreDataSerializedString
			//CoreDataSerializationString = null;


			// if there are no children available, make sure that there's a new object for children
			if (children == null) {
				children = new List<NodeData> ();
			} else {
				foreach (NodeData child in children) {
					child.parent = this;

					// IMPORTANT: this assumes that the provider has been set already
					child.provider = this.provider;

					child.PostDeserializeHook ();
				}
			}
		}


		/// <summary>
		/// Here, we will do all the actions required before serialization
		/// is to take place. 
		/// </summary>
		public virtual void PreSerializeHook ()
		{
			log.LOG ("Performing pre serialization hook");
			// First, serialize the CoreData into a string
			//CoreDataSerializationString = CoreDataObject.SerializeToXML();

			// call the Pre serialization hooks for all it's children so that they 
			// make sure to do their required parts
			foreach (NodeData node in children) {
				node.PreSerializeHook ();
			}
		}

		public virtual void DeserializeData ()
		{

		}

		public void Dispose ()
		{
			// TODO: how?
		}


	}

	public class NodeDataChangedEventArgs
	{
		public Hashtable argData;
		public NodeDataChangedEventArgs ()
		{

		}
	}
}
