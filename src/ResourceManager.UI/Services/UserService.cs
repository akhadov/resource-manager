using ResourceManager.Application.Users.GetUser;
using ResourceManager.UI.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace ResourceManager.UI.Services;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;

    public UserService(HttpClient http)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<UserResponse> GetUser(Guid id)
    {
        try
        {
            var user = await _http.GetFromJsonAsync<UserResponse>($"users/{id}");

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<UserResponse>?> GetUsers()
    {
        try
        {
            var apiResponse = await _http.GetStreamAsync("users");

            var users = await JsonSerializer.DeserializeAsync<List<UserResponse>>(apiResponse, _serializerOptions);

            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
