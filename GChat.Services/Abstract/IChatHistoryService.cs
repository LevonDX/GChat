using GChat.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GChat.Services.Abstract
{
    public interface IChatHistoryService
    {
        Task SaveChatHistoryAsync(ChatHistoryModel chatHistory, Guid userID);
        Task<ChatHistoryModel?> LoadChatHistoryAsync(Guid userID);
    }
}
