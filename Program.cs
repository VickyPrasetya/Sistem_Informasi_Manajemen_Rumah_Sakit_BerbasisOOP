using System;
using System.Collections.Generic;

namespace SistemRumahSakit
{
    class Pasien
    {
        private string noRekamMedis; 
        public string Nama { get; set; }
        public int Umur { get; set; }

        public Pasien(string nama, int umur, string noRM)
        {
            Nama = nama;
            Umur = umur;
            noRekamMedis = noRM;
        }

        public string GetNoRekamMedis()
        {
            return noRekamMedis;
        }
    }

    class PegawaiRS
    {
        public string Nama { get; set; }

        public PegawaiRS(string nama)
        {
            Nama = nama;
        }

        public virtual void Tugas()
        {
            Console.WriteLine("Pegawai RS memiliki tugas umum.");
        }
    }

    class Dokter : PegawaiRS
    {
        public string Spesialisasi { get; set; }

        public Dokter(string nama, string spesialisasi) : base(nama)
        {
            Spesialisasi = spesialisasi;
        }

        public override void Tugas()
        {
            Console.WriteLine($"Dokter {Nama} (Spesialis: {Spesialisasi}) sedang memeriksa pasien.");
        }
    }

    class Perawat : PegawaiRS
    {
        public string Shift { get; set; }

        public Perawat(string nama, string shift) : base(nama)
        {
            Shift = shift;
        }

        public override void Tugas()
        {
            Console.WriteLine($"Perawat {Nama} (Shift: {Shift}) sedang membantu pasien.");
        }
    }

    class Obat
    {
        public string Nama { get; set; }
        public int Dosis { get; set; }

        public Obat(string nama)
        {
            Nama = nama;
            Dosis = 0;
        }

        public Obat(string nama, int dosis)
        {
            Nama = nama;
            Dosis = dosis;
        }
    }

    class RekamMedis
    {
        public Pasien Pasien { get; set; }
        public List<Obat> ObatList { get; set; }

        public RekamMedis(Pasien pasien)
        {
            Pasien = pasien;
            ObatList = new List<Obat>();
        }

        public void TambahObat(Obat obat)
        {
            ObatList.Add(obat);
        }

        public void TampilkanRekamMedis()
        {
            Console.WriteLine($"\nRekam Medis Pasien: {Pasien.Nama} ({Pasien.GetNoRekamMedis()})");
            Console.WriteLine("Daftar Obat:");
            foreach (var obat in ObatList)
            {
                Console.WriteLine($"- {obat.Nama} {(obat.Dosis > 0 ? $"{obat.Dosis}mg" : "")}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Pasien> daftarPasien = new List<Pasien>();
            List<PegawaiRS> daftarPegawai = new List<PegawaiRS>();
            List<RekamMedis> daftarRekamMedis = new List<RekamMedis>();

            while (true)
            {
                Console.WriteLine("\n=== Sistem Rumah Sakit ===");
                Console.WriteLine("1. Tambah Pasien");
                Console.WriteLine("2. Tambah Dokter");
                Console.WriteLine("3. Tambah Perawat");
                Console.WriteLine("4. Buat Rekam Medis");
                Console.WriteLine("5. Lihat Tugas Pegawai");
                Console.WriteLine("6. Lihat Rekam Medis");
                Console.WriteLine("0. Keluar");
                Console.Write("Pilih menu: ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        Console.Write("Nama Pasien: ");
                        string namaP = Console.ReadLine();
                        Console.Write("Umur: ");
                        int umur = int.Parse(Console.ReadLine());
                        Console.Write("No Rekam Medis: ");
                        string noRM = Console.ReadLine();
                        daftarPasien.Add(new Pasien(namaP, umur, noRM));
                        Console.WriteLine("Pasien berhasil ditambahkan.");
                        break;

                    case "2":
                        Console.Write("Nama Dokter: ");
                        string namaD = Console.ReadLine();
                        Console.Write("Spesialisasi: ");
                        string spes = Console.ReadLine();
                        daftarPegawai.Add(new Dokter(namaD, spes));
                        Console.WriteLine("Dokter berhasil ditambahkan.");
                        break;

                    case "3":
                        Console.Write("Nama Perawat: ");
                        string namaW = Console.ReadLine();
                        Console.Write("Shift: ");
                        string shift = Console.ReadLine();
                        daftarPegawai.Add(new Perawat(namaW, shift));
                        Console.WriteLine("Perawat berhasil ditambahkan.");
                        break;

                    case "4":
                        if (daftarPasien.Count == 0)
                        {
                            Console.WriteLine("Belum ada pasien.");
                            break;
                        }

                        Console.WriteLine("Pilih Pasien:");
                        for (int i = 0; i < daftarPasien.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {daftarPasien[i].Nama}");
                        }
                        int pilihP = int.Parse(Console.ReadLine()) - 1;
                        RekamMedis rm = new RekamMedis(daftarPasien[pilihP]);

                        Console.Write("Masukkan jumlah obat: ");
                        int jml = int.Parse(Console.ReadLine());
                        for (int i = 0; i < jml; i++)
                        {
                            Console.Write("Nama Obat: ");
                            string nObat = Console.ReadLine();
                            Console.Write("Dosis (mg, 0 jika tidak ada): ");
                            int dosis = int.Parse(Console.ReadLine());
                            if (dosis > 0)
                                rm.TambahObat(new Obat(nObat, dosis));
                            else
                                rm.TambahObat(new Obat(nObat));
                        }

                        daftarRekamMedis.Add(rm);
                        Console.WriteLine("Rekam medis berhasil dibuat.");
                        break;

                    case "5":
                        if (daftarPegawai.Count == 0)
                        {
                            Console.WriteLine("Belum ada pegawai.");
                        }
                        else
                        {
                            foreach (var pegawai in daftarPegawai)
                            {
                                pegawai.Tugas();
                            }
                        }
                        break;

                    case "6":
                        if (daftarRekamMedis.Count == 0)
                        {
                            Console.WriteLine("Belum ada rekam medis.");
                        }
                        else
                        {
                            foreach (var r in daftarRekamMedis)
                            {
                                r.TampilkanRekamMedis();
                            }
                        }
                        break;

                    case "0":
                        Console.WriteLine("Keluar dari sistem...");
                        return;

                    default:
                        Console.WriteLine("Menu tidak valid.");
                        break;
                }
            }
        }
    }
}
