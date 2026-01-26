using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class CustomException : ApplicationException
    {
        public CustomException(string message): base(message)
        {
        }
    }
}
