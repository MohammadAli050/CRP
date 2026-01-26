using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill.report
{
    public partial class RptPeriodicalOrDaily : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                LoadProgramDropDownList();
            }
        }

        private void LoadProgramDropDownList()
        {
            List<Program> programList = new List<Program>();
            programList = ProgramManager.GetAll();

            ddlProgram.Items.Clear();
            ddlProgram.AppendDataBoundItems = true;

            if (programList != null)
            {
                ddlProgram.Items.Add(new ListItem("-All-", "0"));
                ddlProgram.DataTextField = "ShortName";
                ddlProgram.DataValueField = "ProgramID";

                ddlProgram.DataSource = programList;
                ddlProgram.DataBind();
            }
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ddlProgram.SelectedValue));
            
            ucBatch.Visible = true;
        }
        

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}