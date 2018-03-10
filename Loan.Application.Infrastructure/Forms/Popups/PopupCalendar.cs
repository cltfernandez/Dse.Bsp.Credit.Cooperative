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
    public partial class PopupCalendar : Forms.Windows.BaseForm
    {
        public DateTime GetSelectedDate()
        {
            return mclDefault.SelectionRange.Start;
        }

        public PopupCalendar()
        {
            InitializeComponent();
        }

        private void mclDefault_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            Close();
        }
    }
}
