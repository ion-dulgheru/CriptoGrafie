using Lab4DES.Constants;
using Lab4DES.Modules;
using Lab4DES.Services;
using System.Text;

public static class ConsolePrinter
{
    public static string Group(string bits, int size = 4)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < bits.Length; i++)
        {
            if (i > 0 && i % size == 0)
                sb.Append(' ');
            sb.Append(bits[i]);
        }
        return sb.ToString();
    }

    public static void PrintTable(string name, int[] table, int columns)
    {
        Console.WriteLine($"Tabelul {name}:");
        for (int i = 0; i < table.Length; i++)
        {
            Console.Write(table[i].ToString().PadLeft(3));
            if ((i + 1) % columns == 0)
                Console.WriteLine();
        }
    }

    public static void PrintRoundResult(RoundResult r)
    {
        Console.WriteLine();
        Console.WriteLine("=== Sarcina 2.10: calcul Ri ===");
        Console.WriteLine();

        // Etichete S1..S8 deasupra grupelor de 4 biti
        var labels = new StringBuilder();
        for (int j = 1; j <= 8; j++)
            labels.Append(("S" + j).PadRight(5));
        Console.WriteLine("Pas 1: iesirea S-boxurilor");
        Console.WriteLine("        " + labels.ToString().TrimEnd());
        Console.WriteLine("S-out = " + Group(r.SOut));
        Console.WriteLine();

        Console.WriteLine("Pas 2: permutarea P");
        ConsolePrinter.PrintTable("P", DesTables.P, 4);
        Console.WriteLine();
        Console.WriteLine("f = P(S-out) = " + Group(r.F));
        Console.WriteLine();

        Console.WriteLine("Pas 3: Ri = L(i-1) XOR f");
        Console.WriteLine("L(i-1) = " + Group(r.LPrev));
        Console.WriteLine("f      = " + Group(r.F));
        Console.WriteLine("         " + new string('-', 39));
        Console.WriteLine("Ri     = " + Group(r.Ri));
        Console.WriteLine();

        Console.WriteLine("Rezultat:");
        Console.WriteLine("Ri (binar) = " + Group(r.Ri));
        Console.WriteLine("Ri (hex)   = " + BitOperations.BinToHex(r.Ri));
    }
}