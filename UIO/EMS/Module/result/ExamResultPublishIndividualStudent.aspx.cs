using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class ExamResultPublishIndividualStudent : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ResultPublishStudentWise);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ResultPublishStudentWise));

        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                LoadCourse(0, 0);
                //LoadAcaCalSection(0, 0, 0);
                //LoadExam(0);
                lblMsg.Text = "";
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));

            GvResultPublish.DataSource = null;
            GvResultPublish.DataBind();
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            LoadCourse(programId, sessionId);

            GvResultPublish.DataSource = null;
            GvResultPublish.DataBind();
        }

        protected void LoadCourse(int programId, int acaCalId)
        {
            try
            {
                lblMsg.Text = string.Empty;
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("-Select Course-", "0"));
                ddlCourse.AppendDataBoundItems = true;

                BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAllByAcaCalProgram(acaCalId, programId);
                User user = UserManager.GetByLogInId(userObj.LogInID);
                Role userRole = RoleManager.GetById(user.RoleID);
                if (user.Person != null)
                {
                    Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    if (empObj != null && userRole.RoleName != "Admin" && userRole.RoleName != "DepControMarkEntry")
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
                }

                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ToList();
                    foreach (AcademicCalenderSection acaCalSec in acaCalSectionList)
                    {
                        ddlCourse.Items.Add(new ListItem(acaCalSec.Course.Title + ":" + acaCalSec.Course.FormalCode + "(" + acaCalSec.SectionName + ")", acaCalSec.Course.CourseID + "_" + acaCalSec.Course.VersionID + "_" + acaCalSec.AcaCal_SectionID));
                    }
                }


                //List<OfferedCourse> offeredCourseList = OfferedCourseManager.GetAllByProgramIdAcaCalId(programId, acaCalId);
                //offeredCourseList = offeredCourseList.Where(x => x.IsActive == true).ToList();
                //if (offeredCourseList.Count > 0 && offeredCourseList != null)
                //{
                //    foreach (OfferedCourse offeredCourse in offeredCourseList)
                //        ddlCourse.Items.Add(new ListItem(offeredCourse.CourseCode + " " + offeredCourse.CourseTitle, offeredCourse.CourseID + "_" + offeredCourse.VersionID));
                //}
            }
            catch { }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            GvResultPublish.DataSource = null;
            GvResultPublish.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e) 
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalSectionId = Convert.ToInt32(courseVersion[2]);
            ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = new ExamMarkPublishAcaCalSectionRelation();
            examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(acaCalSectionId);
            if (examMarkPublishObj != null)
            {
                if (examMarkPublishObj.IsFinalSubmit == true)
                {
                    LoadStudentForResultPublish(programId, sessionId, courseId, versionId, acaCalSectionId);
                }
                else
                {
                    lblMsg.Text = "Exam mark not submitted yet. To publish mark, please submit exam mark first.";
                }
            }
            else
            {
                lblMsg.Text = "Exam mark not submitted yet. To publish mark, please submit exam mark first.";
            }
        }

        private void LoadStudentForResultPublish(int programId, int sessionId, int courseId, int versionId, int acaCalSectionId)
        {
            List<StudentResultPublishCourseHistoryDTO> resultPublishIndividualDTO = ExamManager.GetStudentForResultPublish(programId, sessionId, courseId, versionId, acaCalSectionId);

            if (resultPublishIndividualDTO.Count > 0)
            {
                GvResultPublish.DataSource = resultPublishIndividualDTO;
                GvResultPublish.DataBind();
            }
        }

        protected void GvResultPublish_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int courseHistoryId = Convert.ToInt32(e.CommandArgument.ToString());
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            string course = ddlCourse.SelectedValue;
            string[] courseVersion = course.Split('_');

            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int acaCalSectionId = Convert.ToInt32(courseVersion[2]);

            PublishExamMark(acaCalSectionId, sessionId, courseId, versionId, courseHistoryId);
            LoadStudentForResultPublish(programId, sessionId, courseId, versionId, acaCalSectionId);
        }

        public void PublishExamMark(int acaCaSectionId, int acaCalId, int courseId, int versionId, int courseHistoryId)
        {
            AcademicCalenderSection acaCalSectionObj = AcademicCalenderSectionManager.GetById(acaCaSectionId);

            if (acaCalSectionObj != null)
            {
                List<ExamTemplateBasicCalculativeItemDTO> examTemplateBasicCalculativeItemList = ExamTemplateMasterManager.ExamTemplateItemGetByAcaCalSectionId(acaCalSectionObj.AcaCal_SectionID).ToList();

                List<ExamMarkColumnWiseDTO> examMarkColumnWiseList = ExamTemplateMasterManager.GetStudentExamMarkColumnWiseByStudentId(courseId, versionId, acaCalId, acaCaSectionId, courseHistoryId);

                List<int> studentIdList = examMarkColumnWiseList.Select(d => d.StudentCourseHistoryId).Distinct().ToList();

                List<int> examSequenceList = examTemplateBasicCalculativeItemList.Select(d => d.ColumnSequence).Distinct().ToList();

                ExamMetaType examMetaTypeTypeObj = ExamMetaTypeManager.GetAll().Where(d => d.ExamMetaTypeName == "Final Exam").FirstOrDefault();

                DataTable table = new DataTable();
                table.Columns.Add("Student Name", typeof(string));
                table.Columns.Add("Roll", typeof(string));

                for (int j = 0; j < examTemplateBasicCalculativeItemList.Count; j++)
                {
                    table.Columns.Add(examTemplateBasicCalculativeItemList[j].ExamTemplateBasicItemName, typeof(string));
                }
                table.Columns.Add("Total Mark", typeof(string));
                table.Columns.Add("GPA", typeof(string));
                table.Columns.Add("Grade", typeof(string));

                for (int i = 0; i < studentIdList.Count; i++)
                {
                    int studentCourseHistoryId = Convert.ToInt32(studentIdList[i]);
                    int studentId = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId).Select(d => d.StudentId).FirstOrDefault();
                    LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetById(studentId);
                    List<GradeDetails> gradeDetailList = new List<GradeDetails>();
                    DataRow newRow;
                    if (studentObj != null)
                    {
                        gradeDetailList = GradeDetailsManager.GetByGradeMasterId(Convert.ToInt32(studentObj.GradeMasterId));
                    }

                    object[] rowArray = new object[examTemplateBasicCalculativeItemList.Count + 5];
                    int newRowCounter = 0;
                    rowArray[0] = studentObj.Name;
                    rowArray[1] = studentObj.Roll;
                    newRowCounter = 1;
                    decimal totalMark = 0;

                    for (int j = 0; j < examSequenceList.Count; j++)
                    {
                        decimal examMarks = 0;
                        int sequenceNo = Convert.ToInt32(examSequenceList[j]);
                        ExamTemplateBasicCalculativeItemDTO examItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ColumnSequence == sequenceNo).FirstOrDefault();
                        if (examItemObj != null)
                        {
                            if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Basic) //(int)CommonUtility.CommonEnum.ExamTemplateItemColumnType.Basic)
                            {
                                decimal studentExamMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.ConvertedMark).FirstOrDefault());
                                examMarks = studentExamMark;
                                examMarks = Math.Round(examMarks, 2);

                                ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType != 0).FirstOrDefault();
                                if (examCalculativeItemObj == null)
                                {
                                    totalMark = totalMark + examMarks;
                                    //if (examItemObj.ExamMetaTypeId != 8 || !examItemObj.ExamTemplateBasicItemName.Contains("Final"))
                                    //{
                                    //    basicExamMark = basicExamMark + examMarks;
                                    //}
                                    //else
                                    //{
                                    //    finalWithoutConvertMark = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamTemplateBasicItemId == examItemObj.ExamTemplateBasicItemId && d.ExamTemplateBasicItemId > 0 && d.ColumnSequence == sequenceNo).Select(d => d.Marks).FirstOrDefault());
                                    //}
                                }
                            }
                            else if (examItemObj.ExamTemplateMasterTypeId == (int)CommonUtility.CommonEnum.ExamTemplateType.Calculative)
                            {
                                if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Average)
                                {
                                    decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark));
                                    int itemCount = Convert.ToInt32(examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 0).ToList().Count);
                                    examMarks = marks / itemCount;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == 1).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne)
                                {
                                    decimal marks = Convert.ToDecimal(examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Max(d => d.ConvertedMark));

                                    examMarks = marks;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestOne).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo)
                                {
                                    decimal[] markArray = new decimal[] { };
                                    markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                    var maxArrayObj = markArray;
                                    var maxNumber = maxArrayObj.Max(z => z);
                                    var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();

                                    examMarks = (maxNumber + secondMax) / 2;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree)
                                {
                                    decimal[] markArray = new decimal[] { };
                                    markArray = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Select(d => d.ConvertedMark).ToArray();

                                    var maxArrayObj = markArray;
                                    var maxNumber = maxArrayObj.Max(z => z);
                                    var secondMax = maxArrayObj.OrderByDescending(z => z).Skip(1).First();
                                    var thirdMax = maxArrayObj.OrderByDescending(z => z).Skip(2).First();

                                    examMarks = (maxNumber + secondMax + thirdMax) / 3;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestThree).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                                else if (examItemObj.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.Sum)
                                {
                                    decimal mark = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examItemObj.ExamMetaTypeId).Sum(d => d.ConvertedMark);

                                    examMarks = mark;
                                    examMarks = Math.Round(examMarks, 2);

                                    ExamTemplateBasicCalculativeItemDTO examCalculativeItemObj = examTemplateBasicCalculativeItemList.Where(d => d.ExamMetaTypeId == examItemObj.ExamMetaTypeId && d.CalculationType == (int)CommonUtility.CommonEnum.ExamCalculationType.BestTwo).FirstOrDefault();
                                    if (examCalculativeItemObj != null)
                                    {
                                        totalMark = totalMark + examMarks;
                                    }
                                }
                            }
                        }
                        rowArray[newRowCounter + 1] = examMarks;
                        newRowCounter = newRowCounter + 1;
                    }
                    totalMark = Math.Ceiling(totalMark);
                    rowArray[newRowCounter + 1] = totalMark;
                    newRowCounter = newRowCounter + 1;

                    decimal gradePoint = 0;
                    string gradeLetter = "Grading System Not Assigned";
                    int? gradeId = 0;

                    if (gradeDetailList != null && gradeDetailList.Count > 0)
                    {
                        gradePoint = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradePoint;
                        gradeLetter = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().Grade;
                        gradeId = gradeDetailList.Where(d => d.MinMarks <= totalMark && d.MaxMarks >= totalMark).FirstOrDefault().GradeId;
                    }
                    if (examMetaTypeTypeObj != null)
                    {
                        ExamMarkColumnWiseDTO examMarkColumnWiseObj = examMarkColumnWiseList.Where(d => d.StudentCourseHistoryId == studentCourseHistoryId && d.ExamMetaTypeId == examMetaTypeTypeObj.ExamMetaTypeId).FirstOrDefault();

                        if (examMarkColumnWiseObj != null)
                        {
                            if (examMarkColumnWiseObj.ExamMarkTypeId == 2)
                            {
                                gradePoint = Convert.ToDecimal(0.00);
                                gradeLetter = "AB";
                                gradeId = gradeDetailList.Where(d => d.Grade == "AB").FirstOrDefault().GradeId;
                            }
                        }
                    }


                    rowArray[newRowCounter + 1] = gradePoint;
                    newRowCounter = newRowCounter + 1;
                    rowArray[newRowCounter + 1] = gradeLetter;
                    newRowCounter = newRowCounter + 1;

                    StudentCourseHistory studentCourseHistory = StudentCourseHistoryManager.GetById(studentCourseHistoryId);
                    List<StudentCourseHistoryReplica> studentCourseHistoryReplicaList = StudentCourseHistoryReplicaManager.GetAllByCourseHistoryID(studentCourseHistoryId);

                    //is Manually grade change ?
                    if (studentCourseHistoryReplicaList != null || studentCourseHistoryReplicaList.Count > 0)
                    {
                        lblMsg.Text = "Exam mark published UnSuccessful because of Manually grade change!";
                    }
                    else
                    {
                        if (studentCourseHistory != null)
                        {
                            studentCourseHistory.ObtainedTotalMarks = totalMark;
                            studentCourseHistory.ObtainedGPA = gradePoint;
                            studentCourseHistory.ObtainedGrade = gradeLetter;
                            studentCourseHistory.GradeId = gradeId;
                            studentCourseHistory.ModifiedBy = userObj.Id;
                            studentCourseHistory.ModifiedDate = DateTime.Now;
                            studentCourseHistory.CourseStatusID = null;


                            bool result = StudentCourseHistoryManager.Update(studentCourseHistory);
                            if (result)
                            {
                                lblMsg.Text = "Exam mark published successfully.";
                            }
                            #region Log Insert
                            try
                            {
                                LogGeneralManager.Insert(
                                         DateTime.Now,
                                         BaseAcaCalCurrent.Code,
                                         BaseAcaCalCurrent.FullCode,
                                         BaseCurrentUserObj.LogInID,
                                         "",
                                         "",
                                         "Exam Result Publish Individual Student",
                                         BaseCurrentUserObj.LogInID + " attempted to publish result for Student :" + studentObj.Roll + ", Course : " + acaCalSectionObj.Course.FormalCode + " - " + acaCalSectionObj.Course.Title + ", Section : " + acaCalSectionObj.SectionName,
                                         "normal",
                                         _pageId,
                                         _pageName,
                                         _pageUrl,
                                         "");
                            }
                            catch (Exception ex)
                            { }
                            #endregion
                        }
                    }

                    newRow = table.NewRow();
                    newRow.ItemArray = rowArray;
                    table.Rows.Add(newRow);
                }
            }
        }
    }
}