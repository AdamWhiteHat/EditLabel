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
		protected bool IsEditing { get; set; }
        protected string _save { get; set; }

        #endregion

        #region Constructors and Initializer

        public EditLabel() : this("editLabel")
        {
			
            this.Text = this.Name;
        }
        public EditLabel(string Text)
        {
            InitializeComponent();
            this.Text = Text;	
			this.AutoValidate = AutoValidate.EnableAllowFocusChange;
			base.BorderStyle = BorderStyle.None;
            Initalize_EditAction();
        }

        void Initalize_EditAction()	// TextBox edit events
        {
            // Begin editing
            ctrlLabel.DoubleClick += EditLabel_OnDoubleClick;

            // End editing events
            ctrlTextBox.KeyDown += EditLabel_OnKeyPress;
            ctrlTextBox.LostFocus += EditLabel_EndEditing;
            ctrlTextBox.Leave += EditLabel_EndEditing;

            this.Invalidated += EditLabel_EndEditing;
            this.Leave += EditLabel_EndEditing;

            ctrlTextBox.Click += delegate (object s, EventArgs e) { ((TextBox)s).Focus(); };
            ctrlLabel.Click += delegate (object s, EventArgs e) { ((Label)s).Focus(); };
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


		//public new event EventHandler TextChanged
		//{
		//	add { _onTextChanged += value; }
		//	remove { _onTextChanged -= value; }
		//}

		//private EventHandler _onTextChanged;

		#endregion

		#region Internal Event Handlers

		void ChangeText(string Text)
        {
            ctrlLabel.Text = Text;
			base.Text = Text;
            EditResizeTextbox();

			// Fire TextChanged event
			OnChangedText(EventArgs.Empty);
			//if (_onTextChanged != null)
			//{
			//    _onTextChanged.Invoke(this, new EventArgs());
			//}
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

		void EditLabel_OnDoubleClick(object sender, EventArgs e)
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
            if (!IsEditing)
            {
                EditToggle();

                _save = ctrlLabel.Text;
                ctrlTextBox.Text = ctrlLabel.Text;

                EditResizeTextbox();
                ctrlTextBox.Focus();
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
                    ChangeText(_save);
            }
        }

        void EditLabel_EndEditing(object sender, EventArgs e)
        {
            EditEnd();
        }

        void EditLabel_OnKeyPress(object sender, KeyEventArgs e)
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
