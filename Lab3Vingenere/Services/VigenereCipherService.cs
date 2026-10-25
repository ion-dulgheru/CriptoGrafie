using System.Text;
using VigenereCipher.Constants;

namespace VigenereCipher.Services;

public static class VigenereCipherService
{
    public static string Encrypt(string plaintext, string key)
    {
        var result = new StringBuilder(plaintext.Length);
        int n = RomanianAlphabet.Length;

        for (int i = 0; i < plaintext.Length; i++)
        {
            int m = RomanianAlphabet.IndexOf(plaintext[i]);
            int k = RomanianAlphabet.IndexOf(key[i % key.Length]);
            result.Append(RomanianAlphabet.Letters[(m + k) % n]);
        }

        return result.ToString();
    }

    public static string Decrypt(string ciphertext, string key)
    {
        var result = new StringBuilder(ciphertext.Length);
        int n = RomanianAlphabet.Length;

        for (int i = 0; i < ciphertext.Length; i++)
        {
            int c = RomanianAlphabet.IndexOf(ciphertext[i]);
            int k = RomanianAlphabet.IndexOf(key[i % key.Length]);
            result.Append(RomanianAlphabet.Letters[((c - k) % n + n) % n]);
        }

        return result.ToString();
    }

    public static string ExtendKey(string key, int length)
    {
        var result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(key[i % key.Length]);
        }

        return result.ToString();
    }
}
