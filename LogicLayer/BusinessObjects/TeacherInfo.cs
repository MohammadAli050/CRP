using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TeacherInfo
    {
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public string AcademicBackground { get; set; }
        public string Publish { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string Phone { get; set; }
        public double MaxNoTobeAdvised { get; set; }
        public string UserID { get; set; }

        #region custom prop
        public Employee EmployeeObj
        {
            get
            {
                Employee em = EmployeeManager.GetById(TeacherId);
                return em;
            }
        }
        public Person BasicInfo
        {
            get
            {
                Person pr = PersonManager.GetById(EmployeeObj.PersonId);
                return pr;
            }
        }
        #endregion
    }
}
