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
        private Int32 chunkID;
        private Int32 fileSize;
        private Int32 riffType;


        // chunk 1
        private Int32 fmtID;
        private Int32 fmtSize;
        private Int16 fmtCode;
        private Int16 channels;
        private Int32 sampleRate;
        private Int32 byteRate;
        private Int16 fmtBlockAlign;
        private Int16 bitDepth;
        private Int32 dataID;
        private Int32 bytes;
        private Int16 fmtExtraSize;
        private int bytesForSamp;
        private int samps;

        byte[] byteArray;

        private float[] L;
        private float[] R;

        private UInt16[] asUInt16;
        private float[] asFloat;

        public WAV()
        {
            
        }

        public void Wczytaj(string sciezka)
        {
            var reader = new BinaryReader(new FileStream(sciezka, FileMode.Open));

            chunkID = reader.ReadInt32();
            fileSize = reader.ReadInt32();
            riffType = reader.ReadInt32();


            // chunk 1
            fmtID = reader.ReadInt32();
            fmtSize = reader.ReadInt32(); // bytes for this chunk
            fmtCode = reader.ReadInt16();
            channels = reader.ReadInt16();
            sampleRate = reader.ReadInt32();
            byteRate = reader.ReadInt32();
            fmtBlockAlign = reader.ReadInt16();
            bitDepth = reader.ReadInt16();

            if (fmtSize == 18)
            {
                // Read any extra values
                fmtExtraSize = reader.ReadInt16();
                reader.ReadBytes(fmtExtraSize);
            }

            // chunk 2
            dataID = reader.ReadInt32();
            bytes = reader.ReadInt32();

            //data
            byteArray = reader.ReadBytes(bytes);

            bytesForSamp = bitDepth / 8;
            samps = bytes / bytesForSamp;

            asFloat = null;
            switch (bitDepth)
            {
                case 64:
                    double[]
                    asDouble = new double[samps];
                    Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                    asFloat = Array.ConvertAll(asDouble, e => (float)e);
                    break;
                case 32:
                    asFloat = new float[samps];
                    Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                    break;
                case 16:
                    asUInt16 = new UInt16[samps];
                    Buffer.BlockCopy(byteArray, 0, asUInt16, 0, bytes);
                    asFloat = Array.ConvertAll(asUInt16, e => e / (float)Int16.MaxValue);
                    break;
                default:
                    break;
            }
            /*
            switch (channels)
            {
                case 1:
                    L = asFloat;
                    R = null;
                    break;
                case 2:
                    L = new float[samps];
                    R = new float[samps];
                    for (int i = 0, s = 0; i < samps; i++)
                    {
                        L[i] = asFloat[s++];
                        R[i] = asFloat[s++];
                    }
                    break;
                default:
                    break;
            }
            */
            reader.Close();
        }
        public void Zapisz(string sciezka)
        {
            var writer = new BinaryWriter(new FileStream(sciezka, FileMode.CreateNew, FileAccess.Write));
            /*chunkID = reader.ReadInt32();
            fileSize = reader.ReadInt32();
            riffType = reader.ReadInt32();


            // chunk 1
            fmtID = reader.ReadInt32();
            fmtSize = reader.ReadInt32(); // bytes for this chunk
            fmtCode = reader.ReadInt16();
            channels = reader.ReadInt16();
            sampleRate = reader.ReadInt32();
            byteRate = reader.ReadInt32();
            fmtBlockAlign = reader.ReadInt16();
            bitDepth = reader.ReadInt16();
            */
            writer.Write(chunkID);
            writer.Write(fileSize);
            writer.Write(riffType);
            writer.Write(fmtID);
            writer.Write(fmtSize);
            writer.Write(fmtCode);
            writer.Write(channels);
            writer.Write(sampleRate);
            writer.Write(byteRate);
            writer.Write(fmtBlockAlign);
            writer.Write(bitDepth);

            writer.Close();
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("chunkID : " + chunkID);
            sb.AppendLine("fileSize : " + fileSize);
            sb.AppendLine("riffType : " + riffType);
            sb.AppendLine("fmtID : " + fmtID);
            sb.AppendLine("fmtSize : " + fmtSize);
            sb.AppendLine("fmtCode : " + fmtCode);
            sb.AppendLine("channels : " + channels);
            sb.AppendLine("sampleRate : " + sampleRate);
            sb.AppendLine("byteRate : " + byteRate);
            sb.AppendLine("fmtBlockAlign : " + fmtBlockAlign);
            sb.AppendLine("bitDepth : " + bitDepth);
            sb.AppendLine("dataID : " + dataID);
            sb.AppendLine("bytes : " + bytes);
            sb.AppendLine("fmtExtraSize : " + fmtExtraSize);
            sb.AppendLine("bytesForSamp : " + bytesForSamp);
            sb.AppendLine("samps : " + samps);
            foreach (var item in asFloat)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
