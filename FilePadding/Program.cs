using System;
using System.IO;

namespace FilePadding
{
    public class Program
    {
        private static readonly Int64 MbSize = 1048576;

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"Usage: FilePadding.exe <inputFilePath> <desiredSizeInMB>");
                return;
            }

            string inputFilePath = args[0];
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Error: Input file does not exist.");
                return;
            }

            if (!Int64.TryParse(args[1], out long desiredSizeInMB))
            {
                Console.WriteLine("Error: Invalid size provided.");
                return;
            }

            long desiredSizeInBytes = MbSize * desiredSizeInMB;
            byte[] fileContent = File.ReadAllBytes(inputFilePath);

            if (fileContent.Length >= desiredSizeInBytes)
            {
                Console.WriteLine("Error: Input file is already larger than the desired size.");
                return;
            }

            string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "padded_" + Path.GetFileName(inputFilePath));

            using (FileStream fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(fileContent);

                long paddingSize = desiredSizeInBytes - fileContent.Length;

                for (long i = 0; i < paddingSize; i++)
                {
                    writer.Write((byte)0);
                }
            }

            Console.WriteLine($"Padded file saved to {outputFilePath}");
        }
    }
}
