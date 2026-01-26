using LogicLayer.BusinessObjects.RO;
using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using LogicLayer.BusinessObjects;


namespace EMS.miu.bill.report
{
    public partial class RptAdmissionRegistrationCount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();
            if(!IsPostBack)
            {
                loadProgralDDL();
                ddList2_3_Populate();
            }
        }

        private void loadProgralDDL()
        {
            ddlProgram.DataSource = ProgramManager.GetAll();
            // ddList1.DataSource = cmd.ExecuteReader();
            ddlProgram.DataTextField = "Code";
            ddlProgram.DataValueField = "ProgramID";
            ddlProgram.DataBind();
        }

        /*protected void RunReport_Click(object sender, EventArgs e)
        {

            //rAdmissionRegistrationCount rAddmissionRegistrationCountObj = new rAdmissionRegistrationCount();
            DataTable DataTable1 = new DataTable();
            DataSet DataSetForReport = new DataSet();
            int ProgramID = Convert.ToInt32(ddlProgram.SelectedValue);
            int YearFrom = Convert.ToInt32(ddList2.SelectedValue);
            int YearTo = Convert.ToInt32(ddList3.SelectedValue);
            DataSetForReport = AdmissionRegistrationCountManager.function_DataForReport(ProgramID, YearFrom, YearTo);
            Label1.Text = "";
            Label1.Text = "Admitted Vs Registered,   " + ddlProgram.SelectedItem + ";   " + YearFrom + "-" + YearTo;

            GridView1.Visible = true;
            GridView1.DataSource = DataSetForReport.Tables[0];
            GridView1.DataBind();

            // GridView1.Columns[1].ItemStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.BorderStyle = BorderStyle.Ridge;
            GridView1.Rows[(GridView1.Rows.Count - 1)].BackColor = System.Drawing.Color.Orange;
            //  GridView1.Rows[0].BackColor = System.Drawing.Color.Olive;
            int columnCount = GridView1.Rows[0].Cells.Count;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                GridView1.Rows[i].Cells[0].BackColor = System.Drawing.Color.DarkTurquoise;
                GridView1.Rows[i].Cells[1].BackColor = System.Drawing.Color.DarkTurquoise;
                GridView1.Rows[i].Cells[2].BackColor = System.Drawing.Color.Goldenrod;
                GridView1.Rows[i].Cells[columnCount - 2].BackColor = System.Drawing.Color.OliveDrab;
                GridView1.Rows[i].Cells[columnCount - 1].BackColor = System.Drawing.Color.DarkTurquoise;
            }

            GridView1.AllowSorting = false;

        }
        */
        public void ddList2_3_Populate()
        {
            List<rYear> yearList = AcademicCalenderManager.GetAllYear();
            ddList2.DataSource = yearList;
            ddList2.DataTextField = "Year";
            ddList2.DataValueField = "Year";
            ddList2.DataBind();

            ddList3.DataSource = yearList;
            ddList3.DataTextField = "Year";
            ddList3.DataValueField = "Year";
            ddList3.DataBind();
        }
    }
}