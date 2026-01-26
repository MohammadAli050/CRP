using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.miu.bill
{
    public partial class StudentDiscountInitialPage : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        string _pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;

        [Serializable]
        public class StudentInitialDiscountLocal
        {
            public int StudentID { get; set; }
            public string Roll { get; set; }
            public string Name { get; set; }
            public int BatchId { get; set; }
            public string BatchCode { get; set; }
            public int ProgramId { get; set; }
            public string Program { get; set; }
            public int StudentDiscountId { get; set; }
            public int StudentDiscountInitialDetailsId { get; set; }
            public int TypeDefinitionId { get; set; }
            public string DiscountType { get; set; }
            public decimal TypePercentage { get; set; }
            public string Comments { get; set; }
        }

        List<StudentInitialDiscountLocal> _studentInitialDiscountLocalList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
            base.CheckPage_Load();
            try
            {
                if (!IsPostBack)
                {
                    FillDiscountTypeChkList();
                }
            }
            catch
            { }
        }

        private void FillDiscountTypeChkList()
        {
            chkTyprDefinition.DataTextField = "Definition";
            chkTyprDefinition.DataValueField = "TypeDefinitionID";
            chkTyprDefinition.DataSource = TypeDefinitionManager.GetAll("Discount").OrderBy(o => o.Priority).ToList();
            chkTyprDefinition.DataBind();

            chkTyprDefinition.SelectedIndex = 0;
        }

        private void CleareGrid()
        {
            gvStudentDiscountInitial.DataSource = null;
            gvStudentDiscountInitial.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            CleareGrid();


            lblCount.Text = "0";
            lblMessage.Text = "";
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string roll = txtStudent.Text.Trim();

            if (batchId == 0 && programId == 0 && string.IsNullOrEmpty(roll))
            {
                lblMessage.Text = "Please select atleast any batch or program.";
                lblMessage.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(roll))
            {
                Student student = StudentManager.GetByRoll(roll);
                programId = student.ProgramID;
                batchId = student.BatchId;
            }


            int filterFrom = 0;
            if (!string.IsNullOrEmpty(txtFilterFrom.Text.Trim()))
                filterFrom = Convert.ToInt32(txtFilterFrom.Text.Trim());

            int filterTo = 0;
            if (!string.IsNullOrEmpty(txtFilterTo.Text.Trim()))
                filterTo = Convert.ToInt32(txtFilterTo.Text.Trim());

            int selectedCount = chkTyprDefinition.Items.Cast<ListItem>().Count(li => li.Selected);
            if (selectedCount == 0)
            {
                lblMessage.Text = "Please Select atleast one Discount.";
                return;
            }

            List<StudentDTO> studentDtoList = StudentManager.GetAllDTOByProgramOrBatchOrRoll(programId, batchId, roll);

            List<TypeDefinition> typeDefinitionList = TypeDefinitionManager.GetAll("Discount").OrderBy(o => o.Priority).ToList(); ;

            foreach (ListItem item in chkTyprDefinition.Items)
            {
                if (!item.Selected)
                {
                    var itemToRemove = typeDefinitionList.SingleOrDefault(r => r.TypeDefinitionID == Convert.ToInt32(item.Value));
                    if (itemToRemove != null)
                        typeDefinitionList.Remove(itemToRemove);
                }
            }

            if (typeDefinitionList == null || typeDefinitionList.Count == 0)
            {
                lblMessage.Text = "Error: 1001; No discount found.";
                return;
            }

            List<StudentInitialDiscountLocal> studentInitialDiscountLocalList = new List<StudentInitialDiscountLocal>();
            StudentInitialDiscountLocal studentInitialDiscountLocal = null;

            foreach (StudentDTO student in studentDtoList)
            {
                foreach (TypeDefinition item in typeDefinitionList)
                {
                    studentInitialDiscountLocal = new StudentInitialDiscountLocal();
                    studentInitialDiscountLocal.StudentID = student.StudentID;
                    studentInitialDiscountLocal.Roll = student.Roll;
                    studentInitialDiscountLocal.Name = student.Name;
                    studentInitialDiscountLocal.Program = student.Program;
                    studentInitialDiscountLocal.DiscountType = item.Definition;
                    studentInitialDiscountLocal.TypeDefinitionId = item.TypeDefinitionID;
                    studentInitialDiscountLocalList.Add(studentInitialDiscountLocal);
                }
            }

            List<StudentDiscountInitialDetailsDTO> studentDiscountInitialList = StudentDiscountInitialDetailsManager.GetAllDiscountInitialByProgramBatchRoll(programId, batchId, roll);

            foreach (StudentDiscountInitialDetailsDTO item in studentDiscountInitialList)
            {
                StudentInitialDiscountLocal sdiDto = studentInitialDiscountLocalList.Find(d => d.StudentID == item.StudentID && d.TypeDefinitionId == item.TypeDefinitionId);
                if (sdiDto != null)
                {
                    sdiDto.TypePercentage = item.TypePercentage;
                    sdiDto.StudentDiscountId = item.StudentDiscountId;
                    sdiDto.StudentDiscountInitialDetailsId = item.StudentDiscountInitialDetailsId;
                    sdiDto.Comments = item.Comments == null ? "" : item.Comments.ToString();
                }
            }

            studentInitialDiscountLocalList = studentInitialDiscountLocalList.OrderBy(d => d.Roll).ToList();

            if (filterFrom > 0 && filterTo > 0)
            {
                studentInitialDiscountLocalList = studentInitialDiscountLocalList.Where(o => o.TypePercentage >= filterFrom && o.TypePercentage <= filterTo).ToList();
            }
            else if (filterFrom > 0 && filterTo == 0)
            {
                studentInitialDiscountLocalList = studentInitialDiscountLocalList.Where(o => o.TypePercentage == filterFrom).ToList();
            }
            else if (filterFrom == 0 && filterTo > 0)
            {
                studentInitialDiscountLocalList = studentInitialDiscountLocalList.Where(o => o.TypePercentage > 0 && o.TypePercentage <= filterTo).ToList();
            }

            SessionManager.SaveListToSession<StudentInitialDiscountLocal>(studentInitialDiscountLocalList, "_studentInitialDiscountLocalList");
            gvStudentDiscountInitial.DataSource = studentInitialDiscountLocalList;
            gvStudentDiscountInitial.DataBind();
            lblCount.Text = studentInitialDiscountLocalList.Count.ToString();
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem item in chkTyprDefinition.Items)
                {
                    item.Selected = chkAll.Checked;
                }

                lblCount.Text = SessionManager.GetObjFromSession<string>(" lblCount_Text ");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvStudentDiscountInitial_Sorting(object sender, GridViewSortEventArgs e)
        {
            _studentInitialDiscountLocalList = SessionManager.GetListFromSession<StudentInitialDiscountLocal>("_studentInitialDiscountLocalList");

            string sortdirection = string.Empty;
            if (Session["direction"] != null)
            {
                if (Session["direction"].ToString() == "ASC")
                {
                    sortdirection = "DESC";
                }
                else
                {
                    sortdirection = "ASC";
                }
            }
            else
            {
                sortdirection = "DESC";
            }
            Session["direction"] = sortdirection;
            Sort(_studentInitialDiscountLocalList, e.SortExpression.ToString(), sortdirection);
        }

        public void Sort(List<StudentInitialDiscountLocal> list, String sortBy, String sortDirection)
        {
            if (sortDirection == "ASC")
            {
                list.Sort(new GenericComparer<StudentInitialDiscountLocal>(sortBy, (int)SortDirection.Ascending));
            }
            else
            {
                list.Sort(new GenericComparer<StudentInitialDiscountLocal>(sortBy, (int)SortDirection.Descending));
            }
            gvStudentDiscountInitial.DataSource = list;
            gvStudentDiscountInitial.DataBind();
            lblCount.Text = list.Count.ToString();
        }

        protected void lBtnSaveAll_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                foreach (GridViewRow row in gvStudentDiscountInitial.Rows)
                {
                    TextBox txtComments = (TextBox)row.FindControl("txtComments");
                    string Comments = txtComments.Text;

                    TextBox txtDiscount = (TextBox)row.FindControl("txtPercentage");
                    decimal discount = Convert.ToDecimal(txtDiscount.Text);

                    HiddenField hdnStudentID = (HiddenField)row.FindControl("hdnStudentID");
                    string studentId = hdnStudentID.Value.ToString();

                    HiddenField hdnTypeDefinitionId = (HiddenField)row.FindControl("hdnTypeDefinitionId");
                    int typeDefinitionId = Convert.ToInt32(hdnTypeDefinitionId.Value.ToString());
                    Label DiscountType = (Label)row.FindControl("txtDiscountType"); 

                    int stdDiscountMasterId = 0;
                    int stdDiscountInitialId = 0;

                    StudentDiscountInitialDetails stdDiscountInitial = null;
                    Student student = StudentManager.GetById(Convert.ToInt32(studentId));

                    StudentDiscountMaster stdDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
                    if (stdDiscountMaster == null)
                    {
                        stdDiscountMaster = new StudentDiscountMaster();
                        stdDiscountMaster.BatchId = student.BatchId;
                        stdDiscountMaster.ProgramId = student.ProgramID;
                        stdDiscountMaster.StudentId = student.StudentID;
                        stdDiscountMaster.CreatedBy = -1;
                        stdDiscountMaster.CreatedDate = DateTime.Now;
                        stdDiscountMaster.ModifiedBy = -1;
                        stdDiscountMaster.ModifiedDate = DateTime.Now;

                        stdDiscountMasterId = StudentDiscountMasterManager.Insert(stdDiscountMaster);
                    }

                    int StudentDiscountId = stdDiscountMasterId == 0 ? stdDiscountMaster.StudentDiscountId : stdDiscountMasterId;

                    stdDiscountInitial = StudentDiscountInitialDetailsManager.GetBy(StudentDiscountId, typeDefinitionId);

                    if (stdDiscountInitial == null)
                    {
                        stdDiscountInitial = new StudentDiscountInitialDetails();
                        stdDiscountInitial.StudentDiscountId = StudentDiscountId;
                        stdDiscountInitial.TypeDefinitionId = typeDefinitionId;
                        stdDiscountInitial.TypePercentage = discount;
                        stdDiscountInitial.Comments = Comments;
                        stdDiscountInitial.AcaCalSession = AcademicCalenderManager.GetIsActiveRegistrationByProgramId(student.ProgramID).AcademicCalenderID;

                        stdDiscountInitialId = StudentDiscountInitialDetailsManager.Insert(stdDiscountInitial);
                    }
                    else
                    {
                        stdDiscountInitial.TypePercentage = discount;
                        stdDiscountInitial.Comments = Comments;
                        bool boo = StudentDiscountInitialDetailsManager.Update(stdDiscountInitial);
                    }
                    LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        "",
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        "Student Discount Initial Save",
                                                        userObj.LogInID + " saved " + discount + " Percentage discount on " + DiscountType.Text + " with comments " + Comments,
                                                        userObj.LogInID + " is Update Discount Initial",
                                                         ((int)CommonEnum.PageName.StudentDiscountInitialPage).ToString(),
                                                        CommonEnum.PageName.StudentDiscountInitialPage.ToString(),
                                                        _pageUrl,
                                                        student.Roll);
                }

                lblCount.Text = gvStudentDiscountInitial.Rows.Count.ToString();
            }
            catch (Exception)
            {
                lblMessage.Text = "Error 101:" + "Please contact with system admin.";
            }

            lblMessage.Text = "Data Update successfully.";
        }

        protected void lBtnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                GridViewRow gvrow = (GridViewRow)(((LinkButton)sender)).NamingContainer;

                TextBox txtDiscount = (TextBox)gvrow.FindControl("txtPercentage");
                decimal Discount = Convert.ToDecimal(txtDiscount.Text);

                TextBox txtComments = (TextBox)gvrow.FindControl("txtComments");
                string Comments = txtComments.Text;

                HiddenField hdnStudentID = (HiddenField)gvrow.FindControl("hdnStudentID");
                string StudentId = hdnStudentID.Value.ToString();

                HiddenField hdnTypeDefinitionId = (HiddenField)gvrow.FindControl("hdnTypeDefinitionId");
                int typeDefinitionId = Convert.ToInt32(hdnTypeDefinitionId.Value.ToString());
                Label DiscountType = (Label)gvrow.FindControl("txtDiscountType"); 

                int stdDiscountMasterId = 0;
                int stdDiscountInitialId = 0;

                StudentDiscountInitialDetails stdDiscountInitial = null;
                Student student = StudentManager.GetById(Convert.ToInt32(StudentId));

                StudentDiscountMaster stdDiscountMaster = StudentDiscountMasterManager.GetByStudentID(student.StudentID);
                if (stdDiscountMaster == null)
                {
                    stdDiscountMaster = new StudentDiscountMaster();
                    stdDiscountMaster.BatchId = student.BatchId;
                    stdDiscountMaster.ProgramId = student.ProgramID;
                    stdDiscountMaster.StudentId = student.StudentID;
                    stdDiscountMaster.CreatedBy = -1;
                    stdDiscountMaster.CreatedDate = DateTime.Now;
                    stdDiscountMaster.ModifiedBy = -1;
                    stdDiscountMaster.ModifiedDate = DateTime.Now;

                    stdDiscountMasterId = StudentDiscountMasterManager.Insert(stdDiscountMaster);
                }

                int StudentDiscountId = stdDiscountMasterId == 0 ? stdDiscountMaster.StudentDiscountId : stdDiscountMasterId;

                stdDiscountInitial = StudentDiscountInitialDetailsManager.GetBy(StudentDiscountId, typeDefinitionId);

                if (stdDiscountInitial == null)
                {
                    stdDiscountInitial = new StudentDiscountInitialDetails();
                    stdDiscountInitial.StudentDiscountId = StudentDiscountId;
                    stdDiscountInitial.TypeDefinitionId = typeDefinitionId;
                    stdDiscountInitial.TypePercentage = Discount;
                    stdDiscountInitial.AcaCalSession = AcademicCalenderManager.GetIsActiveRegistrationByProgramId(student.ProgramID).AcademicCalenderID;
                    stdDiscountInitial.Comments = Comments;
                    stdDiscountInitialId = StudentDiscountInitialDetailsManager.Insert(stdDiscountInitial);
                    
                }
                else
                {
                    stdDiscountInitial.TypePercentage = Discount;
                    stdDiscountInitial.Comments = Comments;
                    bool boo = StudentDiscountInitialDetailsManager.Update(stdDiscountInitial);
                }
                LogGeneralManager.Insert(
                                                        DateTime.Now,
                                                        "",
                                                        "",
                                                        userObj.LogInID,
                                                        "",
                                                        "",
                                                        "Student Discount Initial Save",
                                                        userObj.LogInID + " saved " + Discount + " Percentage discount on " + DiscountType.Text + " with comments " + Comments,
                                                        userObj.LogInID + " is Update Discount Initial",
                                                         ((int)CommonEnum.PageName.StudentDiscountInitialPage).ToString(),
                                                        CommonEnum.PageName.StudentDiscountInitialPage.ToString(),
                                                        _pageUrl,
                                                        student.Roll);
                lblMessage.Text = "Data Update successfully.";
                lblCount.Text = gvStudentDiscountInitial.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error 101:" + "Please contact with system admin.";
            }
        }
    }
}