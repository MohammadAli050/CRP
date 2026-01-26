using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Common
{
    public class Utilities
    {
        #region Struct variables
        public struct StructCourse
        {
            CourseEntity cs;

            public CourseEntity Cs
            {
                get { return cs; }
                set { cs = value; }
            }
            OperatorEntity op;

            public OperatorEntity Op
            {
                get { return op; }
                set { op = value; }
            }
            int opMinOccurence;

            public int OpMinOccurence
            {
                get { return opMinOccurence; }
                set { opMinOccurence = value; }
            }
            decimal reqCredits;

            public decimal ReqCredits
            {
                get { return reqCredits; }
                set { reqCredits = value; }
            }

        }
        public struct StructNode
        {
            NodeEntity node;

            public NodeEntity Node
            {
                get { return node; }
                set { node = value; }
            }
            OperatorEntity op;

            public OperatorEntity Op
            {
                get { return op; }
                set { op = value; }
            }
            int opMinOccurence;

            public int OpMinOccurence
            {
                get { return opMinOccurence; }
                set { opMinOccurence = value; }
            }
            decimal reqCredits;

            public decimal ReqCredits
            {
                get { return reqCredits; }
                set { reqCredits = value; }
            }
        }
        #endregion

        public static void ShowMassage(Label lblMsg, Color foreColor, string message)
        {
            lblMsg.Text = string.Empty;
            lblMsg.ForeColor = foreColor;
            lblMsg.Text = message;
        }

        #region Populate DDL
        //protected void PopulateDDL(DropDownList ddl, object items, string displayMembers)
        //{
        //    PopulateDDL(ddl, items, displayMembers, null, null);
        //}
        //protected void PopulateDDL(DropDownList ddl, object items, string displayMembers, string defaultText)
        //{
        //    PopulateDDL(ddl, items, displayMembers, defaultText, null);
        //}
        //protected void PopulateDDL(DropDownList ddl, object items, string displayMembers, string defaultText, string intialValue)
        //{
        //    ddl.Items.Clear();

        //    //Inserting Default item to dropdown
        //    if (defaultText != null)
        //    {
        //        ListItem item;
        //        if (intialValue != null)
        //        {
        //            item = new ListItem(defaultText, intialValue);
        //        }
        //        else
        //        {
        //            item = new ListItem(defaultText, "0");
        //        }
        //        ddl.Items.Add(item);
        //    }
        //    //end inserting default item

        //    LoadItems(ddl, items, displayMembers);
        //}
        //private void LoadItems(DropDownList ddl, object items, string displayMembers)
        //{
        //    //Split the display members
        //    string[] members = displayMembers.Split(',');
        //    //Casting the User Data to CollectionBase
        //    //Only except those objects, whose are inherited the CollectionBase
        //    CollectionBase list = (CollectionBase)items;
        //    System.Collections.IEnumerator enumerator = list.GetEnumerator();
        //    while (enumerator.MoveNext())
        //    {
        //        int length = members.Length - 1;
        //        string[] memberItems = new string[length];
        //        //get object id				
        //        System.Reflection.PropertyInfo idInfo = (enumerator.Current.GetType()).GetProperty("ID");
        //        object objectID = idInfo.GetValue(enumerator.Current, null);
        //        ListItem item = new ListItem();
        //        item.Value = objectID.ToString();
        //        for (int i = 0; i < members.Length; i++)
        //        {
        //            System.Reflection.PropertyInfo dispMem = (enumerator.Current.GetType()).GetProperty(members[i]);
        //            object rtn = dispMem.GetValue(enumerator.Current, null);
        //            item.Text = rtn.ToString();
        //        }
        //        ddl.Items.Add(item);
        //    }
        //}
        #endregion

        #region Password related function
        public static string Encrypt(string sText)
        {
            int i = 0;
            string sEncrypt = "", sKey = "Cel.Admin"; //sKey="cel.abracadabra";
            char cTextChar, cKeyChar;
            char[] cTextData, cKey;

            //Save Length of Pass
            sText = (char)(sText.Length) + sText;

            //Pad Password with space upto 10 Characters
            if (sText.Length < 10)
            {
                sText = sText + sText.PadRight((10 - sText.Length), ' ');
            }
            cTextData = sText.ToCharArray();

            //Make the key big enough
            while (sKey.Length < sText.Length)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, sText.Length);
            cKey = sKey.ToCharArray();

            //Encrypting Data
            for (i = 0; i < sText.Length; i++)
            {
                cTextChar = (char)cTextData.GetValue(i);
                cKeyChar = (char)cKey.GetValue(i);
                sEncrypt = sEncrypt + IntToHex((int)(cTextChar) ^ (int)(cKeyChar));
            }

            return sEncrypt;
        }

        public static string Decrypt(string sText)
        {
            int j = 0, i = 0, nLen = 0;
            string sTextByte = "", sDecrypt = "", sKey = "Cel.Admin"; //sKey="cel.abracadabra";//
            char[] cTextData, cKey;
            char cTextChar, cKeyChar;

            //Taking Lenght, half of Encrypting data  
            nLen = sText.Length / 2;

            //Making key is big Enough
            while (sKey.Length < nLen)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, nLen);
            cKey = sKey.ToCharArray();
            cTextData = sText.ToCharArray();

            //Decripting data
            for (i = 0; i < nLen; i++)
            {
                sTextByte = "";
                for (j = i * 2; j < (i * 2 + 2); j++)
                {
                    sTextByte = sTextByte + cTextData.GetValue(j).ToString();
                }
                cTextChar = (char)HexToInt(sTextByte);
                cKeyChar = (char)cKey.GetValue(i);
                sDecrypt = sDecrypt + (char)((int)(cKeyChar) ^ (int)(cTextChar));
            }

            //Taking real password
            cTextData = sDecrypt.ToCharArray();
            sDecrypt = "";
            i = (int)(char)cTextData.GetValue(0);
            for (j = 1; j <= i; j++)
            {
                sDecrypt = sDecrypt + cTextData.GetValue(j).ToString();
            }

            return sDecrypt;
        }

        private static string IntToHex(int nIntData)
        {
            return Convert.ToString(nIntData, 16).PadLeft(2, '0');
        }

        private static int HexToInt(string sHexData)
        {
            return Convert.ToInt32(sHexData, 16);
        }
        #endregion

        #region ClearControls

        public static void ClearControls(System.Web.UI.Page ctrlPage)
        {
            foreach (Control ctrl in ctrlPage.Controls)
            {
                if (ctrl.Controls != null)
                    ClearControls(ctrl);
            }
        }
        public static void ClearControls(Control ctrl)
        {
            foreach (Control cntrl in ctrl.Controls)
            {
                if (cntrl.Controls != null)
                    ClearControls(cntrl);

                if (cntrl is TextBox)
                {
                    TextBox tb = (TextBox)cntrl;
                    tb.Text = string.Empty;
                }
                if (cntrl is DropDownList)
                {
                    DropDownList cbo = (DropDownList)cntrl;
                    cbo.SelectedIndex = -1;
                }
                if (cntrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)cntrl;
                    chk.Checked = false;
                }
                if (cntrl is RadioButton)
                {
                    RadioButton rad = (RadioButton)cntrl;
                    rad.Checked = false;
                }
                if (cntrl is GridView)
                {
                    GridView dgv = (GridView)cntrl;
                    dgv.DataSource = null;
                    dgv.DataBind();
                    //for (int i = 0; i < dgv.Rows.Count; i++)
                    //{
                    //    for (int j = 1; j < dgv.Columns.Count; j++)
                    //        dgv.Rows[i].Cells[j].Text = string.Empty;
                    //}                    
                }
            }
        }

        public static void DisableControls(Control ctrl)
        {
            foreach (Control cntrl in ctrl.Controls)
            {
                if (cntrl.Controls != null)
                {
                    DisableControls(cntrl);
                }

                if (cntrl is TextBox)
                {
                    TextBox tb = (TextBox)cntrl;
                    tb.Enabled = false;
                }
                if (cntrl is DropDownList)
                {
                    DropDownList cbo = (DropDownList)cntrl;
                    cbo.Enabled = false;
                }
                if (cntrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)cntrl;
                    chk.Enabled = false;
                }
                if (cntrl is RadioButton)
                {
                    RadioButton rad = (RadioButton)cntrl;
                    rad.Enabled = false;
                }
                if (cntrl is Button)
                {
                    Button btn = (Button)cntrl;
                    btn.Enabled = false;
                }
            }
        }
        public static void EnableControls(Control ctrl)
        {
            foreach (Control cntrl in ctrl.Controls)
            {
                if (cntrl.Controls != null)
                {
                    EnableControls(cntrl);
                }

                if (cntrl is TextBox)
                {
                    TextBox tb = (TextBox)cntrl;
                    tb.Enabled = true;
                }
                if (cntrl is DropDownList)
                {
                    DropDownList cbo = (DropDownList)cntrl;
                    cbo.Enabled = true;
                }
                if (cntrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)cntrl;
                    chk.Enabled = true;
                }
                if (cntrl is RadioButton)
                {
                    RadioButton rad = (RadioButton)cntrl;
                    rad.Enabled = true;
                }
                if (cntrl is Button)
                {
                    Button btn = (Button)cntrl;
                    btn.Enabled = true;
                }
            }
        }
        #endregion        

    
        public static void ShowMassage(Label lblErrorMessage, string errorMessage)
        {
            lblErrorMessage.Text = String.Empty;
            lblErrorMessage.Text = errorMessage;
        }
    }
}
