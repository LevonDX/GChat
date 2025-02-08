using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GChat.Services.Abstract
{
    public interface ILLMChatService
    {
        Task<string> GetResponseAsync(string message);

    }
}
