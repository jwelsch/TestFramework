# .NET Test Framework Library
Provides a lightweight framework for running unit tests.

## How It Works
Tests are defined by decorating class methods with the [TestMethod] attribute.  An instance of a class containing decorated methods can then be passed to a call to TestEngine.RunTests().  Results from the test run are the returned object from TestEngine.RunTests().
### Test Methods
Test methods should have a return type of void and take zero parameters.  If an invalid value is discovered, an exception should be thrown in order to count the test as failed.
## Classes
* ```TestEngine```: Runs tests via the TestEngine.RunTests() instance method.
* ```TestException```: Thrown when an error is encountered while running tests.
* ```TestHelper```: Static class that contains methods to help evaluate test results.
* ```TestResult```: The results from an individual test.
* ```TestRunResults```: The results from a run a collection of individual tests.
## Interfaces
* ```IProgressWriter```: Contains methods called when events happen during a test run.
## Delegates
* ```ActionT<T>```: Passed to TestHelper to extract values to test.

## Example
```
using TestFramework;

public class Tests()
{
   [TestMethod]
   public void SuccessfulTest1()
   {
      var sum = 1 + 1;
      TestHelper.Expected<int>( 2, () =>
      {
         return sum;
      } );
   }

   [TestMethod]
   public void SuccessfulTest2()
   {
      object foo = null;
      TestHelper.ExpectedException<NullReferenceException>( () =>
      {
         foo.GetType();
      } );
   }

   [TestMethod]
   public void FailedTest1()
   {
      TestHelper.Expected<int>( 2, () =>
      {
         return 1 + 1 + 1;
      } );
   }

   [TestMethod]
   public void FailedTest2()
   {
      object foo = new object();
      TestHelper.ExpectedException<NullReferenceException>( () =>
      {
         foo.GetType();
      } );
   }

   [TestMethod]
   public void FailedTest3()
   {
      object foo = new object[0];
      TestHelper.ExpectedException<NullReferenceException>( () =>
      {
         var f = foo[1];
      } );
   }
}

public class Program
{
   static void main( string[] args )
   {
      var tests = new Tests();
      var engine = new TestEngine();
      engine.RunTests( tests, null );
   }
}
```