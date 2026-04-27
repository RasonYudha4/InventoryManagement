using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Services.AI.Plugins;
using OpenAI;
using System.ClientModel;

namespace InventoryManagement.Infrastructure.Services.AI;

public class SemanticKernelCopilotService : IAiCopilotService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatCompletionService;
    private readonly ChatHistory _chatHistory = new ChatHistory("You are a helpful inventory management assistant. You answer questions about warehouse stock levels using the database tools provided.");

    public SemanticKernelCopilotService(IConfiguration config, ApplicationDbContext context)
    {
       var endpoint = config["GrokAi:Endpoint"] ?? throw new InvalidOperationException("HF Endpoint missing");
        var modelId = config["GrokAi:ModelId"] ?? throw new InvalidOperationException("HF ModelId missing");
        var apiKey = config["GrokAi:ApiKey"] ?? throw new InvalidOperationException("HF ApiKey missing");

        var builder = Kernel.CreateBuilder();
        
        var openAIClient = new OpenAIClient(
            new ApiKeyCredential(apiKey), 
            new OpenAIClientOptions { Endpoint = new Uri(endpoint) }
        );
        
        builder.AddOpenAIChatCompletion(modelId, openAIClient);
        
        builder.Plugins.AddFromObject(new WarehousePlugin(context));

        _kernel = builder.Build();
        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task<string> AskWarehouseQuestionAsync(string userPrompt, string userId)
    {
        // 1. Add the user's question to the chat history
        _chatHistory.AddUserMessage(userPrompt);

        // THE FIX: Explicitly authorize Semantic Kernel to send your plugins to Groq!
        var executionSettings = new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        // 2. Pass the settings into the chat completion service
        var response = await _chatCompletionService.GetChatMessageContentAsync(
            _chatHistory, 
            executionSettings, // <-- Pass the authorization here!
            _kernel            // <-- Make sure the kernel is passed so it can execute the C# code
        );

        // 3. Save the AI's response to history and return it
        _chatHistory.AddAssistantMessage(response.Content ?? "");
        
        return response.Content ?? "I couldn't generate an answer.";
    }

    public Task<string> ProcessVoiceCommandAsync(byte[] audioStream)
    {
        // We will build this out when we attach the Speech service later!
        throw new NotImplementedException();
    }
}