using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace EditLabelControl
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class EditLabel : UserControl
    {
        #region Properties
        // Public Properties
        public override string Text
        {
            get { return ctrlLabel.Text; }
            set { ChangeText(value); }
        }

        // Protected Properties
        protected bool isEditing { get; set; }
        protected string _save { get; set; }

        #endregion

        #region Constructors and Initializer

        public EditLabel() : this("editLabel1") { }
        public EditLabel(string Text)
        {
            InitializeComponent();
            this.Text = Text;
            Initalize_EditAction();
        }

        void Initalize_EditAction()	// TextBox edit events
        {
            // Begin editing
            ctrlLabel.DoubleClick += EditLabel_OnDoubleClick;

            // End editing events
            ctrlTextBox.KeyDown += EditLabel_OnKeyPress;
            ctrlTextBox.LostFocus += EditLabel_EndEditing;
            Invalidated += EditLabel_EndEditing;
            Leave += EditLabel_EndEditing;

            ctrlTextBox.Click += delegate(object s, EventArgs e) { ((TextBox)s).Focus(); };
            ctrlLabel.Click += delegate(object s, EventArgs e) { ((Label)s).Focus(); };
        }

        #endregion

        #region Event Handlers

        void ChangeText(string Text)
        {
            ctrlLabel.Text = Text;
            EditResizeTextbox();
        }

        void EditResizeTextbox()
        {
            ctrlTextBox.Size = ctrlLabel.Size;
        }

        void EditLabel_OnDoubleClick(object sender, EventArgs e)
        {
            EditBegin();
        }

        void EditToggle()
        {
            isEditing = !isEditing; // IsEditing boolean
            ctrlLabel.Visible = !ctrlLabel.Visible; // Label visibility
            ctrlTextBox.Visible = !ctrlTextBox.Visible; // TextBox visibility
        }

        void EditBegin()
        {
            if (!isEditing)
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
            if (isEditing)
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
            if (isEditing)
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
