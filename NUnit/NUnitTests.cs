using NUnit.Framework;

namespace NUnitTests;

[TestFixture]
[Category("NUnit Framework")]
[Description("SetUp/TearDown for a TestFixture")]
public class NUnitTests
{
    [OneTimeSetUp]
    public void BeforeAllTests() { }

    [SetUp]
    public void BeforeEachTest() { }

    [Test]
    [Property("Severity", "Critical")]
    public void Test1()
    {
        Assert.Inconclusive();
    }

    [Test]
    [Ignore("Reasons")]
    public void Test2()
    {
        Assert.Fail();
    }

    [Test]
    public void Test3()
    {
        Assert.Pass();
    }

    [TearDown]
    public void AfterEachTest() { }

    [OneTimeTearDown]
    public void AfterAllTests() { }
}
