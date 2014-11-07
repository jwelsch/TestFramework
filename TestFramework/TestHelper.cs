using System;
using System.IO;

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
            throw new TestException( String.Format( "Expected {0} was not thrown.", expectedType.Name ), stackUnwind );
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
            throw new TestException( String.Format( "Expected value: {0}, but got value: {1}.", expected, result ), stackUnwind );
         }
      }

      public static void Compare<T>( T value1, T value2, int stackUnwind = 1 )
      {
         TestHelper.Expected<T>( value1, () => { return value2; }, stackUnwind + 1 );
      }

      public static void CompareStreams( Stream stream1, Stream stream2, int stackUnwind = 1 )
      {
         if ( stream1.Length != stream2.Length )
         {
            throw new TestException( String.Format( "Length of streams did not match ({0} vs {1}).", stream1.Length, stream2.Length ) );
         }

         var bytesRead = 0;

         while ( bytesRead < stream1.Length )
         {
            var b1 = (byte) stream1.ReadByte();
            var b2 = (byte) stream2.ReadByte();

            if ( b1 != b2 )
            {
               throw new TestException( String.Format( "Byte {0} did not match.", bytesRead ) );
            }

            bytesRead++;
         }
      }

      public static void CompareFiles( string filePath1, string filePath2, int stackUnwind = 1 )
      {
         try
         {
            using ( var stream1 = new FileStream( filePath1, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            {
               using ( var stream2 = new FileStream( filePath2, FileMode.Open, FileAccess.Read, FileShare.Read ) )
               {
                  TestHelper.CompareStreams( stream1, stream2, stackUnwind + 1 );
               }
            }
         }
         catch ( Exception ex )
         {
            if ( ex.GetType() == typeof( TestException ) )
            {
               throw ex;
            }

            throw new TestException( ex.Message, ex );
         }
      }

      #endregion
   }
}