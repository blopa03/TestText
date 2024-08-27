using TestMVC.Models;

namespace TestMVC.Services
{
    public interface ITextService
    {
        Task<TextModel> GetWordOrderByMaxLong(string input);
    }
}
