using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTML_integration.Data
{
    public class Transaksi
    {
        public string NoRek { get; set; }
        public string Nama { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string SaldoAkhir { get; set; }
        public string BarisAwal { get; set; }
        public class Histori
        {
            public string tanggalTransaksi { get; set; }
            public string keteranganTransaksi { get; set; }
            public string kodeTransaksi { get; set; }
            public int jumlah { get; set; }
        }

        public List<Histori> historiTransaksi = new List<Histori>();

        public void SetHistori(string strTanggal, string strKeterangan, string strKode, int intJumlah)
        {
            Histori temp = new Histori();
            temp.tanggalTransaksi = strTanggal;
            temp.keteranganTransaksi = strKeterangan;
            temp.kodeTransaksi = strKode;
                temp.jumlah = intJumlah;
            historiTransaksi.Add(temp);
        }

        public void Clear()
        {
            NoRek = string.Empty;
            dateStart = string.Empty;
            dateEnd = string.Empty;
            Nama = string.Empty;
            SaldoAkhir = string.Empty;
            BarisAwal = string.Empty;
            historiTransaksi.Clear();
        }
        public void SetTransaksi(string strNoRek, string strDateStart, string strDateEnd, string strName)
        {
            NoRek = strNoRek;
            dateStart = strDateStart;
            dateEnd = strDateEnd;
            Nama = strName;
        }
        public void SetTransaksi(string strNoRek, string strSaldoAkhir, string strBarisAwal)
        {
            NoRek = strNoRek;
            SaldoAkhir = strSaldoAkhir;
            BarisAwal = strBarisAwal;
        }
    }
}
