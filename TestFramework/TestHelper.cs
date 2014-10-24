using System;

namespace TestFramework
{
   public class TestHelper
   {
      #region Static helper methods

      public static void ExpectedException<T>( Action method, int stackUnwind = 1 )
      {
         if ( method == null )
         {
            throw new ArgumentNullException( "method", "The method to call cannot be null." );
         }

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
         if ( method == null )
         {
            throw new ArgumentNullException( "method", "The method to call cannot be null." );
         }

         var throwException = false;
         var result = method();

         if ( ( result == null ) && ( expected != null ) )
         {
            throwException = true;
         }

         if ( ( result != null ) && ( expected == null ) )
         {
            throwException = true;
         }

         if ( ( result != null ) && ( expected != null ) )
         {
            if ( !result.Equals( expected ) )
            {
               throwException = true;
            }
         }

         if ( throwException )
         {
            throw new TestException( String.Format( "Expected value: {0}, but got value: {1}.", expected, result ), 1 + stackUnwind );
         }
      }

      #endregion
   }
}