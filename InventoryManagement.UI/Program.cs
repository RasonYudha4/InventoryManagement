using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using InventoryManagement.UI;
using Blazored.LocalStorage;
using InventoryManagement.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using InventoryManagement.UI.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5028/") });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IAiService, AiService>();

await builder.Build().RunAsync();
