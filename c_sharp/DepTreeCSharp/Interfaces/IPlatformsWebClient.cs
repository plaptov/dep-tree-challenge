using DepTreeCSharp.Models;

namespace DepTreeCSharp.Interfaces;

public interface IPlatformsWebClient
{
    Task<IReadOnlyCollection<Platform>> GetPlatforms(CancellationToken cancellationToken);
}