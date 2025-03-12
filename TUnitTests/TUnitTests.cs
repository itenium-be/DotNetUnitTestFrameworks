using TUnit.Core;
using TUnit.Core.Interfaces;

namespace TUnitTests;

public class TUnitSetup
{
    [Before(Assembly)]
    public static async Task AssemblySetup() { }

    [After(Assembly)]
    public static async Task AssemblyTeardown()
    {
        // Runs after the last test in this assembly
    }

    [AfterEvery(Assembly)]
    public static async Task AllAssembliesTeardown(AssemblyHookContext context)
    {
        // Runs after the last test in all assemblies
    }
}

[Category("TUnit Framework")]
public class TUnitTests
{
    [Before(Class)]
    public static async Task BeforeAllTests() { }

    [Before(Test)]
    public async Task BeforeEachTest() { }

    [Test]
    [Property("Severity", "Critical")]
    public void Test1() { }

    [Test]
    [Skip("Reasons")]
    public void Test2()
    {
        Assert.Fail("reason");
    }

    [After(Test)]
    public async Task AfterEachTest() { }

    [After(Class)]
    public static async Task AfterAllTests() { }
}
