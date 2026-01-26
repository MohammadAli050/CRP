using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using System.Data;
using System.IO;
using System.Net;

public partial class Admin_Transcript : BasePage
{
    string roll="";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.CheckPage_Load();

        if (!IsPostBack)
        {
           
        }
    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        //Transcript transcript = new Transcript();
        string roll = rollBox.Text;
        List<TranscriptResultDetails> tList = TranscriptManager.GetResultByStudentId(roll);
        List<TranscriptStudentInfo> tIList = TranscriptManager.GetInfoByStudentId(roll);
       
        //List<Transcript> tList = new List<Transcript>();
       // tList.Add(transcript);
        try
        {
            if (tList != null && tIList != null)
            {

                List<TranscriptTransferDetails> tTList = TranscriptManager.GetTransferResultStudentId(tIList[0].StudentID);
                List<TranscriptTransferDetails> tWList = TranscriptManager.GetWaiverResultStudentId(tIList[0].StudentID);

                Transcript.LocalReport.DataSources.Clear();
                ReportDataSource rds1 = new ReportDataSource("TranscriptInfoDataSet", tIList);

                Transcript.LocalReport.DataSources.Add(rds1);

                ReportDataSource rds2 = new ReportDataSource("TranscriptDataSet", tList);

                Transcript.LocalReport.DataSources.Add(rds2);

                ReportDataSource rds3 = new ReportDataSource("TranscriptTransferDataSet", tTList);
                Transcript.LocalReport.DataSources.Add(rds3);

                ReportDataSource rds4 = new ReportDataSource("TranscriptWaiverDataSet", tWList);
                Transcript.LocalReport.DataSources.Add(rds4);

                tList = null;
                tIList = null;
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = Transcript.LocalReport.Render(
        "PDF", null, out mimeType, out encoding, out filenameExtension,
        out streamids, out warnings);

                using (FileStream fs = new FileStream( Server.MapPath("../Upload/" + "Transcript" + roll + ".pdf"), FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                string file_name = Request.QueryString["file"];
                string path = Server.MapPath("../Upload/" + "Transcript" + roll + ".pdf");

                // Open PDF File in Web Browser 

                WebClient client = new WebClient();

                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);

                    // Setup the report viewer object and get the array of bytes

                }
            }
        }
        catch (Exception exp)
        {
        }
    }
   

}