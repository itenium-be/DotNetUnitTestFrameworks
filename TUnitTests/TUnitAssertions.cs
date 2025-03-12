using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using TUnit.Assertions.AssertConditions;
using TUnit.Assertions.AssertConditions.Throws;
using TUnit.Assertions.Enums;

namespace TUnitTests;

public class TUnitAssertions
{
    [Test]
    public async Task Test1()
    {
        bool? actual = true;
        await Assert.That(actual).IsTrue();
        await Assert.That(actual).IsNotFalse();
        await Assert.That(actual).IsNotNull();

        actual = null;
        await Assert.That(actual).IsDefault();
        await Assert.That(actual).IsNull();
    }

    [Test]
    public async Task Strings()
    {
        string actual = "";
        await Assert.That(actual).IsSameReferenceAs("");
        await Assert.That(actual).IsEqualTo("");
        await Assert.That(actual).IsEmpty();
        await Assert.That(actual).DoesNotContain("needle");

        string phrase = "Hello World!";
        await Assert.That(phrase).EndsWith("!");
        await Assert.That(phrase).Contains("Hello");
        await Assert.That(phrase).StartsWith("hello", StringComparison.InvariantCultureIgnoreCase);
        await Assert.That(phrase).Matches(new Regex("Hello.*"));
    }

    [Test]
    public async Task ExceptionsExact()
    {
        Action sut = () => throw new ArgumentException("cause");
        await Assert.That(sut).ThrowsExactly<ArgumentException>().WithMessage("cause");

        Func<Task> cleanAsyncSut = () => Task.CompletedTask;
        await Assert.That(cleanAsyncSut).ThrowsNothing();

        Func<Task> asyncSut = () => throw new ArgumentException("cause");
        await Assert.That(asyncSut)
            .ThrowsExactly<ArgumentException>()
            .WithMessageMatching(StringMatcher.AsRegex("^cause$"));
    }

    [Test]
    public async Task ExceptionsDerived()
    {
        Action sut = () => throw new ArgumentException("cause");
        var ex1 = Assert.Throws<Exception>(sut);
        await Assert.That(ex1.Message).IsEqualTo("cause");

        Func<Task> asyncSut = () => throw new ArgumentException("cause");
        var ex2 = await Assert.ThrowsAsync<Exception>(asyncSut);
        await Assert.That(ex2.Message).IsEqualTo("cause");
    }

    [Test]
    public async Task Types()
    {
        ICollection collection = new ArrayList();
        await Assert.That(collection).IsAssignableFrom(typeof(ArrayList));
        await Assert.That(collection).IsTypeOf<ArrayList>();
    }

    [Test]
    public async Task Collections()
    {
        var expected = new[] { 1, 2, 3 };
        var actual = new[] { 3, 2, 1 };

        await Assert.That(actual).IsNotEmpty();
        await Assert.That(actual).IsEquivalentTo(new[] { 3, 2, 1 });
        await Assert.That(actual).IsNotEquivalentTo(expected);
        await Assert.That(actual).IsEquivalentTo(expected, CollectionOrdering.Any);

        await Assert.That(actual).Contains(2);
        await Assert.That(actual).DoesNotContain(5);
        await Assert.That(actual).Contains(itm => itm > 2);
        await Assert.That(actual).ContainsOnly(itm => itm > 0);

        await Assert.That(actual).IsInDescendingOrder();
        await Assert.That(expected).IsInOrder();

        await Assert.That(actual).HasDistinctItems();
        await Assert.That(new[] {0}).HasSingleItem();
    }

    [Test]
    public async Task Numbers()
    {
        int actual = 42;
        await Assert.That(actual).IsBetween(0, 100);
        await Assert.That(actual).IsGreaterThan(0);
        await Assert.That(actual).IsLessThanOrEqualTo(100);
        await Assert.That(actual).IsPositive();
        await Assert.That(actual).IsNotNegative();
        await Assert.That(actual).IsNotZero();
    }

    [Test]
    public async Task Dates()
    {
        DateTime now = DateTime.Now;
        DateTime later = now + TimeSpan.FromHours(1.0);

        await Assert.That(later).IsAfter(now);
        await Assert.That(later).IsBetween(now, now.AddHours(2));
    }

    [Test]
    public async Task Other()
    {
        await Assert.That(3).IsLessThan(5).Or.IsGreaterThan(10);
        // Also: .And
    }

    [Test]
    public async Task SoftAssertions()
    {
        using var _ = Assert.Multiple();
        await Assert.That(12).IsNotEqualTo(24);
        await Assert.That("Hello world").IsNotNull();
    }
}