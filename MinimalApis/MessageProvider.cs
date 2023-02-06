public interface IMessageProvider
{
    Task<string> GetMessage(string? name);
}

public class MessageProvider : IMessageProvider
{
    public Task<string> GetMessage(string? name)
    {
        var message = $"Hello {name}";
        return Task.FromResult(message);
    }
}