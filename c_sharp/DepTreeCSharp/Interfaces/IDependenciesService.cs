using DepTreeCSharp.Models;

namespace DepTreeCSharp.Interfaces;

public interface IDependenciesService
{
    Task<DependenciesResponse> GetDependencies(
        string platform,
        string project,
        int depth,
        string version = "latest",
        CancellationToken cancellationToken = default);
}