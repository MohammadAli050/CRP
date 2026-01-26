using BussinessObject;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.admin
{
    public partial class StudentForceRegAnyTree : BasePage
    {
        UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if (!IsPostBack)
            {
                ucProgram.LoadDropDownList();
                Session["CourseList"] = null;
                Session["StudentRoll"] = null;
            }
            lblMsg.Text = null;
        }

        protected void btnStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                string studentRoll = txtStudentRoll.Text.Trim();
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    txtStudentName.Text = studentObj.Name;
                    Session["StudentRoll"] = null;
                    Session["StudentRoll"] = studentObj.Roll;
                    ucSession.LoadDropDownList(Convert.ToInt32(studentObj.ProgramID));
                }
                gvStudentCourse.Visible = false;
                gvStudentCourse.DataSource = null;
                gvStudentCourse.DataBind();
                btnRegistration.Visible = false;
                btnRegistrationAndBilling.Visible = false;
                btnWorkSheet.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void OnProgramSelectedIndexChange(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            FillTreeCombo(programId);
        }
        private void FillTreeCombo(int programId)
        {
            try
            {
                ddlTree.Items.Clear();
                ddlTree.Items.Add(new ListItem("Select", "0"));
                List<LogicLayer.BusinessObjects.TreeMaster> treeMasterList = TreeMasterManager.GetAll();

                treeMasterList = treeMasterList.Where(t => t.ProgramID == programId).ToList();

                ddlTree.AppendDataBoundItems = true;

                if (treeMasterList != null)
                {
                    ddlTree.DataSource = treeMasterList.OrderBy(d => d.TreeMasterID).ToList();
                    ddlTree.DataValueField = "TreeMasterID";
                    ddlTree.DataTextField = "Node_Name";
                    ddlTree.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
            finally { }
        }
        protected void ddlTree_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int treeId = Convert.ToInt32(ddlTree.SelectedValue);
            LoadCourseList(programId, sessionId, treeId);
        }
        private void LoadCourseList(int programId, int acaCalId, int treeRoot)
        {
            List<OfferedCourseDTO> offeredCourseList = OfferedCourseManager.GetAllDtoObjByProgramAcacalTreeroot(programId, acaCalId, treeRoot).ToList();

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0"));
            if (offeredCourseList.Count > 0)
            {
                List<OfferedCourseDTO> offeredCourseList2 = offeredCourseList.OrderBy(oc => oc.FormalCode).ToList();
                if (offeredCourseList2.Count > 0 && offeredCourseList2 != null)
                {
                    foreach (OfferedCourseDTO course in offeredCourseList2)
                    {
                        string valueField = course.CourseID + "_" + course.VersionID;
                        string textField = course.FormalCode + "-" + course.Title;
                        ddlCourse.Items.Add(new ListItem(textField, valueField));
                    }
                }
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            try
            {
                string CourseNameNew = ddlCourse.SelectedValue;
                if (CourseNameNew != null)
                {
                    int courseId = Convert.ToInt32(CourseNameNew.Split('_').First());
                    int versionId = Convert.ToInt32(CourseNameNew.Split('_').Last());

                    LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(courseId, versionId);

                    List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                    var courseSessionList = Session["CourseList"];
                    courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                    if (courselist != null)
                    {
                        for (int i = 0; i < courselist.Count; i++)
                        {
                            if (CheckCourseExistInGrid(courselist, courseObj.CourseID))
                            {
                                lblMsg.Text = "";
                            }
                            else
                            {
                                courselist.Add(courseObj);
                                lblMsg.Text = "";
                            }
                        }
                    }
                    else
                    {
                        List<LogicLayer.BusinessObjects.Course> newCourselist = new List<LogicLayer.BusinessObjects.Course>();
                        newCourselist.Add(courseObj);
                        courselist = newCourselist;
                    }
                    gvStudentCourse.Visible = true;
                    gvStudentCourse.DataSource = courselist;
                    gvStudentCourse.DataBind();
                    btnRegistration.Visible = true;
                    //btnRegistrationAndBilling.Visible = true;
                    btnWorkSheet.Visible = true;
                    Session["CourseList"] = courselist;
                }
                else
                {
                    lblMsg.Text = "Please select a course.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckCourseExistInGrid(List<LogicLayer.BusinessObjects.Course> courseList, int courseId)
        {
            //LogicLayer.BusinessObjects.Course courseObj = courseList.Where(d=> d.CourseID == courseId).FirstOrDefault();
            int counter = 0;
            for (int i = 0; i < courseList.Count; i++)
            {
                if (courseList[i].CourseID == courseId)
                {
                    counter = 1;
                }
            }
            if (counter == 1)
            {
                return true;
            }
            else { return false; }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)gvStudentCourse.HeaderRow.FindControl("chkSelectAll");
                if (chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = true;
                    }
                }
                if (!chkHeader.Checked)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        //Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        studentCheckd.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSession.selectedValue == "0")
                {
                    lblMsg.Text = "Please Select Semester. Then try again.";
                    return;
                }

                string roll = Convert.ToString(Session["StudentRoll"]);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                var courseSessionList = Session["CourseList"];
                courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                if (roll != null && acaCalId != 0 && courselist.Count > 0)
                {
                    DoStudentRegistration();
                    //ClearAll();
                    Session["CourseList"] = null;
                    Session["StudentRoll"] = null;
                }
                else
                {
                    lblMsg.Text = "Please provide student roll, registartion session, and course information";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnRegistrationAndBilling_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSession.selectedValue == "0")
                {
                    lblMsg.Text = "Please Select Semester. Then try again.";
                    return;
                }

                string roll = Convert.ToString(Session["StudentRoll"]);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                var courseSessionList = Session["CourseList"];
                courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                if (roll != null && acaCalId != null && courselist.Count > 0)
                {
                    DoStudentRegistration();
                    GenerateStudentBill();
                    //ClearAll();
                    Session["CourseList"] = null;
                    Session["StudentRoll"] = null;
                }
                else
                {
                    lblMsg.Text = "Please provide student roll, rgistartion + billing session, and course information";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void DoStudentRegistration()
        {
            try
            {
                string studentRoll = Convert.ToString(Session["StudentRoll"]);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue));
                if (studentObj != null)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        Label lblCourseId = (Label)row.FindControl("lblCourseId");
                        Label lblVersionId = (Label)row.FindControl("lblVersionId");
                        Label lblCourseCredit = (Label)row.FindControl("lblCourseCredit");
                        CheckBox courseCheckd = (CheckBox)row.FindControl("CheckBox");
                        if (courseCheckd.Checked == true)
                        {
                            LogicLayer.BusinessObjects.CalenderUnitDistribution calUnitDistributionObj = new LogicLayer.BusinessObjects.CalenderUnitDistribution();
                            calUnitDistributionObj = CalenderUnitDistributionManager.GetByCourseId(Convert.ToInt32(lblCourseId.Text));

                            StudentCourseHistory studentCoursehistrory = new StudentCourseHistory();
                            studentCoursehistrory.StudentID = studentObj.StudentID;
                            studentCoursehistrory.CourseStatusID = (int)CommonEnum.CourseStatus.Rn;
                            studentCoursehistrory.AcaCalID = Convert.ToInt32(ucSession.selectedValue);
                            studentCoursehistrory.CourseID = Convert.ToInt32(lblCourseId.Text);
                            studentCoursehistrory.VersionID = Convert.ToInt32(lblVersionId.Text);
                            studentCoursehistrory.CourseCredit = Convert.ToDecimal(lblCourseCredit.Text);
                            if(calUnitDistributionObj != null)
                            {
                                if (calUnitDistributionObj != null && calUnitDistributionObj.CalenderUnitMasterID == 3)
                                {
                                    //studentCoursehistrory.SemesterNo = 1;
                                    studentCoursehistrory.YearNo = calUnitDistributionObj.Sequence;
                                }
                                else
                                {
                                    int value = 2;
                                    if (calUnitDistributionObj.CalenderUnitMasterID == 1)
                                        value = 2;
                                    else if (calUnitDistributionObj.CalenderUnitMasterID == 2)
                                        value = 3;

                                    //studentCoursehistrory.SemesterNo = calUnitDistributionObj.Sequence % value == 0 ? value : calUnitDistributionObj.Sequence % value;
                                    decimal year = Math.Ceiling(Convert.ToDecimal(calUnitDistributionObj.Sequence) / Convert.ToDecimal(value));
                                    studentCoursehistrory.YearNo = Convert.ToInt32(year);
                                }
                                studentCoursehistrory.SemesterNo = calUnitDistributionObj.Sequence;
                            }
                            
                            //studentCoursehistrory.AcaCalSectionID = item.AcaCal_SectionID;
                            studentCoursehistrory.CourseStatusDate = DateTime.Now;
                            studentCoursehistrory.CreatedBy = userObj.Id;
                            studentCoursehistrory.CreatedDate = DateTime.Now;
                            studentCoursehistrory.ModifiedBy = userObj.Id;
                            studentCoursehistrory.ModifiedDate = DateTime.Now;
                            if (IsCourseHistoryExist(studentCourseHistoryList, studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue), studentCoursehistrory.CourseID, studentCoursehistrory.VersionID))
                            {
                                int result = StudentCourseHistoryManager.Insert(studentCoursehistrory);
                                if (result > 0)
                                {
                                    lblMsg.Text = "Registration done successfully";

                                    #region Log Insert
                                    LogGeneralManager.Insert(
                                                DateTime.Now,
                                                "",
                                                "",
                                                userObj.LogInID,
                                                "",
                                                "",
                                                " Student force registration ",
                                                userObj.LogInID + " course history added for " + studentCoursehistrory.CourseName + ", " + studentCoursehistrory.FormalCode + ", " + studentRoll,
                                                userObj.LogInID + " course force registration ",
                                                ((int)CommonEnum.PageName.ForceRegistration).ToString(),
                                                CommonEnum.PageName.ForceRegistration.ToString(),
                                                _pageUrl,
                                                studentRoll);
                                    #endregion
                                }
                                else
                                {
                                    lblMsg.Text = "Registration could not done successfully";
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Course already registered";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please select some course from list.";
                        }
                    }

                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }

        private bool IsCourseHistoryExist(List<StudentCourseHistory> studentCourseHistoryList, int studentId, int acaCalId, int courseId, int versionId)
        {
            StudentCourseHistory newStudentCourseHistoryObj = studentCourseHistoryList.Where(d => d.StudentID == studentId && d.AcaCalID == acaCalId && d.CourseID == courseId && d.VersionID == versionId).FirstOrDefault();
            if (newStudentCourseHistoryObj != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void GenerateStudentBill()
        {
            try
            {
                string studentRoll = Convert.ToString(Session["StudentRoll"]);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    List<StudentBillDetailsDTO> studentBillDeatilList = null; //BillHistoryManager.GetStudentBillingDetails(studentObj.StudentID, Convert.ToInt32(studentObj.ProgramID), studentObj.BatchId, Convert.ToInt32(ucSession.selectedValue));

                    for (int i = 0; i < studentBillDeatilList.Count; i++)
                    {
                        BillHistory billHistoryObj = new BillHistory();
                        //billHistoryObj.StudentCourseHistoryId = studentBillDeatilList[i].StudentCourseHistoryId;
                        billHistoryObj.StudentId = studentObj.StudentID;
                        billHistoryObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                        billHistoryObj.FeeTypeId = studentBillDeatilList[i].TypeDefinitionID;
                        billHistoryObj.Fees = studentBillDeatilList[i].Amount;
                        billHistoryObj.BillingDate = DateTime.Now;
                        billHistoryObj.CreatedBy = userObj.Id;
                        billHistoryObj.CreatedDate = DateTime.Now;
                        billHistoryObj.ModifiedBy = userObj.Id;
                        billHistoryObj.ModifiedDate = DateTime.Now;

                        //if (BillHistoryManager.GetStudentBillHistory(billHistoryObj.StudentCourseHistoryId, billHistoryObj.StudentId, billHistoryObj.AcaCalId, billHistoryObj.TypeDefinationId, billHistoryObj.Fees))
                        //{
                        //    int result = BillHistoryManager.Insert(billHistoryObj);

                        //    if (result > 0)
                        //    {
                        //        lblMsg.Text = "Bill generated successfully";
                        //        #region Log Insert

                        //        LogGeneralManager.Insert(
                        //                    DateTime.Now,
                        //                    "",
                        //                    "",
                        //                    userObj.LogInID,
                        //                    "",
                        //                    "",
                        //                    " Student bill generation for force registration ",
                        //                    userObj.LogInID + " course bill generation for " + studentRoll + " and fees amount " + billHistoryObj.Fees,
                        //                    userObj.LogInID + " bill generation for force registration ",
                        //                    ((int)CommonEnum.PageName.ForceRegistration).ToString(),
                        //                    CommonEnum.PageName.ForceRegistration.ToString(),
                        //                    _pageUrl,
                        //                    studentRoll);
                        //        #endregion
                        //    }
                        //    else
                        //    {
                        //        lblMsg.Text = "Bill could not generated successfully";
                        //    }
                        //}
                    }
                }
                else
                {
                    lblMsg.Text = "Bill could not generated successfully";
                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }

        private void ClearAll()
        {
            txtStudentRoll.Text = string.Empty;
            txtStudentName.Text = string.Empty;
            ucSession.LoadDropDownList(0);
            ddlCourse.DataSource = null;
            ddlCourse.DataBind();
            gvStudentCourse.DataSource = null;
            gvStudentCourse.DataBind();
            btnRegistration.Visible = false;
            btnRegistrationAndBilling.Visible = false;
        }

        protected void btnWorkSheet_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucSession.selectedValue == "0")
                {
                    lblMsg.Text = "Please Select Semester. Then try again.";
                    return;
                }

                string roll = Convert.ToString(Session["StudentRoll"]);
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                List<LogicLayer.BusinessObjects.Course> courselist = new List<LogicLayer.BusinessObjects.Course>();
                var courseSessionList = Session["CourseList"];
                courselist = courseSessionList as List<LogicLayer.BusinessObjects.Course>;
                if (roll != null && acaCalId != null && courselist.Count > 0)
                {
                    GenerateWorkSheetEntry();
                    //ClearAll();
                    Session["CourseList"] = null;
                    Session["StudentRoll"] = null;
                }
                else
                {
                    lblMsg.Text = "Please provide student roll, rgistartion + billing session, and course information";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateWorkSheetEntry()
        {
            try
            {
                string studentRoll = Convert.ToString(Session["StudentRoll"]);
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                //List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentIdAcaCalId(studentObj.StudentID, Convert.ToInt32(ucSession.selectedValue));
                if (studentObj != null)
                {
                    for (int i = 0; i < gvStudentCourse.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentCourse.Rows[i];
                        Label lblCourseId = (Label)row.FindControl("lblCourseId");
                        Label lblVersionId = (Label)row.FindControl("lblVersionId");
                        Label lblCourseCredit = (Label)row.FindControl("lblCourseCredit");
                        CheckBox courseCheckd = (CheckBox)row.FindControl("CheckBox");
                        if (courseCheckd.Checked == true)
                        {
                            LogicLayer.BusinessObjects.Course courseObj = CourseManager.GetByCourseIdVersionId(Convert.ToInt32(lblCourseId.Text), Convert.ToInt32(lblVersionId.Text));
                            RegistrationWorksheet regWorkSheet = new RegistrationWorksheet();
                            regWorkSheet.CourseID = courseObj.CourseID;
                            regWorkSheet.VersionID = courseObj.VersionID;
                            regWorkSheet.Credits = courseObj.Credits;
                            regWorkSheet.IsAutoOpen = true;
                            //regWorkSheet.Node_CourseID = nodeCourseId;
                            regWorkSheet.StudentID = studentObj.StudentID;
                            regWorkSheet.OriginalCalID = Convert.ToInt32(ucSession.selectedValue);
                            regWorkSheet.CourseTitle = courseObj.Title;
                            regWorkSheet.FormalCode = courseObj.FormalCode;
                            regWorkSheet.VersionCode = courseObj.VersionCode;
                            regWorkSheet.ProgramID = studentObj.ProgramID;
                            regWorkSheet.CreatedBy = userObj.Id;
                            regWorkSheet.CreatedDate = DateTime.Now;
                            regWorkSheet.ModifiedBy = userObj.Id;
                            regWorkSheet.ModifiedDate = DateTime.Now;
                            int result = RegistrationWorksheetManager.Insert(regWorkSheet);
                            if (result > 0)
                            {
                                lblMsg.Text = "Course inserted successfully.";
                            }
                            else
                            {
                                lblMsg.Text = "Course could not inserted successfully.";
                            }

                        }
                        else
                        {
                            lblMsg.Text = "Please select some course from list.";
                        }
                    }

                }
            }
            catch (Exception ex) { lblMsg.Text = ex.Message; }
        }
    }
}