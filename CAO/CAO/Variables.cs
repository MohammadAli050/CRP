using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Variables
    {
        public static SaveMode SaveMode = SaveMode.Add;
    }

    public enum SaveMode
    {
        Add,
        Update
    }
   
}
