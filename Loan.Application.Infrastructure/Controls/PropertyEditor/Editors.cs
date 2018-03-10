using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Loan.Application.Infrastructure.Controls.PropertyEditor.Editors
{
    public class PnNoEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            Business.Objects.Loan loan = default(Business.Objects.Loan);

            if (service != null)
            {
                //svc.ShowDialog(New LoanFinder())
                loan = Helpers.Queries.GetLoan();

                if (loan != null)
                {
                    return loan.PN_NO;
                }
            }
            return null;
        }

    }

    public class KbciNoEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            Business.Objects.MemberList member = default(Business.Objects.MemberList);
            if (service != null)
            {
                //svc.ShowDialog(New LoanFinder())
                member = Helpers.Queries.GetMemberList(service);
                if (member != null)
                {
                    return string.Format("{0}|{1}", member.KBCI_NO, member.FULL_NAME);
                }
            }
            return null;
        }
    }
}
