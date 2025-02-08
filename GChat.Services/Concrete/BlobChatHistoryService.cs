using GChat.Services.Abstract;
using GChat.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using Azure.Storage.Blobs.Models;

namespace GChat.Services.Concrete
{
    public class BlobChatHistoryService : IChatHistoryService
    {
        private readonly BlobServiceClient _blobServiceClient;
        const string containerName = "chathistory";

        public BlobChatHistoryService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureStorage") ?? throw new ArgumentNullException("AzureStorage");

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<ChatHistoryModel?> LoadChatHistoryAsync()
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if(!await containerClient.ExistsAsync())
            {
                return new ChatHistoryModel();
            }


            BlobClient blobClient = containerClient.GetBlobClient("chatHistory.json");

            if (!await blobClient.ExistsAsync())
            {
                return new ChatHistoryModel();
            }

            // Download the content
            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
            BinaryData binaryData = downloadResult.Content;

            // Convert the binary data to string (JSON)
            string json = binaryData.ToString();

            // Deserialize to ChatHistoryModel
            ChatHistoryModel? chatHistory = JsonConvert.DeserializeObject<ChatHistoryModel>(json);

            return chatHistory;
        }

        public async Task SaveChatHistoryAsync(ChatHistoryModel chatHistory)
        {

            // 1. Serialize to json
            string json = JsonConvert.SerializeObject(chatHistory);

            // 2. Get or create container
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);


            // 3. save json to container
            BlobClient blobClient = containerClient.GetBlobClient("chatHistory.json");

            await blobClient.UploadAsync(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(json)), true);
        }


    }
}
