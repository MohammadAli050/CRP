using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Module
{
    public class CommonFunctionForCourseMigration
    {

        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        static UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

        public static CourseMigrationObj MigrateCourse(int ProgramId, string FormalCode, string VersionCode, string Title, string Credit, bool ThesisProject, int CourseType, int MigratedBy, string LogInID, string _pageId, string _pageName, string _pageUrl)
        {
            CourseMigrationObj MigrationObj = new CourseMigrationObj();
            try
            {
                MigrationObj.FormaCode = FormalCode;
                MigrationObj.Title = Title;


                string version = GetVersionCode(VersionCode, ProgramId);
                if (VersionCode==version) // Same Version Code Exists . If need to entry with new version code automatically then condition is version!=""
                {

                    var CourseExists = ucamContext.Courses.Where(x => x.VersionCode == version).FirstOrDefault();

                    int CourseId = 0;

                    var CourseList = ucamContext.Courses.ToList();
                    if (CourseList != null && CourseList.Any())
                    {
                        CourseId = CourseList.Max(x => x.CourseID) + 1;
                    }
                    else
                        CourseId = 1;

                    var CourseIdExists = ucamContext.Courses.Where(x=>x.CourseID==CourseId).FirstOrDefault();

                    if (CourseExists == null || CourseIdExists==null)
                    {
                        try
                        {


                            UCAMDAL.Course NewCourseObj = new UCAMDAL.Course();

                            NewCourseObj.CourseID = CourseId;
                            NewCourseObj.VersionID = 1;
                            NewCourseObj.ProgramID = ProgramId;
                            NewCourseObj.FormalCode = FormalCode;
                            NewCourseObj.VersionCode = version;
                            NewCourseObj.Title = Title;
                            NewCourseObj.Credits = Convert.ToDecimal(Credit);
                            NewCourseObj.TypeDefinitionID = CourseType;
                            NewCourseObj.HasMultipleACUSpan = ThesisProject;
                            NewCourseObj.IsActive = true;
                            NewCourseObj.CreatedBy = MigratedBy;
                            NewCourseObj.CreatedDate = DateTime.Now;

                            ucamContext.Courses.Add(NewCourseObj);
                            ucamContext.SaveChanges();
                            if (NewCourseObj.CourseID > 0)
                            {
                                MigrationObj.Status = 1;
                            }
                            else
                            {
                                MigrationObj.Status = 0;
                                MigrationObj.Reason = "Failed to insert";
                            }
                        }
                        catch (Exception ex)
                        {
                            MigrationObj.Status = 0;
                            MigrationObj.Reason = ex.Message;
                        }

                    }
                    else
                    {
                        MigrationObj.Status = 0;
                        MigrationObj.Reason = "Course Exists";
                    }
                }
                else
                {
                    MigrationObj.Status = 0;
                    MigrationObj.Reason = "Version Code already exists";
                }



            }
            catch (Exception ex)
            {
                MigrationObj.Status = 0;
                MigrationObj.Reason = ex.Message;
            }
            return MigrationObj;
        }

        private static string GetVersionCode(string VersionCode, int ProgramId)
        {
            string Version = VersionCode;

            var CourseList = ucamContext.Courses.Where(x => x.VersionCode == VersionCode && x.ProgramID == ProgramId).ToList();

            if (CourseList != null && CourseList.Any())
                Version = Version + "_V" + CourseList.Count()+1;

            return Version;

        }

        private static void InsertLog(string EventName, string Message, string CourseCode, string LoginId, string _pageId, string _pageName, string _pageUrl)
        {
            LogGeneralManager.Insert(
                                      DateTime.Now,
                                      "",
                                      "",
                                      LoginId,
                                      CourseCode,
                                      "",
                                      EventName,
                                      Message,
                                      "Normal",
                                       _pageId.ToString(),
                                      _pageName.ToString(),
                                      _pageUrl,
                                      "");
        }


    }
}