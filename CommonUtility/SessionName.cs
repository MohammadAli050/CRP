using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public static class ConstantValue
    {
        public static readonly string Cookie_Authentication = "EMS_Credentials";

        #region Page specific Session Name       
        #endregion

        #region Common Session Name       
        public static readonly string Session_UserId = "Common_UserId";
        public static readonly string Session_LoginID = "Common_LoginId";
        public static readonly string Session_RoleName = "Common_RoleName";

        public static readonly string Session_Student_Id = "Common_StudentId"; // use to reduce repeted search or load 
        public static readonly string Session_Student = "Common_Session_Student"; // use to reduce repeted search or load 
        public static readonly string Session_Student_Roll = "Common_Session_Student_Roll"; // use to reduce repeted search or load 

        #endregion

        #region Session For Registration
        public static readonly string Session_ForRegistrationPage_Student = "Session_ForRegistrationPage_Student"; 
        #endregion
    }
}
