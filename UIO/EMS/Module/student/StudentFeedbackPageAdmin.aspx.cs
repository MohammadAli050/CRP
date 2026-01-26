using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentFeedbackPageAdmin : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();
        if (!IsPostBack)
        {
            try
            {
                LoadingAllFeedback();

            }
            catch { }
            finally { }
        }
    }

    private void LoadingAllFeedback()
    {
        try
        {

            List<StudentFeedback> list = StudentFeedbackManager.GetAll();
            gvPreviousFeedback.DataSource = list;
            gvPreviousFeedback.DataBind();


        }
        catch { }
        finally { }
    }
     
     
}