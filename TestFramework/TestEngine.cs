using System;
using System.Collections.Generic;

namespace TestFramework
{
   public class TestEngine
   {
      public TestRunResult RunTests( object testObject, IProgressWriter writer )
      {
         var extractor = new TestExtractor();
         var testCollection = extractor.ExtractTestMethods( testObject );

         writer.WriteLine( "Found {0} test{1} to run in {2}.", testCollection.Count, testCollection.Count == 1 ? string.Empty : "s", testObject.GetType().Name );

         var testResults = new List<TestResult>();

         foreach ( var test in testCollection )
         {
            var testMethod = (Action) Delegate.CreateDelegate( typeof( Action ), test.Instance, test.MethodInfo.Name );

            try
            {
               testMethod();
            }
            catch ( Exception ex )
            {
               testResults.Add( TestResult.Failed( test.MethodInfo.Name, ex ) );
               writer.WriteLine( "{0}/{1} failed {2}: {3}", testResults.Count, testCollection.Count, test.MethodInfo.Name, ex.Message );
               System.Diagnostics.Trace.WriteLine( ex );
               continue;
            }

            testResults.Add( TestResult.Suceeded( test.MethodInfo.Name ) );
            writer.WriteLine( "{0}/{1} successful {2}.", testResults.Count, testCollection.Count, test.MethodInfo.Name );
         }

         var result = new TestRunResult( testResults.ToArray() );

         if ( result.FailedTests > 0 )
         {
            writer.WriteLine( "*** {0}/{1} test failed! ***", result.FailedTests, result.TotalTests );
         }
         else
         {
            writer.WriteLine( "{0}/{1} test succeeded.", result.SuccessfulTests, result.TotalTests );
         }

         return result;
      }
   }

   #region Test Attributes

   public class TestMethod : System.Attribute
   {
   }

   #endregion
}