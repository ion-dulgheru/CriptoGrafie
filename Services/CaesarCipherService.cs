using System.Text;
using CaesarCipher.Constants;

namespace CaesarCipher.Services;

public static class CaesarCipherService{
 public static string Encrypt(string plaintext, int key)
    {
        var result = new StringBuilder(plaintext.Length);
        int n = RomanianAlphabet.Length;

        foreach (char c in plaintext)
        {
            int x = RomanianAlphabet.Letters.IndexOf(c);
            int newIndex = (x + key) % n;
            result.Append(RomanianAlphabet.Letters[newIndex]);
        }

        return result.ToString();
    }

    public static string Decrypt(string ciphertext, int key)
    {
        var result = new StringBuilder(ciphertext.Length);
        int n = RomanianAlphabet.Length;

        foreach (char c in ciphertext)
        {
            int y = RomanianAlphabet.Letters.IndexOf(c);
            int newIndex = ((y - key) % n + n) % n;
            result.Append(RomanianAlphabet.Letters[newIndex]);
        }

        return result.ToString();
    }
}
