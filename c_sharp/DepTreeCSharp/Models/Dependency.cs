namespace DepTreeCSharp.Models;

public class Dependency
{
    public string ProjectName { get; set; }

    public string Name { get; set; }

    public string Platform { get; set; }

    public string Requirements { get; set; }

    public string LatestStable { get; set; }

    public string Latest { get; set; }

    public bool Deprecated { get; set; }

    public bool Outdated { get; set; }

    public string Kind { get; set; }

    public IReadOnlyCollection<Dependency>? Children { get; set; }
}