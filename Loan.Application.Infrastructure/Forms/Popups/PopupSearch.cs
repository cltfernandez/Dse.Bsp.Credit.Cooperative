//using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Forms.Popups
{
    public partial class PopupSearch<T> : Forms.Windows.BaseForm
    {
        public bool IsCanceled { get; set; }
        T selectedT;
        List<T> listFiltered;
        List<T> listComplete;
        BindingSource listBindings = new BindingSource();

        public PopupSearch()
        {
            InitializeComponent();
        }

        public T SelectedObject
        {
            get { return selectedT; }
        }

        private void PopupSearch_Load(object sender, System.EventArgs e)
        {
            LoadComboBox();
            SetDataGridView();
            CancelButton = btnOK;
            txtSearch.Focus();
        }

        private void btnFind_Click(System.Object sender, System.EventArgs e)
        {
            Search();
        }

        private void btnOK_Click(System.Object sender, System.EventArgs e)
        {
            GetRecord();
        }

        private void dgvList_Click(object sender, System.EventArgs e)
        {
            GetRecord();
        }

        private void dgvList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetRecord();
            }
        }

        private void txtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void LoadComboBox()
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                cboColumns.Items.Add(prop.Name);
            }
            cboColumns.SelectedIndex = 0;
        }

        public void SetDataSource(List<T> list)
        {
            listComplete = list;
            listBindings.DataSource = listComplete;
            dgvList.DataSource = listBindings;
        }

        private void SetDataGridView()
        {
            dgvList.AllowUserToOrderColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void GetRecord()
        {
            List<T> list = (List<T>)(listBindings.DataSource);
            if (list == null || list.Count <= 0)
            {
                selectedT = default(T);
            }
            else
            {
                selectedT = list[dgvList.CurrentCell.RowIndex];
            }
            IsCanceled = false;
            Close();
        }

        private void Search()
        {
            string searchField = typeof(T).GetProperties().Where(x => x.Name == cboColumns.Text).Select(x => x.Name).First();
            listFiltered = Helpers.Linq.FilterList<T>(Enumerations.Popups.LambdaFiltering.Contains, listComplete, searchField, txtSearch.Text);
            listBindings.DataSource = listFiltered;
        }
        
    }
}
