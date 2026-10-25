public static class InputValidator
{
    public static bool IsValidBits(string? input, int length)
    {
        if (input == null || input.Length != length)
            return false;

        foreach (char c in input)
            if (c != '0' && c != '1')
                return false;

        return true;
    }

    public static string ReadBits(string prompt, int length)
    {
        while (true)
        {
            Console.Write(prompt);
            string? line = Console.ReadLine();
            string cleaned = (line ?? "").Replace(" ", "");

            if (IsValidBits(cleaned, length))
                return cleaned;

            Console.WriteLine($"Eroare: trebuie exact {length} biti (doar 0 si 1). Incearca din nou.");
        }
    }
}