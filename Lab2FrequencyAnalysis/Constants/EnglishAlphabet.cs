namespace FrequencyAnalysis.Constants;

public static class EnglishAlphabet
{
    public const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int Length = 26;
    public static readonly double[] Frequencies =
    {
        8.17, 1.49, 2.78, 4.25, 12.70, 2.23, 2.01, 6.09, 6.97, 0.15, 0.77, 4.03, 2.41,
        6.75, 7.51, 1.93, 0.09, 5.99, 6.33, 9.06, 2.76, 0.98, 2.36, 0.15, 1.97, 0.07
    };

    public static int IndexOf(char c) => Letters.IndexOf(char.ToUpperInvariant(c));

    public static bool IsLetter(char c) => IndexOf(c) >= 0;

    public static string LettersByFrequency =>
        new string(Letters.OrderByDescending(c => Frequencies[IndexOf(c)]).ToArray());
}
