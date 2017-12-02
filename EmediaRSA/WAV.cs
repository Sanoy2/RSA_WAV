using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmediaRSA
{
    class WAV
    {
        // HEADER 
        private byte[] riffID { get; set; } // "riff"
        private uint size { get; set; }
        private byte[] wavID { get; set; } // "WAVE"
        private byte[] fmtID { get; set; }  // "fmt "
        private uint fmtSize { get; set; }
        private ushort format { get; set; }
        private ushort channels { get; set; }
        private uint sampleRate { get; set; }
        private uint bytePerSec { get; set; }
        private ushort blockSize { get; set; }
        private ushort bit { get; set; }
        private byte[] dataID { get; set; } // "data"
        private uint dataSize { get; set; }

        // Data

        private List<short> L { get; set; }
        private List<short> R { get; set; }

        private List<short> L_new { get; set; }
        private List<short> R_new { get; set; }

        public WAV()
        {
            L = new List<short>();
            R = new List<short>();
        }

        public void Wczytaj(string sciezka)
        {
            var br = new BinaryReader(new FileStream(sciezka, FileMode.Open, FileAccess.Read));
            // br - binary reader
            try
            {
                riffID = br.ReadBytes(4);
                size = br.ReadUInt32();
                wavID = br.ReadBytes(4);
                fmtID = br.ReadBytes(4);
                fmtSize = br.ReadUInt32();
                format = br.ReadUInt16();
                channels = br.ReadUInt16();
                sampleRate = br.ReadUInt32();
                bytePerSec = br.ReadUInt32();
                blockSize = br.ReadUInt16();
                bit = br.ReadUInt16();
                dataID = br.ReadBytes(4);
                dataSize = br.ReadUInt32();

                for (int i = 0; i < dataSize / blockSize; i++)
                {
                    L.Add((short)br.ReadUInt16());
                    R.Add((short)br.ReadUInt16());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd odczytu!");
                Console.WriteLine(e.ToString());
            }

            br.Close();

            L_new = L;
            R_new = R;
        }

        public void Zapisz(string sciezka)
        {
            var bw = new BinaryWriter(new FileStream(sciezka, FileMode.Create, FileAccess.Write));
            // bw - binary writer
            try
            {
                bw.Write(riffID);
                bw.Write(size);
                bw.Write(wavID);
                bw.Write(fmtID);
                bw.Write(fmtSize);
                bw.Write(format);
                bw.Write(channels);
                bw.Write(sampleRate);
                bw.Write(bytePerSec);
                bw.Write(blockSize);
                bw.Write(bit);
                bw.Write(dataID);
                bw.Write(dataSize);

                for (int i = 0; i < dataSize / blockSize; i++)
                {
                    if (i < L_new.Count)
                    {
                        bw.Write((ushort)L_new[i]);
                    }
                    else
                    {
                        bw.Write(0);
                    }

                    if (i < R_new.Count)
                    {
                        bw.Write((ushort)R_new[i]);
                    }
                    else
                    {
                        bw.Write(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd zapisu!");
                Console.WriteLine(e.ToString());
            }

            bw.Close();
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.AppendLine("riffID : " + riffID);
            sb.AppendLine("size : " + size);
            sb.AppendLine("wavID : " + wavID);
            sb.AppendLine("fmtID : " + fmtID);
            sb.AppendLine("fmtSize : " + fmtSize);
            sb.AppendLine("format : " + format);
            sb.AppendLine("channels : " + channels);
            sb.AppendLine("sampleRate : " + sampleRate);
            sb.AppendLine("bytePerSec : " + bytePerSec);
            sb.AppendLine("blockSize : " + blockSize);
            sb.AppendLine("bit : " + bit);
            sb.AppendLine("dataID : " + dataID);
            sb.AppendLine("dataSize : " + dataSize);

         
            return sb.ToString();
        }
    }
}
