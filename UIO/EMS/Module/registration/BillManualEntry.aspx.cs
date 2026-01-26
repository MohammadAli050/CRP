using BussinessObject;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration
{
    public partial class BillManualEntry : BasePage
    {
        UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.CheckPage_Load();
                userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                if (!IsPostBack)
                {
                    LoadAllDropDownList();
                    
                }
            }
            catch (Exception Ex)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
            }
            multiStudentDDLPanel.Visible = false;
            singleStudentPanel.Visible = false;
            if (studentTypeList.SelectedValue == Convert.ToString(1))
            {
                singleStudentPanel.Visible = true;
                multiStudentDDLPanel.Visible = false;
            }
            if (studentTypeList.SelectedValue == Convert.ToString(2))
            {
                singleStudentPanel.Visible = false;
                multiStudentDDLPanel.Visible = true;
            }
        }

        private void LoadAllDropDownList()
        {
            LoadFeeDDL();
            LoadProgramDDl();
            LoadBatchDDL();
            InitddlStudentCourse();
            //LoadSemesterDDL();
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
                ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadBatchDDL()
        {
            try
            {
                //ddlBatch.Items.Clear();
                //ddlBatch.Items.Add(new ListItem("Select Batch", "0"));
                //List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
                //academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


                //ddlBatch.AppendDataBoundItems = true;

                //if (academicCalenderList != null)
                //{
                //    int count = academicCalenderList.Count;
                //    foreach (LogicLayer.BusinessObjects.AcademicCalender academicCalender in academicCalenderList)
                //    {
                //        ddlBatch.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                //    }
                //}

            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        //private void LoadSemesterDDL()
        //{
        //    try
        //    {
        //        ddlSemester.Items.Clear();
        //        ddlSemester.Items.Add(new ListItem("Select Semester", "0"));
        //        List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
        //        academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();


        //        ddlSemester.AppendDataBoundItems = true;

        //        if (academicCalenderList != null)
        //        {
        //            int count = academicCalenderList.Count;
        //            foreach (LogicLayer.BusinessObjects.AcademicCalender academicCalender in academicCalenderList)
        //            {
        //                ddlSemester.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally { }
        //}

        private void LoadProgramDDl()
        {
            try
            {
                //ddlProgram.Items.Clear();
                //ddlProgram.Items.Add(new ListItem("Select Program", "0"));
                //List<Program> programList = ProgramManager.GetAll();

                //ddlProgram.AppendDataBoundItems = true;

                //if (programList != null)
                //{
                //    ddlProgram.DataSource = programList.OrderBy(d => d.ProgramID).ToList();
                //    ddlProgram.DataTextField = "ShortName";
                //    ddlProgram.DataValueField = "ProgramID";
                //    ddlProgram.DataBind();
                //}

            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void LoadFeeDDL()
        {
            try
            {
                ddlFeeType.Items.Clear();
                ddlFeeType.Items.Add(new ListItem("Select Fee", "0"));
                List<LogicLayer.BusinessObjects.TypeDefinition> typeDefinitionList = TypeDefinitionManager.GetAll();
                //academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlFeeType.AppendDataBoundItems = true;

                if (typeDefinitionList != null)
                {
                    int count = typeDefinitionList.Count;
                    foreach (LogicLayer.BusinessObjects.TypeDefinition typeDefinition in typeDefinitionList)
                    {
                        ddlFeeType.Items.Add(new ListItem(UtilityManager.UppercaseFirst(typeDefinition.Definition), typeDefinition.TypeDefinitionID.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        protected void studentTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studentTypeList.SelectedValue == Convert.ToString(1))
            {
                singleStudentPanel.Visible = true;
                multiStudentDDLPanel.Visible = false;
                CheckedAllButton.Visible = false;
                UncheckedAllButton.Visible = false;
                StudentGridPanel.Visible = false;
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
            }
            if (studentTypeList.SelectedValue == Convert.ToString(2))
            {
                singleStudentPanel.Visible = false;
                multiStudentDDLPanel.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var studentSessionList = Session["StudentList"];
                List<LogicLayer.BusinessObjects.Student> studentList = studentSessionList as List<LogicLayer.BusinessObjects.Student>;
                if (studentTypeList.SelectedValue == Convert.ToString(1) && txtStudentId.Text != string.Empty)
                {
                    string studentRoll = Convert.ToString(txtStudentId.Text);
                    int typeDefinationId = Convert.ToInt32(ddlFeeType.SelectedValue);
                    decimal amount = Convert.ToDecimal(txtAmount.Text);
                    if (studentRoll != null && Convert.ToInt32(ucSession.selectedValue)!= 0)
                    {
                        LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(txtStudentId.Text.Trim());
                        BillHistory studentBillHistoryObj = new BillHistory();
                        //if (ddlStudentCourse.SelectedValue != Convert.ToString('0') && ddlStudentCourse.SelectedValue != null)
                        //{
                        //    studentBillHistoryObj.StudentCourseHistoryId = Convert.ToInt32(ddlStudentCourse.SelectedValue);
                        //}
                        //else
                        //{
                        //    studentBillHistoryObj.StudentCourseHistoryId = -1;
                        //}
                        studentBillHistoryObj.StudentId = student.StudentID;
                        studentBillHistoryObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue); 
                        studentBillHistoryObj.FeeTypeId = typeDefinationId;
                        studentBillHistoryObj.Fees = amount;
                        studentBillHistoryObj.Remark = txtRemark.Text;
                        studentBillHistoryObj.BillingDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                        studentBillHistoryObj.CreatedBy = userObj.Id;
                        studentBillHistoryObj.CreatedDate = DateTime.Now;
                        studentBillHistoryObj.ModifiedBy = userObj.Id;
                        studentBillHistoryObj.ModifiedDate = DateTime.Now;

                        //if (BillHistoryManager.GetStudentBillHistory(studentBillHistoryObj.StudentCourseHistoryId, studentBillHistoryObj.StudentId, studentBillHistoryObj.AcaCalId, studentBillHistoryObj.TypeDefinationId, studentBillHistoryObj.Fees))
                        //{
                        //    int result = BillHistoryManager.Insert(studentBillHistoryObj);

                        //    if (result > 0)
                        //    {
                        //        #region Log Insert

                        //        LogGeneralManager.Insert(
                        //                                             DateTime.Now,
                        //                                             "",
                        //                                             ucSession.selectedText,
                        //                                             userObj.LogInID,
                        //                                             "",
                        //                                             "",
                        //                                             "Bill Manual Entry",
                        //                                             userObj.LogInID + " assigned bill of amount  " + amount + " Tk from " + studentRoll + (string.IsNullOrEmpty(txtRemark.Text) ? "" : " with remarks " + txtRemark.Text) + " for Course" + ddlStudentCourse.SelectedItem.Text + " for semester " + ucSession.selectedText + " And fee type " + ddlFeeType.SelectedItem.Text,
                        //                                             userObj.LogInID + " is Load Page",
                        //                                              ((int)CommonEnum.PageName.BillManualEntry).ToString(),
                        //                                             CommonEnum.PageName.BillManualEntry.ToString(),
                        //                                             _pageUrl,
                        //                                             studentRoll);
                        //        #endregion
                        //        lblMsg.Visible = true;
                        //        lblMsg.Text = "Bill created successfully.";
                        //        ClearTextField();
                        //    }
                        //}
                        //else
                        //{
                        //    lblMsg.Visible = true;
                        //    lblMsg.Text = "Bill already exist";
                        //}
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Please provide all necessery information";
                    }
                }
                if (studentTypeList.SelectedValue == Convert.ToString(2) && studentList.Count != 0)
                {
                    int typeDefinationId = Convert.ToInt32(ddlFeeType.SelectedValue);
                    decimal amount = Convert.ToDecimal(txtAmount.Text);
                    string studentIds = string.Empty;

                    for (int i = 0; i < gvStudentList.Rows.Count; i++)
                    {
                        GridViewRow row = gvStudentList.Rows[i];
                        Label studentId = (Label)row.FindControl("lblStudentId");
                        CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                        Label studentRoll = (Label)row.FindControl("lblStudentRoll");

                        if (studentCheckd.Checked == true)
                        {
                            if (studentRoll != null && Convert.ToInt32(ucSession.selectedValue) != 0)
                            {
                                //Student student = StudentManager.GetByRoll(txtStudentId.Text.Trim());
                                BillHistory studentBillHistoryObj = new BillHistory();
                                //studentBillHistoryObj.StudentCourseHistoryId = -1;
                                studentBillHistoryObj.StudentId = Convert.ToInt32(studentId.Text);
                                studentBillHistoryObj.AcaCalId = Convert.ToInt32(ucSession.selectedValue);
                                studentBillHistoryObj.FeeTypeId = typeDefinationId;
                                studentBillHistoryObj.Fees = amount;
                                studentBillHistoryObj.Remark = txtRemark.Text;
                                studentBillHistoryObj.BillingDate = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null); ;
                                studentBillHistoryObj.CreatedBy = userObj.Id;
                                studentBillHistoryObj.CreatedDate = DateTime.Now; // 
                                studentBillHistoryObj.ModifiedBy = userObj.Id;
                                studentBillHistoryObj.ModifiedDate = DateTime.Now;

                                //if (BillHistoryManager.GetStudentBillHistory(studentBillHistoryObj.StudentCourseHistoryId, studentBillHistoryObj.StudentId, studentBillHistoryObj.AcaCalId, studentBillHistoryObj.TypeDefinationId, studentBillHistoryObj.Fees))
                                //{

                                //    int result = BillHistoryManager.Insert(studentBillHistoryObj);

                                //    if (result > 0)
                                //    {
                                //        lblMsg.Visible = true;
                                //        studentIds += Convert.ToInt32(studentId.Text)+" ";
                                //        lblMsg.Text = "Bill created successfully.";
                                //        ClearTextField();
                                      
                                //    }
                                //}
                                //else
                                //{
                                //    lblMsg.Visible = true;
                                //    lblMsg.Text = "Bill already exist";
                                //}
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Please provide all necessery information";
                            }
                        }
                    }
                    #region Log Insert

                    LogGeneralManager.Insert(
                                                         DateTime.Now,
                                                         "",
                                                         ucSession.selectedText,
                                                         userObj.LogInID,
                                                         "",
                                                         "",
                                                         "",
                                                         userObj.LogInID + " assigned bill of amount " + amount + " Tk from " + studentIds + (string.IsNullOrEmpty(txtRemark.Text) ? "" : " with remarks " + txtRemark.Text) + " for Course" + ddlStudentCourse.SelectedItem.Text + " for semester " + ucSession.selectedText + " And fee type " + ddlFeeType.SelectedItem.Text,
                                                         userObj.LogInID + " is Load Page",
                                                          ((int)CommonEnum.PageName.BillManualEntry).ToString(),
                                                         CommonEnum.PageName.BillManualEntry.ToString(),
                                                         _pageUrl,
                                                         "");
                    #endregion
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            finally
            {
                
                //ClearField();
                //LoadGrid();
                //this.ModalPopupExtender1.Show();
            }
        }

        protected void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                gvStudentList.DataSource = null;
                gvStudentList.DataBind();
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);

                List<LogicLayer.BusinessObjects.Student> studentList = StudentManager.GetAllByProgramIdBatchId(programId, batchId);
                if (studentList.Count != 0)
                {
                    CheckedAllButton.Visible = true;
                    UncheckedAllButton.Visible = true;
                }

                gvStudentList.DataSource = studentList;
                gvStudentList.DataBind();

                Session["StudentList"] = studentList;
            }
            catch (Exception ex) { }
        }

        protected void CheckedButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvStudentList.Rows.Count; i++)
            {
                GridViewRow row = gvStudentList.Rows[i];
                Label studentId = (Label)row.FindControl("lblStudentId");
                CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                studentCheckd.Checked = true;
            }
        }

        protected void UncheckedButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvStudentList.Rows.Count; i++)
            {
                GridViewRow row = gvStudentList.Rows[i];
                Label studentId = (Label)row.FindControl("lblStudentId");
                CheckBox studentCheckd = (CheckBox)row.FindControl("CheckBox");
                studentCheckd.Checked = false;
            }
        }

        protected void LoadCourseButton_Click(object sender, EventArgs e)
        {
            try
            {
                string studentRoll = txtStudentId.Text.Trim();
                LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(studentRoll);
                if (student != null)
                {
                    //string studentRoll = txtStudentRoll.Text.Trim();
                    //Student studentObj = StudentManager.GetByRoll(studentRoll);
                    //txtStudentName.Text = studentObj.Name;
                    ucSession.LoadDropDownList(Convert.ToInt32(student.ProgramID));
                    int trimesterId = Convert.ToInt32(ucSession.selectedValue);

                    List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID).OrderBy(d => d.CourseTitle).ToList();
                    if (studentCourseHistoryList.Count != 0)
                    {
                        ddlStudentCourse.DataSource = studentCourseHistoryList;
                        ddlStudentCourse.DataTextField = "CourseTitle";
                        ddlStudentCourse.DataValueField = "ID";
                        ddlStudentCourse.DataBind();
                        ddlStudentCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                    }
                }
                else 
                {
                    lblMsg.Text = "Student not found, please provide a valid student roll.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void InitddlStudentCourse()
        {
            try
            {
                lblMsg.Text = null;
                ddlStudentCourse.Items.Clear();
                ddlStudentCourse.Items.Insert(0, new ListItem("Select Course", "0"));
                ddlStudentCourse.DataBind();
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private void ClearTextField()
        {
            try
            {
                InitddlStudentCourse();
                txtStudentId.Text = null;
                ucSession.LoadDropDownList();
                ddlFeeType.SelectedValue = Convert.ToString(0);
                ddlStudentCourse.SelectedValue = Convert.ToString(0);
                txtAmount.Text = null;
                txtRemark.Text = null;
            }
            catch (Exception ex)
            {
            }
            finally { }
        }
    }
}