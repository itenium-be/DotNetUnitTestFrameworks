using InternalsTest;
using System.Collections;
using InternalsOld;
using Xunit;

namespace XUnitTests;

public class XunitAssertions
{
    [Fact]
    public void Test1()
    {
        bool? actual = true;
        Assert.Equal(true, actual);
        Assert.NotEqual(false, actual);
        Assert.NotNull(actual);
        Assert.True(actual);

        actual = false;
        Assert.False(actual);
    }

    [Fact]
    public void Strings()
    {
        const string actual = "";
        Assert.Same("", actual); // same reference
        Assert.Empty(actual);
        Assert.DoesNotContain("needle", actual);

        const string phrase = "Hello World!";
        Assert.Contains("!", phrase);
        Assert.EndsWith("!", phrase);
        Assert.StartsWith("hello", phrase, StringComparison.CurrentCultureIgnoreCase);
        Assert.Matches("Hello.*", phrase); // regex
    }

    [Fact]
    public async Task Exceptions()
    {
        Action sut = () => throw new Exception("cause");
        var ex = Assert.Throws<Exception>(sut);
        Assert.Equal("cause", ex.Message);

        Func<Task> asyncSut = () => throw new Exception("cause");
        var ex2 = await Assert.ThrowsAsync<Exception>(() => asyncSut());
        Assert.Equal("cause", ex2.Message);

        // Assert.ThrowsAny: Also accept derived Exceptions
    }

    private class DerivedArrayList : ArrayList { }

    [Fact]
    public void Types()
    {
        ICollection collection = new DerivedArrayList();

        Assert.IsAssignableFrom(typeof(ArrayList), collection);

        Assert.IsType(typeof(DerivedArrayList), collection);
        Assert.IsType<DerivedArrayList>(collection);

        Assert.IsNotAssignableFrom<string>(collection);
        Assert.IsNotType<string>(collection);
    }

    [Fact]
    public void Collections()
    {
        var expected = new[] { 1, 2, 3 };
        var actual = new[] { 3, 2, 1 };

        Assert.NotEqual(expected, actual);
        Assert.Equivalent(expected, actual);
        Assert.Contains(2, actual);
        Assert.DoesNotContain(5, actual);
        Assert.Contains(actual, itm => itm > 2);
        Assert.All(actual, itm => Assert.True(itm > 0));

        Assert.Collection(
            actual,
            itm => Assert.Equal(3, itm),
            itm => Assert.Equal(2, itm),
            itm => Assert.Equal(1, itm)
        );
    }

    [Fact]
    public void Numbers()
    {
        const int actual = 42;
        Assert.InRange(actual, 0, 100);
    }

    [Fact]
    public void Internals()
    {
        bool result = InternalsImpl.DoIt();
        Assert.True(result);
    }

    [Fact]
    public void InternalsOld()
    {
        bool result = InternalsOldImpl.DoIt();
        Assert.True(result);
    }
}
