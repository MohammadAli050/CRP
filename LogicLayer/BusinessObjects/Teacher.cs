using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Teacher
    {
        public int TeacherId {get; set; }
		public string TeacherName {get; set; }
    }
}

