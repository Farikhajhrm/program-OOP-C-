using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter11hal305
{
    public class KomisiKaryawan : Object
    {
        public String NamaDepan { get; }
        public String NamaBelakang { get; }
        public String SocialSecurityNumber { get; }
        private decimal penjualanKotor; //penjualan mingguan kotor
        private decimal tarifKomisi; //presentase komisi

        //konstruktor lima parameter
        public KomisiKaryawan(String namaDepan, string namaBelakang, string socialSecurityNumber, decimal penjualanKotor, decimal tarifKomisi)
        {
        // panggilan implisit ke konstruktor objek terjadi di sini
        NamaDepan = namaDepan;
        NamaBelakang = namaBelakang;
        SocialSecurityNumber = socialSecurityNumber;
        PenjualanKotor = penjualanKotor; //memvalidasi penjualan kotor
        TarifKomisi = tarifKomisi; //memvalidasi tingkat komisi
        }
        // properti yang mendapatkan dan menetapkan komisi penjualan kotor karyawan
         public decimal PenjualanKotor
         {
            get
            {
              return penjualanKotor;
            }
            set
            {
              if (value < 0) //memvalidasi
              {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(penjualanKotor)} harus >= 0");
              }
              penjualanKotor = value;
            }
         }
         // properti yang mendapatkan dan menetapkan tingkat komisi komisi karyawan
         public decimal TarifKomisi
         {
             get
             {
               return tarifKomisi;
             }
             set
             {
               if (value <= 0 || value >= 1) //memvalidasi
               {
                 throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(TarifKomisi)} harus > 0 and < 1");
               }
               tarifKomisi = value;
             }
         }
         // hitung komisi gaji pegawai
         public decimal Pendapatan() => tarifKomisi * penjualanKotor;

         // mengembalikan representasi string dari objek CommissionEmployee
         public override string ToString() =>
         $"komisikaryawan: {NamaDepan} {NamaBelakang}\n" + $"social security number: {SocialSecurityNumber}\n" + $"penjualan kotor: { penjualanKotor:C}\n" + $"tarif komisi: {tarifKomisi:F2}";
    }
    class TesKomisiKaryawan
    {
        static void Main()
        {
            //memberi contoh objek KomisiKaryawan
            var karyawan = new KomisiKaryawan("Sue", "Jones", "222-22-2222", 10000.00M, .06M);
            //tampilkan data Komisi Karyawan
            Console.WriteLine("Informasi karyawan diperoleh dengan properti dan metode: \n");
            Console.WriteLine($"Nama depan adalah {karyawan.NamaDepan}");
            Console.WriteLine($"Nama belakang adalah {karyawan.NamaBelakang}");
            Console.WriteLine($"Nomor Social security adalah {karyawan.SocialSecurityNumber}");
            Console.WriteLine($"Penjualan kotor adalah {karyawan.PenjualanKotor:C}");
            Console.WriteLine($"Tarif komisi adalah {karyawan.TarifKomisi:F2}");
            Console.WriteLine($"Pendapatan adalah {karyawan.Pendapatan():C}");

            karyawan.PenjualanKotor = 5000.00M; //menetapkan penjualan kotor
            karyawan.TarifKomisi = .1M; //menetapkan tarif komisi

            Console.WriteLine("\nInformasi karyawan yang diperbarui diperoleh oleh ToString:\n");
            Console.WriteLine(karyawan);
            Console.WriteLine($"penghasilan: {karyawan.Pendapatan():C}");
        }
    }
}