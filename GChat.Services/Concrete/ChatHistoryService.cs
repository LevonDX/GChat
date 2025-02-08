using GChat.Services.Abstract;
using GChat.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GChat.Services.Concrete
{
    public class ChatHistoryService : IChatHistoryService
    {
        const string fileName = "chatHistory.json";

        /// <summary>
        /// Serialize the chat history to a JSON file
        /// </summary>
        /// <param name="chatHistory"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SaveChatHistoryAsync(ChatHistoryModel chatHistory)
        {
            string json = JsonConvert.SerializeObject(chatHistory);
            await System.IO.File.WriteAllTextAsync(fileName, json);
        }

        /// <summary>
        /// Load the chat history from a JSON file
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ChatHistoryModel?> LoadChatHistoryAsync()
        {
            if (!System.IO.File.Exists(fileName))
            {
                return new ChatHistoryModel();
            }

            string json = await System.IO.File.ReadAllTextAsync(fileName);
            return JsonConvert.DeserializeObject<ChatHistoryModel>(json);
        }
    }
}
