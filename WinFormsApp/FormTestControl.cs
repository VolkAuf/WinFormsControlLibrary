using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FormTestControl : Form
    {
        public FormTestControl()
        {
            InitializeComponent();
        }

        private void buttonAddToUserList_Click(object sender, EventArgs e)
        {
            userControlListBox.AddElement(userControlTextBox.Value.ToString());
        }
    }
}
