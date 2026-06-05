namespace AgentAI.Services;

public interface IChatService
{
    Task<string> AskAsync(string prompt);
}
