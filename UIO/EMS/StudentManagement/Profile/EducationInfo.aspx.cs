using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.bup
{
    public partial class EducationInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadYear();
                panel2.Visible = false;
                panel3.Visible = false;
            }
        }
        private void LoadYear()
        {
            int year = DateTime.Now.Year;
            for (int i = 0; i < 50; i++)
            {
                ddlSecondaryYear.Items.Add(Convert.ToString(year));
                ddlHigherSecondaryYear.Items.Add(Convert.ToString(year));
                ddlGraduationYear.Items.Add(Convert.ToString(year));
                ddlGraduationYear2.Items.Add(Convert.ToString(year));
                ddlGraduationYear3.Items.Add(Convert.ToString(year--));
            }
        }
        protected void btnAddMore1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            btnAddMore1.Visible = false;
        }
        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            btnAddMore1.Visible = true;
            panel2.Visible = false;
        }
        protected void btnAddMore2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            btnAddMore2.Visible = false;
            btnCancel2.Visible = false;
        }
        protected void btnCancel3_Click(object sender, EventArgs e)
        {
            btnAddMore2.Visible = true;
            btnCancel2.Visible = true;
            panel3.Visible = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }
    }
}