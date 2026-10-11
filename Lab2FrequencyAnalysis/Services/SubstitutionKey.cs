using System.Text;
using FrequencyAnalysis.Constants;

namespace FrequencyAnalysis.Services;

public class SubstitutionKey
{
    private readonly Dictionary<char, char> _cipherToPlain = new();

    public int Count => _cipherToPlain.Count;

    public bool IsComplete => Count == EnglishAlphabet.Length;

    public IReadOnlyDictionary<char, char> Mappings => _cipherToPlain;

    public string? Set(char cipherLetter, char plainLetter)
    {
        cipherLetter = char.ToUpperInvariant(cipherLetter);
        plainLetter = char.ToLowerInvariant(plainLetter);

        foreach (var (cipher, plain) in _cipherToPlain)
        {
            if (plain == plainLetter && cipher != cipherLetter)
            {
                return $"litera clară '{plainLetter}' este deja imaginea lui '{cipher}'. Ștergeți mai întâi substituția {cipher} → {plain}.";
            }
        }

        _cipherToPlain[cipherLetter] = plainLetter;
        return null;
    }

    public bool Remove(char cipherLetter) => _cipherToPlain.Remove(char.ToUpperInvariant(cipherLetter));

    public void Clear() => _cipherToPlain.Clear();

    public char? PlainOf(char cipherLetter) =>
        _cipherToPlain.TryGetValue(char.ToUpperInvariant(cipherLetter), out char plain) ? plain : null;

    public string ApplyPartial(string ciphertext)
    {
        var result = new StringBuilder(ciphertext.Length);
        foreach (char c in ciphertext)
        {
            if (!EnglishAlphabet.IsLetter(c))
            {
                result.Append(c);
                continue;
            }

            char? plain = PlainOf(c);
            result.Append(plain ?? char.ToUpperInvariant(c));
        }

        return result.ToString();
    }

    public string Decrypt(string ciphertext)
    {
        var result = new StringBuilder(ciphertext.Length);
        foreach (char c in ciphertext)
        {
            char? plain = EnglishAlphabet.IsLetter(c) ? PlainOf(c) : null;
            if (plain is null)
            {
                result.Append(c);
            }
            else
            {
                result.Append(char.IsUpper(c) ? char.ToUpperInvariant(plain.Value) : plain.Value);
            }
        }

        return result.ToString();
    }

    public string CipherAlphabet()
    {
        var plainToCipher = _cipherToPlain.ToDictionary(kv => kv.Value, kv => kv.Key);
        var result = new StringBuilder(EnglishAlphabet.Length);
        foreach (char p in EnglishAlphabet.Letters.ToLowerInvariant())
        {
            result.Append(plainToCipher.TryGetValue(p, out char c) ? c : '?');
        }

        return result.ToString();
    }
}
