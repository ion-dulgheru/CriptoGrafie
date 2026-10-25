using VigenereCipher.Constants;
using VigenereCipher.Services;

namespace VigenereCipher.UI;

public static class ConsolePrinter
{
    private const int ColumnsPerBlock = 16;

    public static void PrintAlphabet()
    {
        Console.WriteLine("Codificarea alfabetului român (31 de litere):");
        for (int start = 0; start < RomanianAlphabet.Length; start += ColumnsPerBlock)
        {
            int end = Math.Min(start + ColumnsPerBlock, RomanianAlphabet.Length);
            Console.Write("  ");
            for (int i = start; i < end; i++)
            {
                Console.Write($"{RomanianAlphabet.Letters[i],3}");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = start; i < end; i++)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void PrintSteps(string input, string key, string output, bool encrypt)
    {
        string extendedKey = VigenereCipherService.ExtendKey(key, input.Length);
        string inputLabel = encrypt ? "Textul clar M" : "Textul cifrat C";
        string outputLabel = encrypt ? "Textul cifrat C" : "Textul clar M";
        string formula = encrypt ? "(M+K) mod 31" : "(C-K) mod 31";

        Console.WriteLine("Tabelul de calcul:");
        for (int start = 0; start < input.Length; start += ColumnsPerBlock)
        {
            int end = Math.Min(start + ColumnsPerBlock, input.Length);
            PrintRow(inputLabel, start, end, i => input[i].ToString());
            PrintRow("Cheia K", start, end, i => extendedKey[i].ToString());
            PrintRow(inputLabel, start, end, i => RomanianAlphabet.IndexOf(input[i]).ToString());
            PrintRow("Cheia K", start, end, i => RomanianAlphabet.IndexOf(extendedKey[i]).ToString());
            PrintRow(formula, start, end, i => RomanianAlphabet.IndexOf(output[i]).ToString());
            PrintRow(outputLabel, start, end, i => output[i].ToString());
            Console.WriteLine();
        }
    }

    private static void PrintRow(string label, int start, int end, Func<int, string> cell)
    {
        Console.Write($"  {label,-16}|");
        for (int i = start; i < end; i++)
        {
            Console.Write($"{cell(i),3}");
        }
        Console.WriteLine();
    }
}
