using System.Windows.Forms;

namespace Loan.Application.Infrastructure.Forms.Popups
{
    public partial class PopupButtons : Forms.Windows.BaseForm
    {
        public PopupButtons(params string[] Options)
        {
            InitializeComponent();
            AddButton(Options);
        }

        int _option;

        public int OptionIndex {
            get { return _option + 1; }
        }
        
        private void AddButton(params string[] options)
        {
            int index = 0;

            for (index = 0; index <= options.GetUpperBound(0); index++)
            {
                if (index == options.GetUpperBound(0))
                {
                    grpBox.Text = options.GetValue(index).ToString();
                }
                else
                {
                    SetContainerSizes(index);
                    grpBox.Controls.Add(CreateButton(index, options.GetValue(index).ToString()));
                }
            }
        }

        private Button CreateButton(int index, string message)
        {
            Button button = new Button();

            var _with1 = button;
            _with1.Name = string.Format("btn{0}", index);
            _with1.Size = new System.Drawing.Size(200, 23);
            _with1.Location = new System.Drawing.Point(12, 19 + (index * 29));
            _with1.Text = message;
            _with1.TabIndex = index;

            button.Click += GetOption;
            return button;
        }

        private void SetContainerSizes(int index)
        {
            this.Size = new System.Drawing.Size(240, 64 + (index * 30));
            this.grpBox.Size = new System.Drawing.Size(222, 56 + (index * 28));
        }

        private void GetOption(System.Object sender, System.EventArgs e)
        {
            if (sender is Button)
            {
                _option = ((Button)sender).TabIndex;
            }

            this.Close();
        }
    }
}
