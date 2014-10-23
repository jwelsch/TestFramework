using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestFramework
{
   internal class TestExtractor
   {
      public TestCollection ExtractTestMethods( object testObject )
      {
         var allMethods = testObject.GetType().GetMethods( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
         var testMethods = new TestCollection();

         foreach ( var method in allMethods )
         {
            var attributes = method.GetCustomAttributes( typeof( TestMethod ), false );
            if ( Array.Exists<object>( attributes, ( item ) => { return ( ( item as TestMethod ) != null ); } ) )
            {
               testMethods.Add( new TestMethodData( method, testObject ) );
            }
         }

         return testMethods;
      }
   }
}
