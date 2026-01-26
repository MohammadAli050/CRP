using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxTabControl;
using System.Drawing;
using BussinessObject;
using Common;
using System.Data;
using System.Data.SqlClient;

public partial class Registration_CourseRegistration : BasePage
{
    #region Variables
    Student student = null;
    //TreeMaster treeMaster = null;
    //TreeCalendarMaster treeCalMas = null;
    #endregion

    #region Constants

    #region Session Names
    private const string SESSIONSTUDENT = "STUDENT";
    //private const string SESSIONTREEMASTER = "TREEMASTER";
    //private const string SESSIONTREECALMASTER = "TREECALMASTER";
    #endregion

    #endregion

    #region Methods
    private void RefreshControls(Student student, List<AssignedTreeNode> assignedTreeNodes)
    {
        //lblStdName.Text = student.FirstName + " " + student.MiddleName + " " + student.LastName;
        lblDept.Text = student.StudentProgram.ParentDepartment.Name;
        lblProg.Text = student.StudentProgram.ShortName;
        lblAdmissionSession.Text = Std_AcademicCalender.GetByStudentID(student.Id)[0].LinkAcademicCalender.Name;
        lblCurrentSession.Text = AcademicCalender.GetCurrent().Name;

        Student_ACUDetail sAcuDt = Student_ACUDetail.GetMaxByStudentID(student.Id);
        lblCGPA.Text = (sAcuDt == null) ? "" : sAcuDt.CGPA.ToString();
        
        lblCredits.Text = Math.Round(GetCompletedCredits(assignedTreeNodes), 2).ToString();
    }
    private void GridRowBackColor(GridViewRow gvRow, Color c)
    {
        Style s = new Style();
        s = new Style();
        s.BackColor = c;
        s.BorderStyle = BorderStyle.None;
        gvRow.ApplyStyle(s);
        gvRow.Cells[1].ColumnSpan = 4;
        gvRow.Cells[2].Visible = false;
        gvRow.Cells[3].Visible = false;
        gvRow.Cells[4].Visible = false;
    }

    private void GridRowBackColor(GridViewRow gvRow, Color backColor, Color frontColor, bool isBold)
    {
        Style s = new Style();
        s = new Style();
        s.BackColor = backColor;
        s.ForeColor = frontColor;
        s.Font.Bold = isBold;
        s.BorderStyle = BorderStyle.None;
        gvRow.ApplyStyle(s);
        gvRow.Cells[0].ColumnSpan = 5;
        gvRow.Cells[0].ForeColor = frontColor;
        gvRow.Cells[0].Font.Bold = isBold;
        gvRow.Cells[1].Visible = false;
        gvRow.Cells[2].Visible = false;
        gvRow.Cells[3].Visible = false;
        gvRow.Cells[4].Visible = false;
    }

    private void GridCourseRowColor(GridViewRow gvRow, string grade)
    {
        if (grade == "F")
        {
            gvRow.BackColor = System.Drawing.Color.Red;
        }
        else if (grade == "I")
        {
            gvRow.BackColor = System.Drawing.Color.Purple;
        }
        else if (grade != "N/A")
        {
            gvRow.BackColor = System.Drawing.Color.GreenYellow;
            //gvRow.BackColor = System.Drawing.Color.LawnGreen;
        }
    }
    private void GridOfferRowColor(GridViewRow gvRow, bool blboolean)
    {
        if (blboolean)
        {
            gvRow.BackColor = System.Drawing.Color.Thistle;
            gvRow.Cells[3].Visible = true;
        }

    }

    private decimal GetCompletedCredits(List<AssignedTreeNode> assignedTreeNodes)
    {
        decimal completedcredits = 0;
        List<AssignedTreeNode> innerNodes = null;

        var complete = from node in assignedTreeNodes
                       where node.HasCompleted == true
                       && node.LevelType == FlatAssignedTreeLevels.Course
                       select node;
        if (complete != null)
        {
            innerNodes = complete.ToList<AssignedTreeNode>();
        }

        if (innerNodes != null)
        {
            foreach (AssignedTreeNode item in innerNodes)
            {
                if (item.LevelType == FlatAssignedTreeLevels.Course)
                {
                    Course course = Course.GetCourse(item.DataObjID, item.DataObjID2);
                    if (course.IsCredit)
                    {
                        completedcredits = completedcredits + course.Credits;
                    }
                }

            }
        }

        return completedcredits;
    }

