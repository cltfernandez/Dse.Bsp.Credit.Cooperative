using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Forms.Windows
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Name == "LoanApplication")
            {
                base.OnPaint(e);
                return;
            }
            
            Rectangle rc;

            if (this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                rc = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

                using (System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(rc, Color.Green, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, rc);
                }
            }
        }

        private void ApplyTheme(Control container)
        {
            int cboFontSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Size.ComboBox"));
            int txtFontSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Size.TextBox"));
            int lblFontSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Size.Label"));
            int chkFontSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Size.CheckBox"));
            int dgvFontSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Size.DataGridView"));

            foreach(Control control in container.Controls)
            {
                if (control.GetType() == typeof(ComboBox))
                {
                    control.Font = ApplyThemeFont(control.Font, cboFontSize);
                }
                else if (control.GetType() == typeof(TextBox))
                {
                    control.Font = ApplyThemeFont(control.Font, txtFontSize);
                }
                else if (control.GetType() == typeof(Label))
                {
                    control.BackColor = Color.Transparent;
                    if (control.Name == "lblTitle")
                    {
                        Label label = (Label)control;
                        label.Font = new System.Drawing.Font("Impact", 22, System.Drawing.FontStyle.Italic, label.Font.Unit, label.Font.GdiCharSet);
                        label.BackColor = Color.Transparent;
                        label.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    }
                    else
                    {
                        control.Font = ApplyThemeFont(control.Font, lblFontSize);
                    }
                }
                else if (control.GetType() == typeof(CheckBox))
                {
                    control.BackColor = Color.Transparent;
                    control.Font = ApplyThemeFont(control.Font, chkFontSize);
                }
                else if (control.GetType() == typeof(Button))
                {
                    RepaintControl(control, Color.Turquoise, Color.Teal);
                }
                else if (control.GetType() == typeof(GroupBox))
                {
                    control.BackColor = Color.Transparent;
                }
                else if (control.GetType() == typeof(UserControl))
                {
                    control.BackColor = Color.Transparent;
                }
                else if (control.GetType() == typeof(DataGridView))
                {
                    ((DataGridView)control).DefaultCellStyle.Font = ApplyThemeFont(control.Font, dgvFontSize);
                    ((DataGridView)control).ColumnHeadersDefaultCellStyle.Font = ApplyThemeFont(control.Font, dgvFontSize);
                }
                else if (control.GetType() == typeof(PropertyGrid))
                {
                    ((PropertyGrid)control).Font = ApplyThemeFont(control.Font, lblFontSize);
                }
                else if (control.GetType() == typeof(TabPage))
                {
                    RepaintControl(control, Color.LightGreen, Color.Turquoise);
                }
                else if (control.GetType().BaseType == typeof(UserControl))
                {
                    control.BackColor = Color.Transparent;
                }
                
                if (control.Controls.Count > 0)
                {
                    ApplyTheme(control);
                }
            }
        }

        private Font ApplyThemeFont(Font font, int size)
        {
            string fontName = System.Configuration.ConfigurationManager.AppSettings.Get("Font.Name.Theme");
            Boolean fontBoldOverride = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("Font.Bold.Override"));
            return (new System.Drawing.Font(fontName, size, System.Drawing.FontStyle.Regular, font.Unit, font.GdiCharSet));
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (this.Name != "Master")
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            KeyPreview = true;
            ApplyTheme(this);
        }

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void RepaintControl(Control control, Color color1, Color color2)
        {
            System.Drawing.Drawing2D.LinearGradientBrush gradBrush;
            gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(control.Width, control.Height), color1, color2);
            Bitmap bmp = new Bitmap(control.Width, control.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(gradBrush, new Rectangle(0, 0, control.Width, control.Height));
            control.BackgroundImage = bmp;
            control.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
