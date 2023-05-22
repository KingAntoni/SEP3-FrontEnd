using System.Net.Http.Headers;
using System.Security.Claims;
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
    public static string? Jwt { get; private set; } = "";
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
        
    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }
    
    public Task Logout()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
    
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
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
        using (HttpClient client = new HttpClient())
        {
                MultipartFormDataContent formData = new MultipartFormDataContent();

                // Add user data fields
                formData.Add(new StringContent(register.Email), "Email");
                formData.Add(new StringContent(register.Username), "Username");
                formData.Add(new StringContent(register.Password), "Password");
                formData.Add(new StringContent(register.ConfirmPassword), "ConfirmPassword");
                formData.Add(new StringContent(register.FirstName), "FirstName");
                formData.Add(new StringContent(register.LastName), "LastName");
                formData.Add(new StringContent(register.Birthday.ToString()), "Birthday");
                formData.Add(new StringContent(register.Description), "Description");

                // Add pictures
                for (int i = 0; i < register.Pictures.Count; i++)
                {
                    byte[] pictureData = Convert.FromBase64String(register.Pictures[i]);
                    ByteArrayContent pictureContent = new ByteArrayContent(pictureData);
                    pictureContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    formData.Add(pictureContent, $"Picture{i + 1}", $"Picture{i + 1}.jpg");
                }

                HttpResponseMessage response = await client.PostAsync("http://localhost:8090/users/save", formData);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(responseContent);
                }
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