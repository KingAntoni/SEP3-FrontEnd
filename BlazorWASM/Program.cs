using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWasm.Auth;
using BlazorWASM.Services.Impl;
using BlazorWASM.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Model.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8090/") });
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
await builder.Build().RunAsync();