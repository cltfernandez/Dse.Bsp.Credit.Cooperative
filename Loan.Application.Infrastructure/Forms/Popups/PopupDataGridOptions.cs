using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Forms.Popups
{
    public partial class PopupDataGridOptions : Forms.Windows.BaseForm
    {
        public DataGridView GetDataGrid()
        {
            return this.dgvList;
        }

        public PopupDataGridOptions()
        {
            InitializeComponent();
        }

        bool canceled = false;
        public bool IsCanceled
        {
            get { return canceled; }
        }

        private void dgvList_Click(object sender, System.EventArgs e)
        {
            if (!ControlBox)
            {
                this.Close();
            }
        }

        private void dgvList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    this.Close();
                    break;
                case Keys.Escape:
                    canceled = true;
                    e.Handled = true;
                    this.Close();
                    break;
            }
        }

        private void PopupDataGridOptions_Load(object sender, System.EventArgs e)
        {
            Helpers.Controls.SetDataGridView(dgvList);
        }

    }
}
