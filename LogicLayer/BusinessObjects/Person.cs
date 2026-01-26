 using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Person
    {
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string MatrialStatus { get; set; }
        public string BloodGroup { get; set; }
        public int ReligionId { get; set; }
        public string Nationality { get; set; }
        public string FatherName { get; set; }
        public string FatherProfession { get; set; }
        public string MotherName { get; set; }
        public string MotherProfession { get; set; }
        public string GuardianName { get; set; }
        public string Phone { get; set; }
        public string SMSContactSelf { get; set; }
        public string SMSContactGuardian { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string Remarks { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string PhotoBytes { get; set; }
        public string SignatureBytes { get; set; }


        public List<User> Users
        {
            get
            {
                List<User> users = null;
                users = UserManager.GetByPersonId(PersonID);
                return users;
            }
        }
        public string LoginIdAndName
        {
            get
            {
                if (Users.FirstOrDefault() != null)
                {
                    return Users.FirstOrDefault().LogInID + " - " + FullName;
                }
                else return FullName;
            }
        }
        public Student Student
        {
            get
            {
                Student student = null;
                student = StudentManager.GetBypersonID(PersonID);
                return student;
            }
        }

        public Employee Employee
        {
            get
            {
                Employee employee = null;
                employee = EmployeeManager.GetByPersonId(PersonID);
                return employee;
            }
        }

        public bool IsBlock
        {
            get
            {
                bool isBlock = false;
                isBlock = PersonBlockManager.PersonIsBlock(PersonID);
                return IsBlock;
            }
        }

        public PersonBlock Block
        {
            get
            {
                PersonBlock block = PersonBlockManager.GetByPersonId((int)PersonID);
                return block;
            }
        }

   
        public List<PersonPreviousExam> PreviousExams
        {
            get
            {
               List<PersonPreviousExam> list= PersonPreviousExamManager.GetAllByPersonId(PersonID);
               return list;
            }

        }

        public string ReligionName
        {
            get
            {
                if (ReligionId != 0)
                {
                    return Enum.GetName(typeof(CommonUtility.CommonEnum.Religion), ReligionId);
                }
                else
                    return "";

            }
        }

    }
}
