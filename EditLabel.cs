using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace EditLabelControl
{
	//ParentControlDesigner
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design", typeof(IDesigner))]
	[DefaultEvent("TextChanged")]
	[DefaultProperty("Text")]
	public partial class EditLabel : UserControl
    {
        #region Properties
        // Public Properties

		[Category("Behavior")]
		[Description("Determines whether the control is allowed to be edited.")]
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool IsEditable
		{
			get { return _isEditable; }
			set { if (value == false && IsEditing) { EditEnd(); } _isEditable = value; }
		}
		private bool _isEditable = true;

        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return ctrlLabel.Text; }
            set { ChangeText(value); }
        }

		[Category("Appearance")]
		[Description("Determines the position of the text within the control.")]
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ContentAlignment TextAlign
		{
			get { return ctrlLabel.TextAlign; }
			set { ctrlLabel.TextAlign = value; }
		}
		
		// Protected Properties
		protected string save { get; set; }

		protected bool IsEditing
		{
			get { return _isEditing; }
			set { lock (_lockIsEditing) { _isEditing = value; } }
		}
		private volatile bool _isEditing = false;
		private object _lockIsEditing = new object();
        

		// Private
		private Form owner;

        #endregion

        #region Constructors and Initializer

		public EditLabel()
			: this(null, "editLabel")
		{
			base.BorderStyle = BorderStyle.None;
			this.IsEditable = true;
		}

        public EditLabel(Form Owner) : this(Owner, "editLabel")
        {
        }

        public EditLabel(Form Owner, string Text)
        {
            InitializeComponent();
            this.Text = Text;	
			this.AutoValidate = AutoValidate.EnableAllowFocusChange;
			base.BorderStyle = BorderStyle.None;
			this.IsEditable = true;
            Initalize_EditAction();
			this.owner = Owner;
			owner.Click += EndEditing;
        }

        void Initalize_EditAction()	// TextBox edit events
        {
            ctrlLabel.DoubleClick += BeginEditing;

            ctrlTextBox.LostFocus += EndEditing;
            ctrlTextBox.Leave += EndEditing;

            ctrlTextBox.Click += delegate (object s, EventArgs e) { ((TextBox)s).Focus(); };
            ctrlLabel.Click += delegate (object s, EventArgs e) { ((Label)s).Focus(); };

			ctrlTextBox.KeyDown += OnKeyPress;
        }

		#endregion

		#region Public Events

		[Category("Property Changed")]
		[Description("Event raised when the value of the Text property is changed on Control.")]
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new event EventHandler TextChanged;

		protected virtual void OnChangedText(EventArgs e)
		{
			EventHandler handler = TextChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion

		#region Internal Event Handlers

		void ChangeText(string Text)
        {
            ctrlLabel.Text = Text;
			base.Text = Text;
            EditResizeTextbox();

			// Fire TextChanged event
			OnChangedText(EventArgs.Empty);
		}

        void EditResizeTextbox()
        {
            ctrlTextBox.Size = ctrlLabel.Size;
        }

		void EditLabel_AutoSizeChanged(object sender, EventArgs e)
		{
			if (this.AutoSize == false)
			{
				ctrlLabel.AutoSize = false;
				ctrlLabel.Dock = DockStyle.Fill;
				ctrlTextBox.Dock = DockStyle.Fill;
			}
			else
			{
				ctrlLabel.AutoSize = true;
				ctrlLabel.Dock = DockStyle.None;
				ctrlTextBox.Dock = DockStyle.None;
			}
		}

		void BeginEditing(object sender, EventArgs e)
        {			
			EditBegin();			         
        }

        void EditToggle()
        {
            IsEditing = !IsEditing; // IsEditing boolean
            ctrlLabel.Visible = !ctrlLabel.Visible; // Label visibility
            ctrlTextBox.Visible = !ctrlTextBox.Visible; // TextBox visibility
        }

        void EditBegin()
        {
			if (IsEditable)
			{
				if (!IsEditing)
				{
					EditToggle();

					save = ctrlLabel.Text;
					ctrlTextBox.Text = ctrlLabel.Text;

					EditResizeTextbox();
					ctrlTextBox.Focus();
				}
			}
        }

        void EditEnd(bool AcceptChanges = true)
        {
            if (IsEditing)
            {
                EditToggle();

                if (AcceptChanges)
                    ChangeText(ctrlTextBox.Text);
                else
                    ChangeText(save);
            }
        }

        public void EndEditing(object sender, EventArgs e)
        {
            EditEnd();
        }

        void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (IsEditing)
            {
                switch (e.KeyData)
                {
                    case Keys.Escape:
                        EditEnd(false);
                        break;

                    case Keys.Return:
                        EditEnd();
                        break;

                    default:
                        ChangeText(ctrlTextBox.Text);
                        break;
                }
            }
        }

		#endregion

	}
}
