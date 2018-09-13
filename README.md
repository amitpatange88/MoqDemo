<h1>Moq</h1><br>
Intro to Mocking with Moq
It’s easy to overlook the importance of unit testing. Writing tests can be tedious. They must be updated constantly as code is refactored, and when you have a large code base, you may have to write many tests to even come close to testing all cases. Despite this, unit testing is a necessary part of creating clean, working code. One way to make the testing process easier is through the use of mocks.

What is a Mock?
Mock objects allow you to mimic the behavior of classes and interfaces, letting the code in the test interact with them as if they were real. This isolates the code you’re testing, ensuring that it works on its own and that no other code will make the tests fail.

With mocks, you can set up the object, including giving parameters and return values on method calls and setting properties. You can also verify that the methods you set up are being called in the tested code. This ensures that the flow of the program is as expected.

Example
In the project I’ve been working on, we use the framework Moq for .NET along with NUnit to create our units tests. Moq provides a library that makes it simple to set up, test, and verify mocks.

We can start by creating an instance of the class we’re testing, along with a mock of an interface we want to use. If the class or interface we were mocking required parameters for its constructor, we would simply pass those through when creating the mock in the setup function.

When creating a mock, we can also give it strict or loose behavior. Strict behavior means that exceptions will be thrown if anything that was not set up on our interface is called. Loose behavior, on the other hand, does not throw exceptions in situations like this. Mocks, by default, are loose.

C#
[TestFixture]
public class FooTest 
{
    Foo subject;
    Mock myInterfaceMock;

    [SetUp]
    public void SetUp()
    {
        myInterfaceMock = new Mock();
        subject = new Foo();
    }
}

From here, we can use our new mock for a number of things. If our code under test uses a method in our interface, we can tell the mock exactly what we want returned. We can also set up properties on our mock simply by using SetupGet instead of Setup.

Once we’ve set up everything we want our interface to return when called, we can call the method we’re testing.

C#
[Test]
public void DoWorkTest()
{
    // Set up the mock
    myInterfaceMock.Setup(m => m.DoesSomething()).Returns(true);
    myInterfaceMock.SetupGet(m => m.Name).Returns("Molly");

    var result = subject.DoWork();

    // Verify that DoesSomething was called only once
    myInterfaceMock.Verify((m => m.DoesSomething()), Times.Once());

    // Verify that DoesSomething was never called
    myInterfaceMock.Verify((m => m.DoesSomething()), Times.Never());
}

Of course, once we get the result from the method, we can use Assert to make sure we’re getting expected values and that our calls to our interface occurred. We can use Verify and tell it explicitly what we expect should happen, which could include verifying that a method was called a certain number of times–or even never.

Another option is to create a mock repository. By using a mock repository, we can verify all of the mocks we create in one place, creating consistent verification without repetitive code for each test.

To do this, we can write up a simple unit test base class that contains the MockRepository instance. The MockRepository allows us to have universal configurations on the mocks we create, specifying both the behavior and default values for the mocks.

C#
public class UnitTestBase
{
    public MockRepository MockRepository { get; private set; }

    [SetUp]
    public void UnitTestBaseSetUp()
    {
      MockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Empty };
    }

    [TearDown]
    public void VerifyAndTearDown()
    {
      MockRepository.VerifyAll();
    }
}

When creating the mock, we call Create on the MockRepository. This creates a mock that uses the repository settings. In our case, this is a teardown function that verifies all expectations at the end of a test. We no longer have to call Verify on any of our mocks, as long as we create them using MockRepository.

C#
[TestFixture]
public class FooTest : UnitTestBase
{
    Foo subject;
    Mock myInterfaceMock;

    [SetUp]
    public void SetUp()
    {
        // create the mock in the MockRepository
        myInterfaceMock = MockRepository.Create();
        subject = new Foo();
    }

    [Test]
    public void DoWorkTest()
    {
        // Set up the mock
        myInterfaceMock.Setup(m => m.DoesSomething()).Returns(true);
        myInterfaceMock.SetupGet(m => m.Name).Returns("Molly");

        var result = subject.DoWork();
    }
}

By using mocks for unit testing, we have yet another way to assert that the code is behaving as we would expect. Mocks make it easier to test code by isolating the code under test and give you peace of mind that your code does, in fact, work.
