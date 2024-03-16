using Xunit;

namespace XUnitTests;

public class XUnitTests : IDisposable
{
    public XUnitTests()
    {
        // BeforeEachTest
    }

    [Fact]
    [Trait("Severity", "Critical")]
    public void Test1() { }

    [Fact(Skip = "Reasons")]
    public void Test2()
    {
        Assert.Fail();
    }

    [Fact]
    public void Test3() { }

    public void Dispose()
    {
        // AfterEachTest
    }
}


public class XUnitTestsFixture: IDisposable
{
    public XUnitTestsFixture()
    {
        // BeforeAllTests
    }

    public void Dispose()
    {
        // AfterAllTests
    }
}

public class XUnitTestsWithSetUp : IClassFixture<XUnitTestsFixture>
{
    private readonly XUnitTestsFixture _fixture;

    public XUnitTestsWithSetUp(XUnitTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test1() { }

    [Fact]
    public void Test2() { }
}
