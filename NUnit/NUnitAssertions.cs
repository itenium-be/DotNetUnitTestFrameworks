using System.Collections;
using NUnit.Framework;

namespace NUnitTests;

public class NUnitAssertions
{
    [Test]
    public void Test1()
    {
        bool? actual = true;
        Assert.That(actual, Is.EqualTo(true)); // using IEquatable<T>
        Assert.That(!actual, Is.Not.EqualTo(true));
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.True);
        Assert.That(!actual, Is.False);

        // Is.AnyOf()
    }

    [Test]
    public void Strings()
    {
        string actual = "";
        Assert.That(actual, Is.SameAs(""));
        Assert.That(actual, Is.Empty);
        Assert.That(actual, Does.Not.Contain("needle"));

        string phrase = "Hello World!";
        Assert.That(phrase, Does.EndWith("!"));
        Assert.That(phrase, Does.StartWith("hello").IgnoreCase);
        Assert.That(phrase, Does.Match("Hello.*")); // regex
    }

    [Test]
    public void Exceptions()
    {
        Action sut = () => throw new Exception("cause");
        var ex = Assert.Throws<Exception>(() => sut(), "failure message");
        Assert.That(ex.Message, Is.EqualTo("cause"));

        Func<Task> asyncSut = () => Task.CompletedTask;
        Assert.DoesNotThrowAsync(() => asyncSut());
    }

    [Test]
    public void Types()
    {
        ICollection collection = new ArrayList();
        Assert.That(collection, Is.AssignableFrom(typeof(ArrayList)));
        Assert.That(collection, Is.InstanceOf(typeof(ArrayList)));
        Assert.That(collection, Is.InstanceOf<ArrayList>());
        Assert.That(collection, Is.TypeOf<ArrayList>());

        // Is.AssignableTo
    }

    [Test]
    public void Collections()
    {
        var actual = new[] { 3, 2, 1 };
        Assert.That(actual, Is.EqualTo(new[] { 3, 2, 1 }).AsCollection); // Overrides using IEquatable<T>
        Assert.That(actual, Is.Not.Empty);
        Assert.That(actual, Has.Some.EqualTo(1));
        Assert.That(actual, Contains.Item(1)); // Has.Member(1), Does.Contain(1)

        Assert.That(actual, Is.Ordered.Descending);
        // Is.Ordered.By("fieldName").Then.Descending.By("otherField")

        Assert.That(actual, Is.All.GreaterThan(0));

        Assert.That(actual, Has.None.Null);
        Assert.That(actual, Has.None.LessThan(0));

        Assert.That(actual, Has.Exactly(3).Items);
        Assert.That(actual, Has.Exactly(0).Items.LessThan(0));

        Assert.That(actual, Is.Unique);

        // Is.EquivalentTo()

        // Is.SubsetOf() / SupersetOf()


        var dict = new Dictionary<int, int>
        {
            { 1, 4 },
            { 2, 5 }
        };

        Assert.That(dict, Does.ContainKey(1).WithValue(4));
        // Does.ContainKey

        Assert.That(dict, Contains.Value(4));
        Assert.That(dict, Does.ContainValue(5));
        Assert.That(dict, Does.Not.ContainValue(3));
    }

    [Test]
    public void Numbers()
    {
        int actual = 42;
        Assert.That(actual, Is.GreaterThan(0)); // uses IComparable
        // Is.GreaterThan(0).Using(new MyComparer()); // custom IComparable
        Assert.That(actual, Is.InRange(0, 100));

        // Is.LessThanOrEqualTo
        // Is.Negative/Positive, Is.Zero, Is.AtLeast/AtMost
    }

    [Test]
    public void Dates()
    {
        DateTime now = DateTime.Now;
        DateTime later = now + TimeSpan.FromHours(1.0);

        Assert.That(later, Is.EqualTo(now).Within(TimeSpan.FromHours(3.0)));
        Assert.That(later, Is.EqualTo(now).Within(3).Hours);
    }

    [Test]
    public void Other()
    {
        Assert.That(3, Is.LessThan(5).Or.GreaterThan(10));
        // Also: .And

        // Is.XmlSerializable

        // Is.SamePath
        // Is.SamePathOrUnder

        Assert.That("/folder1/./junk/../folder2", Is.SubPathOf("/folder1/"));
        Assert.That(new DirectoryInfo("c:\\temp"), Does.Exist);
        Assert.That(new DirectoryInfo("c:\\temp"), Is.Not.Empty);
    }

    [Test]
    public void SoftAssertions()
    {
        // Assert.Multiple();
    }
}
