using System;

namespace TestFramework
{
   public interface IProgressWriter
   {
      void Write( string format, params object[] parameters );
      void WriteLine( string format, params object[] parameters );
   }
}
