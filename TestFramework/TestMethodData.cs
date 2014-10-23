using System;
using System.Reflection;

namespace TestFramework
{
   internal class TestMethodData
   {
      public MethodInfo MethodInfo
      {
         get;
         private set;
      }

      public object Instance
      {
         get;
         private set;
      }

      public TestMethodData( MethodInfo methodInfo, object instance )
      {
         this.MethodInfo = methodInfo;
         this.Instance = instance;
      }
   }
}
