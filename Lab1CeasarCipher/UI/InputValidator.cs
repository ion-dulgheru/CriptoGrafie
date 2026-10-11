using CaesarCipher.Constants;

namespace CaesarCipher.UI;
public static class InputValidator
{
    public static string NormalizeText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }
        return input
            .ToUpperInvariant()
            .Replace(" ", "")
            .Replace('Ş', 'Ș')
            .Replace('Ţ', 'Ț');
    }

    public static int ReadValidKey()
    {
        while (true)
        {
            Console.Write("Introduceti cheie numerica (1-30): ");
            string? rawInput = Console.ReadLine();
            if (int.TryParse(rawInput, out int key) && key >= 1 && key <= 30)
            {
                return key;
            }

            Console.WriteLine("Eroare: Cheia trebuie să fie un număr întreg între 1 și 30.\n");
        }
    }

    public static string ReadValidMessage(string promt)
    {
        while (true)
        {
            Console.Write(promt);
            string? rewInput = Console.ReadLine();
            string normalized = NormalizeText(rewInput ?? string.Empty);

            if (string.IsNullOrEmpty(normalized))
            {
                Console.WriteLine("Eroare: Textul introdus nu poate fi gol! Încercați din nou.\n");
                continue;
            }

            char? invalidChar = null;
            foreach (char c in normalized)
            {
                if (!RomanianAlphabet.Letters.Contains(c))
                {
                    invalidChar = c;
                    break;
                }
            }

            if (invalidChar.HasValue)
            {
                Console.WriteLine($"Eroare: Caracterul '{invalidChar.Value}' nu este permis! Sunt permise doar litere din alfabetul român.\n");
                continue;
            }

            return normalized;
        }
    }

    public static string ReadValidKeyword()
    {
        while (true)
        {
            Console.Write("Introduceți cuvântul-cheie (minim 7 litere): ");
            string? rawInput = Console.ReadLine();
            string normalized = NormalizeText(rawInput ?? string.Empty);

            if (normalized.Length < 7)
            {
                Console.WriteLine($"Eroare: Cuvântul-cheie trebuie să aibă cel puțin 7 litere! Ați introdus doar {normalized.Length}.\n");
                continue;
            }

            char? invalidChar = null;
            foreach (char c in normalized)
            {
                if (!RomanianAlphabet.Letters.Contains(c))
                {
                    invalidChar = c;
                    break;
                }
            }

            if (invalidChar.HasValue)
            {
                Console.WriteLine($"Eroare: Caracterul '{invalidChar.Value}' nu este permis! Sunt permise doar litere din alfabetul român.\n");
                continue;
            }

            return normalized;
        }
    }
} 
