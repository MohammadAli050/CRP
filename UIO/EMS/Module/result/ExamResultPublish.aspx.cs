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
    public partial class ExamResultPublish : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.ResultPublishCourseWise);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.ResultPublishCourseWise));

        BussinessObject.UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

            if (!IsPostBack)
            {
                btnPublishAll.Visible = false;
                ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                lblMsg.Text = string.Empty;
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            btnPublishAll.Visible = false;
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            gvExamMarkPublish.DataSource = null;
            gvExamMarkPublish.DataBind();
        }

        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            btnPublishAll.Visible = false;
            gvExamMarkPublish.DataSource = null;
            gvExamMarkPublish.DataBind();
        }

        protected void btnLoadSection_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            btnPublishAll.Visible = false;
            LoadAcaCalSectionForPublish();
        }

        private void LoadAcaCalSectionForPublish()
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);
            List<AcacalSectionResultPublishDTO> acacalSectionResultPublishList = ExamMarkPublishAcaCalSectionRelationManager.GetByProgramIdAcaCalId(programId, acaCalId);

            if (acacalSectionResultPublishList.Count > 0 && acacalSectionResultPublishList != null)
            {
                btnPublishAll.Visible = false;
                lblTotalSection.Text = Convert.ToString(acacalSectionResultPublishList.Count);
                lblTotalFinalSubmitSection.Text = Convert.ToString(acacalSectionResultPublishList.Where(d => d.IsFinalSubmit == true).ToList().Count) + "/" + Convert.ToString(acacalSectionResultPublishList.Count);
                lblTotalPublishedSection.Text = Convert.ToString(acacalSectionResultPublishList.Where(d => d.IsPublished == true).ToList().Count);

                gvExamMarkPublish.DataSource = acacalSectionResultPublishList;
                gvExamMarkPublish.DataBind();
                GridRebind();
            }
        }

        private void GridRebind()
        {
            for (int i = 0; i < gvExamMarkPublish.Rows.Count; i++)
            {
                GridViewRow row = gvExamMarkPublish.Rows[i];
                Label lblAcacalSectionId = (Label)row.FindControl("lblAcacalSectionId");
                Label lblAcademicCalenderId = (Label)row.FindControl("lblAcademicCalenderId");
                Label lblCourseId = (Label)row.FindControl("lblCourseId");
                Label lblVersionId = (Label)row.FindControl("lblVersionId");
                Label lblIsFinalSubmit = (Label)row.FindControl("lblIsFinalSubmit");

                Label lblIsPublished = (Label)row.FindControl("lblIsPublished");
                Button btnResultPublish = (Button)row.FindControl("btnResultPublish");
                Button btnResultRePublish = (Button)row.FindControl("btnResultRePublish");

                if (Convert.ToString(lblIsFinalSubmit.Text) == "True")
                {
                    lblIsFinalSubmit.Text = "Submitted";
                    if (Convert.ToString(lblIsPublished.Text) == "True")
                    {
                        btnResultPublish.Enabled = false;
                        btnResultRePublish.Enabled = true;
                    }
                    else
                    {
                        btnResultPublish.Enabled = true;
                        btnResultRePublish.Enabled = false;
                    }
                }
                else
                {
                    lblIsFinalSubmit.Text = "Not Submitted";
                    btnResultPublish.Enabled = false;
                    btnResultRePublish.Enabled = false;
                }

            }
        }

        protected void gvExamMarkPublish_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int acaCalSectionId = Convert.ToInt32(commandArgs[0]);
            int academicCalenderId = Convert.ToInt32(commandArgs[1]);
            int courseId = Convert.ToInt32(commandArgs[2]);
            int versionId = Convert.ToInt32(commandArgs[3]);

            PublishExamMark(acaCalSectionId, academicCalenderId, courseId, versionId);

            ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = new ExamMarkPublishAcaCalSectionRelation();
            examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(acaCalSectionId);
            if (examMarkPublishObj != null)
            {
                examMarkPublishObj.IsPublished = true;
                examMarkPublishObj.PublishedDate = DateTime.Now;
                examMarkPublishObj.PublishedBy = userObj.Id;
                ExamMarkPublishAcaCalSectionRelationManager.Update(examMarkPublishObj);
            }
            else
            {
                ExamMarkPublishAcaCalSectionRelation obj = new ExamMarkPublishAcaCalSectionRelation();
                obj.AcacalSectionId = acaCalSectionId;
                obj.IsPublished = true;
                obj.PublishedDate = DateTime.Now;
                obj.PublishedBy = userObj.Id;
                ExamMarkPublishAcaCalSectionRelationManager.Insert(obj);
            }
            LoadAcaCalSectionForPublish();
        }

        public void PublishExamMark(int acaCaSectionId, int acaCalId, int courseId, int versionId)
        {
            AcademicCalenderSection acaCalSectionObj = AcademicCalenderSectionManager.GetById(acaCaSectionId);

            if (acaCalSectionObj != null)
            {
                List<ExamTemplateBasicCalculativeItemDTO> examTemplateBasicCalculativeItemList = ExamTemplateMasterManager.ExamTemplateItemGetByAcaCalSectionId(acaCalSectionObj.AcaCal_SectionID).ToList();

                List<ExamMarkColumnWiseDTO> examMarkColumnWiseList = ExamTemplateMasterManager.GetStudentExamMarkColumnWise(courseId, versionId, acaCalId, acaCaSectionId);

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
                    if (studentCourseHistoryReplicaList != null && studentCourseHistoryReplicaList.Count > 0)
                    {
 
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
                                        "Exam Result Publish",
                                        BaseCurrentUserObj.LogInID + " attempted to publish result for Course : " + acaCalSectionObj.Course.FormalCode + " - " + acaCalSectionObj.Course.Title + ", Section : " + acaCalSectionObj.SectionName,
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

        protected void btnPublishAll_Click(object sender, EventArgs e)
        {
            int publishSectionCount = 0;
            int totalSection = gvExamMarkPublish.Rows.Count;
            for (int i = 0; i < gvExamMarkPublish.Rows.Count; i++)
            {
                GridViewRow row = gvExamMarkPublish.Rows[i];
                Label lblAcacalSectionId = (Label)row.FindControl("lblAcacalSectionId");
                Label lblAcademicCalenderId = (Label)row.FindControl("lblAcademicCalenderId");
                Label lblCourseId = (Label)row.FindControl("lblCourseId");
                Label lblVersionId = (Label)row.FindControl("lblVersionId");
                Label lblIsFinalSubmit = (Label)row.FindControl("lblIsFinalSubmit");

                Label lblIsPublished = (Label)row.FindControl("lblIsPublished");

                if (Convert.ToString(lblIsFinalSubmit.Text) == "True")
                {
                    if (Convert.ToString(lblIsPublished.Text) == "False")
                    {
                        PublishExamMark(Convert.ToInt32(lblAcacalSectionId.Text), Convert.ToInt32(lblAcademicCalenderId.Text), Convert.ToInt32(lblCourseId.Text), Convert.ToInt32(lblVersionId.Text));
                        publishSectionCount = publishSectionCount + 1;
                        ExamMarkPublishAcaCalSectionRelation examMarkPublishObj = new ExamMarkPublishAcaCalSectionRelation();
                        examMarkPublishObj = ExamMarkPublishAcaCalSectionRelationManager.GetByAcacalSecId(Convert.ToInt32(lblAcademicCalenderId.Text));
                        if (examMarkPublishObj != null)
                        {
                            examMarkPublishObj.IsPublished = true;
                            examMarkPublishObj.PublishedDate = DateTime.Now;
                            examMarkPublishObj.PublishedBy = userObj.Id;
                            ExamMarkPublishAcaCalSectionRelationManager.Update(examMarkPublishObj);
                        }
                        else
                        {
                            ExamMarkPublishAcaCalSectionRelation obj = new ExamMarkPublishAcaCalSectionRelation();
                            obj.AcacalSectionId = Convert.ToInt32(lblAcademicCalenderId.Text);
                            obj.IsPublished = true;
                            obj.PublishedDate = DateTime.Now;
                            obj.PublishedBy = userObj.Id;
                            ExamMarkPublishAcaCalSectionRelationManager.Insert(obj);
                        }
                    }
                }
                else
                {

                }
                lblMsg.Text = "Total published " + publishSectionCount + " out of " + totalSection + " section.";
            }
        }
    }
}