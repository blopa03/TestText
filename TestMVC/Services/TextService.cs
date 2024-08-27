using System.Text.RegularExpressions;
using TestMVC.Models;
using TestMVC.Services;

public class TextService(IConfiguration conf) : ITextService
{
    private readonly IConfiguration _conf = conf;

    private async Task<IEnumerable<TextModel>> GetWords(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return [];
        }

        // Regex pattern to match sequences of letters including accented characters
        var pattern = _conf.GetValue<string>("patternLettersOnly");
        var matches = Regex.Matches(input, pattern);

        // Convert matches to array of strings
        var wordsArray = matches.Cast<Match>().Select(m => new TextModel
        {
            Word = m.Value,
            QuantityLetters = m.Value.Length
        });

        return wordsArray;
    }

    public async Task<TextModel> GetWordOrderByMaxLong(string input)
    {
        // Find the longest word
        var words = await GetWords(input);
        return words.OrderByDescending(word => word.QuantityLetters).FirstOrDefault();
    }
}
