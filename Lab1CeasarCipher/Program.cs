using System.Text;
using CaesarCipher.Services;
using CaesarCipher.UI;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

while (true)
{
   
    Console.WriteLine("1. Cifrul Cezar Clasic - Criptare");
    Console.WriteLine("2. Cifrul Cezar Clasic - Decriptare");
    Console.WriteLine("3. Cifrul Cezar cu 2 Chei - Criptare");
    Console.WriteLine("4. Cifrul Cezar cu 2 Chei - Decriptare");
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
            int key = InputValidator.ReadValidKey();
            string text = InputValidator.ReadValidMessage("Introduceți mesajul clar: ");
            string encrypted = CaesarCipherService.Encrypt(text, key);
            Console.WriteLine($"\nRezultat criptat: {encrypted}\n");
            break;
        }
        case "2":
        {
            int key = InputValidator.ReadValidKey();
            string cipher = InputValidator.ReadValidMessage("Introduceți criptotextul: ");
            string decrypted = CaesarCipherService.Decrypt(cipher, key);
            Console.WriteLine($"\nRezultat decriptat: {decrypted}\n");
            break;
        }
        case "3":
        {
            int key1 = InputValidator.ReadValidKey();
            string key2 = InputValidator.ReadValidKeyword();
            
            string permutedAlphabet = PermutedCaesarCipherService.BuildPermutedAlphabet(key2);
            Console.WriteLine($"\nAlfabet permutat: {permutedAlphabet}");

            string text = InputValidator.ReadValidMessage("Introduceți mesajul clar: ");
            string encrypted = PermutedCaesarCipherService.Encrypt(text, key1, permutedAlphabet);
            Console.WriteLine($"\nRezultat criptat: {encrypted}\n");
            break;
        }
        case "4":
        {
            int key1 = InputValidator.ReadValidKey();
            string key2 = InputValidator.ReadValidKeyword();

            string permutedAlphabet = PermutedCaesarCipherService.BuildPermutedAlphabet(key2);
            Console.WriteLine($"\nAlfabet permutat: {permutedAlphabet}");

            string cipher = InputValidator.ReadValidMessage("Introduceți criptotextul: ");
            string decrypted = PermutedCaesarCipherService.Decrypt(cipher, key1, permutedAlphabet);
            Console.WriteLine($"\nRezultat decriptat: {decrypted}\n");
            break;
        }
        default:
            Console.WriteLine("Opțiune invalidă! Alegeți o cifră de la 0 la 4.\n");
            break;
    }
}