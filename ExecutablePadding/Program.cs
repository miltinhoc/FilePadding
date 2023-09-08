using System;
using System.IO;

namespace ExecutablePadding
{
    public class Program
    {
        private static readonly Int64 _mbSize = 1048576;

        static void Main(string[] args)
        {
            string filePath = args[0];
            Int64.TryParse(args[1], out long r);

            byte[] file = File.ReadAllBytes(filePath);

            Int64 size = ((_mbSize * r)) - file.Length;
           
            FileStream fileStream = new FileStream("bro.exe", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {

                for (int i = 0; i < size; i++)
                {
                    if (i < file.Length)
                    {
                        writer.Write(file[i]);
                    }
                    else
                    {
                        writer.Write((byte)0);
                    }
                }
            }
        }
    }
}
