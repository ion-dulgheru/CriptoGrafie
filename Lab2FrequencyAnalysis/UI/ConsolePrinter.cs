using FrequencyAnalysis.Constants;
using FrequencyAnalysis.Services;

namespace FrequencyAnalysis.UI;

public static class ConsolePrinter
{
    public static void PrintLetterFrequencies(string ciphertext, SubstitutionKey key)
    {
        var sorted = FrequencyAnalyzer.SortedByFrequency(ciphertext);
        string english = EnglishAlphabet.LettersByFrequency;

        Console.WriteLine($"Numărul total de litere: {FrequencyAnalyzer.CountLetters(ciphertext)}\n");
        Console.WriteLine("Rang  Criptat  Apariții      %   Engleză      %   Substituție");
        Console.WriteLine("----  -------  --------  -----   -------  -----   -----------");
        for (int i = 0; i < sorted.Count; i++)
        {
            var f = sorted[i];
            char eng = english[i];
            double engPercent = EnglishAlphabet.Frequencies[EnglishAlphabet.IndexOf(eng)];
            char? plain = key.PlainOf(f.Letter);

            Console.Write($"{i + 1,4}  {f.Letter,7}  {f.Count,8}  {f.Percent,5:F2}   {eng,7}  {engPercent,5:F2}   ");
            if (plain.HasValue)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{f.Letter} → {plain}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("-");
            }
        }
        Console.WriteLine();
    }

    public static void PrintFrequentWords(string ciphertext, SubstitutionKey key)
    {
        for (int length = 1; length <= 4; length++)
        {
            var words = FrequencyAnalyzer.MostFrequentWords(ciphertext, length, 8);
            Console.WriteLine($"Cuvinte de {length} {(length == 1 ? "literă" : "litere")}:");
            foreach (var (word, count) in words)
            {
                Console.Write($"  {word,-6} {count,4}   → ");
                PrintPartial(key.ApplyPartial(word));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public static void PrintPartial(string partialText)
    {
        foreach (char c in partialText)
        {
            if (char.IsLower(c))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(c);
                Console.ResetColor();
            }
            else
            {
                Console.Write(c);
            }
        }
    }

    public static void PrintPartialText(string ciphertext, SubstitutionKey key)
    {
        Console.WriteLine($"Litere descifrate: {key.Count} din {EnglishAlphabet.Length} " +
                          "(MAJUSCULE = încă criptate, minuscule verzi = descifrate)\n");
        PrintPartial(key.ApplyPartial(ciphertext));
        Console.WriteLine("\n");
    }

    public static void PrintKey(SubstitutionKey key)
    {
        string plainAlphabet = EnglishAlphabet.Letters.ToLowerInvariant();
        string cipherAlphabet = key.CipherAlphabet();

        Console.WriteLine("Cheia de criptare (literă clară → literă criptată):");
        Console.WriteLine($"  Alfabet clar:    {string.Join(' ', plainAlphabet.ToCharArray())}");
        Console.WriteLine($"  Alfabet criptat: {string.Join(' ', cipherAlphabet.ToCharArray())}");

        Console.WriteLine("\nCheia de decriptare (literă criptată → literă clară):");
        Console.Write("  Alfabet criptat: ");
        Console.WriteLine(string.Join(' ', EnglishAlphabet.Letters.ToCharArray()));
        Console.Write("  Alfabet clar:    ");
        Console.WriteLine(string.Join(' ', EnglishAlphabet.Letters.Select(c => key.PlainOf(c) ?? '?')));

        Console.WriteLine($"\nLitere descifrate: {key.Count} din {EnglishAlphabet.Length}.\n");
    }
}
