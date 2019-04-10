using System;
using System.IO;

namespace CustomerBarcodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: <PostCode> <Number>");
                Console.ReadLine();
            }
            else if (args[0].Length != 7)
            {
                Console.WriteLine("PostCodeは、ハイフン無しで7文字指定してください");
                Console.ReadLine();
            }
            else
            {
                Run(args[0], args[1]);
            }
        }

        private static void Run(string data1, string data2)
        {
            var cb = new CustomerBarcode();
            var bars = cb.Generate(data1 + data2);

            using (var writer = new StreamWriter($"{data1}_{data2}.svg"))
            {
                writer.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                writer.WriteLine($@"<svg width=""79.8mm"" height=""3.6mm"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 133 6"">");

                int count = 0;
                foreach (var bar in bars)
                {
                    writer.WriteLine($@"<rect x=""{count * 2}"" y=""{((int)bar - 1) / 2 * 2}"" width=""1"" height=""{(3 - (int)bar / 2) * 2}"" stroke=""none"" stroke-width=""0"" fill=""black"" />");
                    count++;
                }

                writer.WriteLine("</svg>");
            }
        }

    }
}
