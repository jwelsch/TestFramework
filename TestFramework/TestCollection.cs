using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestFramework
{
   internal class TestCollection : ICollection<TestMethodData>
   {
      private List<TestMethodData> list = new List<TestMethodData>();

      public TestCollection()
      {
      }

      public TestCollection( ICollection<TestMethodData> collection )
      {
         this.AddRange( collection );
      }

      public void AddRange( ICollection<TestMethodData> items )
      {
         foreach ( var item in items )
         {
            this.list.Add( item );
         }
      }

      #region ICollection<TestMethodData> Members

      public void Add( TestMethodData item )
      {
         this.list.Add( item );
      }

      public void Clear()
      {
         this.list.Clear();
      }

      public bool Contains( TestMethodData item )
      {
         return this.list.Contains( item );
      }

      public void CopyTo( TestMethodData[] array, int arrayIndex )
      {
         this.list.CopyTo( array, arrayIndex );
      }

      public int Count
      {
         get { return this.list.Count; }
      }

      public bool IsReadOnly
      {
         get { return false; }
      }

      public bool Remove( TestMethodData item )
      {
         return this.list.Remove( item );
      }

      #endregion

      #region IEnumerable<TestMethodData> Members

      public IEnumerator<TestMethodData> GetEnumerator()
      {
         return this.list.GetEnumerator();
      }

      #endregion

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
         return this.list.GetEnumerator();
      }

      #endregion
   }
}
