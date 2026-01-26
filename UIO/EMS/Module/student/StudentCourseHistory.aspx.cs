using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StudentCourseHistory : BasePage
{
    BussinessObject.UIUMSUser userObj = null;

    #region Function

    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
        if (!IsPostBack)
        {
            hdnUserId.Value = userObj.Id.ToString();
            if (userObj.RoleID == 9)
            {
                User user = UserManager.GetById(userObj.Id);
                Student student = StudentManager.GetBypersonID(user.Person.PersonID);

                txtStudentId.Text = student.Roll;
                txtStudentId.ReadOnly = true;
            }
        }
    }

    #endregion

    #region Event

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {

            string studentId = txtStudentId.Text;
            Student student = StudentManager.GetByRoll(studentId);
            if (student != null)
            {
                //if (ProgramAccessAuthentication(userObj, student.ProgramID))
                //{
                if (student.Block != null)
                {
                    if (student.Block.IsResultBlock == true)
                    {
                        showAlert("ID: " + studentId.ToString() + "'s result is blocked because of DUES. (pay dues before deadline to avoid LATE FINE)");
                        return;
                    }
                }

                string batchFullName = "";
                string majorNodeName = "not assign";

                if (student.Major1NodeID != null && student.Major1NodeID != 0)
                {
                    Node node = NodeManager.GetById(student.Major1NodeID);
                    if (node != null)
                        majorNodeName = node.Name;
                }

                Person person = PersonManager.GetById(student.PersonID);
                if (person != null)
                {
                    int acaCalId = student.Batch.AcaCalId;
                    AcademicCalender acaCal = AcademicCalenderManager.GetById(acaCalId);
                    if (acaCal != null)
                    {
                        CalenderUnitType calUnitType = CalenderUnitTypeManager.GetById(acaCal.CalenderUnitTypeID);
                        if (calUnitType != null)
                            batchFullName = calUnitType.TypeName + " " + acaCal.Year;
                    }
                    lblStudentName.Text = person.FullName;
                    lblStudentBatch.Text = batchFullName;// "[" + student.Batch.BatchNO.ToString().PadLeft(3, '0') + "] " + batchFullName;
                    lblStudentProgram.Text = student.Program.ShortName;
                    lblStudentMajor.Text = majorNodeName;
                }
                else
                {
                    lblStudentName.Text = "-------";
                }

                List<StudentCourseHistory> studentCourseHistoryList = StudentCourseHistoryManager.GetAllByStudentId(student.StudentID);

                if (studentCourseHistoryList != null && studentCourseHistoryList.Any())
                {
                    BindResultHistoryNew(studentCourseHistoryList, student.StudentID);
                }
                else
                {
                    showAlert("No Course History Found");
                }
                //}
                //else
                //{
                //    ClearGrid();
                //    CleareTxtField();
                //    showAlert("Access Permission Denied.");
                //}
            }
            else
            {
                showAlert("Student ID Not Found.");
                txtStudentId.Text = "";
                lblStudentName.Text = "";
                lblStudentBatch.Text = "";
                lblStudentProgram.Text = "";
                lblStudentMajor.Text = "";
                ClearGrid();
            }


        }
        catch { ClearGrid(); }
    }

    private void CleareTxtField()
    {
        txtStudentId.Text = "";
        lblStudentName.Text = "";
        lblStudentBatch.Text = "";
        lblStudentProgram.Text = "";
    }

    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }
    private void BindResultHistoryNew(List<StudentCourseHistory> registeredStudentCourseHistoryList, int StudentId)
    {
        try
        {
            string resultDataView = "", resultSummaryView = "";
            var DistinctSession = registeredStudentCourseHistoryList.Select(x => x.AcaCalID).Distinct().ToList();

            if (DistinctSession != null && DistinctSession.Any())
            {
                int Index = 1;

                resultDataView += "<div class='row'> <div class='col-lg-12 col-md-12 col-sm-12'>"
                                       + "<table id='markTable' style='width: 100%;font-weight:bold' cellspacing='0'>"
                                     + "<thead style='background-color:SeaGreen;color:white;'>"
                                     + "<th style='text-align:center'> Academic Session </th>"
                                     + "<th style='text-align:center'> SL </th>"
                                     + "<th> Course Code </th>"
                                     + "<th> Course Title </th>"
                                     + "<th style='text-align:center'> Credit </th>"
                                     + "<th style='text-align:center'> Grade </th>"
                                     + "<th style='text-align:center'> Grade Point </th>"
                                     + "<th style='text-align:center'> View marks </th>"
                                     + "</thead>";


                resultDataView += "<tbody>";


                resultSummaryView += "<div class='row'> <div class='col-lg-12 col-md-12 col-sm-12'>"
                                      + "<table id='summaryTable' style='width: 100%;font-weight:bold' cellspacing='0'>"
                                    + "<thead style='background-color:SeaGreen;color:white;'>"
                                    + "<th style='text-align:center'> SL </th>"
                                    + "<th style='text-align:center'> Academic Session </th>"
                                    + "<th style='text-align:center'> Credit </th>"
                                    + "<th style='text-align:center'> GPA </th>"
                                    + "<th style='text-align:center'> CGPA </th>"

                                    + "</thead>";

                resultSummaryView += "<tbody>";

                var sessionList = AcademicCalenderManager.GetAll();

                var acudetails = StudentACUDetailManager.GetAllByStudentId(StudentId);

                foreach (var item in DistinctSession)
                {
                    decimal TotalCredit = 0;
                    try
                    {
                        string colour = "";
                        if (Index % 2 == 0)
                        {
                            colour = "#d9edf7";
                        }
                        else
                            colour = "aliceblue";

                        var sessionObj = sessionList.Where(x => x.AcademicCalenderID == item).FirstOrDefault();

                        string Session = sessionObj == null ? "" : sessionObj.FullCode;

                        #region Registered Course Result

                        var CourseList = registeredStudentCourseHistoryList.Where(x => x.AcaCalID == item).ToList();

                        if (CourseList != null && CourseList.Any())
                        {
                            int count = CourseList.Count();


                            resultDataView += "<tr style='height:35px;background-color:" + colour + "'>";
                            resultDataView += "<td style='border:1px solid #008080;vertical-align:middle;text-align:center' rowspan=" + count + ">" + Session + " </td>";

                            #region Course Information
                            int I = 1;


                            foreach (var courseItem in CourseList)
                            {
                                string Grade = courseItem.ObtainedGrade;

                                if (courseItem.CourseStatusID == 13)
                                    Grade = "Exempted";

                                if (I == 1)
                                {
                                    TotalCredit = TotalCredit + courseItem.CourseCredit;
                                    try
                                    {
                                        resultDataView += "<td style='border:1px solid #008080;text-align:center'>" + I + "</td>" + "<td style='border:1px solid #008080;padding:2px'>" + courseItem.FormalCode + "</td>" + "<td style='border:1px solid #008080;padding:2px'>" + courseItem.CourseTitle + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + courseItem.CourseCredit + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + Grade + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + courseItem.ObtainedGPA + "</td>";
                                        resultDataView += "<td style='border:1px solid #008080;text-align:center'>" +
     "<button class='btn btn-info' onclick=\"viewCourseDetails('" + courseItem.ID + "')\">View</button>" +
     "</td>";

                                        resultDataView += "</tr>";
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                else
                                {
                                    TotalCredit = TotalCredit + courseItem.CourseCredit;

                                    resultDataView += "<tr style='height:35px;background-color:" + colour + "'>";
                                    resultDataView += "<td style='border:1px solid #008080;text-align:center'>" + I + "</td>" + "<td style='border:1px solid #008080;padding:2px'>" + courseItem.FormalCode + "</td>" + "<td style='border:1px solid #008080;padding:2px'>" + courseItem.CourseTitle + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + courseItem.CourseCredit + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + Grade + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + courseItem.ObtainedGPA + "</td>";

                                    resultDataView += "<td style='border:1px solid #008080;text-align:center'>" +
     "<button class='btn btn-info' onclick=\"viewCourseDetails('" + courseItem.ID + "')\">View</button>" +
     "</td>";


                                    resultDataView += "</tr>";
                                }
                                I++;
                            }


                            #endregion

                        }
                        #endregion

                        #region Summary Table View

                        var acuObj = acudetails.Where(x => x.StdAcademicCalenderID == item).FirstOrDefault();
                        if (acuObj != null)
                        {
                            resultSummaryView += "<tr style='height:35px;background-color:" + colour + "'>";
                            resultSummaryView += "<td style='border:1px solid #008080;text-align:center'>" + Index + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + Session + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + acuObj.Credit.ToString("F2") + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + acuObj.GPA + "</td>" + "<td style='border:1px solid #008080;text-align:center'>" + acuObj.CGPA + "</td>";
                            resultSummaryView += "</tr>";
                        }

                        #endregion

                    }
                    catch (Exception ex)
                    {
                    }

                    resultDataView += "<tr style='height:35px;'>";
                    resultDataView += "<td colspan='4' style='border:1px solid #008080;text-align:center'><b>Total Credit</b></td>" + "<td colspan='4' style='border:1px solid #008080;'><b style='margin-left:25px'>" + TotalCredit + "</b></td>";
                    resultDataView += "</tr>";
                }


                resultDataView += "<tbody></table></div></div>";
                resultSummaryView += "<tbody></table></div></div>";




            }
            pnStudentResultHistory.Controls.Add(new LiteralControl(resultDataView));
            panelResultSummary.Controls.Add(new LiteralControl(resultSummaryView));

        }
        catch (Exception ex)
        {
        }
    }


    private void ClearGrid()
    {
        pnStudentResultHistory.Controls.Add(new LiteralControl(""));
        panelResultSummary.Controls.Add(new LiteralControl(""));

    }

    [WebMethod]
    public static string GetMarksDetails(int studentCourseHistoryId, string userId)
    {
        string html = "";
        try
        {
            UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();

            var coursehistory = ucamContext.StudentCourseHistories
                .FirstOrDefault(x => x.ID == studentCourseHistoryId);

            if (coursehistory != null)
            {
                var TemplateObj = ucamContext.MarksTemplateAndPersonAssigns
                    .FirstOrDefault(x => x.CourseId == coursehistory.CourseID &&
                                         x.VersionId == coursehistory.VersionID &&
                                         x.AcacalId == coursehistory.AcaCalID);

                if (TemplateObj != null && TemplateObj.ExamTemplateMasterId != null)
                {
                    var marksmasterList = ucamContext.ExamMarkMasters
                        .Where(x => x.CourseId == coursehistory.CourseID &&
                                    x.VersionId == coursehistory.VersionID &&
                                    x.AcaCalId == coursehistory.AcaCalID)
                        .ToList();

                    var templateItemList = ExamTemplateBasicItemDetailsManager
                        .GetByExamTemplateMasterId((int)TemplateObj.ExamTemplateMasterId);

                    if (templateItemList != null && templateItemList.Any())
                    {
                        html += "<table class='table table-bordered table-striped'>";

                        html += "<thead><tr><th>Assessment Name</th><th>Marks Obtained</th><th>Converted Marks</th></tr></thead>";

                        html += "<tbody>";

                        decimal totalMarksObtained = 0, totalConvertedMarks = 0,totalExamMarks=0,totamConvertedExamMarks=0;

                        foreach (var item in templateItemList)
                        {
                            try
                            {
                                var markDetail = (from mm in marksmasterList
                                                  join md in ucamContext.ExamMarkDetails
                                                  on mm.ExamMarkMasterId equals md.ExamMarkMasterId
                                                  where md.ExamTemplateBasicItemId == item.ExamTemplateBasicItemId
                                                  && md.CourseHistoryId == studentCourseHistoryId
                                                  select md).FirstOrDefault();


                                html += "<tr>";
                                html += $"<td>{item.ExamTemplateBasicItemName}</td>";
                                if (markDetail != null)
                                {
                                    totalMarksObtained = totalMarksObtained + 0;
                                    totalConvertedMarks = totalConvertedMarks + 0;

                                    totalExamMarks=totalExamMarks + item.ExamTemplateBasicItemMark;
                                    totamConvertedExamMarks=totamConvertedExamMarks + Convert.ToDecimal(item.Attribute2);

                                    if (markDetail.ExamMarkTypeId == 2)
                                    {
                                        html += "<td>Absent</td>";
                                    }
                                    else
                                    {
                                        if (markDetail.Marks != null)
                                        {
                                            html += $"<td>{markDetail.Marks.Value.ToString("0.00")} out of {item.ExamTemplateBasicItemMark.ToString("0.00")}</td>";

                                            html += $"<td>{markDetail.ConvertedMark.Value.ToString("0.00")} out of {Convert.ToDecimal(item.Attribute2).ToString("0.00")} </td>";

                                            totalMarksObtained = totalMarksObtained + markDetail.Marks.Value;
                                            totalConvertedMarks = totalConvertedMarks + markDetail.ConvertedMark.Value;

                                        }
                                        else
                                        {
                                            html += "<td>N/A</td>";
                                        }
                                    }
                                }
                                else
                                {
                                    html += "<td>N/A</td>";
                                }
                                html += "</tr>";
                            }
                            catch (Exception ex)
                            {
                                html += $"<tr><td colspan='3'>Error loading item: {item.ExamTemplateBasicItemName}</td></tr>";
                            }
                        }

                        /// Total Row
                        /// 

                        html += "<tr style='font-weight:bold;'>";
                        html += $"<td>Total</td>";
                        html += $"<td>{totalMarksObtained.ToString("0.00")} out of {totalExamMarks.ToString("0.00")}</td>";
                        html += $"<td>{totalConvertedMarks.ToString("0.00")} out of {totamConvertedExamMarks.ToString("0.00")}</td>";
                        html += "</tr>";


                        html += "</tbody></table>";
                    }
                    else
                    {
                        html = "<p>No marks template item found.</p>";
                    }
                }
                else
                {
                    html = "<p>No marks template found.</p>";
                }
            }
            else
            {
                html = "<p>No course history found.</p>";
            }
        }
        catch (Exception ex)
        {
            html = "<p>An error occurred while fetching marks details.</p>";
        }

        return html;
    }


    #endregion
}