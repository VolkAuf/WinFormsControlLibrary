using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsComponentLibrary.Main.HelperModels.Word;

namespace WinFormsComponentLibrary.Main.HelperModels
{
    public class ComponentWordTable<T>
    {
        public WordInfo WordInfo { get; set; }

        public List<int> RowHeight { get; set; }

        public Dictionary<int,string> ColumnWidthAndHeader { get; set; }

        public List<T> ListData { get; set; }
    }
}
