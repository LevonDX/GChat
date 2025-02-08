using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GChat.Services.Models
{
    public class ChatHistoryModel
    {
       public List<ChatItemModel> History { get; set; } = new List<ChatItemModel>();
    }
}
