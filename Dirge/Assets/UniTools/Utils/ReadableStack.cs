using System;
using System.Collections.Generic;
using System.Text;


public class ReadableStack<T> : List<T>
{
    public T Depile()
    {
            
        if (this.Count > 0)
        {
            int i = this.Count - 1; 
            T ans = this[i];
            RemoveAt(i);
            return ans;
        }
        else
        {
            return default(T);
        }
            
    }
}

