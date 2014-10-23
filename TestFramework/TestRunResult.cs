using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestFramework
{
   public class TestRunResult
   {
      public int TotalTests
      {
         get;
         private set;
      }

      public int SuccessfulTests
      {
         get;
         private set;
      }

      public int FailedTests
      {
         get;
         private set;
      }

      public ReadOnlyCollection<TestResult> Results
      {
         get;
         private set;
      }

      public TestRunResult( TestResult[] results )
      {
         this.Results = new ReadOnlyCollection<TestResult>( results );

         this.TotalTests = this.Results.Count;

         foreach ( var result in this.Results )
         {
            if ( result.IsSuccessful )
            {
               this.SuccessfulTests++;
            }
            else
            {
               this.FailedTests++;
            }
         }
      }
   }
}
