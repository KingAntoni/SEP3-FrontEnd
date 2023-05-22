using System.Net.Http.Json;
using Model.Model;

namespace BlazorWASM.Services.Impl;

public class MessageService : IMessageService
{
    
    private readonly HttpClient client = new();
    
    public async Task SendMessageToServer(string message, string sender, string receiver, string timestamp)
    {
        // Create a new instance of MessageModel
        var newMessage = new MessageModel(message, sender, receiver, timestamp);

        try
        {
            // Use HttpClient to make a POST request to your server's API endpoint
            HttpResponseMessage response = await client.PostAsJsonAsync("api/messages", newMessage);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Optionally, you can handle the successful response
            }
            else
            {
                // Handle the unsuccessful response if needed
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occurred during the API call
        }
    }

}