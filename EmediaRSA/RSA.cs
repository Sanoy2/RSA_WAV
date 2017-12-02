using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
using System.Drawing;

namespace EmediaRSA
{
    public class RSA
    {
        private Klucze klucze;
        private BigInteger p, q;

        public RSA()
        {
            p = 239;
            q = 271;
            init();
        }

        public RSA(BigInteger _p, BigInteger _q)
        {
            p = _p;
            q = _q;
            init();
        }

        private void init()
        {
            klucze = new Klucze(p, q);
            klucze.Wypisz_klucze();
        }

        public void Szyfruj()
        {

        }

        public List<ushort> Szyfruj(List<ushort> liczby)
        {
            BigInteger tmp = 0;
            var zaszyfrowane_ushort = new List<ushort>();

            foreach (var elem in liczby)
            {
                //c = t^e mod n.
                tmp = BigInteger.ModPow(elem, klucze.e, klucze.n);
                zaszyfrowane_ushort.Add((ushort)tmp);
            }

            return zaszyfrowane_ushort;
        }

        public List<ushort> Deszyfruj(List<ushort> liczby)
        {
            BigInteger tmp = 0;
            var zaszyfrowane_ushort = new List<ushort>();

            foreach (var elem in liczby)
            {
                //c = t^e mod n.
                tmp = BigInteger.ModPow(elem, klucze.d, klucze.n);
                zaszyfrowane_ushort.Add((ushort)tmp);
            }

            return zaszyfrowane_ushort;
        }

        public Int16[] Szyfruj(Int16[] liczby)
        {
            var zaszyfrowane = new BigInteger[liczby.Length];
            for (int i = 0; i < liczby.Length; i++)
            {
                //c = t^e mod n.
                zaszyfrowane[i] = BigInteger.ModPow(liczby[i], klucze.e, klucze.n);
                liczby[i] = (Int16)zaszyfrowane[i];
            }
            return liczby;
        }

        public Image Szyfruj(Image bitmap)
        {
            Bitmap d = (Bitmap)bitmap;
            int x, y;
            int r, g, b, a;

            for (x = 0; x < d.Width; x++)
            {
                for (y = 0; y < d.Height; y++)
                {
                    Color pixelColor = d.GetPixel(x, y);

                    a = (int)BigInteger.ModPow(pixelColor.A, klucze.e, klucze.n);
                    r = (int)BigInteger.ModPow(pixelColor.R, klucze.e, klucze.n);
                    g = (int)BigInteger.ModPow(pixelColor.G, klucze.e, klucze.n);
                    b = (int)BigInteger.ModPow(pixelColor.B, klucze.e, klucze.n);

                    d.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return (Image)d;
        }

        public Image Deszyfruj(Image bitmap)
        {
            Bitmap d = (Bitmap)bitmap;
            int x, y;
            int r, g, b, a;

            for (x = 0; x < d.Width; x++)
            {
                for (y = 0; y < d.Height; y++)
                {
                    Color pixelColor = d.GetPixel(x, y);

                    a = (int)BigInteger.ModPow(pixelColor.A, klucze.d, klucze.n);
                    r = (int)BigInteger.ModPow(pixelColor.R, klucze.d, klucze.n);
                    g = (int)BigInteger.ModPow(pixelColor.G, klucze.d, klucze.n);
                    b = (int)BigInteger.ModPow(pixelColor.B, klucze.d, klucze.n);

                    d.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return (Image)d;
        }

        public void Deszyfruj()
        {

        }

        public int[] Szyfruj(int[] liczby)
        {
            var zaszyfrowane = new BigInteger[liczby.Length];
            for (int i = 0; i < liczby.Length; i++)
            {
                //c = t^e mod n.
                zaszyfrowane[i] = BigInteger.ModPow(liczby[i], klucze.e, klucze.n);
                liczby[i] = (int)zaszyfrowane[i];
            }
            return liczby;
        }

        public int Szyfruj(int liczba)
        {
            var zaszyfrowana = new BigInteger();

            //c = t^e mod n.
            zaszyfrowana = BigInteger.ModPow(liczba, klucze.e, klucze.n);
            liczba = (int)zaszyfrowana;

            return liczba;
        }

        public string Szyfruj(string napis)
        {
            byte[] bajty = Encoding.UTF8.GetBytes(napis);
            var zaszyfrowane = new BigInteger[bajty.Length];
            for (int i = 0; i < bajty.Length; i++)
            {
                //c = t^e mod n.
                zaszyfrowane[i] = BigInteger.ModPow(bajty[i], klucze.e, klucze.n);
                bajty[i] = (byte)zaszyfrowane[i];
            }

            string val = Encoding.UTF8.GetString(bajty);
            return val;
        }

        public string Deszyfruj(string napis)
        {
            byte[] bajty = Encoding.UTF8.GetBytes(napis);
            var rozszyfrowane = new BigInteger[bajty.Length];

            for (int i = 0; i < bajty.Length; i++)
            {
                //t = c^d mod n
                rozszyfrowane[i] = BigInteger.ModPow(bajty[i], klucze.d, klucze.n);
                bajty[i] = (byte)rozszyfrowane[i];
            }

            string val = Encoding.UTF8.GetString(bajty);
            return val;
        }

        public int Deszyfruj(int liczba)
        {
            var zaszyfrowana = new BigInteger();

            //c = t^e mod n.
            zaszyfrowana = BigInteger.ModPow(liczba, klucze.d, klucze.n);
            liczba = (int)zaszyfrowana;

            return liczba;
        }
    }
}
