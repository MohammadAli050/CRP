using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BussinessObject;
using System.Drawing;
using Common;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace EMS.Registration
{
    public partial class Registration : BasePage
    {
        #region Trimester
        private const string TRIMESTER1 = "1st";
        private const string TRIMESTER2 = "2nd";
        private const string TRIMESTER3 = "3rd";
        private const string TRIMESTER4 = "4th";
        private const string TRIMESTER5 = "5th";
        private const string TRIMESTER6 = "6th";
        private const string TRIMESTER7 = "7th";
        private const string TRIMESTER8 = "8th";
        private const string TRIMESTER9 = "9th";
        private const string TRIMESTER10 = "10th";
        private const string TRIMESTER11 = "11th";
        private const string TRIMESTER12 = "12th";
        #endregion

        #region Session Trimester Entities
        private const string SESSION_SCCPN = "Sccpn";
        private const string SESSION_SELECTEDTRIMESTERGRIDINDEX = "SelectedTrimesterGridIndex";
        #endregion

        #region Common Session
        private const string SESSION_TRIMESTER = "Trimester";
        private const string SESSION_SELECTEDROW = "SelectedRow";
        private const string SESSION_SECTION = "Section";
        private const string SESSION_COURSE = "Course";
        private const string SESSION_STUDENTID = "StudentId";

        private const string SESSION_FLAGSECTION = "FlagSection";
        private const string SESSION_FLAGCOURSE = "FlagCourse";


        #endregion

        bool FlagSection = false;
        bool FlagCourse = false;

        private List<Student_CalCourseProgNodeEntity> _sccpnEntities;
        private List<Student_CalCourseProgNodeEntity> _sccpnMultySpanEntities;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                if (CurrentUser != null)
                {
                    if (CurrentUser.RoleID > 0)
                    {
                        AuthenticateHome(CurrentUser);
                    }
                }
                else
                {
                    Response.Redirect("~/Security/Login.aspx");
                }
            }
            catch (Exception Ex)
            {
                lblMsg.Text = string.Empty;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = Ex.Message;
            }
        }

        protected void btnSearchStu_Click(object sender, EventArgs e)
        {
            CleareGrid();
            CleareMsg();            
            CleareSession();
            LoadGrd();            
        }

        private void CleareMsg()
        {
            lblMsg.Text = "";
        }

        private void CleareSession()
        {
            Session.Remove(SESSION_SCCPN);
            Session.Remove(SESSION_SELECTEDTRIMESTERGRIDINDEX);
            Session.Remove(SESSION_TRIMESTER);
            Session.Remove(SESSION_SELECTEDROW);
            Session.Remove(SESSION_SECTION);
            Session.Remove(SESSION_COURSE);
            Session.Remove(SESSION_STUDENTID);
            Session.Remove(SESSION_FLAGSECTION);
            Session.Remove(SESSION_FLAGCOURSE);            
        }

        private void CleareGrid()
        {
            GridView1st.DataSource = null;
            GridView1st.DataBind();
            GridView2nd.DataSource = null;
            GridView2nd.DataBind();
            GridView3rd.DataSource = null;
            GridView3rd.DataBind();
            GridView4th.DataSource = null;
            GridView4th.DataBind();
            GridView5th.DataSource = null;
            GridView5th.DataBind();
            GridView6th.DataSource = null;
            GridView6th.DataBind();
            GridView7th.DataSource = null;
            GridView7th.DataBind();
            GridView8th.DataSource = null;
            GridView8th.DataBind();
            GridView9th.DataSource = null;
            GridView9th.DataBind();
            GridView10th.DataSource = null;
            GridView10th.DataBind();
            GridView11th.DataSource = null;
            GridView11th.DataBind();
            GridView12th.DataSource = null;
            GridView12th.DataBind();

            GridViewSection.DataSource = null;
            GridViewSection.DataBind();
            GridViewCourse.DataSource = null;
            GridViewCourse.DataBind();
        }

        private void LoadGrd()
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
             

            if (txtSearchStu.Text.Trim() != string.Empty)
            {
                Student student = Student.GetStudentByRoll(txtSearchStu.Text.Trim());
                
                if (student != null && student.Id != 0 && AccessAuthentication(CurrentUser, student.Roll.Trim()))
                {
                    if (Session[SESSION_STUDENTID] != null)
                    {
                        Session.Remove(SESSION_STUDENTID);
                    }
                    Session[SESSION_STUDENTID] = student.Id;

                    _sccpnEntities = new List<Student_CalCourseProgNodeEntity>();
                    _sccpnEntities = Student_CalCourseProgNode_BAO.LoadByStuID(student.Id);

                    foreach (Student_CalCourseProgNodeEntity item in _sccpnEntities)
                    {
                        if (item.NodeLinkName != string.Empty && item.CourseTitle == string.Empty)
                        {
                            item.CourseTitle = item.NodeLinkName;
                        }
                    }

                    FillGrids("1st", GridView1st);
                    FillGrids("2nd", GridView2nd);
                    FillGrids("3rd", GridView3rd);
                    FillGrids("4th", GridView4th);
                    FillGrids("5th", GridView5th);
                    FillGrids("6th", GridView6th);
                    FillGrids("7th", GridView7th);
                    FillGrids("8th", GridView8th);
                    FillGrids("9th", GridView9th);
                    FillGrids("10th", GridView10th);
                    FillGrids("11th", GridView11th);
                    FillGrids("12th", GridView12th);

                    FillMultySpanGrid(GridViewMultySpan);
                }
                else
                {
                    Common.Utilities.ShowMassage(lblMsg, Color.Red, "Student is not found or you are not permitted to access this student");
                }
            }
        }

        private void FillMultySpanGrid(GridView gv)
        {
            bool Flag = false;
            List<Student_CalCourseProgNodeEntity> entities = new List<Student_CalCourseProgNodeEntity>();

            foreach (Student_CalCourseProgNodeEntity item in _sccpnEntities)
            {
                if (item.IsMultipleACUSpan == true && Flag == false)
                {
                    entities.Add(item);
                    Flag = true;
                }
            }
            gv.DataSource = null;
            gv.DataSource = entities;
            gv.DataBind();
        }

        private void FillGrids(string trimester, GridView gv)
        {
            List<Student_CalCourseProgNodeEntity> entities = new List<Student_CalCourseProgNodeEntity>();
            foreach (Student_CalCourseProgNodeEntity item in _sccpnEntities)
            {
                if (item.CalendarDetailName == trimester && item.IsMultipleACUSpan == false)
                {
                    entities.Add(item);
                }
            }

            gv.DataSource = null;
            gv.DataSource = entities;
            gv.DataBind();

            FilterByAutoAssignAndAutoOpen(gv);
        }

        private void FilterByAutoAssignAndAutoOpen(GridView gv)
        {
            foreach (GridViewRow item in gv.Rows)
            {
                //if (!Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(item.Cells[0].FindControl("chkIsAutoAssign"))).Checked) || !Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(item.Cells[0].FindControl("chkIsAutoOpen"))).Checked))
                //{
                //    item.Enabled = false;
                //}
                if (!Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(item.Cells[0].FindControl("chkIsAutoOpen"))).Checked))
                {
                    item.Enabled = false;
                }
            }
        }

        #region Section And Course Load
        protected void btnLoadSection_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            if (Session[SESSION_FLAGSECTION] != null)
            {
                FlagSection = (bool)Session[SESSION_FLAGSECTION];
            }
            if (!FlagSection)
            {
                int i = LoadSectionGrid();
                if (i == 1)
                {
                    FlagSection = true;
                    if (Session[SESSION_FLAGSECTION] != null)
                    {
                        Session.Remove(SESSION_FLAGSECTION);
                    }
                    Session[SESSION_FLAGSECTION] = FlagSection;
                    btnLoadSection.Text = "Refresh Section";
                }
                else
                {
                    Common.Utilities.ShowMassage(lblMsg, Color.Red, "Section not Found.");
                }
            }
            else if (FlagSection)
            {
                RefreshSectionGrid();               
            }
        }

        private void RefreshSectionGrid()
        {
            FlagSection = false;
            if (Session[SESSION_FLAGSECTION] != null)
            {
                Session.Remove(SESSION_FLAGSECTION);
            }
            Session[SESSION_FLAGSECTION] = FlagSection;
            GridViewSection.DataSource = null;
            GridViewSection.DataBind();
            btnLoadSection.Text = "Load Section";
        }

        private int LoadSectionGrid()
        {
            int i = 0;
            Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();
            sccpnEntity = (Student_CalCourseProgNodeEntity)Session[SESSION_SELECTEDROW];

            if (sccpnEntity != null)
            {
                List<SectionEntity> entities = new List<SectionEntity>();
                entities = Section_BAO.GetSections(AcademicCalender.GetCurrent().Id, sccpnEntity.DeptID
                    , sccpnEntity.ProgramID, sccpnEntity.CourseID, sccpnEntity.VersionID);

                GridViewSection.DataSource = null;
                GridViewSection.DataSource = entities;
                GridViewSection.DataBind();

                if (entities != null && entities.Count > 0)
                {
                    i = 1;
                }
            }
            return i;
        }

        protected void btnLoadCourse_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            if (Session[SESSION_FLAGCOURSE] != null)
            {
                FlagCourse = (bool)Session[SESSION_FLAGCOURSE];
            }

            if (!FlagCourse)
            {
                int i = LoadCourseGrid();
                if (i == 1)
                {
                    FlagCourse = true;
                    if (Session[SESSION_FLAGCOURSE] != null)
                    {
                        Session.Remove(SESSION_FLAGCOURSE);
                    }
                    Session[SESSION_FLAGCOURSE] = FlagCourse;
                    btnLoadCourse.Text = "Refresh Course";
                }
            }
            else if (FlagCourse)
            {
                RefreshCourseGrid();                
            }
        }

        private void RefreshCourseGrid()
        {
            FlagCourse = false;
            if (Session[SESSION_FLAGCOURSE] != null)
            {
                Session.Remove(SESSION_FLAGCOURSE);
            }
            Session[SESSION_FLAGCOURSE] = FlagCourse;
            GridViewCourse.DataSource = null;
            GridViewCourse.DataBind();
            btnLoadCourse.Text = "Load Course"; 
        }

        private int LoadCourseGrid()
        {
            int i = 0;

            Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();
            sccpnEntity = (Student_CalCourseProgNodeEntity)Session[SESSION_SELECTEDROW];

            List<AllCourseByNodeEntity> entities = new List<AllCourseByNodeEntity>();
            
            if (sccpnEntity != null)
            {
               
                entities = AllCourseByNode_BAO.GetAllDataByNodeId(sccpnEntity.NodeID);
            }

            if (entities != null && entities.Count != 0)
            {
                i = 1;
            }

            GridViewCourse.DataSource = null;
            GridViewCourse.DataSource = entities;
            GridViewCourse.DataBind();

            return i;
        }
        #endregion

        #region SelectedIndexChanged
        protected void GridViewSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            AcademicCalenderSection acsEntity = new AcademicCalenderSection();
            int i = 0;

            acsEntity.Id = Convert.ToInt32(((System.Web.UI.WebControls.Label)(GridViewSection.SelectedRow.Cells[i].FindControl("lblId"))).Text);
            acsEntity.SectionName = ((System.Web.UI.WebControls.Label)(GridViewSection.SelectedRow.Cells[i].FindControl("lblSectionName"))).Text;

            if (Session[SESSION_SECTION] != null)
            {
                Session.Remove(SESSION_SECTION);
            }
            Session[SESSION_SECTION] = acsEntity;

            AddSectionNameToGrid();

            RefreshSectionGrid();
        }

        public void AddSectionNameToGrid()
        {            
            AcademicCalenderSection acsEntity = new AcademicCalenderSection();
            acsEntity = (AcademicCalenderSection)Session[SESSION_SECTION];

            #region Check Section overflow
                int vacant = Student_CalCourseProgNode_BAO.ChkVacant(acsEntity.Id);

                if (vacant < 1)
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Section overflow.");
                    return;
                } 
            #endregion

            //TimeSpan tt = new TimeSpan(15, 22, 0);

            int selectedRowIndex = Convert.ToInt32(Session[SESSION_SELECTEDTRIMESTERGRIDINDEX]);

            List<Student_CalCourseProgNodeEntity> sccpnEntities = new List<Student_CalCourseProgNodeEntity>();
            sccpnEntities = (List<Student_CalCourseProgNodeEntity>)Session[SESSION_SCCPN];


            #region Check Section Overlaps

            int overlaps = TimeSlotPlan.CheckOverlaps(acsEntity.Id, sccpnEntities[selectedRowIndex].StudentID);

            if (overlaps > 0)
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "Section overlaps.");
                return;
            } 

            #endregion

            sccpnEntities[selectedRowIndex].SectionName = acsEntity.SectionName;
            sccpnEntities[selectedRowIndex].AcaCal_SectionID = acsEntity.Id;
            sccpnEntities[selectedRowIndex].AcademicCalenderID = AcademicCalender.GetCurrent().Id;
            sccpnEntities[selectedRowIndex].ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            sccpnEntities[selectedRowIndex].ModifiedDate = DateTime.Now;

            int done = Student_CalCourseProgNode_BAO.UpdateRow(sccpnEntities[selectedRowIndex]);
           
            if (done >= 1)
            {
                UpdateTrimesterGrid(sccpnEntities);           
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data cannot be saved.");
            }
        }

        private void UpdateTrimesterGrid(List<Student_CalCourseProgNodeEntity> sccpnEntities)
        {
            string trimester = Session[SESSION_TRIMESTER].ToString();

            if (trimester == TRIMESTER1)
            {
                GridView1st.DataSource = null;
                GridView1st.DataSource = sccpnEntities;
                GridView1st.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView1st);
            }
            else if (trimester == TRIMESTER2)
            {
                GridView2nd.DataSource = null;
                GridView2nd.DataSource = sccpnEntities;
                GridView2nd.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView2nd);
            }
            else if (trimester == TRIMESTER3)
            {
                GridView3rd.DataSource = null;
                GridView3rd.DataSource = sccpnEntities;
                GridView3rd.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView3rd);
            }
            else if (trimester == TRIMESTER4)
            {
                GridView4th.DataSource = null;
                GridView4th.DataSource = sccpnEntities;
                GridView4th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView4th);
            }
            else if (trimester == TRIMESTER5)
            {
                GridView5th.DataSource = null;
                GridView5th.DataSource = sccpnEntities;
                GridView5th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView5th);
            }
            else if (trimester == TRIMESTER6)
            {
                GridView6th.DataSource = null;
                GridView6th.DataSource = sccpnEntities;
                GridView6th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView6th);
            }
            else if (trimester == TRIMESTER7)
            {
                GridView7th.DataSource = null;
                GridView7th.DataSource = sccpnEntities;
                GridView7th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView7th);
            }
            else if (trimester == TRIMESTER8)
            {
                GridView8th.DataSource = null;
                GridView8th.DataSource = sccpnEntities;
                GridView8th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView8th);
            }
            else if (trimester == TRIMESTER9)
            {
                GridView9th.DataSource = null;
                GridView9th.DataSource = sccpnEntities;
                GridView9th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView9th);
            }
            else if (trimester == TRIMESTER10)
            {
                GridView10th.DataSource = null;
                GridView10th.DataSource = sccpnEntities;
                GridView10th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView10th);
            }
            else if (trimester == TRIMESTER11)
            {
                GridView11th.DataSource = null;
                GridView11th.DataSource = sccpnEntities;
                GridView11th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView11th);
            }
            else if (trimester == TRIMESTER12)
            {
                GridView12th.DataSource = null;
                GridView12th.DataSource = sccpnEntities;
                GridView12th.DataBind();

                FilterByAutoAssignAndAutoOpen(GridView12th);
            }
        }

        protected void GridViewCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllCourseByNodeEntity entity = new AllCourseByNodeEntity();
            int i = 0;

            entity.CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblCourseID"))).Text);
            entity.VersionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblVersionID"))).Text);
            entity.Node_CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblNode_CourseID"))).Text);
            entity.FormalCode = ((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblFormalCode"))).Text;
            entity.VersionCode = ((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblVersionCode"))).Text;
            entity.CourseTitle = ((System.Web.UI.WebControls.Label)(GridViewCourse.SelectedRow.Cells[i].FindControl("lblCourseTitle"))).Text;

            if (Session[SESSION_COURSE] != null)
            {
                Session.Remove(SESSION_COURSE);
            }
            Session[SESSION_COURSE] = entity;

            AddCourseNameToGrid();

            RefreshCourseGrid();
        }

        private void AddCourseNameToGrid()
        {
            AllCourseByNodeEntity entity = new AllCourseByNodeEntity();
            entity = (AllCourseByNodeEntity)Session[SESSION_COURSE];

            int selectedRowIndex = Convert.ToInt32(Session[SESSION_SELECTEDTRIMESTERGRIDINDEX]);

            List<Student_CalCourseProgNodeEntity> sccpnEntities = new List<Student_CalCourseProgNodeEntity>();
            sccpnEntities = (List<Student_CalCourseProgNodeEntity>)Session[SESSION_SCCPN];

            sccpnEntities[selectedRowIndex].CourseID = entity.CourseID;
            sccpnEntities[selectedRowIndex].VersionID = entity.VersionID;
            sccpnEntities[selectedRowIndex].Node_CourseID = entity.Node_CourseID;
            sccpnEntities[selectedRowIndex].FormalCode = entity.FormalCode;
            sccpnEntities[selectedRowIndex].VersionCode = entity.VersionCode;
            sccpnEntities[selectedRowIndex].CourseTitle = entity.CourseTitle;
            sccpnEntities[selectedRowIndex].ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
            sccpnEntities[selectedRowIndex].ModifiedDate = DateTime.Now;

            int done = Student_CalCourseProgNode_BAO.UpdateRow(sccpnEntities[selectedRowIndex]);

            if (done >= 1)
            {
                UpdateTrimesterGrid(sccpnEntities);
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data can not save.");
            }
        }

        protected void GridView1st_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView1st, TRIMESTER1, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView2nd_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView2nd, TRIMESTER2, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView3rd_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView3rd, TRIMESTER3, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView4th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView4th, TRIMESTER4, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView5th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView5th, TRIMESTER5, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView6th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView6th, TRIMESTER6, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView7th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView7th, TRIMESTER7, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView8th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView8th, TRIMESTER8, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView9th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView9th, TRIMESTER9, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView10th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView10th, TRIMESTER10, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView11th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView11th, TRIMESTER11, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        protected void GridView12th_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGridDataAndSelectedRowInSession(GridView12th, TRIMESTER12, SESSION_SCCPN);

            RefreshCourseGrid();
            RefreshSectionGrid();
        }

        private void SaveGridDataAndSelectedRowInSession(GridView gv, string trimester, string session_sccpn)
        {
            List<Student_CalCourseProgNodeEntity> sccpnEntities = new List<Student_CalCourseProgNodeEntity>();

            int i = 0;
            int j = 0;// j for row index checking

            foreach (GridViewRow row in gv.Rows)
            {
                Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();

                sccpnEntity.Id = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblSccpnId"))).Text);
                sccpnEntity.StudentID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblStuId"))).Text);
                sccpnEntity.CalCourseProgNodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCcpnID"))).Text);
                sccpnEntity.IsCompleted = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsComplete"))).Checked);
                sccpnEntity.OriginalCalID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblOriginalCalId"))).Text);
                sccpnEntity.IsAutoAssign = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoAssign"))).Checked);
                sccpnEntity.IsAutoOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoOpen"))).Checked);
                sccpnEntity.IsRequisitioned = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsRequisition"))).Checked);
                sccpnEntity.IsMandatory = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsMandatori"))).Checked);
                sccpnEntity.IsManualOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsManualOpen"))).Checked);
                sccpnEntity.TreeCalendarDetailID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderDetailId"))).Text);
                sccpnEntity.TreeCalendarMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderMasterId"))).Text);
                sccpnEntity.TreeMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeMasterId"))).Text);
                sccpnEntity.CalendarMasterName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalMasterName"))).Text;
                sccpnEntity.CalendarDetailName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalDetailName"))).Text;
                sccpnEntity.CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCourseId"))).Text);
                sccpnEntity.VersionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionId"))).Text);
                sccpnEntity.Node_CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeCourseId"))).Text);
                sccpnEntity.NodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeId"))).Text);
                sccpnEntity.NodeLinkName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodelinkName"))).Text;
                sccpnEntity.FormalCode = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseCode"))).Text;
                sccpnEntity.VersionCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionCode"))).Text;
                sccpnEntity.CourseTitle = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseTitle"))).Text;
                sccpnEntity.AcaCal_SectionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalSecId"))).Text);

                sccpnEntity.SectionName = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtSectionName"))).Text;
                //sccpnEntity.ChkSection = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkSection"))).Checked);

                sccpnEntity.ProgramID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblProgramId"))).Text);
                sccpnEntity.DeptID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblDeptId"))).Text);
                sccpnEntity.Priority = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblPriority"))).Text);
                sccpnEntity.RetakeNo = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtRetakeNo"))).Text);
                sccpnEntity.ObtainedGPA = Convert.ToDecimal(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblObtainGpa"))).Text);
                sccpnEntity.ObtainedGrade = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtGrade"))).Text;
                sccpnEntity.AcademicCalenderID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalId"))).Text);
                sccpnEntity.AcaCalYear = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalYear"))).Text);
                sccpnEntity.BatchCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblBatchCode"))).Text;
                sccpnEntity.AcaCalTypeName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalUnitTypName"))).Text;

                if (gv.SelectedIndex == j)
                {
                    if (Session[SESSION_SELECTEDROW] != null)
                    {
                        Session.Remove(SESSION_SELECTEDROW);
                    }
                    Session[SESSION_SELECTEDROW] = sccpnEntity;


                    if (Session[SESSION_TRIMESTER] != null)
                    {
                        Session.Remove(SESSION_TRIMESTER);
                    }
                    Session[SESSION_TRIMESTER] = trimester;


                    if (Session[SESSION_SELECTEDTRIMESTERGRIDINDEX] != null)
                    {
                        Session.Remove(SESSION_SELECTEDTRIMESTERGRIDINDEX);
                    }
                    Session[SESSION_SELECTEDTRIMESTERGRIDINDEX] = j;
                }

                j++;

                sccpnEntities.Add(sccpnEntity);
            }

            if (Session[session_sccpn] != null)
            {
                Session.Remove(session_sccpn);
            }
            Session[session_sccpn] = sccpnEntities;
        }

        #endregion

        #region RowDataBound
        protected void GridViewSection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridViewSection);
        }

        protected void GridViewCourse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridViewCourse);
        }

        protected void GridView1st_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView1st);
        }

        protected void GridView2nd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView2nd);
        }

        protected void GridView3rd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView3rd);
        }

        protected void GridView4th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView4th);
        }

        protected void GridView5th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView5th);
        }

        protected void GridView6th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView6th);
        }

        protected void GridView7th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView7th);
        }

        protected void GridView8th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView8th);
        }

        protected void GridView9th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView9th);
        }

        protected void GridView10th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView10th);
        }

        protected void GridView11th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView11th);
        }

        protected void GridView12th_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView_RowDataBound(sender, e, GridView12th);
        }

        private void GridView_RowDataBound(object sender, GridViewRowEventArgs e, GridView gv)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick",
                    ClientScript.GetPostBackEventReference(gv, "Select$" +
                    e.Row.RowIndex.ToString()));

                e.Row.Style.Add("cursor", "pointer");
            }
        }
        #endregion

        protected void btnSaveMultySpan_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            int done = 0;
            RefreshMultySpanObject();

            if (_sccpnMultySpanEntities != null || _sccpnMultySpanEntities.Count != 0)
            {
                done = Student_CalCourseProgNode_BAO.UpdateMultySpanData(_sccpnMultySpanEntities);
            }
            if (done >= 1)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data successfully save.");
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data can not save.");
            }
        }

        private void RefreshMultySpanObject()
        {
            _sccpnMultySpanEntities = new List<Student_CalCourseProgNodeEntity>();

            int i = 0;

            foreach (GridViewRow row in GridViewMultySpan.Rows)
            {
                Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();

                sccpnEntity.Id = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblSccpnId"))).Text);
                sccpnEntity.StudentID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblStuId"))).Text);
                sccpnEntity.CalCourseProgNodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCcpnID"))).Text);
                sccpnEntity.IsCompleted = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsComplete"))).Checked);
                sccpnEntity.OriginalCalID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblOriginalCalId"))).Text);
                sccpnEntity.IsAutoAssign = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoAssign"))).Checked);
                sccpnEntity.IsAutoOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoOpen"))).Checked);
                sccpnEntity.IsRequisitioned = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsRequisition"))).Checked);
                sccpnEntity.IsMandatory = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsMandatori"))).Checked);
                sccpnEntity.IsManualOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsManualOpen"))).Checked);
                sccpnEntity.TreeCalendarDetailID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderDetailId"))).Text);
                sccpnEntity.TreeCalendarMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderMasterId"))).Text);
                sccpnEntity.TreeMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeMasterId"))).Text);
                sccpnEntity.CalendarMasterName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalMasterName"))).Text;
                sccpnEntity.CalendarDetailName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalDetailName"))).Text;
                sccpnEntity.CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCourseId"))).Text);
                sccpnEntity.VersionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionId"))).Text);
                sccpnEntity.Node_CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeCourseId"))).Text);
                sccpnEntity.NodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeId"))).Text);
                sccpnEntity.NodeLinkName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodelinkName"))).Text;
                sccpnEntity.FormalCode = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseCode"))).Text;
                sccpnEntity.VersionCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionCode"))).Text;
                sccpnEntity.CourseTitle = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseTitle"))).Text;
                sccpnEntity.AcaCal_SectionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalSecId"))).Text);

                sccpnEntity.SectionName = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtSectionName"))).Text;

                sccpnEntity.ProgramID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblProgramId"))).Text);
                sccpnEntity.DeptID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblDeptId"))).Text);
                sccpnEntity.Priority = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblPriority"))).Text);
                sccpnEntity.RetakeNo = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtRetakeNo"))).Text);
                sccpnEntity.ObtainedGPA = Convert.ToDecimal(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblObtainGpa"))).Text);
                sccpnEntity.ObtainedGrade = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtGrade"))).Text;
                sccpnEntity.AcademicCalenderID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalId"))).Text);
                sccpnEntity.AcaCalYear = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalYear"))).Text);
                sccpnEntity.BatchCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblBatchCode"))).Text;
                sccpnEntity.AcaCalTypeName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalUnitTypName"))).Text;

                sccpnEntity.IsMultipleACUSpan = true;//set default

                sccpnEntity.CourseCredit = Convert.ToDecimal(((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseCredit"))).Text);
                sccpnEntity.CompletedCredit = Convert.ToDecimal(((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCompletedCredit"))).Text);

                sccpnEntity.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                sccpnEntity.ModifiedDate = DateTime.Now;

                _sccpnMultySpanEntities.Add(sccpnEntity);
            }
        }

        protected void btnSaveRequisition_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            int done = 0;
            _sccpnEntities = new List<Student_CalCourseProgNodeEntity>();

            RefreshTrimesterObject(GridView1st);
            RefreshTrimesterObject(GridView2nd);
            RefreshTrimesterObject(GridView3rd);
            RefreshTrimesterObject(GridView4th);
            RefreshTrimesterObject(GridView5th);
            RefreshTrimesterObject(GridView6th);
            RefreshTrimesterObject(GridView7th);
            RefreshTrimesterObject(GridView8th);
            RefreshTrimesterObject(GridView9th);
            RefreshTrimesterObject(GridView10th);
            RefreshTrimesterObject(GridView11th);
            RefreshTrimesterObject(GridView12th);

            if (_sccpnEntities != null || _sccpnEntities.Count != 0)
            {
                done = Student_CalCourseProgNode_BAO.UpdateRequisitionData(_sccpnEntities);
            }

            if (done >= 1)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data saved successfully.");
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Data can not be saved.");
            }
        }

        private void RefreshTrimesterObject(GridView gv)
        {
            int i = 0;

            foreach (GridViewRow row in gv.Rows)
            {
                Student_CalCourseProgNodeEntity sccpnEntity = new Student_CalCourseProgNodeEntity();

                sccpnEntity.Id = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblSccpnId"))).Text);
                sccpnEntity.StudentID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblStuId"))).Text);
                sccpnEntity.CalCourseProgNodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCcpnID"))).Text);
                sccpnEntity.IsCompleted = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsComplete"))).Checked);
                sccpnEntity.OriginalCalID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblOriginalCalId"))).Text);
                sccpnEntity.IsAutoAssign = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoAssign"))).Checked);
                sccpnEntity.IsAutoOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsAutoOpen"))).Checked);
                sccpnEntity.IsRequisitioned = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsRequisition"))).Checked);
                sccpnEntity.IsMandatory = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsMandatori"))).Checked);
                sccpnEntity.IsManualOpen = Convert.ToBoolean(((System.Web.UI.WebControls.CheckBox)(row.Cells[i].FindControl("chkIsManualOpen"))).Checked);
                sccpnEntity.TreeCalendarDetailID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderDetailId"))).Text);
                sccpnEntity.TreeCalendarMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeCalenderMasterId"))).Text);
                sccpnEntity.TreeMasterID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblTreeMasterId"))).Text);
                sccpnEntity.CalendarMasterName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalMasterName"))).Text;
                sccpnEntity.CalendarDetailName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCalDetailName"))).Text;
                sccpnEntity.CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblCourseId"))).Text);
                sccpnEntity.VersionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionId"))).Text);
                sccpnEntity.Node_CourseID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeCourseId"))).Text);
                sccpnEntity.NodeID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodeId"))).Text);
                sccpnEntity.NodeLinkName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblNodelinkName"))).Text;
                sccpnEntity.FormalCode = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseCode"))).Text;
                sccpnEntity.VersionCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblVersionCode"))).Text;
                sccpnEntity.CourseTitle = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtCourseTitle"))).Text;
                sccpnEntity.AcaCal_SectionID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalSecId"))).Text);

                sccpnEntity.SectionName = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtSectionName"))).Text;

                sccpnEntity.ProgramID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblProgramId"))).Text);
                sccpnEntity.DeptID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblDeptId"))).Text);
                sccpnEntity.Priority = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblPriority"))).Text);
                sccpnEntity.RetakeNo = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtRetakeNo"))).Text);
                sccpnEntity.ObtainedGPA = Convert.ToDecimal(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblObtainGpa"))).Text);
                sccpnEntity.ObtainedGrade = ((System.Web.UI.WebControls.TextBox)(row.Cells[i].FindControl("txtGrade"))).Text;
                sccpnEntity.AcademicCalenderID = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalId"))).Text);
                sccpnEntity.AcaCalYear = Convert.ToInt32(((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalYear"))).Text);
                sccpnEntity.BatchCode = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblBatchCode"))).Text;
                sccpnEntity.AcaCalTypeName = ((System.Web.UI.WebControls.Label)(row.Cells[i].FindControl("lblAcaCalUnitTypName"))).Text;

                sccpnEntity.ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                sccpnEntity.ModifiedDate = DateTime.Now;

                _sccpnEntities.Add(sccpnEntity);
            }
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            int done = 0;

            done = Registration_BAO.InsertRequisitionData(Convert.ToInt32(Session[SESSION_STUDENTID]), ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id);

            if (done >= 1)
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Registration completed successfully.");
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Registration not complete.");
            }
        }
                
        protected void btnUndoSection_Click(object sender, EventArgs e)
        {
            CleareMsg();     

            int done = 0;
            List<Student_CalCourseProgNodeEntity> sccpnEntities = new List<Student_CalCourseProgNodeEntity>();
            sccpnEntities = (List<Student_CalCourseProgNodeEntity>)Session[SESSION_SCCPN];

            int selectedRowIndex = Convert.ToInt32(Session[SESSION_SELECTEDTRIMESTERGRIDINDEX]);

            if (sccpnEntities != null && sccpnEntities.Count != 0)
            {
                sccpnEntities[selectedRowIndex].SectionName = string.Empty;
                sccpnEntities[selectedRowIndex].AcaCal_SectionID = -1;
                sccpnEntities[selectedRowIndex].ModifierID = ((UIUMSUser)base.GetFromSession(Constants.SESSIONCURRENT_USER)).Id;
                sccpnEntities[selectedRowIndex].ModifiedDate = DateTime.Now;

                 done = Student_CalCourseProgNode_BAO.UndoRow(sccpnEntities[selectedRowIndex]);
            }
            if (done >= 1)
            {
                UpdateTrimesterGrid(sccpnEntities);
                btnLoadSection_Click(null,null);
            }
            else
            {
                Common.Utilities.ShowMassage(lblMsg, Color.Red, "Error.");
            }

        }        
    }
}
