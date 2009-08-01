// 
// DocumentMemento.cs
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
using MonoDevelop.Core.Serialization;


namespace MonoDevelop.TaskForce.Context
{

	/// <summary>
	/// Captures a memento from a document and allows restoring it
	/// </summary>
	public class DocumentMemento
	{
		LogUtil log;
		
		[ItemProperty]
		public string FileName {
			get;
			set;
		}

		[ItemProperty]
		public int CursorLine {
			get;
			set;
		}

		[ItemProperty]
		public int CursorColumn {
			get;
			set;
		}

		public void CaptureMemento (Document document)
		{

			FileName = document.FileName;
			// File name
			CursorLine = document.TextEditor.CursorLine;
			CursorColumn = document.TextEditor.CursorColumn;
			log.INFO (String.Format ("Captured Memento - {0} - {1}:{2}", FileName, CursorLine, CursorColumn));
			//document.Close ();
		}

		public void RestoreMemento ()
		{
			IdeApp.Workbench.OpenDocument (FileName, CursorLine, CursorColumn, false);
		}

		/// <summary>
		/// Check whether a particular document can be captured 
		/// </summary>
		/// <param name="document">
		/// A <see cref="Document"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>		
		public static bool CanCaptureDocument (Document document)
		{
			LogUtil log = new LogUtil ("CanCaptureDocument");
			if (document == null) {
				log.ERROR ("Document is null");
				return false;
			}

			if (document.FileName == null) {
				log.ERROR ("Filename is null");
				return false;
			}
			log.INFO (String.Format ("Document - {0} + {1} + {2}", document.Name, document.IsFile, document.TextEditor != null));
			if (document.HasProject && document.IsFile && document.TextEditor != null) {
				log.INFO ("Capture possible");
				return true;
			}

			log.DEBUG ("Capture not possible");
			return false;
		}
		public DocumentMemento ()
		{
			log = new LogUtil ("DocumentMemento");
			log.SetHash (this);
		}
	}
}
