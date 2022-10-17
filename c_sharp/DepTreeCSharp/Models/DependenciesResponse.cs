namespace DepTreeCSharp.Models;

public class DependenciesResponse
{
    public string DependenciesForVersion { get; set; }

    public List<Dependency> Dependencies { get; set; }
}