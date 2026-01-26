using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class GradeDdlEntity
    {
        public GradeDdlEntity(int id, string displaytext)
        {
            iD = id;
            text = displaytext;
        }

        int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
