using BussinessObject;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.admin
{
    public partial class StudentCreate : BasePage
    {
        UIUMSUser userObj = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
            _scriptMan.AsyncPostBackTimeout = 36000;
            if(!IsPostBack)
            {
                DateTime dd = DateTime.Now;
                string date = dd.ToString("MM/dd/yyyy");
                DateTextBox.Text = date;
            }
            lblMsg.Text = null;
        }

        protected void btnStudentCheck_Click(object sender, EventArgs e)
        {
            try 
            { 
                string studentRoll = Convert.ToString(txtStudentRoll.Text.Trim());
                LogicLayer.BusinessObjects.Student studentObj = StudentManager.GetByRoll(studentRoll);
                if (studentObj != null)
                {
                    lblMsg.Text = "Student already exists.";
                }
                else
                {
                    btnSubmit.Enabled = true;
                    //lblMsg.Text = "";
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try 
            {
                string studentRoll = Convert.ToString(txtStudentRoll.Text.Trim());
                LogicLayer.BusinessObjects.Student studentObj1 = StudentManager.GetByRoll(studentRoll);
                if (studentObj1 == null)
                {
                    LogicLayer.BusinessObjects.Person personObj = new LogicLayer.BusinessObjects.Person();
                    personObj.FullName = Convert.ToString(txtStudentName.Text);
                    personObj.Gender = Convert.ToString(ddlGender.SelectedItem);
                    personObj.Phone = Convert.ToString(txtPhoneNo.Text);
                    personObj.Email = Convert.ToString(txtEmailAddress.Text);
                    personObj.DOB = DateTime.ParseExact(DateTextBox.Text.Replace("/", string.Empty), "ddMMyyyy", null);
                    personObj.CreatedBy = userObj.Id;
                    personObj.CreatedDate = DateTime.Now;
                    personObj.ModifiedBy = userObj.Id;
                    personObj.ModifiedDate = DateTime.Now;

                    int result = PersonManager.Insert(personObj);

                    if (result > 0)
                    {
                        LogicLayer.BusinessObjects.Student studentObj = new LogicLayer.BusinessObjects.Student();
                        studentObj.PersonID = result;
                        studentObj.Roll = txtStudentRoll.Text;
                        studentObj.ProgramID = Convert.ToInt32(ucProgram.selectedValue);
                        studentObj.BatchId = Convert.ToInt32(ucBatch.selectedValue);
                        studentObj.CreatedBy = userObj.Id;
                        studentObj.GradeMasterId = 2;
                        studentObj.CreatedDate = DateTime.Now;
                        studentObj.ModifiedBy = userObj.Id;
                        studentObj.ModifiedDate = DateTime.Now;
                        int result2 = StudentManager.Insert(studentObj);
                        if (result2 > 0)
                        {
                            lblMsg.Text = "Student created successfully.";
                            btnSubmit.Enabled = false;
                        }
                        else
                        {
                            PersonManager.Delete(result);
                            lblMsg.Text = "Student could not created successfully.";
                            PersonManager.Delete(result);
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Student could not created successfully.";
                    }
                }
                else
                {
                    lblMsg.Text = "Student already exists.";
                }
                
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}