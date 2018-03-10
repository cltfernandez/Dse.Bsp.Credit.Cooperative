using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Loan.Application.Infrastructure.Enumerations.Popups;
using Loan.Application.Infrastructure.Enumerations.Sql;

namespace Loan.Application.Infrastructure.Helpers
{
    public static class Controls
    {
        public static void SetDataGridView(DataGridView dgv)
        {
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (!dgvc.ValueType.IsEnum && Validators.IsNumericType(dgvc.ValueType))
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dgvc.DefaultCellStyle.Format = "#,##0.00";
                }
                else if (dgvc.ValueType == typeof(DateTime))
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    dgvc.DefaultCellStyle.Format = "MM/dd/yyyy";
                }

            }

            dgv.AutoSizeColumnsMode = (dgv.ColumnCount <= 3 ? DataGridViewAutoSizeColumnsMode.Fill : DataGridViewAutoSizeColumnsMode.AllCells);
            dgv.Select();
        }
    }

    public static class Linq
    {
        public static List<T> FilterList<T>(LambdaFiltering filter, List<T> source, string columnName, string value)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression propertyOrField = Expression.PropertyOrField(parameter, columnName);
            Expression constant = Expression.Constant(value.ToUpper(), typeof(string));
            Expression process = default(Expression);
            MethodInfo contains = default(MethodInfo);

            switch (filter)
            {
                case LambdaFiltering.Contains:
                    contains = typeof(Helpers.Linq).GetMethod("Contains");
                    process = Expression.Call(contains, propertyOrField, constant);
                    break;
                case LambdaFiltering.Equals:
                    process = Expression.Equal(propertyOrField, constant);
                    break;
            }

            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(process, parameter);
            Func<T, bool> compiled = predicate.Compile();
            return source.Where(compiled).ToList();
        }

        public static bool Contains(string text, string subText)
        {
            return text.Contains(subText);
        }
    }

    public static class Member
    {
        // see http://stackoverflow.com/questions/1329138/how-to-make-databinding-type-safe-and-support-refactoring

        private static string GetMemberName(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpression = (MemberExpression)expression;
                    var supername = GetMemberName(memberExpression.Expression);

                    if (String.IsNullOrEmpty(supername))
                        return memberExpression.Member.Name;

                    return String.Concat(supername, '.', memberExpression.Member.Name);

                case ExpressionType.Call:
                    var callExpression = (MethodCallExpression)expression;
                    return callExpression.Method.Name;

                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)expression;
                    return GetMemberName(unaryExpression.Operand);

                case ExpressionType.Parameter:
                    return String.Empty;

                default:
                    throw new ArgumentException("The expression is not a member access or method call expression");
            }
        }

        public static string Name<T>(Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression.Body);
        }

        public static string Name<T>(Expression<Action<T>> expression)
        {
            return GetMemberName(expression.Body);
        }
    }

    public static class Queries
    {
        public static Business.Objects.MemberList GetMemberList()
        {
            return GetMemberList(null);
        }

        public static Business.Objects.MemberList GetMemberList(System.Windows.Forms.Design.IWindowsFormsEditorService service)
        {
            const string kbciNo = "%";
            
            List<Business.Objects.MemberList> list = Data.Database.GetObjectList<Business.Objects.MemberList>(Query.MemberList, kbciNo);

            if (list.Count > 0)
            {
                using (Forms.Popups.PopupSearch<Business.Objects.MemberList> popup = new Loan.Application.Infrastructure.Forms.Popups.PopupSearch<Business.Objects.MemberList>())
                {
                    popup.SetDataSource(list);
                    if (service == null)
                    {
                        popup.ShowDialog();
                    }
                    else
                    {
                        service.ShowDialog(popup);
                    }

                    if (!popup.IsCanceled) { return popup.SelectedObject; }
                }
            }

            return null;
        }

        public static Business.Objects.Loan GetLoan()
        {
            return GetLoan(Query.MemberLoans);
        }

        public static Business.Objects.Loan GetLoan(Query type)
        {
            string pn_no = string.Empty;
            Business.Objects.MemberList member = GetMemberList();
            if (member.KBCI_NO != null)
            {
                return FindByList<Business.Objects.Loan>(type, member.KBCI_NO);
            }
            return null;
        }

        public static T FindByList<T>(Query type, params object[] parameters)
        {
            List<T> list = Data.Database.GetObjectList<T>(type, parameters);

            if (list.Count <= 0)
            {
                Helpers.Popups.Error("No record found.");
                return default(T);
            }

            using (Forms.Popups.PopupDataGridOptions form = new Forms.Popups.PopupDataGridOptions())
            {
                form.dgvList.DataSource = list;
                form.ShowDialog();
                if (form.IsCanceled) return default(T);
                return list[form.dgvList.CurrentCell.RowIndex];
            }
        }
    }

    public static class Validators
    {
        public static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsNullOrEmpty(string text)
        {
            return (text == null || text.Trim() == String.Empty);
        }
    }

    public static class Popups
    {
        public static DialogResult Error(string message)
        {
            const string caption = "DSE BSP Credit Cooperative";
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Exclamation(string message)
        {
            const string caption = "DSE BSP Credit Cooperative";
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult Information(string message)
        {
            const string caption = "DSE BSP Credit Cooperative";
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Question(string message)
        {
            const string caption = "DSE BSP Credit Cooperative";
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}