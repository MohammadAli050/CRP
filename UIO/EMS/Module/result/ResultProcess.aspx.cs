using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.result
{
    public partial class ResultProcess : BasePage
    {
        int userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;
        
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;

            lblMsg.Text = "";

            if (!IsPostBack)
            {
                lblResult.Visible = false;
                gvResult.Visible = false;
                LoadCamboBox();
                ucProgram.LoadDropdownWithUserAccess(userId);
            }
        }

        #region Function

        protected void LoadCamboBox()
        {
            FillAcaCalStudentSemesterCombo();
        }

        void FillAcaCalStudentSemesterCombo()
        {
            try
            {
                ddlAcaCalStudentSemester.Items.Clear();
                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll().OrderByDescending(x => x.AcademicCalenderID).ToList();

                ddlAcaCalStudentSemester.Items.Add(new ListItem("-All-", "0"));
                ddlAcaCalStudentSemester.AppendDataBoundItems = true;

                if (academicCalenderList != null)
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlAcaCalStudentSemester.Items.Add(new ListItem(academicCalender.CalendarUnitType_TypeName + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        #endregion

        #region Event

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void ProcessGroup_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);

                string processResult = StudentACUDetailManager.Calculate_GpaCgpa(0, programId, batchId, "");
                if (processResult != "")
                {
                    string[] resultInsertUpdate = processResult.Split('-');
                    lblMsg.Text = "Insert : " + resultInsertUpdate[0] + ", Update : " + resultInsertUpdate[1];
                }
                else
                {
                    lblMsg.Text = "Error 2132";
                }
            }
            catch { }
            finally { }
        }

        protected void ProcessStudent_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcaCalStudentSemester.SelectedValue);
                string studentId = txtStudentId.Text;

                if (studentId.Length == 0)
                    lblMsg.Text = "Please Student ID";
                //else if (studentId.Length != 12)
                //    lblMsg.Text = "Student ID format is not Correct";
                else
                {
                    string processResult = StudentACUDetailManager.Calculate_GpaCgpa(acaCalId, 0, 0, studentId);
                    if (processResult != "")
                    {
                        string[] resultInsertUpdate = processResult.Split('-');
                        lblMsg.Text = "Insert : " + resultInsertUpdate[0] + ", Update : " + resultInsertUpdate[1];
                    }
                    else
                    {
                        lblMsg.Text = "Error 2132";
                    }
                }
            }
            catch { }
            finally { }
        }

        protected void ViewGroup_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ucSession.selectedValue);
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);

                List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByAcaCalProgramBatchStudent(acaCalId, programId, batchId, "");
                if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                {
                    lblResult.Visible = true;
                    gvResult.Visible = true;

                    gvResult.DataSource = studentACUDetailList;
                    gvResult.DataBind();
                }
                else
                {
                    lblResult.Visible = false;
                    gvResult.DataSource = null;
                    gvResult.DataBind();
                }
            }
            catch { }
            finally { }
        }

        protected void ViewStudent_Click(Object sender, EventArgs e)
        {
            try
            {
                int acaCalId = Convert.ToInt32(ddlAcaCalStudentSemester.SelectedValue);
                string studentId = txtStudentId.Text;

                if (studentId.Length == 0)
                {
                    lblMsg.Text = "Please Student ID";

                    lblResult.Visible = false;
                    gvResult.Visible = false;
                }
                //else if (studentId.Length != 12)
                //{
                //    lblMsg.Text = "Student ID format is not Correct";

                //    lblResult.Visible = false;
                //    gvResult.Visible = false;
                //}
                else
                {
                    List<StudentACUDetail> studentACUDetailList = StudentACUDetailManager.GetAllByAcaCalProgramBatchStudent(acaCalId, 0, 0, studentId);
                    if (studentACUDetailList.Count > 0 && studentACUDetailList != null)
                    {
                        lblResult.Visible = true;
                        gvResult.Visible = true;

                        gvResult.DataSource = studentACUDetailList;
                        gvResult.DataBind();
                    }
                    else
                    {
                        lblResult.Visible = false;
                        gvResult.DataSource = null;
                        gvResult.DataBind();
                    }
                }
            }
            catch { }
            finally { }
        }

        #endregion

    }
}