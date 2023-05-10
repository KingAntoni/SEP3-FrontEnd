using Model.Model;

namespace BlazorWASM.Services.Impl;

public interface IAuthService
{
    Task Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task<UserModel> GetUser(String username);
    
    Task<UserModel> UpdateUser(UserModel user);
}