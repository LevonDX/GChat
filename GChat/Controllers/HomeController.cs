using System.Diagnostics;
using GChat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GChat.Services.Abstract;
using GChat.Services.Models;
using System.Security.Claims;

namespace GChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILLMChatService _chatService;
        private readonly IChatHistoryService _chatHistoryService;

        public HomeController(ILogger<HomeController> logger,
            ILLMChatService chatService,
            IChatHistoryService chatHistoryService)
        {
            _logger = logger;
            _chatService = chatService;
            _chatHistoryService = chatHistoryService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Trying to load chat history");
            ChatHistoryModel? chatHistory = await _chatHistoryService.LoadChatHistoryAsync();

            if (chatHistory == null)
            {
                _logger.LogInformation("Chat history is empty");
            }
            else
            {
                _logger.LogInformation("Chat history loaded");
            }

            ChatViewModel model = new ChatViewModel
            {
                History = chatHistory?.History ?? new List<ChatItemModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View("Index", model);
            }

            string completion = await _chatService.GetResponseAsync(model.UserMessage ?? "");

            ChatHistoryModel? chatHistoryModel = await _chatHistoryService.LoadChatHistoryAsync();

            model.History = chatHistoryModel?.History ?? new List<ChatItemModel>();

            model.History.Add(new ChatItemModel
            {
                UserMessage = model.UserMessage,
                BotMessage = completion
            });

            await _chatHistoryService.SaveChatHistoryAsync(new ChatHistoryModel
            {
                History = model.History
            });

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
