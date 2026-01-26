using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FAQPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitQuestion_click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQuestion.Text))
        {
            FAQ fac = new FAQ();
            fac.Tag = txtTag.Text.Trim();
            fac.Key = txtKey.Text.Trim();
            fac.Question = txtQuestion.Text.Trim();
            fac.CreatedBy = 1;
            fac.CreatedOn = DateTime.Now;
            FAQManager.Insert(fac);
        }
    }
}