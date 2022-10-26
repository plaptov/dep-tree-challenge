using System.Collections.Concurrent;
using DepTreeCSharp.Interfaces;
using DepTreeCSharp.Models;

namespace DepTreeCSharp.Services;

public class DependenciesService : IDependenciesService
{
    private readonly ConcurrentDictionary<CacheKey, Task<DependenciesResponse>> _cache = new();
    private readonly IDependenciesWebClient _webClient;

    public DependenciesService(IDependenciesWebClient webClient)
    {
        _webClient = webClient;
    }

    public async Task<DependenciesResponse> GetDependencies(
        string platform,
        string project,
        int depth,
        CancellationToken cancellationToken = default)
    {
        var response = await GetCached(platform, project);
        if (depth > 1)
            await FillChidren(response.Dependencies, 1, depth, cancellationToken);
        return response;
    }

    private async Task FillChidren(IReadOnlyCollection<Dependency> dependencies, int curDepth, int maxDepth, CancellationToken cancellationToken)
    {
        if (curDepth == maxDepth)
            return;
        foreach (var dep in dependencies)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            var r = await GetCached(dep.Platform, dep.ProjectName);
            dep.Children = r.Dependencies;
            await FillChidren(dep.Children, curDepth + 1, maxDepth, cancellationToken);
        }
    }

    private async Task<DependenciesResponse> GetCached(string platform, string project) =>
        await _cache.GetOrAdd(
            new(platform, project),
            key => _webClient.GetDependencies(platform, project));

    private readonly struct CacheKey
    {
        public CacheKey(string platform, string projectId)
        {
            Platform = platform;
            ProjectId = projectId;
        }
        public string Platform { get; }
        public string ProjectId { get; }

        public override bool Equals(object? obj)
        {
            return obj is CacheKey key && ProjectId == key.ProjectId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProjectId, Platform);
        }
    }
}