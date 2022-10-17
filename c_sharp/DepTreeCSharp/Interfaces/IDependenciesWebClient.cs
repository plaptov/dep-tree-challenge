using DepTreeCSharp.Models;

namespace DepTreeCSharp.Interfaces;

public interface IDependenciesWebClient
{
    Task<DependenciesResponse> GetDependencies(
        string platform,
        string project,
        string version = "latest",
        CancellationToken cancellationToken = default);
}