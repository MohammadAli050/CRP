using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System.Drawing;
using LogicLayer.BusinessObjects.DTO;
using Microsoft.Reporting.WebForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;


public class PrintUtility
{
    public int m_currentPageIndex;
    public IList<Stream> m_streams;

    public void Export(LocalReport report)
    {
        try
        {
            string deviceInfo =
         "<DeviceInfo>" +
         "  <OutputFormat>EMF</OutputFormat>" +
         "  <PageWidth>8.5in</PageWidth>" +
         "  <PageHeight>11in</PageHeight>" +
         "  <MarginTop>0.25in</MarginTop>" +
         "  <MarginLeft>0.25in</MarginLeft>" +
         "  <MarginRight>0.25in</MarginRight>" +
         "  <MarginBottom>0.25in</MarginBottom>" +
         "</DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Print()
    {
        try
        {
            PrinterSettings defaultPrinter = new PrinterSettings();

            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = defaultPrinter.PrinterName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer \"{0}\".", defaultPrinter);
                //MessageBox.Show(msg, "Print Error");               
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void PrintPage(object sender, PrintPageEventArgs ev)
    {
        try
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
    {
        try
        {

            Stream stream = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/Upload/Report/" + name + "." + fileNameExtension), FileMode.CreateNew);

            m_streams.Add(stream);

            return stream;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    public string GetFilePath(LocalReport report)
    {
        string str = report.ReportPath; //System.Web.HttpContext.Current.Server.MapPath("~/Upload/Report/" + "name" + "." + "fileNameExtension");
        return str;
    }

    public void Dispose()
    {
        try
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
