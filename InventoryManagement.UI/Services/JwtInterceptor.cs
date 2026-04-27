using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace InventoryManagement.UI.Services; // Adjust to match your folder

public class JwtInterceptor(ILocalStorageService localStorage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // 1. Grab the token from browser storage
        var token = await localStorage.GetItemAsync<string>("authToken");

        // 2. If the user is logged in, attach the Bearer token!
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // 3. Let the HTTP request continue to the API
        return await base.SendAsync(request, cancellationToken);
    }
}