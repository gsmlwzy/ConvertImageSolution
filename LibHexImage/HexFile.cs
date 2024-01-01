using System.Drawing;
using Microsoft.Extensions.Configuration;

namespace LibHexImage;

public class HexFile {
    private uint _downloadAddress;
    private ushort _signalLineBatch;
    private bool _reverseColor;
    private bool _reverseByte;

    private string _imagePath;
    private string _hexPath;
    public HexFile(string imagePath, string hexPath, IConfiguration config) {
        CheckConfig(config);
        _imagePath = imagePath;
        _hexPath = hexPath;
    }

    public HexFile(string imagePath, IConfiguration config){
        CheckConfig(config);
        _imagePath = imagePath;
        string resourcePath = imagePath;
        string bmpNameWithoutExt = Path.GetFileNameWithoutExtension(resourcePath); // hj
        string hexName = String.Concat(bmpNameWithoutExt, ".hex");
        string hexPath = Path.Join(Path.GetDirectoryName(resourcePath), hexName);
        _hexPath = hexPath;
    }

    public void DefaultHexPath(string hexPath) {

    }

    private void CheckConfig(IConfiguration config) {
        _downloadAddress = Convert.ToUInt32(config["downloadAddress"] ?? throw new KeyNotFoundException(), 16);
        _signalLineBatch = Convert.ToUInt16(config["signalLineBatch"] ?? throw new KeyNotFoundException(), 16);
        _reverseColor = Convert.ToBoolean(config["reverseColor"] ?? throw new KeyNotFoundException());
        _reverseByte = Convert.ToBoolean(config["reverseByte"] ?? throw new KeyNotFoundException());
    }

    private async Task BuildAsync(FileStream fs, Bitmap bmp) {
        var list = Img2Binary.ConvertBmp(bmp, _reverseColor, _reverseByte);
        StreamWriter sw = new StreamWriter(fs);

        ushort baseAddress = Convert.ToUInt16(_downloadAddress >> 16);
        ushort lowAddress = Convert.ToUInt16(_downloadAddress & 0xffff);

        string s = HexRecord.HighAddress(Convert.ToUInt16(_downloadAddress >> 16));
        await sw.WriteLineAsync(s);

        byte[] byteArray = new byte[list.Count * 2];
        Buffer.BlockCopy(list.ToArray(), 0, byteArray, 0, byteArray.Length);
        for (int index = 0; index < byteArray.Length;) {
            byte[] subBytes;
            if (index + _signalLineBatch > byteArray.Length) {
                subBytes = byteArray[index..];
            }
            else {
                subBytes = byteArray[index..(index + _signalLineBatch)];
            }

            s = HexRecord.DataRecode(lowAddress, subBytes);
            await sw.WriteLineAsync(s);

            int nextAddress = lowAddress + _signalLineBatch;
            if (nextAddress >= UInt16.MaxValue) {
                lowAddress = 0x00;
                baseAddress += 1;
                s = HexRecord.HighAddress(baseAddress);
                await sw.WriteLineAsync(s);
                Console.WriteLine("overflow----------");
            }
            else {
                lowAddress = Convert.ToUInt16(nextAddress);
            }

            index += _signalLineBatch;
        }

        s = HexRecord.EntryAddress(_downloadAddress);
        await sw.WriteLineAsync(s);

        s = HexRecord.EndOfFile();
        await sw.WriteLineAsync(s);

        await sw.FlushAsync();
        sw.Close();
    }

    public async Task GenerateAsync() {
        var bitmap = Img2Binary.LoadImage(_imagePath);
        FileStream stream = File.Create(_hexPath);
        await BuildAsync(stream, bitmap);
    }
}