using System.Collections;
using System.Text.RegularExpressions;
using InternalsOld;
using InternalsTest;
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

        actual = false;
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Strings()
    {
        const string actual = "";
        Assert.AreSame("", actual);

        const string phrase = "Hello World!";
        StringAssert.Contains(phrase, "World");
        Assert.Contains("World", phrase);
        // Assert.DoesNotContain("needle", phrase); // Causes StackOverflow???

        StringAssert.StartsWith(phrase, "Hello");
        StringAssert.EndsWith(phrase, "world!", StringComparison.InvariantCultureIgnoreCase);
        StringAssert.Matches(phrase, new Regex("Hello.*"));
        StringAssert.DoesNotMatch(actual, new Regex("needle"));
    }

    [TestMethod]
    public void ExceptionsExactType()
    {
        // [ExpectedException] and Assert.ThrowsException were removed in MSTest 4.0
        // Use Assert.ThrowsExactly instead
        Action sut = () => throw new Exception();
        Assert.ThrowsExactly<Exception>(sut);
    }

    [TestMethod]
    public async Task ExceptionsExactTypeAsync()
    {
        Action sut = () => throw new ArgumentException("cause");
        var ex = Assert.ThrowsExactly<ArgumentException>(sut);
        Assert.AreEqual("cause", ex.Message);

        Func<Task> asyncSut = () => throw new ArgumentException("cause");
        var ex2 = await Assert.ThrowsExactlyAsync<ArgumentException>(asyncSut);
        Assert.AreEqual("cause", ex2.Message);
    }

    [TestMethod]
    public async Task ExceptionsExact()
    {
        Action sut = () => throw new ArgumentException("cause");
        var newSyntax = Assert.ThrowsExactly<ArgumentException>(sut);
        Assert.AreEqual("cause", newSyntax.Message);

        Func<Task> asyncSut = () => throw new ArgumentException("cause");
        var newSyntax2 = await Assert.ThrowsExactlyAsync<ArgumentException>(asyncSut);
        Assert.AreEqual("cause", newSyntax2.Message);
    }

    [TestMethod]
    public async Task ExceptionsDerived()
    {
        Action sut = () => throw new ArgumentException("cause");
        var ex = Assert.Throws<Exception>(sut);
        Assert.IsInstanceOfType<ArgumentException>(ex);

        Func<Task> asyncSut = () => throw new ArgumentException("cause");
        var ex2 = await Assert.ThrowsAsync<Exception>(asyncSut);
        Assert.IsInstanceOfType<ArgumentException>(ex2);
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
        CollectionAssert.AreEquivalent(expected, actual); // Optional IEqualityComparer
        Assert.HasCount(3, actual);
        Assert.IsNotEmpty(actual);

        // CollectionAssert.AreEqual
        // CollectionAssert.DoesNotContain / Contains
        // CollectionAssert.IsSubsetOf();

        // CollectionAssert.AllItemsAreInstancesOfType();
        // CollectionAssert.AllItemsAreNotNull();
        // CollectionAssert.AllItemsAreUnique();

        Assert.ContainsSingle([1]);
    }

    [TestMethod]
    public void Numbers()
    {
        int actual = 42;
        Assert.IsTrue(actual < 100);
    }

    [TestMethod]
    public void Internals()
    {
        bool result = InternalsImpl.DoIt();
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void InternalsOld()
    {
        bool result = InternalsOldImpl.DoIt();
        Assert.IsTrue(result);
    }
}
