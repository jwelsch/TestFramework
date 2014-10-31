using System;

namespace TestFramework
{
   public enum TestResultDisposition
   {
      Success,
      Fail
   }

   public class TestResult
   {
      public string Name
      {
         get;
         private set;
      }

      public TestResultDisposition Disposition
      {
         get;
         private set;
      }

      public bool IsSuccessful
      {
         get { return this.Disposition == TestResultDisposition.Success; }
      }

      public Exception Exception
      {
         get;
         private set;
      }

      public object FailContext
      {
         get;
         private set;
      }

      private TestResult( string name, TestResultDisposition disposition, Exception exception, object failContext )
      {
         this.Name = name;
         this.Disposition = disposition;
         this.Exception = exception;
         this.FailContext = failContext;
      }

      public static TestResult Suceeded( string name )
      {
         return new TestResult( name, TestResultDisposition.Success, null, null );
      }

      public static TestResult Failed( string name, Exception exception )
      {
         return new TestResult( name, TestResultDisposition.Fail, exception, null );
      }

      public static TestResult Failed( string name, Exception exception, object failContext )
      {
         return new TestResult( name, TestResultDisposition.Fail, exception, failContext );
      }
   }
}
