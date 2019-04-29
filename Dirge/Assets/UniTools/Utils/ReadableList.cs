using System;
using System.Collections.Generic;
using System.Text;

public class ReadableQueue<T> : List<T>
{
    public T Dequeue()
    {
            
        if (this.Count > 0)
        {
            T ans = this[0];
            RemoveAt(0);
            return ans;
        }
        else
        {
            return default(T);
        }
            
    }
}

