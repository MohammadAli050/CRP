using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_ThesisAndProjectRegistration : BasePage
{
    int _courseId = 0;
    int _versionId = 0;
    double _CourseCr = 0.0;
    double _RemainCr = 0.0;
    const string SESSIONS_TUDENT = "SESSIONSTUDENT";

    #region Method

    private void LoadMultipleACUSpanCourse(int treeMasterID)
    {
        List<NodeCourses> CoursesByNodeList = new List<NodeCourses>();
        List<Course> CourseList = CourseManager.GetAll();
        CourseList = CourseList.Where(x => x.HasMultipleACUSpan == true).ToList();

        TreeMaster treeMaster = TreeMasterManager.GetById(treeMasterID);
        if (treeMaster != null)
        {
            CoursesByNodeList = CourseManager.GetAllNodeCoursesByNodeId(treeMaster.RootNodeID);
        }

        var query = CourseList
                   .Join(
                       CoursesByNodeList,
                       cl => new { cl.CourseID, cl.VersionID },
                       c => new { c.CourseID, c.VersionID },
                       (cl, c) => cl).Where(x => x.HasMultipleACUSpan == true);

        ddlMultipleACUSCourse.Items.Clear();
        ddlMultipleACUSCourse.Items.Add(new ListItem("-Select-", "0"));
        ddlMultipleACUSCourse.AppendDataBoundItems = true;

        foreach (var item in query)
        { 
            ddlMultipleACUSCourse.Items.Add(new ListItem(item.FormalCode + " - " + item.Title, item.CourseID.ToString() + "," + item.VersionID.ToString()));
        }
    }

    private void CleareGrid()
    {
        gvThesisProjectReg.DataSource = null;
        gvThesisProjectReg.DataBind();
    }

    private void LoadDataFromCourseHistory(int studentID)
    {
        List<StudentCourseHistory> collection = null;
        collection = StudentCourseHistoryManager.GetAllMultiSpanCourseByStudentID(studentID);

        if (collection != null && collection.Count > 0)
        {
            gvThesisProjectReg.DataSource = collection.OrderBy(c => c.AcaCalID).ToList();
            gvThesisProjectReg.DataBind();

            LoadDdlForCrRegistration(collection[0].CourseID, collection[0].VersionID);

            ddlMultipleACUSCourse.SelectedValue = collection[0].CourseID.ToString() + "," + collection[0].VersionID.ToString();
            ddlMultipleACUSCourse.Enabled = false;

            LoadCourseCr(collection[0].CourseID, collection[0].VersionID);

            double completeCr = (double)collection.Sum(c => c.CompletedCredit);
            UpdateRemainCr(completeCr);

            LoadDataFromRegistrationWOrksheet(studentID, collection[0].CourseID, collection[0].VersionID);
        }
        else
        {
            lblMessage.Text = "Select Thesis/ Project.";
        }
    }

    private void LoadDataFromRegistrationWOrksheet(int studentID, int courseID, int versionID)
    {
        RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetByStudentIdCourseIdVersionId(studentID, courseID, versionID);
        if (registrationWorksheet != null)
        {
            loadMACUSCourseFromWorksheet(registrationWorksheet.ID);
            btnSave.Enabled = false;
        }
    }

    private void UpdateRemainCr(double completeCr)
    {
        _RemainCr = _CourseCr - completeCr;
        txtRemainCr.Text=_RemainCr.ToString("f");
        
    }

    private void LoadDdlForCrRegistration(int CourseID, int VersionID)
    {
        CourseACUSpanMas courseACUSpanMas = CourseACUSpanMasManager.GetByCourseIDVersionID(CourseID, VersionID);

        ddlMultiSpanDtl.Items.Clear();
        ddlMultiSpanDtl.Items.Add(new ListItem("Select", "0"));
        ddlMultiSpanDtl.AppendDataBoundItems = true;
        ddlMultiSpanDtl.DataTextField = "CreditUnits";
        ddlMultiSpanDtl.DataValueField = "CourseACUSpanDtlID";

        if (courseACUSpanMas != null)
        {
            if (courseACUSpanMas.CourseACUSpanDetails != null)
            {
                ddlMultiSpanDtl.DataSource = courseACUSpanMas.CourseACUSpanDetails;
                ddlMultiSpanDtl.DataBind();
            }
        }
    }

    private void FillStudentInfo(Student student)
    {
        lblProgram.Text = student.Program.ShortName;
        lblBatch.Text = student.Batch.BatchNO.ToString();
        lblName.Text = student.BasicInfo.FullName;
    }

    private void loadMACUSCourseFromWorksheet(int id)
    {
        RegistrationWorksheet registrationWorksheet = RegistrationWorksheetManager.GetById(id);

        hdnlblIdRW.Value = registrationWorksheet.ID.ToString();
        lblFormalCodeRW.Text = registrationWorksheet.FormalCode;
        lblCourseTitleRW.Text = registrationWorksheet.CourseTitle;
        lblCreditsRW.Text = registrationWorksheet.CompletedCredit.ToString();
    }

    private string[] SplitValues(string str)
    {
        return str.Split(new char[] { ',', '-' });
    }

    private void SplitCourseIdVersionIdSectionId()
    {
        if (ddlMultipleACUSCourse.SelectedValue.ToString() != "0")
        {
            string[] str = SplitValues(ddlMultipleACUSCourse.SelectedValue.ToString());

            _courseId = Convert.ToInt32(str[0]);
            _versionId = Convert.ToInt32(str[1]);

        }
    }

    private void LoadCourseCr(int _courseId, int _versionId)
    {
        Course course = CourseManager.GetByCourseIdVersionId(_courseId,   _versionId);
        if (course != null)
        {
            _CourseCr =(double)course.Credits;
            txtTotalCr.Text = _CourseCr.ToString("f");
        }
        else
        {
            txtTotalCr.Text = string.Empty;
        }
    }

    private void CleareTextFild()
    {
        txtTotalCr.Text = string.Empty;
        lblMessage.Text = string.Empty;
    }

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            ddlMultipleACUSCourse.Enabled = true;
            lblMessage.Text = "";
        }

    }

    protected void btnDeleteRW_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32( hdnlblIdRW.Value);
       bool result = RegistrationWorksheetManager.Delete(id);
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        CleareGrid();
        CleareTextFild();
        ddlMultipleACUSCourse.Enabled = true;

        if (!string.IsNullOrEmpty(txtRoll.Text.Trim()))
        {
            Student student = StudentManager.GetByRoll(txtRoll.Text.Trim());

            if (student != null)
            {
                SessionManager.SaveObjToSession<Student>(student, SESSIONS_TUDENT);                 

                LoadMultipleACUSpanCourse(Convert.ToInt32(student.TreeMasterID));
                FillStudentInfo(student);
                LoadDataFromCourseHistory(student.StudentID);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Student student = SessionManager.GetObjFromSession<Student>(SESSIONS_TUDENT);
        SplitCourseIdVersionIdSectionId();
        Course course = CourseManager.GetByCourseIdVersionId(_courseId, _versionId);

        RegistrationWorksheet registrationWorksheet = new RegistrationWorksheet();

        registrationWorksheet.StudentID = student.StudentID;
        registrationWorksheet.CourseID = course.CourseID;
        registrationWorksheet.VersionID = course.VersionID;
        registrationWorksheet.CompletedCredit = Convert.ToDecimal(ddlMultiSpanDtl.SelectedValue);
        registrationWorksheet.IsMultipleACUSpan = true;
        registrationWorksheet.CourseTitle = course.Title;
        registrationWorksheet.FormalCode = course.FormalCode;
        registrationWorksheet.CourseCredit = course.Credits;
        registrationWorksheet.CreatedDate = DateTime.Now;
        registrationWorksheet.ModifiedDate = DateTime.Now;

        int id = RegistrationWorksheetManager.Insert(registrationWorksheet);

        loadMACUSCourseFromWorksheet(id);
        btnSave.Enabled = false;

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {

    }

    protected void ddlMultipleACUSCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        SplitCourseIdVersionIdSectionId();
        LoadDdlForCrRegistration(_courseId, _versionId);
        LoadCourseCr(_courseId, _versionId);


    }
       
    #endregion
}