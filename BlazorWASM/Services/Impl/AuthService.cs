using System.Text;
using System.Text.Json;
using BlazorWASM.Pages;
using BlazorWASM.Services.Impl;
using Microsoft.AspNetCore.Components;
using Model.Model;

namespace BlazorWASM.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly HttpClient client = new();
    private String currentUser;
    public async Task Login(LoginModel login)
    {
        string userAsJson = JsonSerializer.Serialize(login);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }currentUser= login.Username;
    }
    public async Task Register(RegisterModel register)
    {
        Console.WriteLine(register.ToString());
        string userAsJson = JsonSerializer.Serialize(register);
        Console.WriteLine(userAsJson);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");Console.WriteLine(content);
        HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/save", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    public async Task<UserModel> GetUser(String username)
    {
        
        HttpResponseMessage response = await client.GetAsync("http://localhost:8090/users/" + username);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        UserModel user = JsonSerializer.Deserialize<UserModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
    
    public async Task<UserModel> UpdateUser(UserModel user)
    {
        string userAsJson = JsonSerializer.Serialize(user);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/update", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        UserModel retUser = JsonSerializer.Deserialize<UserModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return retUser;
        
    }
    public async Task MatchUser(MatchModel match)
    {
        string userAsJson = JsonSerializer.Serialize(match);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/match" , content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    public async Task<List<UserModel>> GetUsers(string username)
    {
        string apiUrl = "http://localhost:8090/users";
    
        if (!string.IsNullOrEmpty(username))
        {
            apiUrl += "?username=" + Uri.EscapeDataString(username);
        }

        HttpResponseMessage response = await client.GetAsync(apiUrl);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        List<UserModel> users = JsonSerializer.Deserialize<List<UserModel>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
    
    public async Task<List<UserModel>> GetMatches(string username)
    {
        string apiUrl = "http://localhost:8090/users/matches";
    
        if (!string.IsNullOrEmpty(username))
        {
            apiUrl += "?username=" + Uri.EscapeDataString(username);
        }

        HttpResponseMessage response = await client.GetAsync(apiUrl);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        List<UserModel> users = JsonSerializer.Deserialize<List<UserModel>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task<bool> LikeUser(String username1, String username2)
    {
        HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/matches/?username1="+username1+"&username2="+username2  , null);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
        var apiResponse = JsonSerializer.Deserialize<APIResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return apiResponse.Success;
    }
    
    public String GetCurrentUser()
    {
        return currentUser;
    }

}