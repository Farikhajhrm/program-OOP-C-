using System;
using System.Collections.Generic;

namespace tespenggajiankaryawan
{
    public abstract class Karyawan
    {
        public string NamaAwal { get; }
        public string NamaAkhir { get; }
        public string NoKTP { get; }
        // tiga parameter konstruktor
        public Karyawan(string namaAwal, string namaAkhir, string noKtp)
        {
            NamaAwal = namaAwal;
            NamaAkhir = namaAkhir;
            NoKTP = noKtp;
        }
        // mengembalikan representasi string dari objek Karyawan
        public override string ToString() => $"{NamaAwal} {NamaAkhir}\n" + $"SSN\t\t: {NoKTP}";

        // metode abstrak diganti oleh kelas turunan
        public abstract decimal Penghasilan(); // tidak ada implementasi yang terjadi
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
        public override decimal Penghasilan() => GajiMingguan;

        // mengembalikan representasi string dari objek gaji karyawan
        public override string ToString() => $"nama karyawan\t: {base.ToString()}\n" + $"gaji mingguan\t: {GajiMingguan:C}";
    }
    public class JamKerjaKaryawan : Karyawan
    {
        private decimal upah; // upah per jam
        private decimal jamKerja; // jam kerja seminggu nya

        // five-parameter constructor
        public JamKerjaKaryawan(string namaAwal, string namaAkhir, string sSN, decimal upahPerJam, decimal jamKerja) : base(namaAwal, namaAkhir, sSN)
        {
            Upah = upahPerJam; // validasi upah per jam
            Jam = jamKerja; // validasi jam kerja
        }
        // atribut get dan set upah karyawan per jam
        public decimal Upah
        {
            get
            {
                return upah;
            }
            set
            {
                if (value < 0) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Upah)} harus >= 0");
                }
                upah = value;
            }
        }
        // atribut get dan set jam kerja karyawan
        public decimal Jam
        {
            get
            {
                return jamKerja;
            }
            set
            {
                if (value < 0 || value > 168) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Jam)} harus >= 0 dan <= 168");
                }

                jamKerja = value;
            }
        }
        // menghitung penghasilan karyawan
        public override decimal Penghasilan()
        {
            if (Jam <= 40) // tidak ada lembur
            {
                return Upah * Jam;
            }
            else
            {
                return (40 * Upah) + ((Jam - 40) * Upah * 1.5M);
            }
        }
        // mengembalikan representasi string dari objek jamkerjakaryawan
        public override string ToString() => $"nama karyawan\t: {base.ToString()}\n" + $"upah perjam\t: {Upah:C}\njam kerja\t: {Jam:F2}";
    }
    public class KomisiKaryawan : Karyawan
    {
        private decimal penjualanKotor; // penjualan kotor mingguan
        private decimal nilaiKomisi; // persentase komisi

        // lima parameter konstruktor
        public KomisiKaryawan(string namaAwal, string namaAkhir, string sSN, decimal penjualanKotor, decimal nilaiKomisi) : base(namaAwal, namaAkhir, sSN)
        {
            PenjualanKotor = penjualanKotor; // validasi penjualan kotor
            NilaiKomisi = nilaiKomisi; // validasi nilai komisi
        }
        // atribut get dan set penjualan kotor karyawan
        public decimal PenjualanKotor
        {
            get
            {
                return penjualanKotor;
            }
            set
            {
                if (value < 0) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(PenjualanKotor)} harus >= 0");
                }
                penjualanKotor = value;
            }
        }
        // atribut get dan set nilai komisi karyawan
        public decimal NilaiKomisi
        {
            get
            {
                return nilaiKomisi;
            }
            set
            {
                if (value <= 0 || value >= 1) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(NilaiKomisi)} harus > 0 dan < 1");
                }
                nilaiKomisi = value;
            }
        }
        // menghitung penghasilan karyawan
        public override decimal Penghasilan() => NilaiKomisi * PenjualanKotor;
        // mengembalikan representasi string dari objek Komisi Karyawan
        public override string ToString() => $"nama karyawan\t: {base.ToString()}\n" + $"penjualan kotor\t: {PenjualanKotor:C}\n" + $"nilai komisi\t: {NilaiKomisi:F2}";
    }
    public class KomisiTambahanKaryawan : KomisiKaryawan
    {
        private decimal gajiPokok; // gaji pokok per jam
        // enam parameter kontruktor
        public KomisiTambahanKaryawan(string namaAwal, string namaAkhir, string sSN, decimal penjualanKotor, decimal nilaiKomisi, decimal gajiPokok)
            : base(namaAwal, namaAkhir, sSN, penjualanKotor, nilaiKomisi)
        {
            GajiPokok = gajiPokok; // validasi gaji pokok
        }

        // atribut get dan set
        // KomisiTambahanKaryawan
        public decimal GajiPokok
        {
            get
            {
                return gajiPokok;
            }
            set
            {
                if (value < 0) // validasi
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(GajiPokok)} harus >= 0");
                }

                gajiPokok = value;
            }
        }
        // menghitung pendapatan karyawan
        public override decimal Penghasilan() => GajiPokok + base.Penghasilan();

        // mengembalikan representasi string dari KomisiTambahanKarywan
        public override string ToString() => $"{base.ToString()}\ngaji pokok\t: {GajiPokok:C}";
    }
    class PayrollSystemTest
    {
        static void Main()
     
        {
            // membuat objek kelas turunan
            var gajiKaryawan = new GajiKaryawan("John", "Smith", "111-11-1111", 800.00M);
            var jamKerjaKaryawan = new JamKerjaKaryawan("Karen", "Price", "222-22-2222", 16.75M, 40.0M);
            var komisiKaryawan = new KomisiKaryawan("Sue", "Jones", "333-33-3333", 10000.00M, .06M);
            var komisiTambahanKaryawan = new KomisiTambahanKaryawan("Bob", "Lewis", "444-44-4444", 5000.00M, .04M, 300.00M);

            Console.WriteLine("Karyawan diproses secara individual:\n");
            Console.WriteLine($"{gajiKaryawan}\npenghasilan\t: " + $"{gajiKaryawan.Penghasilan():C}\n");
            Console.WriteLine($"{jamKerjaKaryawan}\npenghasilan\t: {jamKerjaKaryawan.Penghasilan():C}\n");
            Console.WriteLine($"{komisiKaryawan}\npenghasilan\t: " + $"{komisiKaryawan.Penghasilan():C}\n");
            Console.WriteLine($"{komisiTambahanKaryawan}\npenghasilan\t: " + $"{komisiTambahanKaryawan.Penghasilan():C}\n");

            // membuat Daftar<Karyawan> dan inisialisasi objek karyawan
            var karyawan = new List<Karyawan>() { gajiKaryawan, jamKerjaKaryawan, komisiKaryawan, komisiTambahanKaryawan };
            Console.WriteLine("Karyawan diproses secara polimorfik :\n");
            // memproses secara umum setiap elemen dalam karyawan
            foreach (var pekerjaSekarang in karyawan)
            {
                Console.WriteLine(pekerjaSekarang); // memanggil ToString

                // menentukan apakah benar elemen adalah KomisiTambahanKaryawan
                if (pekerjaSekarang is KomisiTambahanKaryawan)
                {
                    // downcast employee reference to
                    // BasePlusCommissionEmployee reference
                    var karyawan1 = (KomisiTambahanKaryawan)pekerjaSekarang;

                    karyawan1.GajiPokok *= 1.10M;
                    Console.WriteLine("gaji pokok baru dengan kenaikan 10% adalah : " + $"{karyawan1.GajiPokok:C}");
                }
                Console.WriteLine($"penghasilan\t: {pekerjaSekarang.Penghasilan():C}\n");
            }
            // get type name of each object in employees
            for (int j = 0; j < karyawan.Count; j++)
            {
                Console.WriteLine($"Karyawan {j} adalah {karyawan[j].GetType()}");
            }
        }
    }
}