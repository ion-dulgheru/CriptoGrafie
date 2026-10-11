using FrequencyAnalysis.Constants;

namespace FrequencyAnalysis.UI;

public static class InputValidator
{
    public static List<(char Cipher, char Plain)> ReadSubstitutions()
    {
        while (true)
        {
            Console.Write("Introduceți substituțiile (ex: V=e W=t): ");
            string rawInput = Console.ReadLine() ?? string.Empty;
            string[] tokens = rawInput.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 0)
            {
                Console.WriteLine("Eroare: Nu ați introdus nicio substituție! Încercați din nou.\n");
                continue;
            }

            var result = new List<(char, char)>();
            string? error = null;
            foreach (string token in tokens)
            {
                string pair = token.Replace("=", "").Replace("->", "").Replace("→", "");
                if (pair.Length != 2 || !EnglishAlphabet.IsLetter(pair[0]) || !EnglishAlphabet.IsLetter(pair[1]))
                {
                    error = $"Eroare: '{token}' nu este o substituție validă! Folosiți forma LiteraCriptată=literaClară, de exemplu V=e.\n";
                    break;
                }

                result.Add((char.ToUpperInvariant(pair[0]), char.ToLowerInvariant(pair[1])));
            }

            if (error != null)
            {
                Console.WriteLine(error);
                continue;
            }

            return result;
        }
    }

    public static char ReadLetter(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string rawInput = (Console.ReadLine() ?? string.Empty).Trim();
            if (rawInput.Length == 1 && EnglishAlphabet.IsLetter(rawInput[0]))
            {
                return char.ToUpperInvariant(rawInput[0]);
            }

            Console.WriteLine("Eroare: Introduceți o singură literă din alfabetul englez (A–Z).\n");
        }
    }
}
