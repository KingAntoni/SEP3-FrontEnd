using System.Security.Claims;
using Model.Model;

namespace BlazorWASM.Services.Impl;

public interface IAuthService
{
    Task Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task<UserModel> GetUser(String username);
    
    Task<UserModel> UpdateUser(UserModel user);
    
    Task MatchUser(MatchModel match);

    Task<List<UserModel>> GetUsers(string username);

    Task<List<UserModel>> GetMatches(string username);

    Task<bool> LikeUser(String username1, String username2);

    public String GetCurrentUser();
    
    
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }

    public Task Logout();
    Task<ClaimsPrincipal> GetAuthAsync();
}