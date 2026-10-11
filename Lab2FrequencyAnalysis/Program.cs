using System.Text;
using FrequencyAnalysis.Services;
using FrequencyAnalysis.UI;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

string variantPath = Path.Combine(AppContext.BaseDirectory, "Data", "variant10.txt");
string ciphertext = File.Exists(variantPath) ? File.ReadAllText(variantPath).Trim() : string.Empty;
var key = new SubstitutionKey();

if (!ciphertext.Any(char.IsAsciiLetter))
{
    Console.WriteLine($"Eroare: Fișierul {variantPath} lipsește sau nu conține nicio literă A–Z.");
    Console.WriteLine("Scrieți criptograma în Data/variant10.txt și porniți din nou programul.");
    return;
}

Console.WriteLine($"Criptograma a fost încărcată din Data/variant10.txt ({FrequencyAnalyzer.CountLetters(ciphertext)} litere).\n");

while (true)
{
    Console.WriteLine("1. Frecvența literelor (comparație cu limba engleză)");
    Console.WriteLine("2. Cele mai frecvente cuvinte scurte");
    Console.WriteLine("3. Adăugare substituții (ex: V=e W=t)");
    Console.WriteLine("4. Ștergere substituție");
    Console.WriteLine("5. Afișare text parțial decriptat");
    Console.WriteLine("6. Afișare cheie curentă");
    Console.WriteLine("7. Salvare text decriptat în fișier");
    Console.WriteLine("0. Ieșire");
    Console.Write("\nAlegeți o opțiune: ");

    string? option = Console.ReadLine();
    Console.WriteLine();

    if (option == "0")
    {
        Console.WriteLine("La revedere!");
        break;
    }

    switch (option)
    {
        case "1":
            ConsolePrinter.PrintLetterFrequencies(ciphertext, key);
            break;
        case "2":
            ConsolePrinter.PrintFrequentWords(ciphertext, key);
            break;
        case "3":
        {
            foreach (var (cipher, plain) in InputValidator.ReadSubstitutions())
            {
                string? error = key.Set(cipher, plain);
                Console.WriteLine(error == null
                    ? $"  Adăugat: {cipher} → {plain}"
                    : $"  Eroare: {error}");
            }
            Console.WriteLine();
            ConsolePrinter.PrintPartialText(ciphertext, key);
            if (key.IsComplete)
            {
                Console.WriteLine("Toate cele 26 de litere au fost descifrate!\n");
            }
            break;
        }
        case "4":
        {
            char letter = InputValidator.ReadLetter("Introduceți litera criptată a cărei substituție se șterge: ");
            Console.WriteLine(key.Remove(letter)
                ? $"Substituția pentru '{letter}' a fost ștearsă.\n"
                : $"Eroare: Litera '{letter}' nu are nicio substituție.\n");
            break;
        }
        case "5":
            ConsolePrinter.PrintPartialText(ciphertext, key);
            break;
        case "6":
            ConsolePrinter.PrintKey(key);
            break;
        case "7":
        {
            string outputPath = Path.Combine(Environment.CurrentDirectory, "decrypted.txt");
            File.WriteAllText(outputPath, key.Decrypt(ciphertext) + Environment.NewLine);
            Console.WriteLine($"Textul decriptat a fost salvat în: {outputPath}");
            if (!key.IsComplete)
            {
                Console.WriteLine($"Atenție: cheia are doar {key.Count} din 26 de litere; literele necunoscute au rămas criptate.");
            }
            Console.WriteLine();
            break;
        }
        default:
            Console.WriteLine("Opțiune invalidă! Alegeți o cifră de la 0 la 7.\n");
            break;
    }
}
