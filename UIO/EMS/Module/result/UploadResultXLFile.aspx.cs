
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.result
{
    public partial class UploadResultXLFile : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.UploadResultXLFile);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.UploadResultXLFile));
        public BussinessObject.UIUMSUser CurrentUser;
        int userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            CurrentUser = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            string loginID = CurrentUser.LogInID;
            User user = UserManager.GetByLogInId(loginID);
            if (user != null)
                userId = user.User_ID;
            if (!IsPostBack)
            {
                ucProgram.LoadDropdownWithUserAccess(userId);
                // ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            }
        }
        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucSession.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            Label2.Visible = false;
            btnsave.Visible = false;
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();
            string saveFolder = "~/Upload/";
            string filename = FileUpload1.FileName;
            string filePath = Path.Combine(saveFolder, FileUpload1.FileName);
            string excelpath = Server.MapPath(filePath);

            lblMsg.Text = "";

            if (File.Exists(excelpath))
            {
                System.IO.File.Delete(excelpath);
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "File already exists.Please Upload again.";
            }
            else
            {
                FileUpload1.SaveAs(excelpath);

                try
                {
                    System.Data.OleDb.OleDbConnection MyConnection;
                    System.Data.DataTable DtTable;
                    System.Data.OleDb.OleDbDataAdapter MyCommand;
                    MyConnection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelpath + ";Extended Properties=Excel 12.0 xml;");
                    MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Course$]", MyConnection);
                    MyCommand.TableMappings.Add("Table", "TestTable");
                    DtTable = new System.Data.DataTable();
                    MyCommand.Fill(DtTable);
                    PopulateData(DtTable, filename);
                    MyConnection.Close();
                }
                catch (Exception ex)
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = ex.ToString();
                    System.IO.File.Delete(excelpath);
                }
            }
        }
        private void PopulateData(DataTable DtTable, string filename)
        {
            string[] info = filename.Split('_');
            Label2.Visible = false;
            int xlStudent = 0, gridstudent = 0;
            List<StudentList> stdlist = new List<StudentList>();
            int i = 0;
            string code = "";
            string title = "";
            List<Course> course = new List<Course>();
            Course c = new Course();
            foreach (DataRow row in DtTable.Rows)
            {
                if (i == 6)
                {
                    code = row[2].ToString();
                }
                if (i == 7)
                {
                    title = row[2].ToString();
                    course = CourseManager.GetAllByFormalCode(code).Where(x => x.Title == title).ToList();
                    c = course.FirstOrDefault();
                }

                if (i > 13)
                {
                    string sl = row[0].ToString();
                    if (sl == "")
                    {
                        Session["StudentList"] = stdlist;
                        Label2.Visible = true;
                        gvStudentList.DataSource = stdlist;
                        gvStudentList.DataBind();
                        goto Check;
                    }
                    else
                    {
                        xlStudent++;
                        StudentList std = new StudentList();
                        std.CourseCode = Convert.ToInt32(code);
                        std.CourseId = c.CourseID;
                        std.VersionId = c.VersionID;
                        std.Roll = row[3].ToString();
                        if (row[4].ToString() != "")
                        {
                            gridstudent++;
                            std.Marks = Convert.ToDecimal(row[4]);
                            stdlist.Add(std);
                        }
                        else
                        {
                            std.Marks = -1;
                            stdlist.Add(std);
                        }
                    }
                }
                i++;
            }

        Check:
            lblMsg.ForeColor = Color.Red;
            string msg = "Total Students " + xlStudent + " and Mark Exist " + gridstudent;
            //if (xlStudent == gridstudent)
            //{
            msg = msg + " .save data";
            btnsave.Visible = true;
            //}
            //else
            //{
            //    msg = msg + ". Update students mark for save.";
            //}
            lblMsg.Text = msg;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                List<StudentList> _stdinfo = null;
                _stdinfo = new List<StudentList>();
                _stdinfo = (List<StudentList>)Session["StudentList"];
                int updatedStudent = 0;
                int nup = 0;
                string nupRoll = "";
                if (_stdinfo.Count > 0)
                {
                    foreach (var item in _stdinfo)
                    {
                        int courseId = Convert.ToInt32(item.CourseId);
                        int versionId=Convert.ToInt32(item.VersionId);
                        string roll = item.Roll.ToString();
                        decimal mark = item.Marks;
                        Student student = StudentManager.GetByRoll(roll);
                        if (student != null)
                        {
                            //check student course history 
                            StudentCourseHistory sch = StudentCourseHistoryManager.GetAllByAcaCalId(Convert.ToInt32(ucSession.selectedValue)).Where(x => x.CourseID == courseId && x.VersionID == versionId && x.StudentID == student.StudentID).FirstOrDefault();
                            if (sch != null)
                            {
                                bool isUpdate = false;
                                if (mark == -1)
                                {
                                    #region UpdateCourseHistory
                                    sch.CourseStatusID = 2;
                                    sch.GradeId = 11;
                                    sch.ObtainedGrade = "AB";
                                    sch.ObtainedGPA = 0;
                                    sch.ModifiedBy = userId;
                                    sch.ModifiedDate = DateTime.Now;
                                    sch.Remark = "Upload by XL";
                                    #endregion
                                }
                                else
                                {
                                    GradeDetails grd = GradeDetailsManager.GetByMarks(mark);
                                    if (grd != null)
                                    {
                                        #region UpdateCourseHistory
                                        if (grd.Grade != "F")
                                            sch.CourseStatusID = 1;
                                        else
                                            sch.CourseStatusID = 2;
                                        sch.GradeId = grd.GradeId;
                                        sch.ObtainedGrade = grd.Grade;
                                        sch.ObtainedGPA = grd.GradePoint;
                                        sch.ObtainedTotalMarks = mark;
                                        sch.ModifiedBy = userId;
                                        sch.ModifiedDate = DateTime.Now;
                                        sch.Remark = "Upload by XL";
                                        #endregion
                                    }
                                }

                                isUpdate = StudentCourseHistoryManager.Update(sch);

                                if (isUpdate)
                                {
                                    updatedStudent++;

                                }
                                else
                                {
                                    nup++;
                                    nupRoll = nupRoll + " " + roll;
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
                                            "Student Course History Update",
                                            BaseCurrentUserObj.LogInID + " Update result for Course : " + courseId + " Session " + Convert.ToInt32(ucSession.selectedValue) + " " + " Mark " + mark,
                                            "normal",
                                            _pageId,
                                            _pageName,
                                            _pageUrl,
                                            roll);
                                }
                                catch (Exception ex)
                                { }
                                #endregion
                            }
                            else
                            {
                                nup++;
                                nupRoll = nupRoll + " " + roll;
                            }
                        }
                        else
                        {
                            nup++;
                            nupRoll = nupRoll + " " + roll;
                        }
                    }
                }
                if (updatedStudent > 0)
                {
                    string msg = "Marks Updated Successfully " + updatedStudent;
                    if (nup > 0)
                        msg = msg + " not Updated " + nup + " and not updated rolls are " + nupRoll;
                    gvStudentList.DataSource = null;
                    gvStudentList.DataBind();
                    lblMsg.Text = msg;

                }
                else
                    lblMsg.Text = "0 students Updated.";
            }
            catch
            {
            }
        }
    }
}