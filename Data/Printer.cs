using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;

namespace HTML_integration.Data
{
    public class Printer
    {
        PrintDocument printDoc = new PrintDocument();
        Transaksi _trx = new Transaksi();
        Font font = new Font("Times New Roman", 12, FontStyle.Regular);
        int ypoint = 0;
        int starty = 0;
        int startx = 0;
        int jumlahTabungan = 0;
        public async Task PrintPassbook(Transaksi trx)
        {
            try
            {
                font = new Font("Times New Roman", 8, FontStyle.Regular);
                ypoint = 10;
                _trx = trx;
                Int32.TryParse(trx.BarisAwal, out int starty);
                Int32.TryParse(trx.SaldoAkhir, out int jumlahTabungan);
                starty += 66;

                printDoc.DefaultPageSettings = GetPrinterPageInfo("PsiPR-OLI");
                printDoc.PrintPage += new PrintPageEventHandler(PrintPagePassbook);
                //printDoc.DefaultPageSettings.PaperSize = new PaperSize("custom", 826, 1169);
                printDoc.PrinterSettings.PrinterName = "PsiPR-OLI";
                printDoc.Print();
            }
            catch { }
        }

        public void BeginPrintEH(object sender, PrintEventArgs peArgs)
        {
            SolidBrush redBrush = new SolidBrush(Color.Black);
        }

        public void EndPrintEH(object sender, PrintEventArgs peArgs)
        {
            SolidBrush redBrush = new SolidBrush(Color.Black);
            redBrush.Dispose();
        }
        private void PrintPagePassbook(object sender, PrintPageEventArgs ppeArgs)
        {
            try
            {
                Graphics g = ppeArgs.Graphics;
                //g.DrawString(strJumlahTabungan, font, Brushes.Black, new Point(320, 70));
                foreach (var listTrx in _trx.historiTransaksi)
                {
                    g.DrawString(listTrx.tanggalTransaksi, font, Brushes.Black, new Point(2, starty));
                    g.DrawString(listTrx.kodeTransaksi, font, Brushes.Black, new Point(90, starty));
                    g.DrawString(listTrx.keteranganTransaksi, font, Brushes.Black, new Point(130, starty));
                    g.DrawString(listTrx.jumlah.ToString(), font, Brushes.Black, new Point(170, starty));
                    starty += ypoint;
                    jumlahTabungan += listTrx.jumlah;
                }
                string strJumlahTabungan = string.Join("Saldo akhir :", " ", jumlahTabungan);
                g.DrawString(strJumlahTabungan, font, Brushes.Black, new Point(2, starty));
            }
            catch { }
        }

        public async Task PrintA4(Transaksi trx)
        {
            font = new Font("Times New Roman", 12, FontStyle.Regular);
            ypoint = 20;
            starty = 170;
            _trx = trx;
            Int32.TryParse(trx.SaldoAkhir, out int jumlahTabungan);
            printDoc.PrinterSettings.PrinterName = "Brother HL-L2360D series";
            printDoc.DefaultPageSettings = GetPrinterPageInfo("Brother HL-L2360D series");
            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEH);
            printDoc.EndPrint += new PrintEventHandler(EndPrintEH);
            printDoc.PrintPage += new PrintPageEventHandler(PrintPageA4);
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("custom", 826, 1169);
            printDoc.Print();
        }
        public static PageSettings GetPrinterPageInfo(String printerName)
        {
            PrinterSettings settings;

            // printer by its name 
            settings = new PrinterSettings();

            settings.PrinterName = printerName;

            return settings.DefaultPageSettings;
        }
        private void PrintPageA4(object sender, PrintPageEventArgs ppeArgs)
        {
            Graphics g = ppeArgs.Graphics;

            g.DrawString("CETAK TRANSAKSI", new Font("Times New Roman", 24, FontStyle.Bold), Brushes.Black, new Point(250, 20));
            string strJumlahTabungan1 = string.Join("Saldo awal :", " ", jumlahTabungan);
            g.DrawString(strJumlahTabungan1, font, Brushes.Black, new Point(200, 130));
            g.DrawString("Tanggal", font, Brushes.Black, new Point(200, 150));
            g.DrawString("Kode", font, Brushes.Black, new Point(300, 150));
            g.DrawString("Transaksi", font, Brushes.Black, new Point(400, 150));
            g.DrawString("Jumlah", font, Brushes.Black, new Point(500, 150));
            //g.DrawString(strJumlahTabungan, font, Brushes.Black, new Point(320, 70));
            foreach (var listTrx in _trx.historiTransaksi)
            {
                g.DrawString(listTrx.tanggalTransaksi, font, Brushes.Black, new Point(200, starty));
                g.DrawString(listTrx.kodeTransaksi, font, Brushes.Black, new Point(300, starty));
                g.DrawString(listTrx.keteranganTransaksi, font, Brushes.Black, new Point(400, starty));
                g.DrawString(listTrx.jumlah.ToString(), font, Brushes.Black, new Point(500, starty));
                starty += ypoint;
                jumlahTabungan += listTrx.jumlah;
            }
            string strJumlahTabungan = string.Join("Saldo akhir :", " ", jumlahTabungan);
            g.DrawString(strJumlahTabungan, font, Brushes.Black, new Point(200, starty));
        }
    }
}
