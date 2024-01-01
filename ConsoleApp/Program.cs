using LibHexImage;
using Microsoft.Extensions.Configuration;

string resourcePath = @"C:\Users\gsmlwzy\Desktop\FullSpeedUSB\ConvertImageSolution\Resources";
string bmpName = "plane.png";
string hexName = String.Concat(Path.GetFileNameWithoutExtension(bmpName), ".hex");

IConfigurationRoot root = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .Build();

HexFile hexFile = new HexFile(Path.Join(resourcePath, bmpName), Path.Join(resourcePath, hexName), root);
await hexFile.GenerateAsync();


Console.WriteLine("End of Program");