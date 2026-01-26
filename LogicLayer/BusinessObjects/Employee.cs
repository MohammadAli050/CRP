using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Code { get; set; }
        public int DeptID { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public int SchoolId { get; set; }
        public string Remarks { get; set; }
        public string History { get; set; }
        public int PersonId { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Program { get; set; }
        public DateTime? DOJ { get; set; }
        public int Status { get; set; }
        public string Designation { get; set; }
        public string LibraryCardNo { get; set; }
        public string MIUEmployeeID { get; set; }
        public int EmployeeTypeId { get; set; }

        public int InstituteId { get; set; }

        #region Custom Property
        public string CodeAndName
        {
            get
            {
                Person person = PersonManager.GetById(PersonId);
                if (person != null)
                    return Code + " - " + person.FullName;
                else
                    return "";
            }
        }
        public string EmployeeName
        {
            get
            {
                Person person = PersonManager.GetById(PersonId);
                if (person != null)
                    return person.FullName;
                else
                    return "";
            }
        }

        public Person BasicInfo
        {
            get
            {
                Person person = new Person();
                person = PersonManager.GetById(PersonId);
                return person;
            }
        }
        public string LoginIdAndName
        {
            get
            {
                string loginIdAndName = "";
                User user = UserManager.GetByPersonId(PersonId).FirstOrDefault();
                Person person = PersonManager.GetById(PersonId);
                if (user != null)
                    loginIdAndName = user.LogInID + " - " + person.FullName;
                else loginIdAndName = person.FullName;

                return loginIdAndName;
            }
        }
        public string UserLogInId
        {
            get
            {
                User user = UserManager.GetByPersonId(PersonId).FirstOrDefault();
                if (user != null)
                    return user.LogInID;
                else
                    return "";

            }
        }

        public string StatusDetails
        {
            get
            {
                switch (Status)
                {
                    case 1: return "Full Time";
                    case 2: return "Part Time";
                    case 3: return "Half Time";
                    default: return string.Empty;
                }
            }
        }

        public ContactDetails ContactDetails
        {
            get
            {
                ContactDetails cd = ContactDetailsManager.GetContactDetailsByPersonID(BasicInfo.PersonID);
                return cd;
            }
        }

        public Department Department
        {
            get
            {
                return DepartmentManager.GetById(DeptID);
            }
        }

        public string institutionName
        {
            get
            {
                UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
                try
                {

                    var Obj = ucamContext.Institutions.Where(x => x.InstituteId == InstituteId).FirstOrDefault();
                    if (Obj != null)
                        return Obj.InstituteName;
                    else
                        return "";
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }
        public string programName
        {
            get
            {
                UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
                try
                {
                    var Obj = ucamContext.Programs.Where(x => x.ProgramID.ToString() == Program).FirstOrDefault();
                    if (Obj != null)
                        return Obj.ShortName;
                    else
                        return "";
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }


        #endregion
    }
}
