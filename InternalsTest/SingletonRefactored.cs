namespace InternalsTest;

/// <summary>
/// This is how we can refactor this so that we can inject a test double
/// </summary>
public class SingletonRefactored : ISingleton
{
    private static ISingleton _instance = new SingletonRefactored();

    public static ISingleton Instance
    {
        get => _instance;
        internal set => _instance = value;
    }

    private SingletonRefactored() { }
}

public interface ISingleton {}