using System.Text.RegularExpressions;
using FrequencyAnalysis.Constants;

namespace FrequencyAnalysis.Services;

public record LetterFrequency
{
    public char Letter { get; init; }
    public int Count { get; init; }
    public double Percent { get; init; }

    public LetterFrequency(char letter, int count, double percent)
    {
        Letter = letter;
        Count = count;
        Percent = percent;
    }
}

public static class FrequencyAnalyzer
{
    public static int CountLetters(string text)
    {
        return text.Count(EnglishAlphabet.IsLetter);
    }

    public static List<LetterFrequency> LetterFrequencies(string text)
    {
        int[] counts = new int[EnglishAlphabet.Length];
        foreach (char c in text)
        {
            int index = EnglishAlphabet.IndexOf(c);
            if (index >= 0)
            {
                counts[index]++;
            }
        }

        int total = counts.Sum();
        var result = new List<LetterFrequency>(EnglishAlphabet.Length);
        for (int i = 0; i < EnglishAlphabet.Length; i++)
        {
            double percent = total == 0 ? 0 : 100.0 * counts[i] / total;
            result.Add(new LetterFrequency(EnglishAlphabet.Letters[i], counts[i], percent));
        }

        return result;
    }

    public static List<LetterFrequency> SortedByFrequency(string text)
    {
        return LetterFrequencies(text)
            .OrderByDescending(delegate (LetterFrequency f)
            {
                return f.Count;
            })
            .ThenBy(delegate (LetterFrequency f)
            {
                return f.Letter;
            })
            .ToList();
    }

    public static List<(string Word, int Count)> MostFrequentWords(string text, int length, int top)
    {
        return Regex.Matches(text.ToUpperInvariant(), "[A-Z]+")
            .Select(delegate (Match m)
            {
                return m.Value;
            })
            .Where(delegate (string w)
            {
                return w.Length == length;
            })
            .GroupBy(delegate (string w)
            {
                return w;
            })
            .Select(delegate (IGrouping<string, string> g)
            {
                return (Word: g.Key, Count: g.Count());
            })
            .OrderByDescending(delegate ((string Word, int Count) x)
            {
                return x.Count;
            })
            .ThenBy(delegate ((string Word, int Count) x)
            {
                return x.Word;
            })
            .Take(top)
            .ToList();
    }
}