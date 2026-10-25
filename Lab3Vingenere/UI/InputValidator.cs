using VigenereCipher.Constants;

namespace VigenereCipher.UI;

public static class InputValidator
{
    private const string AllowedRange =
        "Sunt permise doar literele 'A'–'Z', 'a'–'z' și diacriticele românești Ă Â Î Ș Ț (ă â î ș ț).";

    public static string NormalizeText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        string withoutSpaces = string.Concat(input.Where(c => !char.IsWhiteSpace(c)));
        return withoutSpaces
            .ToUpperInvariant()
            .Replace('Ş', 'Ș')
            .Replace('Ţ', 'Ț');
    }

    public static string ReadValidMessage(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string normalized = NormalizeText(Console.ReadLine() ?? string.Empty);

            if (string.IsNullOrEmpty(normalized))
            {
                Console.WriteLine("Eroare: Textul introdus nu poate fi gol! Încercați din nou.\n");
                continue;
            }

            char? invalidChar = FindInvalidChar(normalized);
            if (invalidChar.HasValue)
            {
                Console.WriteLine($"Eroare: Caracterul '{invalidChar.Value}' nu este permis! {AllowedRange}\n");
                continue;
            }

            return normalized;
        }
    }

    public static string ReadValidKey()
    {
        while (true)
        {
            Console.Write($"Introduceți cheia (minim {RomanianAlphabet.MinKeyLength} litere): ");
            string normalized = NormalizeText(Console.ReadLine() ?? string.Empty);

            char? invalidChar = FindInvalidChar(normalized);
            if (invalidChar.HasValue)
            {
                Console.WriteLine($"Eroare: Caracterul '{invalidChar.Value}' nu este permis în cheie! {AllowedRange}\n");
                continue;
            }

            if (normalized.Length < RomanianAlphabet.MinKeyLength)
            {
                Console.WriteLine($"Eroare: Cheia trebuie să aibă cel puțin {RomanianAlphabet.MinKeyLength} litere! " +
                                  $"Ați introdus doar {normalized.Length}.\n");
                continue;
            }

            return normalized;
        }
    }

    private static char? FindInvalidChar(string normalized)
    {
        foreach (char c in normalized)
        {
            if (!RomanianAlphabet.IsLetter(c))
            {
                return c;
            }
        }

        return null;
    }
}
