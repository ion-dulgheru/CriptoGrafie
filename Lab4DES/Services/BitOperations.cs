using Lab4DES.Constants;
using Lab4DES.Modules;
using System.Text;
namespace Lab4DES.Services;

public static class BitOperations
{
     private static readonly Random Rng=new Random();
    public static string Permute(string bits, int[] table){
        var sb = new StringBuilder(table.Length);
        foreach (int t in table)
        {
            sb.Append(bits[t-1]);
        }
            return sb.ToString();
    }
    public static string Xor(string a,string b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Sirurile trebuie sa aiba aceeasi lungime.");
        }
                var sb = new StringBuilder(a.Length);
                for (int i=0; i < a.Length; i++)
        {
            sb.Append(a[i]==b[i] ? '0' : '1');
        }


        return sb.ToString();
    }
      public static string RandomBits(int length)
    {
        var sb = new StringBuilder(length);
        for (int i=0 ; i<length;i++)
        {
            sb.Append(Rng.Next(2));
        }
        return sb.ToString();
    }
    public static string BinToHex(string bits)
    {
        if (bits.Length % 4 != 0)
            throw new ArgumentException("Lungimea trebuie sa fie multiplu de 4.");

        var sb = new StringBuilder();
        for (int i = 0; i < bits.Length; i += 4)
            sb.Append(Convert.ToInt32(bits.Substring(i, 4), 2).ToString("X"));
        return sb.ToString();
    } 

}