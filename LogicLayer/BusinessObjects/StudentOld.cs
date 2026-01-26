using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentOld
    {
       public int StudentID { get; set; }
       public string Roll { get; set; }
       public int Prefix { get; set; }
       public string FirstName { get; set; }
       public string MiddleName { get; set; }
       public string LastName { get; set; }
       public string NickOrOtherName { get; set; }
       public DateTime DOB { get; set; }
       public int Gender { get; set; }
       public int MatrialStatus { get; set; }
       public int BloodGroup { get; set; }
       public int ReligionID { get; set; }
       public int NationalityID { get; set; }
       public string PhotoPath { get; set; }
       public int ProgramID { get; set; }
       public decimal TotalDue { get; set; }
       public decimal TotalPaid { get; set; }
       public decimal Balance { get; set; }
       public int TuitionSetUpID { get; set; }
       public int WaiverSetUpID { get; set; }
       public int DiscountSetUpID { get; set; }
       public int RelationTypeID { get; set; }
       public int RelativeID { get; set; }
       public int TreeMasterID { get; set; }
       public int Major1NodeID { get; set; }
       public int Major2NodeID { get; set; }
       public int Major3NodeID { get; set; }
       public int Minor1NodeID { get; set; }
       public int Minor2NodeID { get; set; }
       public int Minor3NodeID { get; set; }
       public int CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public int ModifiedBy { get; set; }
       public DateTime ModifiedDate { get; set; }
    }
}
