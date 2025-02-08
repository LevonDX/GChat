using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenAI;
using GChat.Services.Abstract;

namespace GChat.Services.Concrete
{
    public class OpenAIChatService : ILLMChatService
    {
        private readonly ChatClient _chatClient;

        public OpenAIChatService(IConfiguration configuration)
        {
            string apiKey = configuration["OpenAI:Key"] ?? throw new ArgumentException("OpenAI key is missing");
            string model = configuration["OpenAI:Model"] ?? throw new ArgumentException("OpenAI model is missing");

            _chatClient = new ChatClient(model, apiKey);
        }

        public async Task<string> GetResponseAsync(string message)
        {
            string prompt = message;

            string systemMessage = "you are very kind and empyathetic person. You always sey hello in Armenian before answer, but still asnwer in English";

            ChatCompletion response = await _chatClient.CompleteChatAsync(
                    new SystemChatMessage(systemMessage),
                    new UserChatMessage(prompt)
                );

            return response.Content[0].Text;
        }
    }
}
