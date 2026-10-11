using System.Text.RegularExpressions;
using FrequencyAnalysis.Constants;

namespace FrequencyAnalysis.Services;

public record LetterFrequency(char Letter, int Count, double Percent);

public static class FrequencyAnalyzer
{
    public static int CountLetters(string text) => text.Count(EnglishAlphabet.IsLetter);

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

    public static List<LetterFrequency> SortedByFrequency(string text) =>
        LetterFrequencies(text)
            .OrderByDescending(f => f.Count)
            .ThenBy(f => f.Letter)
            .ToList();

    public static List<(string Word, int Count)> MostFrequentWords(string text, int length, int top)
    {
        return Regex.Matches(text.ToUpperInvariant(), "[A-Z]+")
            .Select(m => m.Value)
            .Where(w => w.Length == length)
            .GroupBy(w => w)
            .Select(g => (Word: g.Key, Count: g.Count()))
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Word)
            .Take(top)
            .ToList();
    }
}
