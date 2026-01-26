using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

namespace EMS.BasicSetup
{
    public partial class RelationDiscountPage : System.Web.UI.Page
    {
        Student _applicant1st = new Student();
        List<Student> _otherApplicants = new List<Student>();
        int _applicantGroup = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRelationDiscountType();
            }

            lblCount.Text = "0";
        }

        private void LoadRelationDiscountType()
        {
            ddlRelationDiscountType.AppendDataBoundItems = true;
            ddlRelationDiscountType.Items.Add(new ListItem("-Select-","0"));
            ddlRelationDiscountType.DataValueField = "TypeDefinitionID";
            ddlRelationDiscountType.DataTextField = "Definition";

            ddlRelationDiscountType.DataSource = TypeDefinitionManager.GetAll("Discount_Relation").OrderBy(o => o.Priority).ToList();
            ddlRelationDiscountType.DataBind();

            ddlRelationDiscountTypePopUp.AppendDataBoundItems = true;
            ddlRelationDiscountTypePopUp.Items.Add(new ListItem("-Select-", "0"));
            ddlRelationDiscountTypePopUp.DataValueField = "TypeDefinitionID";
            ddlRelationDiscountTypePopUp.DataTextField = "Definition";

            ddlRelationDiscountTypePopUp.DataSource = TypeDefinitionManager.GetAll("Discount_Relation").OrderBy(o => o.Priority).ToList();
            ddlRelationDiscountTypePopUp.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            CleareGrid();

            lblCount.Text = "0";
            lblMsg.Text = "";
            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int acaCalId = Convert.ToInt32(ucBatch.selectedValue);
            int relationDiscountType = Convert.ToInt32(ddlRelationDiscountTypePopUp.SelectedValue);
            string roll = txtStudentId.Text.Trim();

            List<StudentDTO> studentDtoList = StudentManager.GetAllDTOForSiblingByProgramOrBatchOrRoll(programId, acaCalId, relationDiscountType, roll);
            
            SessionManager.SaveListToSession<StudentDTO>(studentDtoList, "_studentDtoList");

            gvStudentList.DataSource = studentDtoList;
            gvStudentList.DataBind();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            CleareGrid();
            btnSearchName1st.Enabled = true;
            btnSearchName1st.Visible = true;

            CleareAllText();
            SessionCleare();

            btnAddSibling.CommandArgument = "A";

            ModalPopupExtender1.Show();
        }

        private void CleareAllText()
        {
            txtStudent1st.Text = "";
            txtStudent2nd.Text = "";
            txtName1st.Text = "";
            txtName2nd.Text = "";
            lblMsgPopUp.Text = "";

            ddlRelationDiscountTypePopUp.SelectedValue = "0";
        }

        private void SessionCleare()
        {
            SessionManager.DeletFromSession("_applicant1st");
            SessionManager.DeletFromSession("_otherApplicants");
            SessionManager.DeletFromSession("_applicantGroup");
        }
        
        protected void btnSearchName1st_Click(object sender, ImageClickEventArgs e)
        {
            lblMsgPopUp.Text = "";

            if (string.IsNullOrEmpty(txtStudent1st.Text.Trim()))
            {
                lblMsgPopUp.Text = "Please insert 1st Applicant's Student ID ";
                ModalPopupExtender1.Show();
                return;
            }

            _applicant1st = StudentManager.GetByRoll(txtStudent1st.Text.Trim());
            if (_applicant1st == null)
            {
                lblMsgPopUp.Text = "First applicant not found.";
                ModalPopupExtender1.Show();
               // btnLoad_Click(null, null);
                return;
            }

            txtName1st.Text = _applicant1st.BasicInfo.FullName;
            SessionManager.SaveObjToSession<Student>(_applicant1st, "_applicant1st");

            SiblingSetup sibling = SiblingSetupManager.GetByApplicantId(_applicant1st.StudentID);
            if (sibling != null)
            {
                _applicantGroup = sibling.GroupID;
                SessionManager.SaveObjToSession<int>(_applicantGroup, "_applicantGroup");
            }

            ModalPopupExtender1.Show();
        }

        protected void btnSearchName2nd_Click(object sender, ImageClickEventArgs e)
        {
            lblMsgPopUp.Text = "";

            if (string.IsNullOrEmpty(txtStudent2nd.Text.Trim()))
            {
                lblMsgPopUp.Text = "Please insert 2nd Applicant's Student ID ";
                ModalPopupExtender1.Show();
                return;
            }

            Student applicant2nd = StudentManager.GetByRoll(txtStudent2nd.Text.Trim());

            if (applicant2nd == null)
            {
                lblMsgPopUp.Text = "Second applicant not found.";
                ModalPopupExtender1.Show();
               // btnLoad_Click(null, null);
                return;
            }

            txtName2nd.Text = applicant2nd.BasicInfo.FullName;
            SessionManager.SaveObjToSession<Student>(applicant2nd, "_applicant2nd");


            ModalPopupExtender1.Show();
        }

        protected void btnAddSibling_Click(object sender, EventArgs e)
        {
            List<Student> _otherApplicants = new List<Student>();

            Student student = null;

            int relationDiscountType = Convert.ToInt32(ddlRelationDiscountTypePopUp.SelectedValue);

            if (relationDiscountType == 0)
            {
                lblMsgPopUp.Text = "Please Select any relation type.";
                ModalPopupExtender1.Show();
                return;
            }

            if (btnAddSibling.CommandArgument == "U")
            {
                student = StudentManager.GetByRoll(txtStudent2nd.Text.Trim());
                if (student == null)
                {
                    lblMsgPopUp.Text = "2nd Applicant not found.";
                    ModalPopupExtender1.Show();
                    return;
                }

                SiblingSetup siblingSetup = new SiblingSetup();
                siblingSetup.GroupID = SessionManager.GetObjFromSession<int>("_applicantGroup");
                siblingSetup.ApplicantId = student.StudentID;
                siblingSetup.TypeDefinitionID = Convert.ToInt32(ddlRelationDiscountTypePopUp.SelectedValue);
                siblingSetup.CreatedDate = DateTime.Now;
                siblingSetup.CreatedBy = -1;
                siblingSetup.ModifiedDate = DateTime.Now;
                siblingSetup.ModifiedBy = -1;

                int id = SiblingSetupManager.Insert(siblingSetup);
            }
            else if (btnAddSibling.CommandArgument == "A")
            {

                student = StudentManager.GetByRoll(txtStudent1st.Text.Trim());
                if (student == null)
                {
                    lblMsgPopUp.Text = "1st Applicant not found.";
                    ModalPopupExtender1.Show();
                    return;
                }

                SiblingSetup siblingSetup = new SiblingSetup();
                siblingSetup.GroupID = 0;
                siblingSetup.ApplicantId = student.StudentID;
                siblingSetup.TypeDefinitionID = Convert.ToInt32(ddlRelationDiscountTypePopUp.SelectedValue);
                siblingSetup.CreatedDate = DateTime.Now;
                siblingSetup.CreatedBy = -1;
                siblingSetup.ModifiedDate = DateTime.Now;
                siblingSetup.ModifiedBy = -1;

                int id1 = SiblingSetupManager.Insert(siblingSetup);
                SiblingSetup obj = SiblingSetupManager.GetById(id1);
                SessionManager.SaveObjToSession<int>(obj.GroupID, "_applicantGroup");

                student = StudentManager.GetByRoll(txtStudent2nd.Text.Trim());
                if (student == null)
                {
                    lblMsgPopUp.Text = "2nd Applicant not found.";
                    ModalPopupExtender1.Show();
                    return;
                }

                siblingSetup = new SiblingSetup();
                siblingSetup.GroupID = obj.GroupID;
                siblingSetup.ApplicantId = student.StudentID;
                siblingSetup.TypeDefinitionID = Convert.ToInt32(ddlRelationDiscountTypePopUp.SelectedValue);
                siblingSetup.CreatedDate = DateTime.Now;
                siblingSetup.CreatedBy = -1;
                siblingSetup.ModifiedDate = DateTime.Now;
                siblingSetup.ModifiedBy = -1;

                int id2 = SiblingSetupManager.Insert(siblingSetup);
            }

            List<StudentDTO> studentList = StudentManager.GetDTOAllBySiblingGroupId(SessionManager.GetObjFromSession<int>("_applicantGroup"));
            studentList = studentList.Where(s => s.Roll != txtStudent1st.Text.Trim()).ToList();
            gvSiblingList.DataSource = studentList;
            gvSiblingList.DataBind();

            ModalPopupExtender1.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();

            btnLoad_Click(null, null);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            CleareAllText();
            CleareGrid();

            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);

            btnAddSibling.CommandArgument = "U";

            _applicant1st = StudentManager.GetById(id);
            if (_applicant1st != null)
            {
                txtStudent1st.Text = _applicant1st.Roll;
                txtName1st.Text = _applicant1st.BasicInfo.FullName;
                SessionManager.SaveObjToSession<Student>(_applicant1st, "_applicant1st");
            }

            SiblingSetup sibling = SiblingSetupManager.GetByApplicantId(_applicant1st.StudentID);
            if (sibling != null)
            {
                _applicantGroup = sibling.GroupID;
                SessionManager.SaveObjToSession<int>(_applicantGroup, "_applicantGroup");

                List<StudentDTO> studentList = StudentManager.GetDTOAllBySiblingGroupId(sibling.GroupID);

                studentList = studentList.Where(s => s.Roll != _applicant1st.Roll).ToList();
                gvSiblingList.DataSource = studentList;
                gvSiblingList.DataBind();
            }

            ModalPopupExtender1.Show();
        }

        protected void lnkSiblingDelete_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = new LinkButton();
            linkButton = (LinkButton)sender;
            int id = Convert.ToInt32(linkButton.CommandArgument);
            int groupId = SessionManager.GetObjFromSession<int>("_applicantGroup");

            bool boo = SiblingSetupManager.DeleteByApplicantIdGroupId(id, groupId);

            List<StudentDTO> studentList = StudentManager.GetDTOAllBySiblingGroupId(SessionManager.GetObjFromSession<int>("_applicantGroup"));

            gvSiblingList.DataSource = studentList;
            gvSiblingList.DataBind();

            ModalPopupExtender1.Show();
        }

        protected void gvStudentList_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<StudentDTO> studentDtoList = SessionManager.GetListFromSession<StudentDTO>("_studentDtoList");

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
            Sort(studentDtoList, e.SortExpression.ToString(), sortdirection);
        }

        public void Sort(List<StudentDTO> list, String sortBy, String sortDirection)
        {
            if (sortDirection == "ASC")
            {
                list.Sort(new GenericComparer<StudentDTO>(sortBy, (int)SortDirection.Ascending));
            }
            else
            {
                list.Sort(new GenericComparer<StudentDTO>(sortBy, (int)SortDirection.Descending));
            }
            gvStudentList.DataSource = list;
            gvStudentList.DataBind();
            lblCount.Text = list.Count.ToString();
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
            
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            CleareGrid();
        }

        private void CleareGrid()
        {
            gvStudentList.DataSource = null;
            gvStudentList.DataBind();

            gvSiblingList.DataSource = null;
            gvSiblingList.DataBind();
        }
    }
}