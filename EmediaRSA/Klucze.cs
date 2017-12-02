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

        public Klucze()
        {
            p = 65537;
            q = 65521;
            n = 4294049777;
            fi = 4293918720;
            e = 11;
            d = 2732493731;
        }

        private void init(BigInteger _p, BigInteger _q)
        {
            p = _p;
            q = _q;

            Console.WriteLine("Mam p ! : " + p);
            Console.WriteLine("Mam q ! : " + q);

            n = p * q;

            Console.WriteLine("Mam n ! : " + n);

            fi = (p - 1) * (q - 1);

            Console.WriteLine("Mam fi ! : " + fi);

            var szukajE = new SzukajE(n);
            e = szukajE.LiczE(n, fi);

            Console.WriteLine("Mam e ! : " + e);

            var szukajD = new SzukajD();
            d = szukajD.LiczD(e, fi);

            Console.WriteLine("Mam d ! : " + d);
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("p: " + p);
            sb.AppendLine("q : " + q);
            sb.AppendLine("n : " + n);
            sb.AppendLine("fi : " + fi);
            sb.AppendLine("e : " + e);
            sb.AppendLine("d : " + d);

            sb.AppendLine("Klucz publiczny (e,n) : " + e + ", " + n);
            sb.AppendLine("Klucz prywatny (d,n): " + d + ", " + n);
            sb.AppendLine("UInt32 max : " + UInt32.MaxValue.ToString());

            return sb.ToString();
        }

        public void Wypisz_klucze()
        {
            /*
            Console.WriteLine("p : " + p);
            Console.WriteLine("q : " + q);
            Console.WriteLine("n : " + n);
            Console.WriteLine("fi : " + fi);
            Console.WriteLine("e : " + e);
            Console.WriteLine("d : " + d);

            Console.WriteLine("Klucz publiczny (e,n) : " + e + ", " + n);
            Console.WriteLine("Klucz prywatny (d,n): " + d + ", " + n);
            */
        }
    }
}
