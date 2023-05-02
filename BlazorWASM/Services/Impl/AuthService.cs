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
        HttpResponseMessage response = await client.PostAsync("auth/login", content);
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
        HttpResponseMessage response = await client.PostAsync("auth/register", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}