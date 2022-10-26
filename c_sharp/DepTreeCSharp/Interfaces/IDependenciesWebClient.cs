using DepTreeCSharp.Models;

namespace DepTreeCSharp.Interfaces;

public interface IDependenciesWebClient
{
    Task<DependenciesResponse> GetDependencies(
        string platform,
        string project,
        CancellationToken cancellationToken = default);
}