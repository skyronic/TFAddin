// 
// DocumentStore.cs
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
using MonoDevelop.TaskForce.Utilities;
using MonoDevelop.Ide.Gui;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace MonoDevelop.TaskForce.Context
{


	public class DocumentStore
	{
		List<DocumentMemento> Documents;

		/// <summary>
		/// Captures the mementoes and closes all the documents as well
		/// </summary>
		public void CaptureMemento ()
		{
			// TODO: sanity check before clearing current context
			Documents.Clear ();

			ReadOnlyCollection<Document> openDocuments = IdeApp.Workbench.Documents;

			foreach (Document doc in openDocuments) {
				if (DocumentMemento.CanCaptureDocument (doc)) {
					// Capture the memento and close it
					DocumentMemento dm = new DocumentMemento ();
					dm.CaptureMemento (doc);

					Documents.Add (dm);
				}
			}
		}

		/// <summary>
		/// Restore all the documents associated with this task
		/// </summary>
		public void RestoreMemento ()
		{
			foreach (DocumentMemento dm in Documents) {
				dm.RestoreMemento ();
			}
		}

		public DocumentStore ()
		{
			Documents = new List<DocumentMemento> ();
		}
	}
}
