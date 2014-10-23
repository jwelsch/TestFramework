using System;

namespace TestFramework
{
   public class TestHelper
   {
      #region Static helper methods

      public static void ExpectedException<T>( Action method, int stackUnwind = 1 )
      {
         var caught = false;
         var expectedType = typeof( T );

         try
         {
            method();
         }
         catch ( Exception ex )
         {
            if ( ex.GetType() == expectedType )
            {
               caught = true;
            }
         }

         if ( !caught )
         {
            throw new TestException( String.Format( "Expected {0} was not thrown.", expectedType.Name ), 1 + stackUnwind );
         }
      }

      public static void Expected<T>( T expected, ActionT<T> method, int stackUnwind = 1 ) //where T : IEquatable<T>
      {
         var result = method();

         if ( !result.Equals( expected ) )
         {
            throw new TestException( String.Format( "Expected value: {0}, but got value: {1}.", expected, result ), 1 + stackUnwind );
         }
      }

      #endregion
   }
}