using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DayLectureEntry : BasePage
{
    int userId = 0;
    BussinessObject.UIUMSUser userObj = null;
    string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

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
            ucProgram.LoadDropdownWithUserAccess(userId);
            FillLectureNo();
            ClearAllField();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int programId = Convert.ToInt32(ucProgram.selectedValue);

            if (programId == 0)
            {
                ShowMessage("Please Select Program!", Color.Red);
            }
            else
            {
                int sessionId = Convert.ToInt32(ucSession.selectedValue);

                if (sessionId != 0)
                {
                    int lectureNo = Convert.ToInt32(ddlLectureNo.SelectedValue);

                    string[] courseInfo = ddlCourse.SelectedValue.Split('_');
                    int courseId = Convert.ToInt32(courseInfo[0]);
                    int versionId = Convert.ToInt32(courseInfo[1]);

                    if (courseId != 0 && lectureNo != 0)
                    { 

                        string topic = txtTopics.Text.Trim();
                        string remarks1 = txtRemarks1.Text.Trim();
                        string remarks2 = txtRemarks2.Text.Trim();
                        string remarks3 = txtRemarks3.Text.Trim();
                        string remarks4 = txtRemarks4.Text.Trim();

                        if (string.IsNullOrEmpty(topic) && string.IsNullOrEmpty(remarks1) && string.IsNullOrEmpty(remarks2) && string.IsNullOrEmpty(remarks3) && string.IsNullOrEmpty(remarks4))
                        {
                            ShowMessage("Nothings To Save", Color.Red);
                        }
                        else
                        {
                            DayLecture daylecture = DayLectureManager.GetByProgramIdSessionIdCourseIdVersionIdlectureNo(programId, sessionId, courseId, versionId, lectureNo);
                    
                            if (daylecture == null)
                            {
                                daylecture = new DayLecture();

                                daylecture.ProgramId = programId;
                                daylecture.SessionId = sessionId;
                                daylecture.CourseId = courseId;
                                daylecture.VersionId = versionId;
                                daylecture.LectureNo = lectureNo;
                                daylecture.Topic = topic;
                                daylecture.Remarks1 = remarks1;
                                daylecture.Remarks2 = remarks2;
                                daylecture.Remarks3 = remarks3;
                                daylecture.Remarks4 = remarks4;

                                daylecture.CreatedBy = userId;
                                daylecture.CreatedDate = DateTime.Now;

                                int isExecute = DayLectureManager.Insert(daylecture);
                                if (isExecute > 0)
                                {
                                    ShowMessage("Save Successfully", Color.Green);
                                }
                                else
                                {
                                    ShowMessage("Save Unsuccessful", Color.Red);
                                }
                            }
                            else
                            {
                                daylecture.Topic = topic;
                                daylecture.Remarks1 = remarks1;
                                daylecture.Remarks2 = remarks2;
                                daylecture.Remarks3 = remarks3;
                                daylecture.Remarks4 = remarks4;

                                daylecture.ModifiedBy = userId;
                                daylecture.ModifiedDate = DateTime.Now;

                                bool isUpdate = DayLectureManager.Update(daylecture);
                                if (isUpdate)
                                {
                                    ShowMessage("Update Successfully", Color.Green);
                                }
                                else
                                {
                                    ShowMessage("Update Unsuccessful", Color.Red);
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowMessage("Please Select Course & Lecture No", Color.Red);
                    }
                }
                else
                {
                    ShowMessage("Please Select Session!", Color.Red);
                }

            }

            
        }
        catch (Exception)
        {
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        ShowMessage("", Color.Red);
        ClearAllField();
        int programId = Convert.ToInt32(ucProgram.selectedValue);

        if (programId == 0)
        {
            ShowMessage("Please Select Program!", Color.Red);
        }
        else
        {
            int sessionId = Convert.ToInt32(ucSession.selectedValue);

            if (sessionId != 0)
            {
                int lectureNo = Convert.ToInt32(ddlLectureNo.SelectedValue);

                string[] courseInfo = ddlCourse.SelectedValue.Split('_');
                int courseId = Convert.ToInt32(courseInfo[0]);
                int versionId = Convert.ToInt32(courseInfo[1]);

                if (courseId != 0 && lectureNo != 0)
                {
                    DayLecture daylecture = DayLectureManager.GetByProgramIdSessionIdCourseIdVersionIdlectureNo(programId, sessionId, courseId, versionId, lectureNo);
                    if (daylecture != null)
                    {
                        LoadDayLecture(daylecture);
                    }
                    else
                    {
                        ShowMessage("No Data Found", Color.Red);
                    }
                }
                else
                {
                    ShowMessage("Please Select Course & Lecture No", Color.Red);
                }
            }
            else
            {
                ShowMessage("Please Select Session!", Color.Red);
            }

        }

    }

    protected void OnSessionSelectedIndexChanged(object sender, EventArgs e)
    {
        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int acaCalId = Convert.ToInt32(ucSession.selectedValue);

        FillCourseCombo(acaCalId, programId);
    }

    protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
    {
        ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
    }

    private void FillCourseCombo(int acaCalId, int programId)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));

            List<Course> list = CourseManager.GetOfferedCourseByProgramSession(programId, acaCalId);

            if (list != null && list.Count > 0)
            {
                ddlCourse.DataTextField = "CourseFullInfo";
                ddlCourse.DataValueField = "CoureIdVersionId";

                ddlCourse.DataSource = list.OrderBy(l=> l.FormalCode);
                ddlCourse.DataBind();
            }

        }
        catch (Exception) { }
    }

    private void FillLectureNo()
    {
        try
        {

            ddlLectureNo.Items.Clear();
            ddlLectureNo.Items.Add(new ListItem("Select", "0"));


            for (int i = 1; i < 37; i++)
            {
                ddlLectureNo.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        catch (Exception)
        { }

    }

    private void LoadDayLecture(DayLecture dayLecture)
    {
        try
        { 
            txtTopics.Text = dayLecture.Topic;
            txtRemarks1.Text = dayLecture.Remarks1;
            txtRemarks2.Text = dayLecture.Remarks2;
            txtRemarks3.Text = dayLecture.Remarks3;
            txtRemarks4.Text = dayLecture.Remarks4;

        }
        catch (Exception)
        { }
    }

    private void ClearAllField()
    { 
        txtTopics.Text = "";
        txtRemarks1.Text = "";
        txtRemarks2.Text = "";
        txtRemarks3.Text = "";
        txtRemarks4.Text = "";
    }

    private void ShowMessage(string Message, Color color)
    {
        lblMsg.Text = Message;
        lblMsg.ForeColor = color;
    }

}