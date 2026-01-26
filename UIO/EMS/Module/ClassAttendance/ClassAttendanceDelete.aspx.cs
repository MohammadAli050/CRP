using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.miu.ClassAttendance
{
    public partial class ClassAttendanceDelete : BasePage
    {
        int userId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                txtAttendanceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                SetUserInfoInSession();
                ucProgram.LoadDropdownWithUserAccess(userId);
            }
        }

        protected void SetUserInfoInSession()
        {
            try
            {
                int employeeId = 0;
               
                string loginID = GetFromSession(Constants.SESSIONCURRENT_LOGINID).ToString();

                User user = UserManager.GetByLogInId(loginID);
                if (user != null)
                {
                    Role role = RoleManager.GetById(user.RoleID);
                    if (role != null)
                    {
                        Session["Role"] = role.RoleName;
                    }
                    if (user.Person != null)
                    {
                        if (user.Person.Employee != null)
                            employeeId = user.Person.Employee.EmployeeID;
                    }
                }
            }
            catch { }
        }

        
        protected void btnLoad_Click(object sender, EventArgs e)
        {

            if (ddlAcaCalSection.SelectedValue == "0")
                return;
            int acaCalSectionId = Convert.ToInt32(ddlAcaCalSection.SelectedValue);

            DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

            LoadData(acaCalSectionId, attendanceDate);

        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucSession.selectedValue);

            FillAcaCalSectionCombo(acaCalId, programId, "");
        }

        void FillAcaCalSectionCombo(int acaCalId, int programId, string searchKey)
        {
            try
            {
                BussinessObject.UIUMSUser userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                List<AcademicCalenderSection> acaCalSectionList = AcademicCalenderSectionManager.GetAll();
                User user = UserManager.GetByLogInId(userObj.LogInID);
                if (user.Person != null)
                {
                    Employee empObj = EmployeeManager.GetByPersonId(user.Person.PersonID);

                    if (empObj != null && empObj.EmployeeID != 2)
                    {
                        acaCalSectionList = acaCalSectionList.Where(x => x.TeacherOneID == empObj.EmployeeID || x.TeacherThreeID == empObj.EmployeeID || x.TeacherTwoID == empObj.EmployeeID).ToList();
                    }
                }
                if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                {
                    ddlAcaCalSection.Items.Clear();
                    ddlAcaCalSection.Items.Add(new ListItem("Select", "0"));

                    if (acaCalId != 0 && programId != 0)
                        acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId && (x.ProgramID == programId)).ToList();
                    else if (acaCalId == 0)
                        acaCalSectionList = acaCalSectionList.Where(x => x.ProgramID == programId).ToList();
                    else if (programId == 0)
                        acaCalSectionList = acaCalSectionList.Where(x => x.AcademicCalenderID == acaCalId).ToList();

                    if (acaCalSectionList.Count > 0 && acaCalSectionList != null)
                    {
                        List<Course> courseList = CourseManager.GetAll();
                        Hashtable hashCourse = new Hashtable();
                        foreach (Course course in courseList)
                            hashCourse.Add(course.CourseID.ToString() + "_" + course.VersionID.ToString(), course.Title + ":" + course.FormalCode);

                        //acaCalSectionList = acaCalSectionList.OrderBy(x => x.CourseID).ThenBy(x => x.VersionID).ToList();
                        Dictionary<string, string> dicAcaCalSec = new Dictionary<string, string>();
                        foreach (AcademicCalenderSection acaCalSection in acaCalSectionList)
                        {
                            string courseVersion = acaCalSection.CourseID.ToString() + "_" + acaCalSection.VersionID.ToString();
                            //ddlAcaCalSection.Items.Add(new ListItem(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString()));
                            try
                            {
                                dicAcaCalSec.Add(hashCourse[courseVersion] + "(" + acaCalSection.SectionName + ") ", acaCalSection.AcaCal_SectionID.ToString());
                            }
                            catch { }
                        }
                        var acaCalSecList = dicAcaCalSec.Where(c => c.Key.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Key).ToList();
                        foreach (var temp in acaCalSecList)
                            ddlAcaCalSection.Items.Add(new ListItem(temp.Key, temp.Value));
                    }
                }
            }
            catch { }
        }
        protected void rdoHeaderIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvheader = gvStudentlists.HeaderRow;
            RadioButtonList rdoStatusAll = (RadioButtonList)gvheader.FindControl("rdoStatusAll");
            if (rdoStatusAll.SelectedValue == "1")
            {
                foreach (GridViewRow gvrow in gvStudentlists.Rows)
                {
                    RadioButtonList rdoStatus = (RadioButtonList)gvrow.FindControl("rdoStatus");
                    rdoStatus.SelectedValue = "1";
                }
            }
            else
            {
                foreach (GridViewRow gvrow in gvStudentlists.Rows)
                {
                    RadioButtonList rdoStatus = (RadioButtonList)gvrow.FindControl("rdoStatus");
                    rdoStatus.SelectedValue = "2";
                }
            }
        }

        private void LoadData(int acaCalSectionId, DateTime attendanceDate)
        {
            gvStudentlists.DataSource = null;
            gvStudentlists.DataBind();
            btnDeleteAll.Visible = false;
            Button1.Visible = false;
            try
            {
                if (acaCalSectionId == 0)
                    return;

                List<LogicLayer.BusinessObjects.ClassAttendance> list = ClassAttendanceManager.GetAttendanceByAcaCalSectionDate(acaCalSectionId, attendanceDate);

                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.Roll).ToList();
                    btnDeleteAll.Visible = true;
                    Button1.Visible = true;
                    gvStudentlists.DataSource = list;
                    gvStudentlists.DataBind();

                    ShowMessage("", Color.Red);
                }
                else
                {
                    ShowMessage("No Data Found", Color.Red);
                    btnDeleteAll.Visible = false;
                    Button1.Visible = false;
                }


            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }

        }

        private void ShowMessage(String Message, Color color)
        {
            lblMsg.Text = Message;
            lblMsg.ForeColor = color;

        }
        protected void btnDeleteSingle_Click(object sender, EventArgs e)
        {
            int acaCalSectionId = Convert.ToInt16(ddlAcaCalSection.SelectedValue);
            DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
            Button btn = new Button();
            btn = (Button)sender;
            bool isDeleted = false;
            string AttendanceId = btn.CommandArgument;
            int Id = Convert.ToInt32(AttendanceId);

            isDeleted = ClassAttendanceManager.Delete(Id);
            LoadData(acaCalSectionId, attendanceDate);
            if (isDeleted)
                ShowMessage("Attendance deleted.", Color.Red);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int DeleteC = 0;
                int acaCalSectionId = Convert.ToInt16(ddlAcaCalSection.SelectedValue);

                DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                bool isDeleted = false;
                foreach (GridViewRow gvrow in gvStudentlists.Rows)
                {
                    Label hdnId = (Label)gvrow.FindControl("lblId");
                    int Id = Convert.ToInt32(hdnId.Text);

                    isDeleted = ClassAttendanceManager.Delete(Id);
                    if (isDeleted)
                        DeleteC++;
                }

                LoadData(acaCalSectionId, attendanceDate);
                ShowMessage(DeleteC + " row deleted.", Color.Red);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
        }

        protected void txtAttendanceDate_TextChanged(object sender, EventArgs e)
        {
            int acaCalSectionId = Convert.ToInt16(ddlAcaCalSection.SelectedValue);

            DateTime attendanceDate = DateTime.ParseExact(txtAttendanceDate.Text.Replace("/", string.Empty), "ddMMyyyy", null);

            LoadData(acaCalSectionId, attendanceDate);

        }

    }
}