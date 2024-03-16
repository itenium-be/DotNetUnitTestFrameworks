using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest;

[TestClass]
public class MsTestAssertions
{
    [TestMethod]
    public void Test1()
    {
        bool? actual = true;
        Assert.AreEqual(true, actual);
        Assert.AreNotEqual(false, actual);
        Assert.IsNotNull(actual);
        Assert.IsTrue(actual);
        Assert.IsFalse(!actual);
    }

    [TestMethod]
    public void Strings()
    {
        string actual = "";
        Assert.AreSame("", actual);

        // MSTest does not have a DoesNotContain?
        StringAssert.DoesNotMatch(actual, new Regex("needle"));

        string phrase = "Hello World!";
        StringAssert.Contains(phrase, "World");
        StringAssert.StartsWith(phrase, "Hello");
        StringAssert.EndsWith(phrase, "world!", StringComparison.InvariantCultureIgnoreCase);
        StringAssert.Matches(phrase, new Regex("Hello.*"));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Exceptions()
    {
        Action sut = () => throw new Exception();
        sut();
    }

    [TestMethod]
    public void ExceptionsAlt()
    {
        Action sut = () => throw new Exception();
        Assert.ThrowsException<Exception>(sut);

        Func<Task> asyncSut = () => throw new Exception();
        Assert.ThrowsExceptionAsync<Exception>(asyncSut);
    }

    [TestMethod]
    public void Types()
    {
        ICollection collection = new ArrayList();
        Assert.IsInstanceOfType(collection, typeof(ArrayList));
        Assert.IsNotInstanceOfType<string>(collection);
        // Assert.IsNotInstanceOfType
    }

    [TestMethod]
    public void Collections()
    {
        var expected = new[] { 1, 2, 3 };
        var actual = new[] { 3, 2, 1 };
        CollectionAssert.AreEquivalent(expected, actual);
        // CollectionAssert.AreEqual
        // CollectionAssert.DoesNotContain / Contains
        // CollectionAssert.IsSubsetOf();

        // CollectionAssert.AllItemsAreInstancesOfType();
        // CollectionAssert.AllItemsAreNotNull();
        // CollectionAssert.AllItemsAreUnique();
    }

    [TestMethod]
    public void Numbers()
    {
        int actual = 42;
        Assert.IsTrue(actual < 100);
    }
}
