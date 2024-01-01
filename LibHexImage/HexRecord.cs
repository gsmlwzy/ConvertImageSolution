namespace LibHexImage;


/// <summary>
/// string s = "A6BB03082DE9F0410D6907460C461E46";
/// byte[] data = Convert.FromHexString(s);
/// var str = GenerateHex.Data(0x0090, data);
/// Console.WriteLine(str);
/// str = GenerateHex.EndOfFile();
/// Console.WriteLine(str);
/// str = GenerateHex.HighAddress(0x0801);
/// Console.WriteLine(str);
/// str = GenerateHex.EntryAddress(0x0800D2E5);
/// Console.WriteLine(str);
/// </summary>
public static class HexRecord {
    public static string EntryAddress(uint address) {
        // :040000050800D2E538
        List<byte> list = new List<byte>(Convert.FromHexString("04000005"));
        byte[] addr = BitConverter.GetBytes(address);

        list.AddRange(addr.Reverse());

        list.Add(CheckCode(list));

        string s = Convert.ToHexString(list.ToArray());
        s = s.Insert(0, new string(':', 1));
        return s;
    }

    public static string HighAddress(ushort address) {
        List<byte> list = new List<byte>(Convert.FromHexString("02000004"));
        byte[] addr = BitConverter.GetBytes(address);

        list.AddRange(addr.Reverse());

        list.Add(CheckCode(list));

        string s = Convert.ToHexString(list.ToArray());
        s = s.Insert(0, new string(':', 1));
        return s;
    }

    public static string EndOfFile() => ":00000001FF";

    public static string DataRecode(ushort address, byte[] data) {
        List<byte> list = new List<byte>(data.Length + 13);

        list.Add(Convert.ToByte(data.Length));

        byte[] addr = BitConverter.GetBytes(address);
        list.AddRange(addr.Reverse());

        byte funCode = 0;
        list.Add(funCode);

        list.AddRange(data);

        var cc = CheckCode(list);
        list.Add(cc);

        string s = Convert.ToHexString(list.ToArray());
        s = s.Insert(0, new string(':', 1));
        return s;
    }

    private static byte CheckCode(IList<byte> list) {
        // 校验和正好为0时，只保留低8位就行了
        Int64 sum = (256 - list.Sum(d => Convert.ToInt32(d)) & 0xff) & 0xff;
        return Convert.ToByte(sum);
    }
}