using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmediaRSA
{
    public class WAV
    {
        // HEADER 
        public byte[] riffID { get; set; } // "riff"
        public uint size { get; set; }
        public byte[] wavID { get; set; } // "WAVE"
        public byte[] fmtID { get; set; }  // "fmt "
        public uint fmtSize { get; set; }
        public ushort format { get; set; }
        public ushort channels { get; set; }
        public uint sampleRate { get; set; }
        public uint bytePerSec { get; set; }
        public ushort blockSize { get; set; }
        public ushort bit { get; set; }
        public byte[] dataID { get; set; } // "data"
        public uint dataSize { get; set; }

        // Data

        public List<ushort> L { get; set; }
        public List<ushort> R { get; set; }

        public List<UInt32> UInts32 { get; set; }

        public WAV()
        {
            L = new List<ushort>();
            R = new List<ushort>();

            UInts32 = new List<UInt32>();
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
                    //L.Add((ushort)br.ReadUInt16());
                    //R.Add((ushort)br.ReadUInt16());
                    UInts32.Add((UInt32)br.ReadUInt32());
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd odczytu!");
                Console.WriteLine(e.ToString());
            }

            br.Close();
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

                    bw.Write(UInts32[i]);
                    /*
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
                    }*/
                }

                bw.Write(0);
                bw.Write(0);

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

            sb.AppendLine("riffID : " + ByteToString(riffID));
            sb.AppendLine("size : " + size);
            sb.AppendLine("wavID : " + ByteToString(wavID));
            sb.AppendLine("fmtID : " + ByteToString(fmtID));
            sb.AppendLine("fmtSize : " + fmtSize);
            sb.AppendLine("format : " + format);
            sb.AppendLine("channels : " + channels);
            sb.AppendLine("sampleRate : " + sampleRate);
            sb.AppendLine("bytePerSec : " + bytePerSec);
            sb.AppendLine("blockSize : " + blockSize);
            sb.AppendLine("bit : " + bit);
            sb.AppendLine("dataID : " + dataID);
            sb.AppendLine("dataSize : " + dataSize);
            sb.AppendLine("DataSize/blockSize : " + dataSize / blockSize);
         
            return sb.ToString();
        }

        private string ByteToString(byte[] bytes)
        {
            string result = System.Text.Encoding.UTF8.GetString(bytes);
            return result;
        }

    }
}
