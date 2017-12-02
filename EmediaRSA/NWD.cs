using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace EmediaRSA
{
    public class NWD // najwiekszy wspolny dzielnik
    {
        public NWD()
        {

        }

        public BigInteger LiczNWD(BigInteger l1, BigInteger l2)
        {
            BigInteger a = l1;
            BigInteger b = l2;
            BigInteger c = 0;

            while (b != 0)
            {
                c = b;
                b = a % b;
                a = c;
            }
            return a;
        }
    }
}