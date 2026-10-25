namespace VigenereCipher.Constants;

public static class RomanianAlphabet
{
    public const string Letters = "AĂÂBCDEFGHIÎJKLMNOPQRSȘTȚUVWXYZ";

    public const int Length = 31;

    public const int MinKeyLength = 7;

    public static int IndexOf(char c) => Letters.IndexOf(c);

    public static bool IsLetter(char c) => IndexOf(c) >= 0;
}
