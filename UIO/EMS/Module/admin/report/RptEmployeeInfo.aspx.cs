using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects;
using System.Drawing;

namespace EMS.miu.admin
{
    public partial class RptEmployeeInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if (!Page.IsPostBack)
            {
                LoadDropDown();
            }
        }

        private void LoadDropDown()
        {
            #region Department

            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("-Select-", "0"));
            ddlDepartment.AppendDataBoundItems = true;
            List<Department> deptList = DepartmentManager.GetAll();

            if (deptList != null && deptList.Count > 0)
            {
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataTextField = "Code";

                ddlDepartment.DataSource = deptList;
                ddlDepartment.DataBind();
            }

            #endregion

            ddlGender.Items.Add(new ListItem("-All-", "0"));
            ddlGender.Items.Add(new ListItem("Male", "1"));
            ddlGender.Items.Add(new ListItem("Female", "2"));

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMessage("", Color.Red);
                int deptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                LoadData(deptId);
            }
            catch (Exception)
            {

            }
        }

        private void LoadData(int deptId)
        {
            try
            {
                ReportParameter p1 = new ReportParameter("FullName", chkFullName.Checked.ToString());
                ReportParameter p2 = new ReportParameter("DateOfBirth", chkDateOfBirth.Checked.ToString());
                ReportParameter p3 = new ReportParameter("Gender", chkGender.Checked.ToString());
                ReportParameter p4 = new ReportParameter("Phone", chkPhone.Checked.ToString());
                ReportParameter p5 = new ReportParameter("Email", chkEmail.Checked.ToString());
                ReportParameter p6 = new ReportParameter("Photo", chkPhoto.Checked.ToString());

                List<rEmployeeInfo> employeeList = EmployeeManager.GetEmployeeInfoByDeptId(deptId);

                if (employeeList != null && employeeList.Count != 0)
                {
                    if (ddlGender.SelectedValue == "1")
                        employeeList = employeeList.Where(x => x.Gender != null && x.Gender.Equals("Male")).ToList();
                    else if (ddlGender.SelectedValue == "2")
                        employeeList = employeeList.Where(x => x.Gender != null && x.Gender.Equals("Female")).ToList();

                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/miu/admin/report/RptEmployeeInfo.rdlc");
                    this.ReportViewer.LocalReport.EnableExternalImages = true;
                    this.ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
                    ReportDataSource rds = new ReportDataSource("EmployeeDataSet", employeeList);
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rds);
                    ReportViewer.Visible = true;

                }
                else
                { 
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.Visible = false;
                    ShowMessage("No Data Found!",Color.Red);
                }
            }
            catch (Exception)
            { }
        }

        private void ShowMessage(string Message, Color color)
        {
            lblMsg.Text = Message;
            lblMsg.ForeColor = color;
        }
    }
}