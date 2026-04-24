using BlazorWeb.Models;

namespace BlazorWeb.Services;

public interface IGitHubService
{
    Task<GitHubRelease?> GetLatestReleaseAsync();
    Task<List<GitHubRelease>> GetReleasesAsync(int count = 5);
}
