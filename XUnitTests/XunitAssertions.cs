using System.Collections;
using System.Data;
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
        Assert.False(!actual);
    }

    [Fact]
    public void Strings()
    {
        string actual = "";
        Assert.Same("", actual); // same reference
        Assert.Empty(actual);
        Assert.DoesNotContain("needle", actual);

        string phrase = "Hello World!";
        Assert.Contains("!", phrase);
        Assert.EndsWith("!", phrase);
        Assert.StartsWith("hello", phrase, StringComparison.CurrentCultureIgnoreCase);
        Assert.Matches("Hello.*", phrase); // regex
    }

    [Fact]
    public void Exceptions()
    {
        Action sut = () => throw new Exception("cause");
        var ex = Assert.Throws<Exception>(sut);
        Assert.Equal("cause", ex.Message);

        Func<Task> asyncSut = () => throw new Exception("cause");
        var ex2 = Assert.ThrowsAsync<Exception>(() => asyncSut());
        Assert.Equal("cause", ex2.Result.Message);

        // Assert.ThrowsAny: Also accept derived Exceptions
    }

    [Fact]
    public void Types()
    {
        ICollection collection = new ArrayList();
        Assert.IsAssignableFrom(typeof(ArrayList), collection);
        Assert.IsType(typeof(ArrayList), collection);
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
            itm => Assert.True(itm == 3),
            itm => Assert.True(itm == 2),
            itm => Assert.True(itm == 1)
        );
    }

    [Fact]
    public void Numbers()
    {
        int actual = 42;
        Assert.InRange(actual, 0, 100);
    }
}
