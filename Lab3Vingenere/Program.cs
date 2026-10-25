using System.Text;
using VigenereCipher.Services;
using VigenereCipher.UI;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

while (true)
{
    Console.WriteLine("1. Cifrul Vigenère - Criptare");
    Console.WriteLine("2. Cifrul Vigenère - Decriptare");
    Console.WriteLine("3. Afișare alfabet și codificare (0-30)");
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
        {
            string key = InputValidator.ReadValidKey();
            string text = InputValidator.ReadValidMessage("Introduceți mesajul clar: ");
            string encrypted = VigenereCipherService.Encrypt(text, key);
            Console.WriteLine($"\nMesaj prelucrat: {text}");
            Console.WriteLine($"Cheia:           {key}\n");
            ConsolePrinter.PrintSteps(text, key, encrypted, encrypt: true);
            Console.WriteLine($"Criptograma: {encrypted}\n");
            break;
        }
        case "2":
        {
            string key = InputValidator.ReadValidKey();
            string cipher = InputValidator.ReadValidMessage("Introduceți criptograma: ");
            string decrypted = VigenereCipherService.Decrypt(cipher, key);
            Console.WriteLine($"\nCriptograma prelucrată: {cipher}");
            Console.WriteLine($"Cheia:                  {key}\n");
            ConsolePrinter.PrintSteps(cipher, key, decrypted, encrypt: false);
            Console.WriteLine($"Mesajul decriptat: {decrypted}\n");
            break;
        }
        case "3":
            ConsolePrinter.PrintAlphabet();
            break;
        default:
            Console.WriteLine("Opțiune invalidă! Alegeți o cifră de la 0 la 3.\n");
            break;
    }
}
