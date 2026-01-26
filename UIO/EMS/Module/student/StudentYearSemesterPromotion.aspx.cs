using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.student
{
    public partial class StudentYearSemesterPromotion : BasePage
    {
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
        string _pageName = Convert.ToString(CommonUtility.CommonEnum.PageName.StudentYearSemesterPromotion);
        string _pageId = Convert.ToString(Convert.ToInt32(CommonUtility.CommonEnum.PageName.StudentYearSemesterPromotion));

        BussinessObject.UIUMSUser UserObj = null;
        UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);

                base.CheckPage_Load();
                if (!IsPostBack)
                {
                    divUpdateCard.Visible = false;
                    divGridView.Visible = false;
                    LoadInstitute();
                    LoadYear();
                    LoadSemester();
                    ucProgram.LoadDropdownWithUserAccessAndInstitute(UserObj.Id, 0);
                }
            }
            catch (Exception Ex)
            {
            }
        }

        private void LoadYear()
        {
            try
            {
                var year = ucamContext.Years.ToList();
                ddlYear.Items.Clear();
                ddlYear.AppendDataBoundItems = true;
                ddlYear.Items.Add(new ListItem("All", "0"));

                ddlUpdateYear.Items.Clear();
                ddlUpdateYear.AppendDataBoundItems = true;
                ddlUpdateYear.Items.Add(new ListItem("Select", "0"));

                if (year != null && year.Any())
                {
                    ddlYear.DataTextField = "YearName";
                    ddlYear.DataValueField = "YearId";
                    ddlYear.DataSource = year.OrderBy(x => x.YearId);
                    ddlYear.DataBind();

                    ddlUpdateYear.DataTextField = "YearName";
                    ddlUpdateYear.DataValueField = "YearId";
                    ddlUpdateYear.DataSource = year.OrderBy(x => x.YearId);
                    ddlUpdateYear.DataBind();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadSemester()
        {
            try
            {
                var semester = ucamContext.Semesters.ToList();

                ddlSemester.Items.Clear();
                ddlSemester.AppendDataBoundItems = true;
                ddlSemester.Items.Add(new ListItem("All", "0"));

                ddlUpdateSemester.Items.Clear();
                ddlUpdateSemester.AppendDataBoundItems = true;
                ddlUpdateSemester.Items.Add(new ListItem("Select", "0"));

                if (semester != null && semester.Any())
                {
                    ddlSemester.DataTextField = "SemesterName";
                    ddlSemester.DataValueField = "SemesterId";
                    ddlSemester.DataSource = semester.OrderBy(x => x.SemesterName);
                    ddlSemester.DataBind();

                    ddlUpdateSemester.DataTextField = "SemesterName";
                    ddlUpdateSemester.DataValueField = "SemesterId";
                    ddlUpdateSemester.DataSource = semester.OrderBy(x => x.SemesterName);
                    ddlUpdateSemester.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadInstitute()
        {
            try
            {
                var InstituteList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();

                ddlInstitute.Items.Clear();
                ddlInstitute.AppendDataBoundItems = true;
                ddlInstitute.Items.Add(new ListItem("Select", "0"));


                if (InstituteList != null && InstituteList.Any())
                {
                    ddlInstitute.DataTextField = "InstituteName";
                    ddlInstitute.DataValueField = "InstituteId";
                    ddlInstitute.DataSource = InstituteList.OrderBy(x => x.InstituteName);
                    ddlInstitute.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucProgram.LoadDropdownWithUserAccessAndInstitute(UserObj.Id, Convert.ToInt32(ddlInstitute.SelectedValue));
        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearAll();

            try
            {
                int ProgramId = Convert.ToInt32(ucProgram.selectedValue);
                int BatchId = Convert.ToInt32(ucBatch.selectedValue);
                int YearId = Convert.ToInt32(ddlYear.SelectedValue);
                int SemesterId = Convert.ToInt32(ddlSemester.SelectedValue);
                int InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
                int StudentId = 0;

                #region Get Student ID

                if (!string.IsNullOrWhiteSpace(txtStudent.Text.Trim()))
                {
                    var student = ucamContext.Students.Where(x => x.Roll == txtStudent.Text.Trim()).FirstOrDefault();
                    if (student != null)
                    {
                        StudentId = student.StudentID;
                    }
                }

                #endregion

                if (ProgramId == 0 && StudentId == 0)
                {
                    showAlert("Please select a program or enter a student ID");
                    return;
                }

                var studentList = (from student in ucamContext.Students
                                   join per in ucamContext.People on student.PersonID equals per.PersonID
                                   join prog in ucamContext.Programs on student.ProgramID equals prog.ProgramID
                                   // Left join with student year semester history
                                   join sys in ucamContext.StudentYearSemesterHistories
                                    on student.StudentID equals sys.StudentId into sysList
                                   from sys in sysList.DefaultIfEmpty()
                                   join yr in ucamContext.Years on sys.YearId equals yr.YearId into yrList
                                   from yr in yrList.DefaultIfEmpty()
                                   join sem in ucamContext.Semesters on sys.SemesterId equals sem.SemesterId into semList
                                   from sem in semList.DefaultIfEmpty()
                                   where (student.ProgramID == ProgramId || ProgramId == 0)
                                      && (student.BatchId == BatchId || BatchId == 0)
                                      && (student.StudentID == StudentId || StudentId == 0)
                                      && (sys.YearId == YearId || YearId == 0)
                                      && (sys.SemesterId == SemesterId || SemesterId == 0)
                                      && (sys == null || sys.IsActive == true)
                                   select new
                                   {
                                       student.StudentID,
                                       student.Roll,
                                       per.FullName,
                                       prog.ShortName,
                                       yr.YearName,
                                       sem.SemesterName

                                   }).ToList();

                if (studentList != null && studentList.Count > 0)
                {
                    divGridView.Visible = true;
                    divUpdateCard.Visible = true;

                    GvStudent.DataSource = studentList;
                    GvStudent.DataBind();
                }
                else
                {
                    showAlert("No data found!");
                }

            }
            catch (Exception ex)
            {
            }

        }

        private void ClearAll()
        {
            divUpdateCard.Visible = false;
            divGridView.Visible = false;
            GvStudent.DataSource = null;
            GvStudent.DataBind();

            ddlUpdateSemester.SelectedValue = "0";
            ddlUpdateYear.SelectedValue = "0";
        }

        protected void showAlert(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int updateYearId = Convert.ToInt32(ddlUpdateYear.SelectedValue);
                int updateSemesterId = Convert.ToInt32(ddlUpdateSemester.SelectedValue);

                if (GvStudent.Rows.Count == 0)
                {
                    showAlert("No student to update. Please load students first.");
                    return;
                }
                List<int> studentIds = new List<int>();
                foreach (GridViewRow row in GvStudent.Rows)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        int studentId = Convert.ToInt32(GvStudent.DataKeys[row.RowIndex].Value);
                        studentIds.Add(studentId);
                    }
                }
                if (studentIds.Count == 0)
                {
                    showAlert("Please select at least one student to update.");
                    return;
                }
                int UpdateCount = 0;
                foreach (int studentId in studentIds)
                {
                    var activeSys = ucamContext.StudentYearSemesterHistories
                                    .Where(x => x.StudentId == studentId && x.IsActive == true)
                                    .FirstOrDefault();
                    var student = ucamContext.Students.Where(x => x.StudentID == studentId).FirstOrDefault();

                    if (activeSys != null)
                    {
                        if (activeSys.YearId == updateYearId && activeSys.SemesterId == updateSemesterId)
                        {
                            // No update needed, already in the desired year and semester
                            continue;
                        }
                        activeSys.IsActive = false;
                        ucamContext.Entry(activeSys).State = System.Data.Entity.EntityState.Modified;
                        ucamContext.SaveChanges();

                        UCAMDAL.StudentYearSemesterHistory newSys = new UCAMDAL.StudentYearSemesterHistory();
                        newSys.StudentId = studentId;
                        newSys.YearId = updateYearId;
                        newSys.SemesterId = updateSemesterId;
                        newSys.IsActive = true;
                        newSys.CreatedBy = UserObj.Id;
                        newSys.CreatedDate = DateTime.Now;
                        ucamContext.StudentYearSemesterHistories.Add(newSys);
                        ucamContext.SaveChanges();
                        UpdateCount++;

                        MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Year/Semester Update", UserObj.LogInID + "updated Student Year/Semester for Roll : " + student.Roll + " to Year : " + ddlUpdateYear.SelectedItem.ToString() + " and Semester : " + ddlUpdateSemester.SelectedItem.ToString(), "", "", _pageId, _pageName, _pageUrl);
                    }
                    else
                    {
                        UCAMDAL.StudentYearSemesterHistory yearSemesterHistory = new UCAMDAL.StudentYearSemesterHistory();
                        yearSemesterHistory.StudentId = studentId;
                        yearSemesterHistory.YearId = updateYearId;
                        yearSemesterHistory.SemesterId = updateSemesterId;
                        yearSemesterHistory.IsActive = true;
                        yearSemesterHistory.CreatedBy = UserObj.Id;
                        yearSemesterHistory.CreatedDate = DateTime.Now;
                        ucamContext.StudentYearSemesterHistories.Add(yearSemesterHistory);
                        ucamContext.SaveChanges();
                        UpdateCount++;
                        MisscellaneousCommonMethods.InsertLog(UserObj.LogInID, "Year/Semester Update", UserObj.LogInID + "updated Student Year/Semester for Roll : " + student.Roll + " to Year : " + ddlUpdateYear.SelectedItem.ToString() + " and Semester : " + ddlUpdateSemester.SelectedItem.ToString(), "", "", _pageId, _pageName, _pageUrl);

                    }
                }
                if (UpdateCount > 0)
                {
                    showAlert(UpdateCount + " student(s) updated successfully.");
                    btnLoad_Click(null, null);
                }
                else
                {
                    showAlert("No updates were made. Selected students may already be in the specified year and semester.");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}