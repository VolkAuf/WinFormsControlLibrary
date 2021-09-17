using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using WinFormsControlLibrary.Main.HelperModel;

namespace WinFormsControlLibrary.Main
{
    public partial class UserControlDataGridView : UserControl
    {
        public UserControlDataGridView()
        {
            InitializeComponent();
        }

        public void AddElement<T>(T element)
        {
            if (element != null)
            {
                List<object> newRow = new List<object>(dataGridView.Columns.Count);
                foreach (DataGridViewTextBoxColumn column in dataGridView.Columns)
                {
                    PropertyInfo property = element.GetType().GetProperty(column.Tag.ToString()); 
                    object value;
                    if (property != null)
                    {
                        value = property.GetValue(element, null);
                    }
                    else
                    {
                        value = null;
                    }
                    newRow.Add(value);
                }
                dataGridView.Rows.Add(newRow.ToArray());
            }
        }

        public int SelectedRowIndex
        {
            get
            {
                return (dataGridView.SelectedRows.Count > 0 ? dataGridView.SelectedRows[0].Index : -1);
            }
            set
            {
                if (value > -1 && value < dataGridView.Rows.Count)
                {
                    dataGridView.Rows[value].Selected = true;
                }
            }
        }

        public void LoadColumns(List<TableConfig> columns)
        {
            {
                dataGridView.Columns.Clear();
                foreach (TableConfig column in columns)
                {
                    int index = dataGridView.Columns.Add(column.PropertyName, column.Header);
                    dataGridView.Columns[index].Visible = column.Visible;
                    dataGridView.Columns[index].Width = (int)column.Width;
                }
            }
        }
        
        public void Clear()
        {
            dataGridView.Rows.Clear();
        }
        
        public T GetSelectedObject<T>()
        where T : class, new()
        {
            T t;

            if (dataGridView.SelectedRows.Count != 0)
            {
                T tempT = Activator.CreateInstance<T>();
                foreach (DataGridViewCell cell in dataGridView.SelectedRows[0].Cells)
                {
                    Type type = tempT.GetType();
                    string str;
                    object tag = dataGridView.Columns[cell.ColumnIndex].Tag;
                    str = tag?.ToString();
                    PropertyInfo property = type.GetProperty(str);

                    if (property != null)
                    {
                        property.SetValue(tempT, cell.Value);
                    }
                }
                t = tempT;
            }
            else
            {
                t = default;
            }
            return t;
        }
    }
}
