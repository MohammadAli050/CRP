using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentCourseWaiver : BasePage
{
    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        lblMsg.Text = "";
        gvCourseWaiver.Columns[9].Visible = false;
        if (!IsPostBack)
        {
            LoadComboBox();
        }
    }

    protected void LoadComboBox()
    {
        try
        {
            LoadCourseComboBox("");
            LoadStudentNameIDComboBox();
            LoadWaiverTypeComboBox();
            LoadUniversityComboBox("");
        }
        catch { }
        finally { }
    }

    protected void LoadCourseComboBox(string searchKey)
    {
        try
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Select", "0_0"));
            ddlCourse.AppendDataBoundItems = true;

            List<Course> courseList = CourseManager.GetAll();
            if (courseList.Count > 0 && courseList != null)
            {
                Dictionary<string, string> dicCourse = new Dictionary<string, string>();
                foreach (Course course in courseList)
                {
                    try
                    {
                        string CourseVersion = course.CourseID.ToString() + "_" + course.VersionID.ToString();
                        dicCourse.Add(CourseVersion,course.ProgramShortName +" "+ course.VersionCode + " (" + course.Title + ")");
                    }
                    catch { }
                }
                var courseCodeList = dicCourse.OrderBy(x => x.Value).ToList();
                if (searchKey.Replace(" ", "").Length > 0)
                {
                    courseCodeList.Clear();
                    courseCodeList = dicCourse.Where(c => c.Value.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Value).ToList();
                }
                foreach (var temp in courseCodeList)
                    ddlCourse.Items.Add(new ListItem(temp.Value, temp.Key));
            }
        }
        catch { }
        finally { }
    }

    protected void LoadStudentNameIDComboBox()
    {
        try
        {
            ddlStudentNameID.Items.Clear();
            ddlStudentNameID.Items.Add(new ListItem("Select", "0"));
        }
        catch { }
        finally { }
    }

    protected void LoadStudentNameIDComboBox(int flag, string searchKey)
    {
        try
        {
            //Here flag variable use for trace(Who call this function)
            //1 Means this funciton work for single student
            //2 Means this function work for multiple student

            List<Student> studentList = StudentManager.GetAllByNameOrID(searchKey);
            if (studentList.Count > 0 && studentList != null)
            {
                Dictionary<string, string> dicStudent = new Dictionary<string, string>();
                if (flag == 1)
                {
                    if (studentList.Count != 1)
                    {
                        lblMsg.Text = "Student ID not found";
                    }
                    else
                    {
                        ddlStudentNameID.Items.Clear();
                        ddlStudentNameID.Items.Add(new ListItem(studentList[0].Remarks, studentList[0].StudentID.ToString()));
                    }
                }
                else if (flag == 2)
                {
                    ddlStudentNameID.Items.Clear();
                    ddlStudentNameID.Items.Add(new ListItem("Select", "0"));
                    ddlStudentNameID.AppendDataBoundItems = true;
                    foreach (Student student in studentList)
                    {
                        try
                        {
                            dicStudent.Add(student.StudentID.ToString(), student.Remarks);
                        }
                        catch { }
                    }
                    var studentNameIDList = dicStudent.Where(c => c.Value.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Value).ToList();
                    foreach (var temp in studentNameIDList)
                        ddlStudentNameID.Items.Add(new ListItem(temp.Value, temp.Key));
                }
                LoadCourseList();
            }
            else
            {
                lblMsg.Text = "Student not found";
            }
        }
        catch { }
        finally { }
    }

    protected void LoadGradeComboBox(string searchKey)
    {
        try
        {
            ddlGrade.Items.Clear();
            ddlGrade.Items.Add(new ListItem("Select", "0"));
            ddlGrade.AppendDataBoundItems = true;

            ddlGrade.Enabled = true;
            Student std= StudentManager.GetByRoll(searchKey);
            List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetAll();
            if (gradeDetailsList.Count > 0 && gradeDetailsList != null)
            {
                gradeDetailsList = gradeDetailsList.FindAll(x=>x.GradeMasterId==std.GradeMasterId).OrderBy(x => x.GradeId).ToList();

                ddlGrade.DataSource = gradeDetailsList;
                ddlGrade.DataTextField = "Grade";
                ddlGrade.DataValueField = "GradeId";
                ddlGrade.DataBind();
            }
            else
            {
                lblMsg.Text = "grade load error 53ac2";
            }
        }
        catch { }
        finally { }
    }

    protected void LoadSemesterComboBox(string searchKey)
    {
        try
        {
            Student std = StudentManager.GetByRoll(searchKey);
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Select", "0"));
            ddlSemester.AppendDataBoundItems = true;

            Program program = ProgramManager.GetById(std.ProgramID);

            List<AcademicCalender> sessionList = new List<AcademicCalender>();
            if (program != null)
                sessionList = AcademicCalenderManager.GetAll(program.CalenderUnitMasterID);

            ddlSemester.Items.Clear();
            ddlSemester.AppendDataBoundItems = true;

            if (sessionList != null)
            {
                // sessionList = sessionList.Where(b => b.ProgramId == programId).ToList();

                ddlSemester.Items.Add(new ListItem("-Select-", "0"));
                ddlSemester.DataTextField = "FullCode";
                ddlSemester.DataValueField = "AcademicCalenderID";

                ddlSemester.DataSource = sessionList;
                ddlSemester.DataBind();
            }
            else
            {
                lblMsg.Text = "Semester load error 53ac8";
            }
        }
        catch { }
        finally { }
    }

    protected void LoadWaiverTypeComboBox()
    {
        try
        {
            List<CourseStatus> courseStatusList = CourseStatusManager.GetAll();
            if (courseStatusList.Count > 0 && courseStatusList != null)
            {
                courseStatusList = courseStatusList.Where(x => x.Description.Contains("Waiver") || x.Description.Contains("Transfer")).ToList();
                if (courseStatusList.Count > 0 && courseStatusList != null)
                {
                    ddlWaiverType.DataSource = courseStatusList;
                    ddlWaiverType.DataTextField = "Description";
                    ddlWaiverType.DataValueField = "CourseStatusID";
                    ddlWaiverType.DataBind();
                }
                else
                {
                    lblMsg.Text = "Type load error 53aca";
                }
            }
            else
            {
                lblMsg.Text = "Type load error 53ac9";
            }
        }
        catch { }
        finally { }
    }

    protected void LoadUniversityComboBox(string searchKey)
    {
        try
        {
            Dictionary<string, string> dicUniversity = new Dictionary<string, string>();

            List<CourseWavTransfr> courseWavTransfrList = CourseWavTransfrManager.GetUniqueAll();
            if (courseWavTransfrList.Count > 0 && courseWavTransfrList != null)
            {
                ddlUniversity.Items.Clear();
                ddlUniversity.Items.Add(new ListItem("Select", "0"));
                ddlUniversity.AppendDataBoundItems = true;
                foreach (CourseWavTransfr courseWavTransfr in courseWavTransfrList)
                {
                    try
                    {
                        dicUniversity.Add(courseWavTransfr.CourseWavTransfrID.ToString(), courseWavTransfr.UniversityName);
                    }
                    catch { }
                }
                var universityIDList = dicUniversity.Where(c => c.Value.ToUpper().Contains(searchKey.ToUpper())).OrderBy(x => x.Value).ToList();
                foreach (var temp in universityIDList)
                    ddlUniversity.Items.Add(new ListItem(temp.Value, temp.Key));


                //ddlUniversity.DataSource = courseWavTransfrList;
                //ddlUniversity.DataTextField = "UniversityName";
                //ddlUniversity.DataValueField = "CourseWavTransfrID";
                //ddlUniversity.DataBind();
            }
            else
            {
                lblMsg.Text = "University load error 53acb";
            }
        }
        catch { }
        finally { }
    }

    #endregion

    #region Event

    protected void Search_Click(Object sender, EventArgs e)
    {
        try
        {
            if (txtStudentId.Text.Replace(" ", "").Length == 12)
            {
                string searchKey = txtStudentId.Text;
                LoadStudentNameIDComboBox(1, searchKey);
                LoadGradeComboBox(searchKey);
                LoadSemesterComboBox(searchKey);
            }
            else
            {
                lblMsg.Text = "your inputed Student ID is incorrect";
            }
        }
        catch { }
        finally { }
    }

    protected void SearchCourse_Click(Object sender, EventArgs e)
    {
        try
        {
            string searchKey = txtSearchCourse.Text;
            if ((searchKey.Replace(" ", "")).Length == 0)
            {
                lblMsg.Text = "Please input the Search Value for Course";
            }
            else
            {
                LoadCourseComboBox(searchKey);
              
            }
        }
        catch { }
        finally { }
    }

    protected void SearchUniversity_Click(Object sender, EventArgs e)
    {
        try
        {

        }
        catch { }
        finally { }
    }


    protected void StudentNameID_Changed(Object sender, EventArgs e)
    {
        try
        {
            if (ddlStudentNameID.SelectedValue == "0")
            {
                txtStudentId.Text = "";
              //  txtSearchName.Text = "";
                ddlCourse.SelectedValue = "0_0";
                txtSearchCourse.Text = "";
                txtCredit.Text = "";
                ddlGrade.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                txtUniversity.Text = "";
                ddlUniversity.SelectedValue = "0";
                txtSearchUniversity.Text = "";
            }
            else
            {
                string[] studentInfo = ddlStudentNameID.SelectedItem.Text.Split('(');
                txtStudentId.Text = studentInfo[0].Replace(" ", "");
            }
        }
        catch { }
        finally { }
    }

    protected void University_Chenged(Object sender, EventArgs e)
    {
        try
        {
            if (ddlUniversity.SelectedValue == "0")
            {
                txtUniversity.Text = "";
            }
            else
            {
                txtUniversity.Text = ddlUniversity.SelectedItem.Text;
            }
        }
        catch { }
        finally { }
    }

    protected void Save_Click(Object sender, EventArgs e)
    {
        try
        {
            int studentId = Convert.ToInt32(ddlStudentNameID.SelectedValue);
            string[] courseVersion = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);
            int gradeId = Convert.ToInt32(ddlGrade.SelectedValue);
            int acaCalId = Convert.ToInt32(ddlSemester.SelectedValue);
            int courseStatusId = Convert.ToInt32(ddlWaiverType.SelectedValue);
            decimal credit = Convert.ToDecimal(txtCredit.Text);
            string universityName = txtUniversity.Text;

            GradeDetails gradeDetails = GradeDetailsManager.GetById(gradeId);
            int id = 0;

            #region CourseWavTransfr Create/Retrieve

            List<CourseWavTransfr> tempList = CourseWavTransfrManager.GetByStudentId(studentId);
            if (tempList.Count > 0 && tempList != null)
            {
                tempList = tempList.Where(x => x.CourseStatusID == courseStatusId && (x.UniversityName == universityName || x.UniversityName == (universityName == "" ? null : universityName))).ToList();
                if (tempList.Count > 0 && tempList != null)
                {
                    id = tempList[0].CourseWavTransfrID;
                }
                else
                {
                    CourseWavTransfr courseWavTransfr = new CourseWavTransfr();
                    courseWavTransfr.StudentID = studentId;
                    courseWavTransfr.UniversityName = universityName;
                    courseWavTransfr.CourseStatusID = courseStatusId;
                    courseWavTransfr.CreatedBy = 99;
                    courseWavTransfr.CreatedDate = DateTime.Now;

                    id = CourseWavTransfrManager.Insert(courseWavTransfr);
                }
            }
            else
            {
                CourseWavTransfr courseWavTransfr = new CourseWavTransfr();
                courseWavTransfr.StudentID = studentId;
                courseWavTransfr.UniversityName = universityName;
                courseWavTransfr.CourseStatusID = courseStatusId;
                courseWavTransfr.CreatedBy = 99;
                courseWavTransfr.CreatedDate = DateTime.Now;

                id = CourseWavTransfrManager.Insert(courseWavTransfr);
            }

            #endregion

            List<StudentCourseHistory> historyList = StudentCourseHistoryManager.GetAllByStudentId(studentId);
            if (historyList != null && historyList.Count > 0)
            {
                historyList = historyList.Where(x => x.CourseID == courseId && x.VersionID == versionId).ToList();
             /*   if (historyList.Count > 0 && historyList != null)
                {
                    lblMsg.Text = "this course already exist in his course history";
                }
                else
                {*/
                    StudentCourseHistory studentCourseHistory = new StudentCourseHistory();
                    studentCourseHistory.StudentID = studentId;
                    if (gradeDetails != null)
                    {
                        studentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                        studentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                        studentCourseHistory.GradeId = gradeDetails.GradeId;
                    }
                    studentCourseHistory.CourseStatusID = courseStatusId;
                    studentCourseHistory.AcaCalID = acaCalId;
                    studentCourseHistory.CourseID = courseId;
                    studentCourseHistory.VersionID = versionId;
                    studentCourseHistory.CourseCredit = credit;
                    studentCourseHistory.CourseWavTransfrID = id;
                    studentCourseHistory.CreatedBy = 99;
                    studentCourseHistory.CreatedDate = DateTime.Now;

                    int resultInsert = StudentCourseHistoryManager.Insert(studentCourseHistory);
                    if (resultInsert > 0)
                    {
                        lblMsg.Text = "Data save successfully";
                        LoadCourseList();
                    }
                    else
                        lblMsg.Text = "error a342e";
               // }
            }
            else
            {
                StudentCourseHistory studentCourseHistory = new StudentCourseHistory();
                studentCourseHistory.StudentID = studentId;
                if(gradeDetails != null)
                {
                    studentCourseHistory.ObtainedGPA = gradeDetails.GradePoint;
                    studentCourseHistory.ObtainedGrade = gradeDetails.Grade;
                    studentCourseHistory.GradeId = gradeDetails.GradeId;
                }
                studentCourseHistory.CourseStatusID = courseStatusId;
                studentCourseHistory.AcaCalID = acaCalId;
                studentCourseHistory.CourseID = courseId;
                studentCourseHistory.VersionID = versionId;
                studentCourseHistory.CourseCredit = credit;
                studentCourseHistory.CourseWavTransfrID = id;
                studentCourseHistory.CreatedBy = 99;
                studentCourseHistory.CreatedDate = DateTime.Now;

                int resultInsert = StudentCourseHistoryManager.Insert(studentCourseHistory);
                if (resultInsert > 0)
                {
                    lblMsg.Text = "Data save successfully";
                    LoadCourseList();
                }
                else
                    lblMsg.Text = "error a342d";
            }
        }
        catch { }
        finally { }
    }

    protected void Update_Click(Object sender, EventArgs e)
    {
        try
        {
        }
        catch { }
        finally { }
    }

    private void LoadCourseList()
    {
        try
        {
            if (txtStudentId.Text.Replace(" ", "").Length == 12)
            {
                Student student = StudentManager.GetByRoll(txtStudentId.Text);
                if (student != null)
                {
                    List<StudentCourseHistory> historyList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);
                    if (historyList.Count > 0 && historyList != null)
                    {
                        int waiver = Convert.ToInt32(ddlWaiverType.Items[0].Value);
                        int transfer = Convert.ToInt32(ddlWaiverType.Items[1].Value);

                        historyList = historyList.Where(x => x.CourseStatusID == waiver || x.CourseStatusID == transfer).ToList();
                        if (historyList.Count > 0 && historyList != null)
                        {
                            #region Hashing
                            List<Course> courseList = CourseManager.GetAll();
                            Hashtable courseHashCourseCode = new Hashtable();
                            Hashtable courseHashCourseName = new Hashtable();
                            foreach (Course course in courseList)
                            {
                                courseHashCourseCode.Add(course.CourseID + "_" + course.VersionID, course.VersionCode);
                                courseHashCourseName.Add(course.CourseID + "_" + course.VersionID, course.Title);
                            }
                            #endregion

                            foreach (StudentCourseHistory temp in historyList)
                            {
                                if (courseHashCourseCode.ContainsKey(temp.CourseID + "_" + temp.VersionID))
                                    temp.CourseCode = courseHashCourseCode[temp.CourseID + "_" + temp.VersionID].ToString();
                                if (courseHashCourseName.ContainsKey(temp.CourseID + "_" + temp.VersionID))
                                    temp.CourseName = courseHashCourseName[temp.CourseID + "_" + temp.VersionID].ToString();

                                CourseWavTransfr courseWavTransfr = CourseWavTransfrManager.GetById(temp.CourseWavTransfrID);
                                if (courseWavTransfr != null)
                                    temp.StudentName = courseWavTransfr.UniversityName;

                                temp.IsConsiderGPA = false;
                                temp.IsMultipleACUSpan = false;
                                if (temp.CourseStatusID == waiver)
                                    temp.IsConsiderGPA = true;
                                else if (temp.CourseStatusID == transfer)
                                    temp.IsMultipleACUSpan = true;
                            }
                            gvCourseWaiver.Visible = true;
                            gvCourseWaiver.DataSource = historyList;
                            gvCourseWaiver.DataBind();
                        }
                        else
                        {
                            gvCourseWaiver.Visible = false;
                            gvCourseWaiver.DataSource = null;
                            gvCourseWaiver.DataBind();
                            lblMsg.Text = "he/she has no waivar/transfer course(s)";
                        }
                    }
                    else
                    {
                        gvCourseWaiver.Visible = false;
                        gvCourseWaiver.DataSource = null;
                        gvCourseWaiver.DataBind();
                        lblMsg.Text = "he/she has no course(s)";
                    }
                }
                else
                {
                    lblMsg.Text = "student not found";
                }
            }
            else
            {
                lblMsg.Text = "your inputed Student ID is incorrect";
            }
        }
        catch { }
        finally { }
    }

    protected void lbUpdate_Click(Object sender, EventArgs e)
    {
        try
        {
        }
        catch { }
        finally { }
    }

    protected void lbDelete_Click(Object sender, EventArgs e)
    {
        try
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            bool resultDelte = StudentCourseHistoryManager.Delete(id);
            if (resultDelte)
            {
                lblMsg.Text = "Delete Successfully";
                LoadCourseList();
            }
            else
            {
                lblMsg.Text = "Delete Fail";
            }
        }
        catch { }
        finally { }
    }

    protected void Course_Changed(Object sender, EventArgs e)
    {
        try
        {
            string[] courseVersion = ddlCourse.SelectedValue.Split('_');
            int courseId = Convert.ToInt32(courseVersion[0]);
            int versionId = Convert.ToInt32(courseVersion[1]);

            Course course = CourseManager.GetByCourseIdVersionId(courseId, versionId);
            if (course != null)
            {
                txtCredit.Text = course.Credits.ToString();
            }
        }
        catch { }
        finally { }
    }

    protected void Clean_Click(Object sender, EventArgs e)
    {
        try
        {
            GridClear();

            txtStudentId.Text = "";
            LoadStudentNameIDComboBox();
           // txtSearchName.Text = "";

            ddlCourse.SelectedValue = "0_0";
            txtSearchCourse.Text = "";
            txtCredit.Text = "";
            ddlGrade.SelectedValue = "0";

            ddlSemester.SelectedValue = "0";
            ddlWaiverType.Items[0].Selected = true;

            txtUniversity.Text = "";
            ddlUniversity.SelectedValue = "0";
            txtSearchUniversity.Text = "";


           
        }
        catch { }
        finally { }
    }

    private void GridClear()
    {
        gvCourseWaiver.DataSource = null;
        gvCourseWaiver.DataBind();
    }


    #endregion
}