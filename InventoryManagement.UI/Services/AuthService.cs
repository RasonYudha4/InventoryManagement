using System.Net.Http.Json;
using Blazored.LocalStorage;
using InventoryManagement.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace InventoryManagement.UI.Services;

public class AuthService(
    HttpClient httpClient, 
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider)
{
    public async Task<string?> LoginAsync(LoginRequest request)
    {
        // 1. Send the request to your actual backend API
        var response = await httpClient.PostAsJsonAsync("api/auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            return "Invalid email or password."; // Return the error message
        }

        // 2. Parse the successful response
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            // 3. Save the token to the browser's Local Storage!
            await localStorage.SetItemAsync("authToken", result.Token);
            ((CustomAuthStateProvider)authStateProvider).NotifyUserAuthentication(result.Token);
            return null; // Null means success (no errors)
        }

        return "An unknown error occurred.";
    }

    public async Task LogoutAsync()
    {
        await localStorage.RemoveItemAsync("authToken");
    }
}