using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class GridViewTemplate : ITemplate
{
    //A variable to hold the type of ListItemType.
    ListItemType _templateType;

    //A variable to hold the column name.
    string _columnName;
    bool _boolValue;

    //Constructor where we define the template type and column name.
    public GridViewTemplate(ListItemType type, string colname, bool boolChkBox)
    {
        //Stores the template type.
        _templateType = type;

        //Stores the column name.
        _columnName = colname;
        _boolValue = boolChkBox;
    }

    void ITemplate.InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case ListItemType.Header:
                //Creates a new label control and add it to the container.
                Label lbl = new Label();            //Allocates the new label object.
                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                //lbl.Width = 50;
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                break;

            case ListItemType.Item:

                if (_boolValue)
                {
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.AutoPostBack = false;
                    chkColumn.DataBinding += new EventHandler(chk_DataBinding);
                    container.Controls.Add(chkColumn);
                }
                else
                {
                    //Creates a new text box control and add it to the container.
                    TextBox tb1 = new TextBox();                            //Allocates the new text box object.
//                    tb1.CssClass = "aa";
                    tb1.AutoPostBack = false;
                    tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                    tb1.Columns = 4;                                        //Creates a column with size 4.
                    container.Controls.Add(tb1);                          //Adds the newly created textbox to the container.

                }

                break;

            case ListItemType.EditItem:
                break;

            case ListItemType.Footer:
                break;
        }
    }

    /// <summary>
    /// This is the event, which will be raised when the binding happens.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tb1_DataBinding(object sender, EventArgs e)
    {
        TextBox txtdata = (TextBox)sender;
        txtdata.ID = _columnName;
        if (_columnName == "Roll")
        {
            txtdata.Width = 100;
        }
        else if (_columnName == "Name")
        {
            txtdata.Width = 150;
        }
        else if (_columnName == "Remarks")
        {
            txtdata.Width = 300;
        }
        else
        {
            txtdata.Width = 150;
        }
        
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        if (dataValue != DBNull.Value)
        {
            txtdata.Text = dataValue.ToString();
            if (_columnName == "StudentRoll" || _columnName == "StudentName" || _columnName == "DiscountName" || _columnName == "Roll" || _columnName == "Name" || _columnName == "TCrPrev" || _columnName == "GPAPrev" || _columnName == "CGPAPrev" || _columnName == "TCrCur")
            {
                txtdata.Enabled = false;
            }
        }
    }
    void chk_DataBinding(object sender, EventArgs e)
    {
        CheckBox chkColumn = (CheckBox)sender;
        chkColumn.ID = "Chk" + _columnName;
        GridViewRow container = (GridViewRow)chkColumn.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        chkColumn.Checked = (bool)dataValue;
    }
}
