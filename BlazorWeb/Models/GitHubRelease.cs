namespace BlazorWeb.Models;

public class GitHubRelease
{
    public string TagName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public string HtmlUrl { get; set; } = string.Empty;
    public List<GitHubAsset> Assets { get; set; } = new();
}

public class GitHubAsset
{
    public string Name { get; set; } = string.Empty;
    public string BrowserDownloadUrl { get; set; } = string.Empty;
    public long Size { get; set; }
    public int DownloadCount { get; set; }
}
