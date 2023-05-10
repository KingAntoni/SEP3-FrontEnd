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
    public async Task Login(LoginModel login)
    {
        string userAsJson = JsonSerializer.Serialize(login);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("users/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    public async Task Register(RegisterModel register)
    {
        string userAsJson = JsonSerializer.Serialize(register);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("users/save", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
    public async Task<UserModel> GetUser(String username)
    {
        
        HttpResponseMessage response = await client.GetAsync("users/" + username);
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
        HttpResponseMessage response = await client.PostAsync("users/update", content);
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
}