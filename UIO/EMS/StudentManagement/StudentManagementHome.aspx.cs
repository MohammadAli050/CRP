using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using CommonUtility;
using LogicLayer.BusinessObjects;

public partial class StudentManagement_StudentManagementHome : BasePage
{
    BussinessObject.UIUMSUser userObj = null;
    UCAMDAL.UCAMEntities ucamContext = new UCAMDAL.UCAMEntities();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
        {
            Page.ClientTarget = "uplevel";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            SessionManager.DeletFromSession("StudentEdit");
            base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadInstitute();
                ucProgram.LoadDropdownWithUserAccessAndInstitute(userObj.Id, 0);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
            //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
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

    protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        ClearAll();

        int programId = Convert.ToInt32(ucProgram.selectedValue);
        int batchId = Convert.ToInt32(ucBatch.selectedValue);
        string roll = txtStudent.Text == "" ? "0" : txtStudent.Text;
        List<StudentRegistration> studentRegistrationList = StudentRegistrationManager.GetAllByProgramBatchStudent(programId, batchId, roll);
        var instList = ucamContext.Institutions.Where(x => x.ActiveStatus == 1).ToList();
        if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
        {
            try
            {
                List<LogicLayer.BusinessObjects.Student> stdList = StudentManager.GetAllByProgramIdBatchId(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucBatch.selectedValue));
                if (stdList != null && stdList.Count > 0)
                {
                    foreach (LogicLayer.BusinessObjects.Student tempStudent in stdList)
                    {
                        tempStudent.Attribute3 = "";
                        tempStudent.Remarks = "";

                        if (studentRegistrationList.Count > 0 && studentRegistrationList != null)
                        {

                            tempStudent.Attribute1 = "";
                            tempStudent.Attribute2 = "";
                            StudentRegistration tempStudentRegistration = studentRegistrationList.Where(x => x.StudentId == tempStudent.StudentID).FirstOrDefault();
                            if (tempStudentRegistration != null)
                            {
                                //This field Temporary Use for Student Registration Number
                                tempStudent.Attribute1 = tempStudentRegistration.RegistrationNo;
                                //This field Temporary Use for  Student Session
                                tempStudent.Attribute2 = tempStudentRegistration.Attribute1;//attritube pass session name using at Query
                                //This field Temporary Use for Studet Photo Path
                            }
                        }
                    }

                    stdList = stdList.OrderBy(x => x.Roll).ToList();
                    GvStudent.DataSource = stdList;
                    GvStudent.DataBind();
                }
            }
            catch (Exception ex) { }
        }
        else
        {
            try
            {
                LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                if (student != null)
                {
                    if (studentRegistrationList.Count > 0 && studentRegistrationList != null)
                    {
                        student.Attribute1 = studentRegistrationList[0].RegistrationNo;
                        student.Attribute2 = studentRegistrationList[0].Attribute1;//attritube pass session name using at Query
                    }


                    if (base.ProgramAccessAuthentication(userObj, student.ProgramID))
                    {
                        List<LogicLayer.BusinessObjects.Student> stdlist = new List<LogicLayer.BusinessObjects.Student>();
                        stdlist.Add(student);
                        GvStudent.DataSource = stdlist;
                        GvStudent.DataBind();
                    }
                    else
                        showAlert("Access denied");
                }
                else
                    showAlert("Student not found!");
            }
            catch { }
        }
    }

    private void ClearAll()
    {
        GvStudent.DataSource = null;
        GvStudent.DataBind();
    }

    protected void GvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int studentId = Convert.ToInt32(e.CommandArgument);
        LogicLayer.BusinessObjects.Student studentObj = new LogicLayer.BusinessObjects.Student();
        studentObj = StudentManager.GetById(studentId);
        //HiddenStudentId.Value = Convert.ToString(studentId);

        if (e.CommandName == "StudentEdit")
        {
            if (studentObj != null)
            {
                SessionManager.SaveObjToSession(studentObj, "StudentEdit");
                Response.Redirect("~/StudentManagement/Profile/StudentProfile.aspx");
            }
        }
    }

    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucProgram.LoadDropdownWithUserAccessAndInstitute(userObj.Id, Convert.ToInt32(ddlInstitute.SelectedValue));
    }


    protected void showAlert(string msg)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", "swal('" + msg + "');", true);
    }

    protected void GvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            #region Show Image And Signature
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgPhoto = (Image)e.Row.FindControl("Photo");
                Image imgSig = (Image)e.Row.FindControl("Signature");

                HiddenField hdnStudentId = (HiddenField)e.Row.FindControl("hdnStudentId");
                int studentId = Convert.ToInt32(hdnStudentId.Value);
                var student = ucamContext.Students.Find(studentId);
                if (student != null)
                {
                    try
                    {
                        var person = ucamContext.People.Find(student.PersonID);
                        if (person != null)
                        {
                            if (person.PhotoBytes != null)
                            {
                                // Show the uploaded photo in imgPhoto control
                                imgPhoto.ImageUrl = "data:image/png;base64," + person.PhotoBytes;
                                imgPhoto.Width = imgPhoto.Width;
                                imgPhoto.Height = imgPhoto.Height;
                            }

                            if (person.SignatureBytes != null)
                            {
                                // Show the uploaded photo in imgPhoto control
                                imgSig.ImageUrl = "data:image/png;base64," + person.SignatureBytes;
                                imgSig.Width = imgSig.Width;
                                imgSig.Height = imgSig.Height;
                            }
                        }
                    }
                    catch
                    {
                        imgPhoto.ImageUrl = "../Upload/Avatar/Female.jpg";
                    }
                }
            }
            #endregion

        }
        catch (Exception ex)
        {
        }

    }
}
