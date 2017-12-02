using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace EmediaRSA
{
    public class SzukajE
    {
        private BigInteger n;
        public SzukajE(BigInteger _n)
        {
            n = _n;
        }

        public BigInteger LiczE(BigInteger n, BigInteger fi)
        {
            BigInteger e = 3;

            while (e < n)
            {
                if (Sprawdz_czy_wzglednie_pierwsza(fi, e))
                {
                    return e;
                }
                else
                {
                    e += 2;
                }
            }
            return 0;
        }

        private bool Czy_parzysta(BigInteger e)
        {
            if (e % 2 == 0)
                return true;
            else
                return false;
        }

        private bool Sprawdz_czy_wzglednie_pierwsza(BigInteger fi, BigInteger e)
        {
            var nwd = new NWD();
            if (nwd.LiczNWD(fi, e) == 1)
                return true;
            else
                return false;
        }
    }
}