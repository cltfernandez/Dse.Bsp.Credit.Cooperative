namespace Loan.Application.Infrastructure.Forms.Popups
{
    partial class PopupCalendar
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
            this.mclDefault = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // mclDefault
            // 
            this.mclDefault.Location = new System.Drawing.Point(4, 5);
            this.mclDefault.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.mclDefault.Name = "mclDefault";
            this.mclDefault.TabIndex = 1;
            this.mclDefault.DateSelected += mclDefault_DateSelected;
            // 
            // PopupCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 173);
            this.ControlBox = false;
            this.Controls.Add(this.mclDefault);
            this.Name = "PopupCalendar";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MonthCalendar mclDefault;
    }
}