using GChat.Services.Abstract;
using GChat.Services.Concrete;
using Microsoft.Extensions.Configuration;

namespace GChat.Tests
{
    public class TestChatService
    {
        [Fact]
        public async Task TestOpenAIChatServiceCompletion()
        {
            // Arrange
            string prompt = "Say 'hello' if everything is OK, or 'Error' if there's a problem";

            // Fake config (replace with actual valid key/model for real integration test)
            Dictionary<string, string> inMemorySettings = new Dictionary<string, string>
        {
            { "OpenAI:Key", "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" },

            { "OpenAI:Model", "gpt-3.5-turbo" }
            };

            IConfiguration testConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            OpenAIChatService service = new OpenAIChatService(testConfig);

            // Act
            string result;
            result = await service.GetResponseAsync(prompt);

            // Assert
            Assert.NotNull(result);
            //Assert.True(result.Equals("hello", StringComparison.OrdinalIgnoreCase) == true, $"result is {result}");

            // If you have a real key & model, you might expect "hello" to appear in the text:
            // Assert.Contains("hello", result, StringComparison.OrdinalIgnoreCase);
        }
    }
}
