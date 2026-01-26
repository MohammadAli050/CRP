using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.miu.registration.report
{
    public partial class RptExamSeatPlan : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckPage_Load();

            pnlMessage.Visible = false;

            if (!IsPostBack)
            {
                LoadComboBox();
            }
        }

        protected void LoadComboBox()
        {
            LoadCalenderType();
        }

        protected void LoadCalenderType()
        {
            try
            {
                ddlCalenderType.Items.Clear();
                //ddlCalenderType.Items.Add(new ListItem("Select", "0"));
                //ddlCalenderType.AppendDataBoundItems = true;

                List<CalenderUnitMaster> calenderUnitMasterList = CalenderUnitMasterManager.GetAll();

                if (calenderUnitMasterList.Count > 0 && calenderUnitMasterList != null)
                {
                    ddlCalenderType.DataValueField = "CalenderUnitMasterID";
                    ddlCalenderType.DataTextField = "Name";
                    ddlCalenderType.DataSource = calenderUnitMasterList;
                    ddlCalenderType.DataBind();
                }
            }
            catch { }
            finally
            {
                int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
                LoadAcademicCalender(calenderTypeId);
            }
        }

        protected void CalenderType_Changed(Object sender, EventArgs e)
        {
            int calenderTypeId = Convert.ToInt32(ddlCalenderType.SelectedValue);
            LoadAcademicCalender(calenderTypeId);
        }

        protected void LoadAcademicCalender(int calenderTypeId)
        {
            try
            {
                ddlAcademicCalender.Items.Clear();
                ddlAcademicCalender.Items.Add(new ListItem("Select", "0"));
                ddlAcademicCalender.AppendDataBoundItems = true;

                List<AcademicCalender> academicCalenderList = AcademicCalenderManager.GetAll(calenderTypeId);

                if (academicCalenderList.Count > 0 && academicCalenderList != null)
                {
                    foreach (AcademicCalender academicCalender in academicCalenderList)
                        ddlAcademicCalender.Items.Add(new ListItem(UtilityManager.UppercaseFirst(academicCalender.CalendarUnitType_TypeName) + " " + academicCalender.Year, academicCalender.AcademicCalenderID.ToString()));

                    academicCalenderList = academicCalenderList.Where(x => x.IsActiveRegistration == true).ToList();
                    ddlAcademicCalender.SelectedValue = academicCalenderList[0].AcademicCalenderID.ToString();

                    AcademicCalender_Changed(null, null);
                }
            }
            catch { }
        }

        protected void AcademicCalender_Changed(Object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            LoadExamScheduleSet(acaCalId);
        }

        protected void LoadExamScheduleSet(int acaCalId)
        {
            try
            {
                ddlExamScheduleSet.Items.Clear();
                ddlExamScheduleSet.Items.Add(new ListItem("Select", "0"));
                ddlExamScheduleSet.AppendDataBoundItems = true;

                List<ExamScheduleSet> examScheduleSetList = ExamScheduleSetManager.GetAllByAcaCalId(acaCalId);

                ddlExamScheduleSet.DataSource = examScheduleSetList;
                ddlExamScheduleSet.DataValueField = "Id";
                ddlExamScheduleSet.DataTextField = "SetName";
                ddlExamScheduleSet.DataBind();
            }
            catch { }
        }

        protected void ExamScheduleSet_Changed(Object sender, EventArgs e)
        {
            int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            LoadExamScheduleDay(examScheduleSetId);
            LoadExamScheduleTimeSlot(examScheduleSetId);
        }

        protected void LoadExamScheduleDay(int examScheduleSetId)
        {
            try
            {
                ddlDay.Items.Clear();
                ddlDay.Items.Add(new ListItem("Select", "0"));
                ddlDay.AppendDataBoundItems = true;

                List<ExamScheduleDay> examScheduleDayList = ExamScheduleDayManager.GetAllByExamSet(examScheduleSetId);

                ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
                if (examScheduleSet != null)
                {
                    for (int i = 1; i <= examScheduleSet.TotalDay; i++)
                    {
                        List<ExamScheduleDay> tempExamScheduleDayList = examScheduleDayList.Where(x => x.DayNo == i).ToList();
                        if (tempExamScheduleDayList.Count > 0)
                            ddlDay.Items.Add(new ListItem(tempExamScheduleDayList[0].DayDate.ToString("dd-MMM-yyyy"), tempExamScheduleDayList[0].Id.ToString()));
                        else
                            ddlDay.Items.Add(new ListItem("" + i.ToString(), "0"));
                    }
                }
            }
            catch { }
        }

        protected void LoadExamScheduleTimeSlot(int examScheduleSetId)
        {
            try
            {
                ddlTimeSlot.Items.Clear();
                ddlTimeSlot.Items.Add(new ListItem("Select", "0"));

                List<ExamScheduleTimeSlot> examScheduleTimeSlotList = ExamScheduleTimeSlotManager.GetAllByExamSet(examScheduleSetId);

                ExamScheduleSet examScheduleSet = ExamScheduleSetManager.GetById(examScheduleSetId);
                if (examScheduleSet != null)
                {
                    for (int i = 1; i <= examScheduleSet.TotalTimeSlot; i++)
                    {
                        List<ExamScheduleTimeSlot> tempExamScheduleTimeSlotList = examScheduleTimeSlotList.Where(x => x.TimeSlotNo == i).ToList();
                        if (tempExamScheduleTimeSlotList.Count > 0)
                            foreach (ExamScheduleTimeSlot e in tempExamScheduleTimeSlotList)
                                ddlTimeSlot.Items.Add(new ListItem(e.StartTime + "-" + e.EndTime, e.Id.ToString()));
                        else
                            ddlTimeSlot.Items.Add(new ListItem("" + i, "0"));
                    }
                }
            }
            catch { }
        }

        protected void TimeSlot_Changed(Object sender, EventArgs e)
        {
            int examScheduleSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            LoadRoom(examScheduleSetId, dayId, timeSlotId);
        }

        protected void LoadRoom(int examScheduleSetId, int dayId, int timeSlotId)
        {
            try
            {
                ddlRoom.Items.Clear();
                ddlRoom.Items.Add(new ListItem("All", "0"));
                ddlRoom.AppendDataBoundItems = true;

                List<RoomList> roomList = RoomInformationManager.LoadRoom(examScheduleSetId, dayId, timeSlotId);

                if (roomList.Count > 0 && roomList != null)
                {
                    ddlRoom.DataValueField = "RoomNo";
                    ddlRoom.DataTextField = "RoomName";
                    ddlRoom.DataSource = roomList;
                    ddlRoom.DataBind();
                }
            }
            catch { }
        }

        protected void buttonView_Click(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            int roomId = Convert.ToInt32(ddlRoom.SelectedValue);

            LoadExamSeatPlan(acaCalId, examSetId, dayId, timeSlotId, roomId);
        }

        //View Report On Webpage
        private void LoadExamSeatPlan(int acaCalId, int examSetId, int dayId, int timeSlotId, int roomId)
        {
            string session = ddlAcademicCalender.SelectedItem.Text;
            string sessionType = ddlCalenderType.SelectedItem.Text;
            string examDate = ddlDay.SelectedItem.Text;
            string examTime = ddlTimeSlot.SelectedItem.Text;
            string examSet = ddlExamScheduleSet.SelectedItem.Text;

            ReportParameter p2 = new ReportParameter("Session", session);
            ReportParameter p3 = new ReportParameter("SessionType", sessionType);
            ReportParameter p4 = new ReportParameter("ExamDate", examDate); 
            ReportParameter p5 = new ReportParameter("ExamTime", examTime);
            ReportParameter p6 = new ReportParameter("ExamSet", examSet);

            List<rExamSeatPlanByRoom> list = ExamScheduleManager.GetExamSeatPlanByRoom(acaCalId, examSetId,  dayId, timeSlotId, roomId);

            try
            {
                if (list.Count != 0)
                {
                    ExamSeatPlan.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptExamSeatPlanByRoom.rdlc");
                    this.ExamSeatPlan.LocalReport.SetParameters(new ReportParameter[] { p2, p3, p4, p5, p6 });
                    ReportDataSource rds = new ReportDataSource("RptExamSeatPlan", list);

                    ExamSeatPlan.LocalReport.DataSources.Clear();
                    ExamSeatPlan.LocalReport.DataSources.Add(rds);
                    lblMessage.Text = "";
                }
                else
                {
                    ShowMessage("NO Data Found.");
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        //View Report In PDF For Print
        protected void buttonPrint_Click(object sender, EventArgs e)
        {
            int acaCalId = Convert.ToInt32(ddlAcademicCalender.SelectedValue);
            int examSetId = Convert.ToInt32(ddlExamScheduleSet.SelectedValue);
            int dayId = Convert.ToInt32(ddlDay.SelectedValue);
            int timeSlotId = Convert.ToInt32(ddlTimeSlot.SelectedValue);
            int roomId = Convert.ToInt32(ddlRoom.SelectedValue);

            string session = ddlAcademicCalender.SelectedItem.Text;
            string sessionType = ddlCalenderType.SelectedItem.Text;
            string examDate = ddlDay.SelectedItem.Text;
            string examTime = ddlTimeSlot.SelectedItem.Text;
            string examSet = ddlExamScheduleSet.SelectedItem.Text;

            ReportParameter p2 = new ReportParameter("Session", session);
            ReportParameter p3 = new ReportParameter("SessionType", sessionType);
            ReportParameter p4 = new ReportParameter("ExamDate", examDate);
            ReportParameter p5 = new ReportParameter("ExamTime", examTime);
            ReportParameter p6 = new ReportParameter("ExamSet", examSet);

            List<rExamSeatPlanByRoom> list = ExamScheduleManager.GetExamSeatPlanByRoom(acaCalId, examSetId, dayId, timeSlotId, roomId);

            try
            {
                if (list.Count != 0)
                {
                    ExamSeatPlan.LocalReport.ReportPath = Server.MapPath("~/miu/registration/report/RptExamSeatPlanByRoom.rdlc");
                    this.ExamSeatPlan.LocalReport.SetParameters(new ReportParameter[] { p2, p3, p4, p5, p6 });
                    ReportDataSource rds = new ReportDataSource("RptExamSeatPlan", list);

                    ExamSeatPlan.LocalReport.DataSources.Clear();
                    ExamSeatPlan.LocalReport.DataSources.Add(rds);

                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;

                    byte[] bytes = ExamSeatPlan.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    using (FileStream fs = new FileStream(Server.MapPath("~/Upload/ReportPDF/" + "ExamSeatPlan" + ".pdf"), FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    string path = Server.MapPath("~/Upload/ReportPDF/" + "ExamSeatPlan" + ".pdf");

                    WebClient client = new WebClient();   // Open PDF File in Web Browser 
                    Byte[] buffer = client.DownloadData(path);
                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", buffer.Length.ToString());
                        Response.BinaryWrite(buffer);
                    } 
                }
                else
                {
                    ShowMessage("NO Data Found.");
                    return;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        private void ShowMessage(string msg)
        {
            pnlMessage.Visible = true;

            lblMessage.Text = msg;
            lblMessage.ForeColor = Color.Red;
        }

    }
}