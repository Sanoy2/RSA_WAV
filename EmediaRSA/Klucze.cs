using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EmediaRSA
{
    public class Klucze
    {

        public BigInteger p { get; set; }
        public BigInteger q { get; set; }
        public BigInteger n { get; set; }
        public BigInteger fi { get; set; }
        public BigInteger e { get; set; }
        public BigInteger d { get; set; }

        public Klucze(BigInteger _p, BigInteger _q)
        {
            init(_p, _q);
        }

        private void init(BigInteger _p, BigInteger _q)
        {
            p = _p;
            q = _q;

            n = p * q;

            fi = (p - 1) * (q - 1);

            var szukajE = new SzukajE(n);
            e = szukajE.LiczE(n, fi);

            var szukajD = new SzukajD();
            d = szukajD.LiczD(e, fi);
        }

        public void Wypisz_klucze()
        {
            Console.WriteLine("p : " + p);
            Console.WriteLine("q : " + q);
            Console.WriteLine("n : " + n);
            Console.WriteLine("fi : " + fi);
            Console.WriteLine("e : " + e);
            Console.WriteLine("d : " + d);

            Console.WriteLine("Klucz publiczny (e,n) : " + e + ", " + n);
            Console.WriteLine("Klucz prywatny (d,n): " + d + ", " + n);
        }
    }
}
