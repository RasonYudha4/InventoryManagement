namespace InventoryManagement.UI.Services;

public interface IAiService
{
    Task<string> AskCopilotAsync(string question);
}