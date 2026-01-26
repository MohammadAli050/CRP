using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessLogic;
using CommonUtility;

namespace EMS.bup
{
    public partial class AddressInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.CheckPage_Load();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");
            
            List<AddressType> addressTypeList = AddressTypeManager.GetAll();
            List<Address> addressList = AddressManager.GetAddressByPersonId(student.PersonID);

            if (addressList.Count > 0 && addressList != null)
            {
                foreach (AddressType addressType in addressTypeList)
                {
                    List<Address> tempAddressList = addressList.Where(x => x.AddressTypeId == addressType.AddressTypeId).ToList();
                    if (tempAddressList.Count > 0 && tempAddressList != null)
                    {
                        if (tempAddressList.FirstOrDefault().AddressTypeId == 1)
                        {
                            txtPresentAddress.Text = tempAddressList.FirstOrDefault().AddressLine;
                            ddlPresentDistrict.SelectedValue = Convert.ToString(tempAddressList.FirstOrDefault().District);
                        }
                        else if (tempAddressList.FirstOrDefault().AddressTypeId == 2)
                        {
                            txtPermanentAddress.Text = tempAddressList.FirstOrDefault().AddressLine;
                            ddlPermanentDistrict.SelectedValue = Convert.ToString(tempAddressList.FirstOrDefault().District);
                        }
                        else if (tempAddressList.FirstOrDefault().AddressTypeId == 3)
                        {
                            txtGuardianAddress.Text = tempAddressList.FirstOrDefault().AddressLine;
                            ddlGuardianDistrict.SelectedValue = Convert.ToString(tempAddressList.FirstOrDefault().District);
                        }
                        else if (tempAddressList.FirstOrDefault().AddressTypeId == 4)
                        {
                            txtMailingAddress.Text = tempAddressList.FirstOrDefault().AddressLine;
                            ddlMailingDistrict.SelectedValue = Convert.ToString(tempAddressList.FirstOrDefault().District);
                        }
                    }
                }
            }
        }

        protected void chkSameAddressChanged(object sender, EventArgs e)
        {
            if (chkSameAddress.Checked == true)
            {
                txtPermanentAddress.Text = txtPresentAddress.Text;
                ddlPermanentDistrict.SelectedValue = ddlPresentDistrict.SelectedValue;
            }
            else
            {
                txtPermanentAddress.Text = "";
                ddlPermanentDistrict.SelectedValue = "0";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Student student = SessionManager.GetObjFromSession<Student>("StudentEdit");
            List<AddressType> addressTypeList = AddressTypeManager.GetAll();
            List<Address> addressList = AddressManager.GetAddressByPersonId(student.PersonID);

            Address tempAddress = addressList.Where(x => x.AddressTypeId == 1).FirstOrDefault();
            if (tempAddress != null)
            {
                tempAddress.AddressLine = txtPresentAddress.Text;
                tempAddress.District = Convert.ToInt32(ddlPresentDistrict.SelectedValue);
                tempAddress.ModifiedDate = DateTime.Now;
                AddressManager.Update(tempAddress);
            }
            else
            {
                if (txtPresentAddress.Text.Trim() != "")
                {
                    Address presentAddress = new Address();

                    presentAddress.PersonId = student.PersonID;
                    presentAddress.AddressTypeId = 1;
                    presentAddress.AddressLine = txtPresentAddress.Text;
                    presentAddress.District = Convert.ToInt32(ddlPresentDistrict.SelectedValue);
                    presentAddress.CreatedBy = 1;
                    presentAddress.CreatedDate = DateTime.Now;

                    int resultPresentAddress = AddressManager.Insert(presentAddress);
                }
            }
            tempAddress = addressList.Where(x => x.AddressTypeId == 2).FirstOrDefault();
            if (tempAddress != null)
            {
                tempAddress.AddressLine = txtPermanentAddress.Text;
                tempAddress.District = Convert.ToInt32(ddlPermanentDistrict.SelectedValue);
                tempAddress.ModifiedDate = DateTime.Now;
                AddressManager.Update(tempAddress);
            }
            else
            {
                if (txtPermanentAddress.Text.Trim() != "")
                {
                    Address permanentAddress = new Address();

                    permanentAddress.PersonId = student.PersonID;
                    permanentAddress.AddressTypeId = 2;
                    permanentAddress.AddressLine = txtPermanentAddress.Text;
                    permanentAddress.District = Convert.ToInt32(ddlPermanentDistrict.SelectedValue);
                    permanentAddress.CreatedBy = 1;
                    permanentAddress.CreatedDate = DateTime.Now;

                    int resultPresentAddress = AddressManager.Insert(permanentAddress);
                }
            }
            tempAddress = addressList.Where(x => x.AddressTypeId == 3).FirstOrDefault();
            if (tempAddress != null)
            {
                tempAddress.AddressLine = txtGuardianAddress.Text;
                tempAddress.District = Convert.ToInt32(ddlGuardianDistrict.SelectedValue);
                tempAddress.ModifiedDate = DateTime.Now;
                AddressManager.Update(tempAddress);
            }
            else
            {
                if (txtGuardianAddress.Text.Trim() != "")
                {
                    Address guardianAddress = new Address();

                    guardianAddress.PersonId = student.PersonID;
                    guardianAddress.AddressTypeId = 3;
                    guardianAddress.AddressLine = txtGuardianAddress.Text;
                    guardianAddress.District = Convert.ToInt32(ddlGuardianDistrict.SelectedValue);
                    guardianAddress.CreatedBy = 1;
                    guardianAddress.CreatedDate = DateTime.Now;

                    int resultPresentAddress = AddressManager.Insert(guardianAddress);
                }
            }
            tempAddress = addressList.Where(x => x.AddressTypeId == 4).FirstOrDefault();
            if (tempAddress != null)
            {
                tempAddress.AddressLine = txtMailingAddress.Text;
                tempAddress.District = Convert.ToInt32(ddlMailingDistrict.SelectedValue);
                tempAddress.ModifiedDate = DateTime.Now;
                AddressManager.Update(tempAddress);
            }
            else
            {
                if (txtMailingAddress.Text.Trim() != "")
                {
                    Address mailingAddress = new Address();

                    mailingAddress.PersonId = student.PersonID;
                    mailingAddress.AddressTypeId = 4;
                    mailingAddress.AddressLine = txtMailingAddress.Text;
                    mailingAddress.District = Convert.ToInt32(ddlMailingDistrict.SelectedValue);
                    mailingAddress.CreatedBy = 1;
                    mailingAddress.CreatedDate = DateTime.Now;

                    int resultPresentAddress = AddressManager.Insert(mailingAddress);
                }
            }
        }
    }
}