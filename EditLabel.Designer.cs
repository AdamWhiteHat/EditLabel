namespace EditLabelControl
{
    partial class EditLabel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }			
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.ctrlLabel = new System.Windows.Forms.Label();
			this.ctrlTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ctrlLabel
			// 
			this.ctrlLabel.AutoSize = true;
			this.ctrlLabel.Location = new System.Drawing.Point(0, 0);
			this.ctrlLabel.Margin = new System.Windows.Forms.Padding(0);
			this.ctrlLabel.MinimumSize = new System.Drawing.Size(15, 0);
			this.ctrlLabel.Name = "ctrlLabel";
			this.ctrlLabel.Size = new System.Drawing.Size(50, 13);
			this.ctrlLabel.TabIndex = 3;
			this.ctrlLabel.Text = "editLabel";
			this.ctrlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctrlTextBox
			// 
			this.ctrlTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.ctrlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ctrlTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ctrlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrlTextBox.Location = new System.Drawing.Point(0, 0);
			this.ctrlTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.ctrlTextBox.MinimumSize = new System.Drawing.Size(15, 0);
			this.ctrlTextBox.Name = "ctrlTextBox";
			this.ctrlTextBox.Size = new System.Drawing.Size(50, 13);
			this.ctrlTextBox.TabIndex = 2;
			this.ctrlTextBox.Text = "editLabel";
			this.ctrlTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ctrlTextBox.Visible = false;
			this.ctrlTextBox.WordWrap = false;
			// 
			// EditLabel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.ctrlLabel);
			this.Controls.Add(this.ctrlTextBox);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 13);
			this.Name = "EditLabel";
			this.Size = new System.Drawing.Size(50, 13);
			this.AutoSizeChanged += new System.EventHandler(this.EditLabel_AutoSizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ctrlLabel;
        private System.Windows.Forms.TextBox ctrlTextBox;
    }
}
