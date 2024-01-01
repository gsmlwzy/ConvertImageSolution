namespace LibHexImage;

public static class ExtendUtil {
    public static void Print<T>(this IEnumerable<T> list) where T : IFormattable {
        foreach (var item in list) {
            Console.Write($"{item} ");
        }

        Console.WriteLine();
    }

    public static void Print(this IEnumerable<string> list) {
        foreach (var item in list) {
            Console.Write($"{item} ");
        }

        Console.WriteLine();
    }

    public static void PrintHex<T>(this IEnumerable<T> list) where T : IFormattable {
        foreach (var item in list) {
            Console.Write($"{item:x2} ");
        }

        Console.WriteLine();
    }
}