using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.SyllabusMan
{
    public partial class CourseGroupPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.CheckPage_Load();

                if (!IsPostBack)
                {
                    LoadDropdownList();
                    LoadGrid();
                }
            }
            catch
            { }
        }

        private void LoadDropdownList()
        {
            List<TypeDefinition> tdList = TypeDefinitionManager.GetAll();

            ddlTypeDefinition.Items.Clear();
            ddlTypeDefinition.Items.Add(new ListItem("-Select-", "0"));
            ddlTypeDefinition.DataTextField = "Definition";
            ddlTypeDefinition.DataValueField = "TypeDefinitionID";
            ddlTypeDefinition.AppendDataBoundItems = true;

            if (tdList != null)
            {
                ddlTypeDefinition.DataSource = tdList;
                ddlTypeDefinition.DataBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = int.Parse(btn.CommandArgument.ToString());
            CourseGroupManager.Delete(id);
            LoadGrid();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Insert();

                btnInsert.CommandArgument = "";
                btnInsert.Text = "Insert";
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ClearField();
                LoadGrid();
            }
        }

        private void Insert()
        {
            CourseGroup courseGroup = null;
            if (btnInsert.Text != "Update")
            {
                courseGroup = new CourseGroup();

                courseGroup.GroupName = txtGroupName.Text.Trim();
                courseGroup.TypeDefinitionId = Convert.ToInt32(ddlTypeDefinition.SelectedValue);
                courseGroup.Remarks = txtremarks.Text.Trim();
                courseGroup.CreatedBy = -1;
                courseGroup.CreatedDate = DateTime.Now;
                courseGroup.ModifiedBy = -1;
                courseGroup.ModifiedDate = DateTime.Now;

                int id = CourseGroupManager.Insert(courseGroup);
            }
            else
            {
                courseGroup = CourseGroupManager.GetById(Convert.ToInt32(btnInsert.CommandArgument));

                courseGroup.CourseGroupId = Convert.ToInt32(btnInsert.CommandArgument);
                courseGroup.GroupName = txtGroupName.Text.Trim();
                courseGroup.TypeDefinitionId = Convert.ToInt32(ddlTypeDefinition.SelectedValue);
                courseGroup.Remarks = txtremarks.Text.Trim();
                courseGroup.CreatedBy = -1;
                courseGroup.CreatedDate = DateTime.Now;
                courseGroup.ModifiedBy = -1;
                courseGroup.ModifiedDate = DateTime.Now;

                CourseGroupManager.Update(courseGroup);
            }
        }

        private void LoadGrid()
        {
            List<CourseGroup> list = CourseGroupManager.GetAll();

            gvCourseGroup.DataSource = list;
            gvCourseGroup.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
               
                LinkButton btn = (LinkButton)sender;
                int id = int.Parse(btn.CommandArgument.ToString());

                CourseGroup courseGroup = CourseGroupManager.GetById(id);

                txtGroupName.Text = courseGroup.GroupName;
                txtremarks.Text = courseGroup.Remarks;
                ddlTypeDefinition.SelectedValue = courseGroup.TypeDefinitionId.ToString();

                btnInsert.CommandArgument = id.ToString();
                btnInsert.Text = "Update";

                this.ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid();
            ClearField();
        }

        private void ClearField()
        {
            txtGroupName.Text = string.Empty;
            txtremarks.Text = string.Empty;
            ddlTypeDefinition.SelectedValue = "0";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearField();
            this.ModalPopupExtender1.Show();
        }

        protected void btnAddAndNext_Click(object sender, EventArgs e)
        {
            try
            {
                Insert();

                btnInsert.CommandArgument = "";
                btnInsert.Text = "Insert";
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ClearField();
                LoadGrid();
                this.ModalPopupExtender1.Show();
            }
        }
    }
}