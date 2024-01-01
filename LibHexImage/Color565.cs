namespace LibHexImage;

using System;
using System.Drawing;

public static class Color565 {
    public static ushort Convert(Color value) {
        var r = (value.R & 0b11111000) >> 3;
        var g = (value.G & 0b01111110) >> 1;
        var b = (value.B & 0b00011111) >> 0;
        return System.Convert.ToUInt16((r << 11) | (g << 5) | (b << 0));
    }

    public static Color ConvertBack(ushort color565) {
        throw new NotImplementedException();
    }
}