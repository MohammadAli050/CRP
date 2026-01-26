using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using CommonUtility;
using System.Drawing;

public partial class StudentUnBlockByProgram : BasePage
{
    #region Events
    //protected void Page_Load(object sender, EventArgs e)
    //{
    protected void Page_Load(object sender, EventArgs e)
    {
         base.CheckPage_Load();

        pnlMessage.Visible = false;       

        if (!IsPostBack)
        {
            LoadGrid();
        }
    }

    private void LoadGrid()
    {
        List<StudentBlockCountByProgramDTO> blockStudenList = StudentManager.GetAllBlockStudentByProgram();
        gvBlockStudentList.DataSource = blockStudenList;
        gvBlockStudentList.DataBind();


        List<StudentBlockCountByProgramDTO> inActiveStudentList = StudentManager.GetAllInActiveStudentByProgram();
        gvInActiveStudentList.DataSource = inActiveStudentList;
        gvInActiveStudentList.DataBind();
    }   

    private void ShowMessage(string msg)
    {
        pnlMessage.Visible = true;

        lblMessage.Text = msg;
        lblMessage.ForeColor = Color.Red;
    }

    protected void lnkBtnUnBlock_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int programId = int.Parse(btn.CommandArgument.ToString());

        bool boo = StudentManager.DeleteAllBlockStudentByProgram(programId);

        LoadGrid();
    }
    #endregion
    protected void lnkBtnActive_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int programId = int.Parse(btn.CommandArgument.ToString());

        bool boo = StudentManager.UpdateInActiveToActiveByProgram(programId);

        LoadGrid();
    }
}