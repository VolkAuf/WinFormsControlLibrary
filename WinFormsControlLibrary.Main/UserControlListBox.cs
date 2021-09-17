using System;
using System.Windows.Forms;

namespace WinFormsControlLibrary.Main
{
    public partial class UserControlListBox : UserControl
    {
        public UserControlListBox()
        {
            InitializeComponent();
        }
        
        public void AddElement(string element)
        {
            if (!string.IsNullOrEmpty(element))
            {
                listBox.Items.Add(element);
            }
        }

        public void Clear()
        {
            listBox.Items.Clear();
        }
        
        public string SelectedElement
        {
            get
            {
                return (listBox.SelectedIndex >= 0 ? listBox.SelectedItem.ToString() : string.Empty);
            }
            set
            {
                listBox.SelectedItem = value;
            }
        }
       
        public event EventHandler ChangeElement
        {
            add
            {
                listBox.SelectedIndexChanged += value;
            }
            remove
            {
                listBox.SelectedIndexChanged -= value;
            }
        }
    }
}
