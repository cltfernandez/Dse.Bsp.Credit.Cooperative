using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Forms.Popups
{
    public partial class PopupParameters : Forms.Windows.BaseForm
    {
        public Enumerations.Popups.PopupResponses Response { get; set; }

        public PopupParameters(object generic)
        {
            InitializeComponent();
            int propertyCount = generic.GetType().GetProperties().Count();

            if (propertyCount > 3)
            {
                pgGeneric.Height = (generic.GetType().GetProperties().Count() * 19);
                Height += (generic.GetType().GetProperties().Count() * 19);
            }

            pgGeneric.SelectedObject = generic;
        }

        private void Parameters_Load(System.Object sender, System.EventArgs e)
        {
            CancelButton = btnCancel;
            AcceptButton = btnOk;
            Infrastructure.Controls.PropertyGrid.EnterNavigator.Add(pgGeneric);
        }

        private void btnOk_Click(System.Object sender, System.EventArgs e)
        {
            if (btnOk.Focused)
            {
                Response = Enumerations.Popups.PopupResponses.Ok;
                this.Close();
            }

            int index = 0;
            object obj;
            if (!Infrastructure.Controls.PropertyGrid.EnterNavigator.IsNextSelected(pgGeneric))
            {
                
                foreach (PropertyInfo property in pgGeneric.SelectedObject.GetType().GetProperties())
                {
                    obj = property.GetValue(pgGeneric.SelectedObject, null);
                    if(property.PropertyType == System.Type.GetType("System.DateTime") )
                    {
                        if ((DateTime)obj == DateTime.MinValue)
                        {
                            Helpers.Popups.Error("Please fill all details.");
                            return;
                        }
                    }
                    if (obj == null || obj.ToString().Trim().Length == 0)
                    {
                        Helpers.Popups.Error("Please fill all details.");
                        return;
                    }
                    index++;
                }
                
                Response = Enumerations.Popups.PopupResponses.Ok;
                this.Close();
            }
        }

        private void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            Response = Enumerations.Popups.PopupResponses.Quit;
            this.Close();
        }
    }
}
