namespace Loan.Application.Infrastructure.Forms.Popups
{
    partial class PopupButtons
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // grpBox
            // 
            this.grpBox.Location = new System.Drawing.Point(1, -3);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(222, 28);
            this.grpBox.TabIndex = 1;
            this.grpBox.TabStop = false;
            // 
            // PopupButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 22);
            this.ControlBox = false;
            this.Controls.Add(this.grpBox);
            this.Name = "PopupButtons";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpBox;
    }
}