using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    [Serializable]
    public class Student_SkillType : Base
    {
        private int _skillTypeID;
        private int _studentID;
        private string _description;
        
        
        public int SkillTypeID
        {
            get { return _skillTypeID; }
            set { _skillTypeID = value; }
        }
        public int StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
