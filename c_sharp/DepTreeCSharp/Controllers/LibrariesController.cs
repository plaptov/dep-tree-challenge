using Microsoft.AspNetCore.Mvc;
using DepTreeCSharp.Interfaces;
using DepTreeCSharp.Models;

namespace DepTreeCSharp.Controllers;

[ApiController]
[Route("libraries")]
public class LibrariesController : ControllerBase
{
    private readonly IPlatformsWebClient _platformsWebClient;
    private readonly IDependenciesService _dependenciesService;

    public LibrariesController(
        IPlatformsWebClient platformsWebClient,
        IDependenciesService dependenciesService)
    {
        _platformsWebClient = platformsWebClient;
        _dependenciesService = dependenciesService;
    }

    [HttpGet("platforms")]
    public async Task<IReadOnlyCollection<Platform>> GetIndex(CancellationToken cancellationToken = default)
    {
        return await _platformsWebClient.GetPlatforms(cancellationToken);
    }

    [HttpGet("dependencies/{platform}/{project}")]
    public async Task<DependenciesResponse> GetDependencies(
        [FromRoute] string platform,
        [FromRoute] string project,
        [FromQuery] int depth = 0,
        [FromQuery] string version = "latest",
        CancellationToken cancellationToken = default)
    {
        return await _dependenciesService.GetDependencies(platform, project, depth, version, cancellationToken);
    }
}
