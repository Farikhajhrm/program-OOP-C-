using System;
using System.Collections.Generic;

namespace Tes_Antarmuka_Hutang
{
    public interface Ipayable
    {
        decimal DapatkanJumlahPembayaran();
    }
    public class Invoice : Ipayable
    {
        public string NomorPasangan { get; }
        public string BagianDeskripsi { get; }
        private int kuantitas;
        private decimal HargaPerItem;

        //empat parameter konstruktor
        public Invoice(string Nomorpasangan, string bagianDeskripsi, int kuantitas, decimal hargaperItem)
        {
            NomorPasangan = Nomorpasangan;
            BagianDeskripsi = bagianDeskripsi;
            Kuantitas = kuantitas;
            HargaPerItem = hargaperItem;
        }
        public int Kuantitas
        {
            get
            {
                return kuantitas;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Kuantitas)} must be >= 0");
                }
                kuantitas = value;
            }
        }
        public decimal PricePerItem
        {
            get
            {
                return HargaPerItem;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(HargaPerItem)} must be >= 0");
                }
                HargaPerItem = value;
            }
        }
        public override string ToString() => $"faktur:\nnomor pasangan: {NomorPasangan} ({BagianDeskripsi})\n" + $"kuantitas: {Kuantitas}\nharga per item: {HargaPerItem:C}";
        public decimal DapatkanJumlahPembayaran() => Kuantitas * HargaPerItem;
    }
    public abstract class Karyawan : Ipayable
    {
        public string NamaDepan { get; }
        public string NamaBelakang { get; }
        public string SocialSecurityNumber { get; }

        public Karyawan(string namaDepan, string namaBelakang, string socialSecurityNumber)
        {
            NamaDepan = namaDepan;
            NamaBelakang = namaBelakang;
            SocialSecurityNumber = socialSecurityNumber;
        }
        public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" + $"social security number: {SocialSecurityNumber}";
        public abstract decimal Pendapatan();
        public decimal DapatkanJumlahPembayaran() => Pendapatan();
    }
    public class GajiKaryawan : Karyawan
    {
        private decimal gajiMingguan;
        // empat parameter konstruktor
        public GajiKaryawan(string namaAwal, string namaAkhir, string sSN, decimal gajiMingguan) : base(namaAwal, namaAkhir, sSN)
        {
            GajiMingguan = gajiMingguan; // validate salary
        }
        // property that gets and sets salaried employee's salary
        public decimal GajiMingguan
        {
            get
            {
                return gajiMingguan;
            }
            set
            {
                if (value < 0) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(GajiMingguan)} harus >= 0");
                }

                gajiMingguan = value;
            }
        }
        // menghitung penghasilan karyawan
        public override decimal Pendapatan() => GajiMingguan;

        // mengembalikan representasi string dari objek gaji karyawan
        public override string ToString() => $"nama karyawan\t: {base.ToString()}\n" + $"gaji mingguan\t: {GajiMingguan:C}";
    }
    class PayableInterfaceTest
    {
        static void Main()
        {
            var ObjekHutang = new List<Ipayable>() {
            new Invoice("01234", "seat", 2, 375.00M),
            new Invoice("56789", "tire", 4, 79.95M),
            new GajiKaryawan("John", "Smith", "111-11-1111", 800.00M),
            new GajiKaryawan("Lisa", "Barnes", "888-88-8888" , 1200.00M)};

            Console.WriteLine("Faktur dan Karyawan diproses secara polimorfisme:\n");
            foreach (var payable in ObjekHutang)
            {
                Console.WriteLine($"{payable}");
                Console.WriteLine($"jadwal pembayaran: {payable.DapatkanJumlahPembayaran():C}\n");
            }
        }
    }
}
