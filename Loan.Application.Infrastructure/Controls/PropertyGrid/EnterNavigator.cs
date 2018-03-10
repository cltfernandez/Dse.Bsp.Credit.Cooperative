using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Controls.PropertyGrid
{
    public class EnterNavigator
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public static void Add(System.Windows.Forms.PropertyGrid propertyGrid)
        {
            EnterNavigator.SubclassHWND shwnd = new EnterNavigator.SubclassHWND(propertyGrid);
            StringBuilder lpClassName = new StringBuilder(100);
            EnterNavigator.GetClassName(propertyGrid.Handle, lpClassName, lpClassName.Capacity);
            IntPtr propertyGridViewHandle = EnterNavigator.FindWindowEx(propertyGrid.Handle, new IntPtr(0), lpClassName.ToString(), "PropertyGridView");
            shwnd.AssignHandle(propertyGridViewHandle);
        }

        public static bool IsNextSelected(System.Windows.Forms.PropertyGrid propertyGrid)
        {
            GridItem gi = propertyGrid.SelectedGridItem;
            GridItem p_gi = gi.Parent;
            int i = 0;
            for (i = 0; i <= p_gi.GridItems.Count - 1; i++)
            {
                if (object.ReferenceEquals(p_gi.GridItems[i], gi)) { break; }
            }

            if (i + 1 < p_gi.GridItems.Count)
            {
                p_gi.GridItems[i + 1].Select();
                return true;
            }
            else
            {
                return false;
            }
        }

        public class SubclassHWND : NativeWindow
        {

            private System.Windows.Forms.PropertyGrid m_propertyGrid;
            const int WM_KEYDOWN = 0x100;

            const int VK_ENTER = 0xd;
            public SubclassHWND(System.Windows.Forms.PropertyGrid propertyGrid)
            {
                m_propertyGrid = propertyGrid;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_KEYDOWN)
                {
                    if (m.WParam.ToInt32() == VK_ENTER)
                    {
                        IsNextSelected(m_propertyGrid);
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
        }
    }
}
