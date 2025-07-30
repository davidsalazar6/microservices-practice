using System.Net.Http.Json;

namespace OrderService.Services;

public class UserServiceClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public UserServiceClient(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<bool> UserExists(int userId)
    {
        var baseUrl = _config["UserService:BaseUrl"]; // from appsettings
        var response = await _http.GetAsync($"{baseUrl}/api/users/{userId}");
        return response.IsSuccessStatusCode;
    }
}