    #region Fuctionsold
    //private void DrillNode(Node node, DataTable dt, DataRow drow)
    //{
    //    if (node.Node_Courses != null && node.IsLastLevel && !node.IsVirtual)
    //    {
    //        foreach (Node_Course node_course in node.Node_Courses)
    //        {
    //            if (node_course.ChildCourse.IsActive)
    //            {
    //                drow = dt.NewRow();
    //                drow["Data"] = node_course.ChildCourse.FullCodeAndCourse;
    //                dt.Rows.Add(drow);  
    //            }
    //        }
    //    }
    //    else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
    //    {
    //        List<TreeDetail> treeDetails = TreeDetail.GetByParentNode(node.Id);

    //        foreach (TreeDetail treeDetail in treeDetails)
    //        {
    //            drow = dt.NewRow();
    //            drow["Data"] = treeDetail.ChildNode.Name;
    //            dt.Rows.Add(drow);
    //            DrillNode(treeDetail.ChildNode, dt, drow);
    //        }
    //    }
    //    else if (node.Node_Courses == null && !node.IsLastLevel && node.IsVirtual)
    //    {
    //        foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
    //        {
    //            foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
    //            {
    //                if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
    //                {
    //                    if (vNodeSet.OperandCourse.IsActive)
    //                    {
    //                        drow = dt.NewRow();
    //                        drow["Data"] = vNodeSet.OperandCourse.FullCodeAndCourse;
    //                        dt.Rows.Add(drow); 
    //                    }
    //                }
    //                else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
    //                {
    //                    DrillNode(vNodeSet.OperandNode, dt, drow);
    //                }
    //            }
    //        }
    //    }

    //}

    //private void FillGrid(TreeCalendarMaster treeCalMas)
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Data");
    //    foreach (TreeCalendarDetail treeCalDet in treeCalMas.TreeCalendarDetails)
    //    {
    //        DataRow drow = dt.NewRow();
    //        drow["Data"] = treeCalDet.CalendarDetail.Name;
    //        dt.Rows.Add(drow);
    //        foreach (Cal_Course_Prog_Node cal_Course_Prog_Node in treeCalDet.Cal_Course_Prog_Nodes)
    //        {
    //            if (cal_Course_Prog_Node.CourseID != 0 && cal_Course_Prog_Node.VersionID != 0)
    //            {
    //                if (cal_Course_Prog_Node.Course.IsActive)
    //                {
    //                    drow = dt.NewRow();
    //                    drow["Data"] = cal_Course_Prog_Node.Course.FullCodeAndCourse;
    //                    dt.Rows.Add(drow);  
    //                }
    //            }
    //            else if (cal_Course_Prog_Node.NodeID != 0)
    //            {
    //                drow = dt.NewRow();
    //                drow["Data"] = cal_Course_Prog_Node.NodeLinkName + " " + cal_Course_Prog_Node.Node.Name;
    //                dt.Rows.Add(drow);

    //                Node node = cal_Course_Prog_Node.Node;

    //                if (node.Node_Courses != null && node.IsLastLevel)
    //                {
    //                    foreach (Node_Course node_course in node.Node_Courses)
    //                    {
    //                        if (node_course.ChildCourse.IsActive)
    //                        {
    //                            drow = dt.NewRow();
    //                            drow["Data"] = node_course.ChildCourse.FullCodeAndCourse;
    //                            dt.Rows.Add(drow);  
    //                        }
    //                    }
    //                }
    //                else if (node.Node_Courses == null && !node.IsLastLevel && !node.IsVirtual)
    //                {
    //                        //drow = dt.NewRow();
    //                        //drow["Data"] = node._name;
    //                        //dt.Rows.Add(drow);

    //                        DrillNode(node, dt, drow);
    //                }
    //                else if (node.IsVirtual)
    //                {
    //                    foreach (VNodeSetMaster vNodeSetMas in node.VNodeSetMasters)
    //                    {
    //                        foreach (VNodeSet vNodeSet in vNodeSetMas.VNodeSets)
    //                        {
    //                            if (vNodeSet.OperandCourseID != 0 && vNodeSet.OperandVersionID != 0)
    //                            {
    //                                if (vNodeSet.OperandCourse.IsActive)
    //                                {
    //                                    drow = dt.NewRow();
    //                                    drow["Data"] = vNodeSet.OperandCourse.FullCodeAndCourse;
    //                                    dt.Rows.Add(drow); 
    //                                }
    //                            }
    //                            else if (vNodeSet.OperandCourseID == 0 && vNodeSet.OperandVersionID == 0 && vNodeSet.OperandNodeID != 0)
    //                            {
    //                                DrillNode(vNodeSet.OperandNode, dt, drow);
    //                            }
    //                        }
    //                    }
    //                }

    //            }
    //        }

    //    }
    //    gvRoutine.DataSource = dt;
    //    gvRoutine.DataBind();
    //}
    #endregion
    #endregion

