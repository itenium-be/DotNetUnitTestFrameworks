namespace InternalsTest;

/// <summary>
/// Your old legacy codebase has something like this singleton
/// This results in untestable code...
/// </summary>
public class SingletonLegacy
{
    public static readonly SingletonLegacy Instance = new();

    private SingletonLegacy() { }
}
