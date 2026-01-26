using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.bill.report
{
    public partial class MoneyReceipt : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            if (!this.IsPostBack)
            {
                string filePath = Server.MapPath("~/Upload/ReportPDF/") + Request.QueryString["FN"];

                if (File.Exists(filePath))
                {
                    this.Response.ContentType = "application/pdf";
                    this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + Request.QueryString["FN"]);
                    this.Response.WriteFile(filePath);
                    this.Response.Flush();

                    File.Delete(filePath);
                }
            }
        }
    }
}