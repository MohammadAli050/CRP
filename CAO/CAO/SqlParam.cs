using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SqlParam
    {
        private string _sqlParamName;
        private object _sqlParamValu;

        public string SqlParamName 
        { 
            get { return _sqlParamName; }
            set { _sqlParamName = value; }
        }

        public object SqlParamValue 
        { 
            get { return _sqlParamValu; } 
            set { _sqlParamValu = value; } 
        }
    }
}
