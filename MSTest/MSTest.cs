using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest;

[TestClass]
[TestCategory("MSTest Framework")]
public class MSTest
{
    [ClassInitialize]
    public static void BeforeAllTests(TestContext context) { }

    [TestInitialize]
    public void BeforeEachTest() { }

    [TestMethod]
    [TestProperty("Severity", "Critical")]
    // MSTest also has: [Priority(10)]
    [Description("Initialize/Cleanup")]
    public void Test1()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [Ignore("Reasons")]
    public void Test2()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Test3()
    {
        Assert.AreEqual(0, 0);
    }

    [TestCleanup]
    public void AfterEachTest() { }

    [ClassCleanup(ClassCleanupBehavior.EndOfClass)]
    public static void AfterAllTests() { }
}
