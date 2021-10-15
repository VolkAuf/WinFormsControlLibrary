using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsComponentLibrary.Main.HelperModels;
using WinFormsComponentLibrary.Main.HelperModels.Word;

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

        private void buttonTable_Click(object sender, EventArgs e)
        {
            
        }

        private string[,] generateTable(string str, int width, int height)
        {
            string[,] strOut = new string[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    strOut[i, j] = str + " " + i.ToString() + " " + j.ToString();
                }
            }

            return strOut;
        }

        private void buttonReportContextTables_Click(object sender, EventArgs e)
        {
            List<string[,]> tables = new List<string[,]>();

            for (int i = 10; i < 20; i++)
            {
                string[,] table = generateTable(userControlTextBox.Value.ToString(), i / 2, i);
                tables.Add(table);
            }

            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    componentWordContextTables.CreateContextTables(new ComponentWordContextTablesConfig()
                    {
                        WordInfo = new WordInfo()
                        {
                            Title = "title",
                            Path = dialog.FileName
                        },
                        Tables = tables
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
