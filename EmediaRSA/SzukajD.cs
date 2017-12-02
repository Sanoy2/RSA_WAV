using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EmediaRSA
{
    public class SzukajD
    {
        public SzukajD()
        {

        }

        public BigInteger LiczD(BigInteger e, BigInteger fi)
        {
            BigInteger d = 1;
            while (d < fi)
            {
                //Console.WriteLine(d + " * " + e + " % " + fi + " = " + d*e%fi);
                if ((d * e) % fi == 1)
                {
                    Console.WriteLine(d * e % fi);
                    return d;
                }
                d++;
            }
            return -1;
        }
    }
}
