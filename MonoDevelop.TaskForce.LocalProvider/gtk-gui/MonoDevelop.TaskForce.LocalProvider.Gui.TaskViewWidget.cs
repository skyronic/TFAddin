// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.TaskForce.LocalProvider.Gui {
    
    
    public partial class TaskViewWidget {
        
        private Gtk.ScrolledWindow scrolledwindow1;
        
        private Gtk.VBox vbox1;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Button button1;
        
        private Gtk.Table table1;
        
        private MonoDevelop.TaskForce.Gui.Components.CommentWidget2 commentwidget21;
        
        private Gtk.ScrolledWindow GtkScrolledWindow1;
        
        private Gtk.TextView descriptionTextView;
        
        private Gtk.Label label1;
        
        private Gtk.Label label2;
        
        private Gtk.Label label3;
        
        private Gtk.Label label4;
        
        private Gtk.Entry nameEntry;
        
        private Gtk.ComboBox priorityCombo;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.TaskForce.LocalProvider.Gui.TaskViewWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "MonoDevelop.TaskForce.LocalProvider.Gui.TaskViewWidget";
            // Container child MonoDevelop.TaskForce.LocalProvider.Gui.TaskViewWidget.Gtk.Container+ContainerChild
            this.scrolledwindow1 = new Gtk.ScrolledWindow();
            this.scrolledwindow1.CanFocus = true;
            this.scrolledwindow1.Name = "scrolledwindow1";
            this.scrolledwindow1.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow1.Gtk.Container+ContainerChild
            Gtk.Viewport w1 = new Gtk.Viewport();
            w1.ShadowType = ((Gtk.ShadowType)(0));
            // Container child GtkViewport.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            // Container child vbox1.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.button1 = new Gtk.Button();
            this.button1.CanFocus = true;
            this.button1.Name = "button1";
            this.button1.UseUnderline = true;
            // Container child button1.Gtk.Container+ContainerChild
            Gtk.Alignment w2 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w3 = new Gtk.HBox();
            w3.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w4 = new Gtk.Image();
            w4.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-yes", Gtk.IconSize.Button, 20);
            w3.Add(w4);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w6 = new Gtk.Label();
            w6.LabelProp = Mono.Unix.Catalog.GetString("Activate Task");
            w6.UseUnderline = true;
            w3.Add(w6);
            w2.Add(w3);
            this.button1.Add(w2);
            this.hbox1.Add(this.button1);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.hbox1[this.button1]));
            w10.Position = 1;
            w10.Expand = false;
            w10.Fill = false;
            this.vbox1.Add(this.hbox1);
            Gtk.Box.BoxChild w11 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
            w11.Position = 0;
            w11.Expand = false;
            w11.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.table1 = new Gtk.Table(((uint)(6)), ((uint)(2)), false);
            this.table1.Name = "table1";
            this.table1.RowSpacing = ((uint)(6));
            this.table1.ColumnSpacing = ((uint)(6));
            // Container child table1.Gtk.Table+TableChild
            this.commentwidget21 = new MonoDevelop.TaskForce.Gui.Components.CommentWidget2();
            this.commentwidget21.Events = ((Gdk.EventMask)(256));
            this.commentwidget21.Name = "commentwidget21";
            this.table1.Add(this.commentwidget21);
            Gtk.Table.TableChild w12 = ((Gtk.Table.TableChild)(this.table1[this.commentwidget21]));
            w12.TopAttach = ((uint)(4));
            w12.BottomAttach = ((uint)(5));
            w12.LeftAttach = ((uint)(1));
            w12.RightAttach = ((uint)(2));
            w12.XOptions = ((Gtk.AttachOptions)(4));
            w12.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.GtkScrolledWindow1 = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
            this.GtkScrolledWindow1.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
            this.descriptionTextView = new Gtk.TextView();
            this.descriptionTextView.CanFocus = true;
            this.descriptionTextView.Name = "descriptionTextView";
            this.GtkScrolledWindow1.Add(this.descriptionTextView);
            this.table1.Add(this.GtkScrolledWindow1);
            Gtk.Table.TableChild w14 = ((Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow1]));
            w14.TopAttach = ((uint)(1));
            w14.BottomAttach = ((uint)(2));
            w14.LeftAttach = ((uint)(1));
            w14.RightAttach = ((uint)(2));
            w14.XOptions = ((Gtk.AttachOptions)(4));
            w14.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Name");
            this.table1.Add(this.label1);
            Gtk.Table.TableChild w15 = ((Gtk.Table.TableChild)(this.table1[this.label1]));
            w15.XOptions = ((Gtk.AttachOptions)(4));
            w15.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Description");
            this.table1.Add(this.label2);
            Gtk.Table.TableChild w16 = ((Gtk.Table.TableChild)(this.table1[this.label2]));
            w16.TopAttach = ((uint)(1));
            w16.BottomAttach = ((uint)(2));
            w16.XOptions = ((Gtk.AttachOptions)(4));
            w16.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("Priority");
            this.table1.Add(this.label3);
            Gtk.Table.TableChild w17 = ((Gtk.Table.TableChild)(this.table1[this.label3]));
            w17.TopAttach = ((uint)(2));
            w17.BottomAttach = ((uint)(3));
            w17.XOptions = ((Gtk.AttachOptions)(4));
            w17.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.Xpad = 35;
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("Comments");
            this.table1.Add(this.label4);
            Gtk.Table.TableChild w18 = ((Gtk.Table.TableChild)(this.table1[this.label4]));
            w18.TopAttach = ((uint)(4));
            w18.BottomAttach = ((uint)(5));
            w18.XOptions = ((Gtk.AttachOptions)(4));
            w18.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.nameEntry = new Gtk.Entry();
            this.nameEntry.CanFocus = true;
            this.nameEntry.Name = "nameEntry";
            this.nameEntry.IsEditable = true;
            this.nameEntry.InvisibleChar = '•';
            this.table1.Add(this.nameEntry);
            Gtk.Table.TableChild w19 = ((Gtk.Table.TableChild)(this.table1[this.nameEntry]));
            w19.LeftAttach = ((uint)(1));
            w19.RightAttach = ((uint)(2));
            w19.XOptions = ((Gtk.AttachOptions)(4));
            w19.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.priorityCombo = Gtk.ComboBox.NewText();
            this.priorityCombo.AppendText(Mono.Unix.Catalog.GetString("Minor"));
            this.priorityCombo.AppendText(Mono.Unix.Catalog.GetString("Normal"));
            this.priorityCombo.AppendText(Mono.Unix.Catalog.GetString("Major"));
            this.priorityCombo.AppendText(Mono.Unix.Catalog.GetString("Urgent"));
            this.priorityCombo.AppendText(Mono.Unix.Catalog.GetString("Critical"));
            this.priorityCombo.AppendText("");
            this.priorityCombo.Name = "priorityCombo";
            this.table1.Add(this.priorityCombo);
            Gtk.Table.TableChild w20 = ((Gtk.Table.TableChild)(this.table1[this.priorityCombo]));
            w20.TopAttach = ((uint)(2));
            w20.BottomAttach = ((uint)(3));
            w20.LeftAttach = ((uint)(1));
            w20.RightAttach = ((uint)(2));
            w20.YOptions = ((Gtk.AttachOptions)(4));
            this.vbox1.Add(this.table1);
            Gtk.Box.BoxChild w21 = ((Gtk.Box.BoxChild)(this.vbox1[this.table1]));
            w21.Position = 1;
            w1.Add(this.vbox1);
            this.scrolledwindow1.Add(w1);
            this.Add(this.scrolledwindow1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Hide();
        }
    }
}
