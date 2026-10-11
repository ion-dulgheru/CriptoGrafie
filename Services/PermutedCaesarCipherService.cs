using System.Text;
using CaesarCipher.Constants;

namespace CaesarCipher.Services;

public static class PermutedCaesarCipherService
{
    public static string BuildPermutedAlphabet(string keyword)
    {
        var permuted = new StringBuilder(RomanianAlphabet.Length);

        foreach (char c in keyword)
        {
            if (!permuted.ToString().Contains(c))
            {
                permuted.Append(c);
            }
        }

        foreach (char c in RomanianAlphabet.Letters)
        {
            if (!permuted.ToString().Contains(c))
            {
                permuted.Append(c);
            }
        }

        return permuted.ToString();
    }

    public static string Encrypt(string plaintext, int key, string permutedAlphabet)
    {
        var result = new StringBuilder(plaintext.Length);
        int n = permutedAlphabet.Length;

        foreach (char c in plaintext)
        {
            int x = permutedAlphabet.IndexOf(c);
            int newIndex = (x + key) % n;
            result.Append(permutedAlphabet[newIndex]);
        }

        return result.ToString();
    }

    public static string Decrypt(string ciphertext, int key, string permutedAlphabet)
    {
        var result = new StringBuilder(ciphertext.Length);
        int n = permutedAlphabet.Length;

        foreach (char c in ciphertext)
        {
            int y = permutedAlphabet.IndexOf(c);
            int newIndex = ((y - key) % n + n) % n;
            result.Append(permutedAlphabet[newIndex]);
        }

        return result.ToString();
    }
}