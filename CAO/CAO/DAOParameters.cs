using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Common
{
    public class DAOParameters
    {
        public Hashtable hs = new Hashtable();
        
        public DAOParameters()
        {
            hs.Clear();
        }
        public void AddParameter(object key, object value)
        {
            hs.Add(key, value);
        }
        public DAOParameters Remove(object key, DAOParameters dps)
        {
            dps.hs.Remove(key);
            return dps;
        }

    }
}
