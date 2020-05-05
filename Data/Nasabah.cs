using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTML_integration.Data
{
    public class Nasabah
    {
        public string Nama { get; set; }
        public List<string> NoRek = new List<string>();

        public void Clear()
        {
            Nama = string.Empty;
            NoRek.Clear();
        }
    }
}
