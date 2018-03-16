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
        public  Klucze klucze { private set; get; }
        private BigInteger p, q;

        public RSA()
        {
            p = 65537;
            q = 65521;
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
            //klucze = new Klucze(p, q);        // generowanie kluczy
            klucze = new Klucze();              // reczne wpisanie wczesniej wygenerowanych kluczy
        }

        public WAV Szyfruj(WAV wav)
        {
            wav.UInts32 = Szyfruj(wav.UInts32);
            return wav;
        }

        public WAV Deszyfruj(WAV wav)
        {
            wav.UInts32 = Deszyfruj(wav.UInts32);
            return wav;
        }

        public List<UInt16> Szyfruj(List<UInt16> uints)
        {
            for (int i = 0; i < uints.Count; i++)
            {
                uints[i] = (UInt16)BigInteger.ModPow(uints[i], klucze.e, klucze.n);
            }
            return uints;
        }

        public List<UInt16> Deszyfruj(List<UInt16> uints)
        {
            for (int i = 0; i < uints.Count; i++)
            {
                uints[i] = (UInt16)BigInteger.ModPow(uints[i], klucze.e, klucze.n);
            }
            return uints;
        }

        public List<UInt32> Szyfruj(List<UInt32> uints)
        {
            for (int i = 0; i < uints.Count; i++)
            {
                uints[i] = (UInt32)BigInteger.ModPow(uints[i], klucze.e, klucze.n);
            }
            return uints;
        }

        public List<UInt32> Deszyfruj(List<UInt32> uints)
        {
            for (int i = 0; i < uints.Count; i++)
            {
                uints[i] = (UInt32)BigInteger.ModPow(uints[i], klucze.d, klucze.n);
            }
            return uints;
        }

        public int Szyfruj(int liczba)
        {
            return (int)BigInteger.ModPow(liczba, klucze.e, klucze.n); 
        }

        public int Deszyfruj(int liczba)
        {
            return (int)BigInteger.ModPow(liczba, klucze.d, klucze.n);
        }
    }
}
