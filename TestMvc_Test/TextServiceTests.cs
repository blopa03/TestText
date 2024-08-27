using Microsoft.Extensions.Configuration;

namespace TestMvc_Test
{
    public class TextServiceTests()
    {
        private TextService _myService;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {            
            var inMemorySettings = new Dictionary<string, string> {
                                        {"patternLettersOnly", "\\p{L}+"},
                                    };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _myService = new TextService(_configuration);

        }
        
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("1133424")]
        [TestCase("[]++´´")]
        public async Task Test_GetWordOrderByMaxLong_IsNUll(string? input)
        {
            var res = await _myService.GetWordOrderByMaxLong(input);
            Assert.That(res, Is.Null);
            Assert.Pass();
        }

        [TestCase("The quick brown fox jumps over the lazy dog", "quick", 5)]
        [TestCase("A journey of a thousand miles begins with a single step", "thousand", 8)]
        [TestCase("123 In the middle 24234  of difficulty @ lies opportunity", "opportunity", 11)]        
        [TestCase("To be yourself in a world that is constantly trying to make you something else is the greatest accomplishment", "accomplishment", 14)]
        public async Task Test_GetWordOrderByMaxLong_ExactWord(string input, string expected, int quantityLetters)
        {            
            var result = await _myService.GetWordOrderByMaxLong(input);

            Assert.Multiple(() =>
            {                
                Assert.That(result.Word, Is.EqualTo(expected));
                Assert.That(result.QuantityLetters, Is.EqualTo(quantityLetters));
                Assert.That(result, Is.Not.Null);
            });
            Assert.Pass();
        }
    }
}