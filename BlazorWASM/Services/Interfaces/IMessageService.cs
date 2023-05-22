namespace BlazorWASM.Services.Impl;

public interface IMessageService
{
    Task SendMessageToServer(string message, string sender, string receiver, string timestamp);
}