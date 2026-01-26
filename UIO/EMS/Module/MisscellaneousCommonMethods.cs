using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EMS.Module
{
    public class MisscellaneousCommonMethods
    {
        static UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

        public static bool TimeCheck(DateTime StartDate, DateTime StartTime, DateTime EndDate, DateTime EndTime)
        {
            DateTime actualStartDate = Convert.ToDateTime(StartDate.Date + StartTime.TimeOfDay);
            DateTime actualEndDate = Convert.ToDateTime(EndDate.Date + EndTime.TimeOfDay);
            if (actualStartDate <= DateTime.Now && DateTime.Now <= actualEndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static int GetEmployeeId(string LogInID)
        {
            int EmployeeId = 0;

            try
            {
                #region Get Employee Id
                try
                {
                    User user = UserManager.GetByLogInId(LogInID);
                    if (user != null)
                    {
                        Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);
                        if (empObj != null)
                        {
                            EmployeeId = empObj.EmployeeID;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                #endregion
            }
            catch (Exception ex)
            {
            }

            return EmployeeId;
        }

        public static List<UCAMDAL.Institution> GetInstitutionsByUserId(int userId)
        {
            List<UCAMDAL.Institution> institutionList = new List<UCAMDAL.Institution>();
            try
            {
                /// for Admin role all institute will be shown

                var AllInstituteList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();
                var ProgramList = ucamContext.Programs.ToList();

                var userAccessObj = ucamContext.UserAccessPrograms.Where(x => x.User_ID == userId).FirstOrDefault();
                if (userAccessObj != null)
                {
                    var programIds = userAccessObj.AccessPattern.Split('-').Select(int.Parse).ToList();
                    ProgramList = ProgramList.Where(x => programIds.Contains(x.ProgramID)).ToList();
                    var institutionIds = ProgramList.Select(x => x.InstituteId).Distinct().ToList();
                    institutionList = AllInstituteList.Where(x => institutionIds.Contains(x.InstituteId)).ToList();
                }
                else
                {
                    var userObj = ucamContext.Users.Where(x => x.User_ID == userId).FirstOrDefault();
                    if (userObj != null && (userObj.RoleID == 1 || userObj.RoleID == 2))
                    {
                        institutionList = AllInstituteList;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return institutionList;
        }

        public static List<UCAMDAL.Program> GetProgramByUserIdAndInstituteId(int userId, int instituteId)
        {
            List<UCAMDAL.Program> programList = new List<UCAMDAL.Program>();
            try
            {
                /// for Admin role all program will be shown
                var userAccessObj = ucamContext.UserAccessPrograms.AsNoTracking().Where(x => x.User_ID == userId).FirstOrDefault();
                if (userAccessObj != null)
                {
                    var programIds = userAccessObj.AccessPattern.Split('-').Select(int.Parse).ToList();
                    programList = ucamContext.Programs.Where(x => programIds.Contains(x.ProgramID) && x.InstituteId == instituteId).ToList();
                }
                else
                {
                    var userObj = ucamContext.Users.Where(x => x.User_ID == userId).FirstOrDefault();
                    if (userObj != null && (userObj.RoleID == 1 || userObj.RoleID == 2))
                    {
                        programList = ucamContext.Programs.Where(x => x.InstituteId == instituteId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return programList;
        }

        public static void InsertLog(string LogInID, string EventName, string Message, string Roll, string Course, string pageId, string pageName, string pageUrl)
        {
            LogGeneralManager.Insert(
                                      DateTime.Now,
                                      "",
                                      "",
                                      LogInID,
                                      Course,
                                      "",
                                      EventName,
                                      Message,
                                      "Normal",
                                      pageId.ToString(),
                                      pageName.ToString(),
                                      pageUrl,
                                      Roll);
        }


        public static bool IsFileSizeOk(FileUpload file, int maxFileSizeInKB)
        {
            if (file.HasFile)
            {
                int fileSizeInKB = file.PostedFile.ContentLength / 1024;
                if (fileSizeInKB <= maxFileSizeInKB)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;

        }

        public static bool IsFileTypeOk(FileUpload file, string[] allowedExtensions)
        {
            if (file.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (allowedExtensions.Contains(fileExtension))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}