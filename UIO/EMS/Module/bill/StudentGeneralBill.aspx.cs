using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessLogic;
using CommonUtility;

public partial class Student_StudentGeneralBill : BasePage
{
    decimal _totalAmount = 0;
    decimal _totalAmountByCollectiveDiscount = 0;
    decimal _totalAmountByIterativeDiscount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["stdId"] != null && Request.QueryString["acacal"] != null)
        {
            Student student = StudentManager.GetById(Convert.ToInt32(Request.QueryString["stdId"]));
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;

            List<BillView> billViewList = BillViewManager.GetBy(Convert.ToInt32(Request.QueryString["stdId"]), Convert.ToInt32(Request.QueryString["acacal"]));
            gvBillView.DataSource = billViewList;
            gvBillView.DataBind();
        }
        else if (Request.QueryString["stdId"] != null)
        {
            Student student = StudentManager.GetById(Convert.ToInt32(Request.QueryString["stdId"]));
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;


            List<BillView> billViewList = BillViewManager.GetBy(Convert.ToInt32(Request.QueryString["stdId"]));
            gvBillView.DataSource = billViewList;
            gvBillView.DataBind();
        }
        else
        {
            gvBillView.DataSource = null;
            gvBillView.DataBind();
        }

        if (!IsPostBack)
        {
            FillSessionAcademicCalenderCombo();
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);
        lblMessage.Text = string.Empty;

        if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
        {
            lblMessage.Text = "Insert Student ID.";
            lblMessage.Focus();
            return;
        }
        else if (sessionId == 0)
        {
            lblMessage.Text = "Please select Session.";
            lblMessage.Focus();
            return;
        }

        Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());

        if (student != null)
        {
            lblProgram.Text = student.Program.ShortName;
            lblBatch.Text = student.Batch.BatchNO.ToString();
            lblName.Text = student.BasicInfo.FullName;


            List<BillView> billViewList = BillViewManager.GetBy(student.StudentID);
            gvBillView.DataSource = billViewList.Where(b => b.TrimesterId == sessionId).ToList();
            gvBillView.DataBind();
        }
    }

    private void FillSessionAcademicCalenderCombo()
    {
        try
        {
            ddlAcaCalSession.Items.Clear();
            ddlAcaCalSession.Items.Add(new ListItem("Select", "0")); 

            List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll();
            academicCalenderList = academicCalenderList.OrderByDescending(x => x.AcademicCalenderID).ToList();

            ddlAcaCalSession.AppendDataBoundItems = true;

            if (academicCalenderList != null)
            {
                int count = academicCalenderList.Count;
                foreach (AcademicCalender academicCalender in academicCalenderList)
                {
                    ddlAcaCalSession.Items.Add(new ListItem("[" + academicCalender.Code + "] " + UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally { }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;
            int sessionId = Convert.ToInt32(ddlAcaCalSession.SelectedItem.Value);

            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                lblMessage.Text = "Insert Student ID.";
                lblMessage.Focus();
                return;
            }
            else if (sessionId == 0)
            {
                lblMessage.Text = "Please select Session.";
                lblMessage.Focus();
                return;
            }
            else
            {
                Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                if (student == null)
                {
                    lblMessage.Text = "Student not found";
                    return;
                }

                lblMessage.Text = BillViewManager.GenerateBill(student, sessionId);

                List<BillView> billViewList = BillViewManager.GetBy(student.StudentID);
                gvBillView.DataSource = billViewList.Where(b => b.TrimesterId == sessionId).ToList();
                gvBillView.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void gvBillView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            if (lblAmount.Text != string.Empty)
            {
                _totalAmount += decimal.Parse(lblAmount.Text);
            }

            //Label lblAmountByCollectiveDiscount = (Label)e.Row.FindControl("lblAmountByCollectiveDiscount");
            //if (lblAmount.Text != string.Empty)
            //{
            //    _totalAmountByCollectiveDiscount += decimal.Parse(lblAmountByCollectiveDiscount.Text);
            //}

            //Label lblAmountByIterativeDiscount = (Label)e.Row.FindControl("lblAmountByIterativeDiscount");
            //if (lblAmount.Text != string.Empty)
            //{
            //    _totalAmountByIterativeDiscount += decimal.Parse(lblAmountByIterativeDiscount.Text);
            //}
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
            lblTotalAmount.Text = Convert.ToString(_totalAmount);

            //Label lblTotalAmountByCollectiveDiscount = (Label)e.Row.FindControl("lblTotalAmountByCollectiveDiscount");
            //lblTotalAmountByCollectiveDiscount.Text = Convert.ToString(_totalAmountByCollectiveDiscount);

            //Label lblTotalAmountByIterativeDiscount = (Label)e.Row.FindControl("lblTotalAmountByIterativeDiscount");
            //lblTotalAmountByIterativeDiscount.Text = Convert.ToString(_totalAmountByIterativeDiscount);
        }
    }
}