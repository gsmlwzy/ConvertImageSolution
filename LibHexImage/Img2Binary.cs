namespace LibHexImage;


using System.Drawing;
using System.Net.NetworkInformation;

/// <summary>
/// 图像展开为一个u16集合，前面2个byte表示图片高度和宽度
/// </summary>
public static class Img2Binary {
    public static Bitmap LoadImage(string imagePath) {
        if (!File.Exists(imagePath))
            throw new FileNotFoundException(
                $"cannot find bmp file, check the path\n  CurrentPath: {Directory.GetCurrentDirectory()}\n");
        // BMP、GIF、EXIF、JPG、PNG 和 TIFF
        Bitmap bmp = new Bitmap(imagePath);
        return bmp;
    }
    
    public static List<ushort> ConvertBmp(Bitmap bmp, bool reverseColor, bool reverseByte) {
        List<ushort> list = new List<ushort>(bmp.Width * bmp.Height + 2);

        list.Add(Convert.ToUInt16((bmp.Height | bmp.Width << 8) & 0xffff));

        for (int y = 0; y < bmp.Height; y++) {
            for (int x = 0; x < bmp.Width; x++) {
                ushort color565 = Color565.Convert(bmp.GetPixel(x, y));
                var mid = reverseColor ? Convert.ToUInt16(~color565 & 0xffff) : color565;  // 像素取反
                var tmp = (mid >> 8) | (mid << 8);
                mid = reverseByte ? Convert.ToUInt16(tmp & 0xffff) : mid;    // 交换高8位和低8位
                list.Add(Convert.ToUInt16(mid & 0xffff));
            }
        }
        return list;
    }
}
