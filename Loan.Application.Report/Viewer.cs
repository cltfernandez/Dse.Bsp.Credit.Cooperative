using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loan.Application.Infrastructure.Data;

namespace Loan.Application.Report
{
    public partial class Viewer<T> : Form
    {
        DataSet ds;
        object[] m_parameters;
        string spName;
        string rdlcName;

        public Viewer(params object[] parameters)
        {
            InitializeComponent();
            m_parameters = parameters;
            rdlcName = String.Format("{0}.rdlc", typeof(T).FullName);
            spName = typeof(T).FullName.Replace("Loan.Application.", "").Replace('.', '_');
        }

        private void Master_Load(object sender, EventArgs e)
        {
            PreLoad();
            SetDataSource();
            PostLoad();
        }

        private void PreLoad()
        {
            this.rvMain.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            this.rvMain.Reset();
            this.rvMain.LocalReport.DataSources.Clear();
            this.rvMain.LocalReport.ReportEmbeddedResource = rdlcName;
        }

        private void PostLoad()
        {
            SetFileName();
            this.rvMain.LocalReport.Refresh();
            this.rvMain.RefreshReport();
        }

        private void SetFileName()
        {
            String name = typeof(T).Name;
            
            if (name == "MonthlyRunup")
            {
                name = name + ((String) m_parameters[1] == "1" ? ".PastDue" : ".Current");
            }
            else if (name == "Advice")
            {
                name = name.ToUpper();
            }
            
            this.rvMain.LocalReport.DisplayName = name;
            this.Text = name;
        }

        private void SetDataSource()
        {
            ds = ExecuteQuery();
            AddDataSource();
        }

        private DataSet ExecuteQuery()
        {
            Infrastructure.Data.Database db = new Loan.Application.Infrastructure.Data.Database();
            return db.ExecuteQuerySp(spName, m_parameters);
        }

        private void AddDataSource()
        {
            int index = this.rvMain.LocalReport.DataSources.Count;
            if (typeof(T) == typeof(Loans.SubsidiaryLoanLedger))
            {
                AddDataSource<Loans.SubsidiaryLoanLedgerHeader>();
                AddDataSource<Loans.SubsidiaryLoanLedgerBody>();
            }
            else if (typeof(T) == typeof(Maintenance.LoansStatement))
            {
                AddDataSource<Maintenance.LoansStatementHeader>();
                AddDataSource<Maintenance.LoansStatementBody>();
            }
            else if (typeof(T) == typeof(Adhoc.Philam))
            {
                AddDataSource<Adhoc.Remit>();
                AddDataSource<Adhoc.Refund>();
            }
            else if (typeof(T) == typeof(Voucher.LoansPayment))
            {
                AddDataSource<Voucher.LoansPaymentHeader>();
                AddDataSource<Voucher.LoansPaymentBody>();
            }
            else if(typeof(T) == typeof(PaymentOrder.Loans))
            {
                AddDataSource<PaymentOrder.LoansHeader>();
                AddDataSource<PaymentOrder.LoansBody>();
            }
            else
            {
                AddDataSource<T>();
            }
        }

        private void AddDataSource<Tx>()
        {
            int index = this.rvMain.LocalReport.DataSources.Count;
            this.rvMain.LocalReport.DataSources.Add(new ReportDataSource(typeof(Tx).FullName.Replace('.', '_'), Database.GetObjectList<Tx>(ds.Tables[index])));
        }

    }
}