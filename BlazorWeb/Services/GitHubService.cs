using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorWeb.Models;

namespace BlazorWeb.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GitHubService> _logger;
    private GitHubRelease? _cachedLatestRelease;
    private DateTime _cacheExpiration = DateTime.MinValue;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public GitHubService(HttpClient httpClient, IConfiguration configuration, ILogger<GitHubService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<GitHubRelease?> GetLatestReleaseAsync()
    {
        // Return cached version if still valid
        if (_cachedLatestRelease != null && DateTime.UtcNow < _cacheExpiration)
        {
            return _cachedLatestRelease;
        }

        try
        {
            var owner = _configuration["GitHub:Owner"] ?? "otherdeniz";
            var repo = _configuration["GitHub:Repository"] ?? "CnC_RulesEditor";
            var url = $"repos/{owner}/{repo}/releases/latest";

            _logger.LogInformation("Fetching latest release from GitHub: {Url}", url);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            var release = JsonSerializer.Deserialize<GitHubRelease>(json, options);

            if (release != null)
            {
                _cachedLatestRelease = release;
                _cacheExpiration = DateTime.UtcNow.Add(_cacheDuration);
                _logger.LogInformation("Successfully fetched release: {Version}", release.TagName);
            }

            return release;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching latest release from GitHub");
            return _cachedLatestRelease; // Return cached version if available
        }
    }

    public async Task<List<GitHubRelease>> GetReleasesAsync(int count = 5)
    {
        try
        {
            var owner = _configuration["GitHub:Owner"] ?? "otherdeniz";
            var repo = _configuration["GitHub:Repository"] ?? "CnC_RulesEditor";
            var url = $"repos/{owner}/{repo}/releases?per_page={count}";

            _logger.LogInformation("Fetching releases from GitHub: {Url}", url);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            var releases = JsonSerializer.Deserialize<List<GitHubRelease>>(json, options);
            return releases ?? new List<GitHubRelease>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching releases from GitHub");
            return new List<GitHubRelease>();
        }
    }
}
