using System;
using System.IO;
using System.Diagnostics;

namespace TestFramework
{
   #region Test Exception

   /// <summary>
   /// Thrown when an error is encountered while running tests.
   /// </summary>
   [Serializable]
   public class TestException : Exception
   {
      /// <summary>
      /// Constructs an object of type TestException.
      /// </summary>
      public TestException( int stackUnwind = 1 )
         : this( string.Empty, null, stackUnwind )
      {
      }

      /// <summary>
      /// Constructs an object of type TestException.
      /// </summary>
      /// <param name="message">The message associated with the exception.</param>
      public TestException( string message, int stackUnwind = 1 )
         : this( message, null, stackUnwind )
      {
      }

      /// <summary>
      /// Constructs an object of type TestException.
      /// </summary>
      /// <param name="message">The message associated with the exception.</param>
      /// <param name="innerException">Another exception associated with the TestException.</param>
      public TestException( string message, Exception innerException, int stackUnwind = 1 )
         : base( String.Format( "{0}; line {1}: {2}", Path.GetFileName( ( new StackTrace( true ) ).GetFrame( stackUnwind + 1 ).GetFileName() ), ( new StackTrace( true ) ).GetFrame( stackUnwind + 1 ).GetFileLineNumber(), message ), innerException )
      {
      }

      /// <summary>
      /// Constructs a TestException object.
      /// </summary>
      /// <param name="info">The serialization information.</param>
      /// <param name="context">The streaming context.</param>
      protected TestException( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context )
         : base( info, context )
      {
      }
   }

   #endregion
}