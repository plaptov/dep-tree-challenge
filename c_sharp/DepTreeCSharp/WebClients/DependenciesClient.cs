using Microsoft.Extensions.Options;
using DepTreeCSharp.Interfaces;
using DepTreeCSharp.Models;

namespace DepTreeCSharp.WebClients;

public class DependenciesWebClient : WebClientBase, IDependenciesWebClient
{
    public DependenciesWebClient(IOptions<ApiSettings> settings) : base(settings)
    {
    }

    public async Task<DependenciesResponse> GetDependencies(
        string platform,
        string project,
        CancellationToken cancellationToken = default)
    {
        return await Get<DependenciesResponse>($"{platform}/{project}/latest/dependencies", cancellationToken);
    }
}