using System;
using Gtk;
using MonoDevelop.TaskForce.Gui.Components;
using System.Collections.Generic;
using MonoDevelop.TaskForce.Utilities;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		CreateWidgetSandbox();
		Build ();
	}
		protected CommentWidget commentWidget;
		List<CommentData> comments = new List<CommentData>();	
	protected void CreateWidgetSandbox()
	{
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
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}