    #region Events
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
    protected void gvRoutine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gvRow = e.Row;
            AssignedTreeNode atn = (AssignedTreeNode)gvRow.DataItem;
            if (atn == null)
            {
                return;
            }

            //setting row color
            if (atn.DataObjTypeName.Equals("TreeCalendarDetail"))
            {
                GridRowBackColor(gvRow, System.Drawing.Color.MidnightBlue, Color.Gainsboro, true);
            }
            else if (atn.DataObjTypeName.Equals("Cal_Course_Prog_Node"))
            {
                GridRowBackColor(gvRow, System.Drawing.Color.RoyalBlue, Color.White, true);
            }
            else if (atn.DataObjTypeName.Equals("Node"))
            {
                GridRowBackColor(gvRow, System.Drawing.Color.DarkViolet, Color.WhiteSmoke, true);
            }
            else if (atn.DataObjTypeName.Equals("Course"))
            {
                //gvRow.Cells[3].Visible = true;

                #region Course Offering wise Grid Color Setting and check box setting

                GridOfferRowColor(gvRow, atn.IsOffered);
                if (atn.IsOffered || atn.IsAssigned)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chk");
                    chk.Visible = true;
                    if (atn.IsAssigned)
                    {
                        chk.Enabled = false;
                        chk.Checked = true;
                    }
                }

                #region Retake history

                if (atn.Retakes > 0)
                {
                    e.Row.Cells[2].Attributes.Add("title", atn.RetakeHistory);                    
                }

                #endregion

                #endregion

                #region Grade wise Grid Color Setting

                GridCourseRowColor(gvRow, atn.HighestGrade);

                #endregion

                #region Grid Combo box populating
                if (atn.IsAssigned)
                {
                    gvRow.BackColor = Color.DarkKhaki;
                    //gvRow.BackColor = Color.Fuchsia;
                    AcademicCalender ac = AcademicCalender.GetNext();
                    if (ac == null)
                    {
                        return;
                    }

                    List<AcademicCalenderSection> acs = AcademicCalenderSection.Gets(ac.Id, atn.DataObjID, atn.DataObjID2);
                    if (acs == null)
                    {
                        gvRow.Cells[4].Visible = false;
                        DropDownList ddl = (DropDownList)gvRow.FindControl("ddlSection");
                        ddl.DataSource = null;
                        ddl.Visible = false;
                        return;
                    }
                    else
                    {
                        gvRow.Cells[4].Visible = true;
                        DropDownList ddl = (DropDownList)gvRow.FindControl("ddlSection");
                        ddl.DataSource = acs;
                        ddl.DataBind();
                        ddl.Visible = true;
                    }
                }
                else
                {
                    gvRow.Cells[4].Visible = false;
                    DropDownList ddl = (DropDownList)gvRow.FindControl("ddlSection");
                    ddl.DataSource = null;
                    ddl.Visible = false;
                    return;
                }

                #endregion
            }
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ch.NamingContainer;
        ch.Checked = ((CheckBox)sender).Checked;        

        if (ch.Checked)
        {
            row.BackColor = System.Drawing.Color.Tomato;
        }
        else
        {
            row.BackColor = System.Drawing.Color.Thistle;
        }

        if (row.Cells[14].Text.Equals("true"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "op", "window.open ('" + Request.ApplicationPath + "MultiSpanCourseRegistration.aspx', null,'top=1,left=1,center=yes,resizable=no,Width=700px,Height=500px,status=no,titlebar=no;toolbar=no,menubar=no,location=no,scrollbars=yes');", true);                
        }
        else
        { }
    }

    protected void txtStudent_TextChanged(object sender, EventArgs e)
    {
        try
        {
            UIUMSUser CurrentUser = (UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            
            lblMsg.Text = string.Empty;
            if (AccessAuthentication(CurrentUser, txtStudent.Text.Trim()))
            {
                student = Student.GetStudentByRoll(txtStudent.Text.Trim());
                if (student != null && student.Id != 0)
                {                    
                    List<AssignedTreeNode> assignedTreeNodes = AssignedTreeNode.GetAssignedTree(student);
                    gvRoutine.DataSource = assignedTreeNodes;
                    gvRoutine.DataBind();
                    RefreshControls(student, assignedTreeNodes);
                }
                else
                {
                    Utilities.ShowMassage(lblMsg, Color.Red, "Not Found.");
                }
            }
            else
            {
                Utilities.ShowMassage(lblMsg, Color.Red, "You are not permitted to access this person.");
            }
        }
        catch (Exception Ex)
        {
            Utilities.ShowMassage(lblMsg, Color.Red, Ex.Message);
        }
    }
    #endregion
}
