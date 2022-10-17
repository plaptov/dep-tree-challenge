using Microsoft.Extensions.Options;
using DepTreeCSharp.Interfaces;
using DepTreeCSharp.Models;

namespace DepTreeCSharp.WebClients;

public class PlatformsWebClient : WebClientBase, IPlatformsWebClient
{
    public PlatformsWebClient(IOptions<ApiSettings> settings) : base(settings)
    {
    }

    public async Task<IReadOnlyCollection<Platform>> GetPlatforms(CancellationToken cancellationToken)
    {
        return await Get<IReadOnlyCollection<Platform>>("platforms", cancellationToken);
    }
}