using GChat.Services.Models;

namespace GChat.Models
{
    public class ChatViewModel
    {
        public List<ChatItemModel> History { get; set; } = new List<ChatItemModel>();

        public string? UserMessage { get; set; }
    }
}
