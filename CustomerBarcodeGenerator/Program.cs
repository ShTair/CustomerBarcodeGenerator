using CustomerBarcodeGenerator;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().AddCommandLine(args).Build();

var postCode = configuration["PostCode"];
var number = configuration["Number"];

if (postCode is null || number is null)
{
    Console.WriteLine("usage: <PostCode> <Number>");
    Console.ReadLine();
}
else if (postCode.Length != 7)
{
    Console.WriteLine("PostCodeは、ハイフン無しで7文字指定してください");
    Console.ReadLine();
}
else
{
    var bars = BarcodeUtils.Generate(postCode + number);
    using (var writer = new StreamWriter($"{postCode}_{number}.svg"))
    {
        writer.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
        writer.WriteLine($@"<svg width=""79.8mm"" height=""3.6mm"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 133 6"">");

        writer.WriteLine("<g>");
        int count = 0;
        foreach (var bar in bars)
        {
            writer.WriteLine($@"<rect x=""{count * 2}"" y=""{((int)bar - 1) / 2 * 2}"" width=""1"" height=""{(3 - (int)bar / 2) * 2}"" />");
            count++;
        }
        writer.WriteLine("</g>");

        writer.WriteLine("</svg>");
    }
}
