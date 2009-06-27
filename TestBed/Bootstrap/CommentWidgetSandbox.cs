
using System;
using MonoDevelop.TaskForce.Gui.Components;
using System.Collections.Generic;
using MonoDevelop.TaskForce.Utilities;
namespace TestBed
{
		
	
	public partial class CommentWidgetSandbox : Gtk.Window
	{
		protected CommentWidget commentWidget;
		List<CommentData> comments = new List<CommentData>();
		
		public CommentWidgetSandbox() : 
				base(Gtk.WindowType.Toplevel)
		{
			
			/*
			 * Testing the comments using the seed function. do not use in production
			 */
			comments.Add(new CommentData("mhutch", 45));
			comments.Add(new CommentData("lluis", 34));
			comments.Add(new CommentData("Sandy", 12));
			comments.Add(new CommentData("mkestner", 36));
			comments.Add(new CommentData("antileet",2));
			LogUtil log = new LogUtil("CommentWidgetSandbox");
			
			log.WARN("Creating the widget");
			commentWidget = new CommentWidget(comments);
			log.INFO("Widget creation complete");
			commentWidget.ShowAll();
			
			
			this.Add(commentWidget);
			
			this.Title = "Comment widget sandbox bootstrap";
			this.Build();
		}
	}
}
