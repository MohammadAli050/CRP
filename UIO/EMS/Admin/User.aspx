<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true"
    Inherits="Admin_User" CodeBehind="User.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    User
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

     <script type="text/javascript">
         function Client_OnTreeNodeChecked() {
             javascript: __doPostBack('tvwMenu', '');
         }
    </script>


    <style type="text/css">
        .dxeButtonEdit
        {

            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }

        .style2
        {
            border: 1px solid #DF5900;
            font: 11px Arial, Helvetica, sans-serif;
            color: #FF6600;
            vertical-align: Middle;
            width: 204px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

     <div class="PageTitle">
            <label>User Editor</label>
        </div>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 717px;">
                <tr>
                    <td colspan="2" align="center" class="td" style="width: 890px">

                        <asp:Label ID="lblErr" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="td" style="width: 600px; vertical-align: top; text-align: left; border: 1px solid black" valign="top">
                        <asp:Panel ID="pnlMaster" runat="server" Width="550px">
                            <table style="width: 550px; height: 30px; border: 1px solid black">
                                <tr>
                                     <td class="td">
                                      <dxe:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Height="16px" Width="84px">
                                        </dxe:ASPxDateEdit> 
                                         </td>                                      
                                </tr>
                                <tr  >
                                    <td class="td">
                                      
                                        <asp:Label ID="lblb1" runat="server" Text="Login ID"></asp:Label> &nbsp
                                        <asp:TextBox ID="txtSearchParam" runat="server" Width="156px"></asp:TextBox>  &nbsp  &nbsp
                                        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label> &nbsp
                                        <asp:TextBox ID="txtName" runat="server" Width="156px"></asp:TextBox>
                                    </td>
                                    <td class="td" style="width: 80px" align="center">
                                        <asp:Button ID="btnFind" runat="server" Text="Find"
                                            BackColor="#FF6600" BorderColor="#FF9933"
                                            OnClick="btnFind_Click" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 550px; height: 326px">
                                <tr>
                                    <td class="td" style="height: 16px" align="center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                            BackColor="#FF6600" BorderColor="#FF9933"
                                            OnClick="btnAdd_Click" CssClass="button" />
                                    </td>
                                    <td class="td" style="height: 16px" align="center">
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit"
                                            BackColor="#FF6600" BorderColor="#FF9933"
                                            OnClick="btnEdit_Click" CssClass="button" />
                                    </td>
                                    <td class="td" style="height: 16px" align="center">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                            BackColor="#FF6600" BorderColor="#FF9933"
                                            OnClick="btnDelete_Click" CssClass="button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" class="td" valign="top" style="vertical-align: top; text-align: left">
                                        <asp:GridView ID="gdvUsers" runat="server" BackColor="LightGoldenrodYellow"
                                            BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black"
                                            AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <asp:CommandField HeaderText="Choose" ShowSelectButton="True">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle Width="50px" />
                                                </asp:CommandField>
                                                <asp:BoundField HeaderText="LogIn ID" DataField="LogInID"  ItemStyle-Width="80"/>
                                                 <asp:BoundField HeaderText="Name" DataField="UserName" />
                                                <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                            </Columns>
                                            <FooterStyle BackColor="Tan" />
                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"
                                                HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="td" align="left" style="vertical-align: top; text-align: left; border: 1px solid black""
                        valign="top">
                        <asp:Panel ID="pnlDetails" runat="server" Width="426px">
                            <table style="width: 423px;">
                                <tr>
                                    <td class="td" align="left" style="width: 100px">
                                        <asp:Label ID="lblLogInID" runat="server" ForeColor="#FF6600" Text="LogInID"></asp:Label>
                                    </td>
                                    <td align="left" class="td">
                                        <asp:TextBox ID="txtLogIn" runat="server" MaxLength="50" Width="252px"></asp:TextBox>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr class="td">
                                    <td class="td" align="left" style="width: 100px">
                                        <asp:Label ID="lblPass" runat="server" ForeColor="#FF6600" Text="Password"></asp:Label>
                                    </td>
                                    <td align="left" class="td">
                                        <%--<asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="251px" 
                                            TextMode="Password"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPassword" runat="server"  MaxLength="50" Width="251px"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" align="left" style="width: 100px">
                                        <asp:Label ID="lblUsrSrcTbl" runat="server" ForeColor="#FF6600"
                                            Text="Roles"></asp:Label>
                                    </td>
                                    <td align="left" class="td">
                                        <asp:DropDownList ID="ddlUsrSrc" runat="server" Height="17px" Width="163px">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" align="left" style="width: 100px">Role Valid From</td>
                                    <td align="left" class="style2">
                                        <dxe:ASPxDateEdit ID="clrRoleStartDate" runat="server" Height="16px"
                                            Width="149px" AutoPostBack="True">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" align="left" style="width: 100px">Role Valid To</td>
                                    <td align="left" class="style2">
                                        <dxe:ASPxDateEdit ID="clrRoleEndDate" runat="server" Width="149px"
                                            Height="16px" AutoPostBack="True">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" align="left" style="width: 100px">AccessIDPattern</td>
                                    <td align="left" class="td">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsSpecStd" runat="server" ForeColor="#FF6600"
                                                        Text="Is Specific Student"
                                                        OnCheckedChanged="chkIsSpecStd_CheckedChanged" AutoPostBack="True" />
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox ID="chkIsNonSpecStd" runat="server" ForeColor="#FF6600"
                                                        Text="Is Non-Specific Student"
                                                        OnCheckedChanged="chkIsNonSpecStd_CheckedChanged" AutoPostBack="True" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="td">
                                        <table style="width: 416px">
                                            <tr>
                                                <td class="td" align="left">Program
                                                </td>
                                                <td class="td">
                                                    <asp:DropDownList ID="ddlProg" runat="server" Width="100px" OnSelectedIndexChanged="ddlProg_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td" align="left">Batch</td>
                                                <td class="td">
                                                    <asp:DropDownList ID="ddlBatch" runat="server" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td" align="left">Access Valid From</td>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrAccessStartDate" runat="server" Width="100%"
                                                        Height="20px">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td class="td" align="left">Access Valid To</td>
                                                <td class="td">
                                                    <dxe:ASPxDateEdit ID="clrAccessEndDate" runat="server" Width="100%"
                                                        Height="16px">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlSpecStd" runat="server" Width="420px" Height="143px"
                                            HorizontalAlign="Left" ScrollBars="Vertical">
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:TextBox ID="txtFindStd" runat="server" Width="169px"></asp:TextBox>

                                                    </td>
                                                    <td>

                                                        <asp:Button ID="btnFindStd" runat="server" Text="Find" Width="50px"
                                                            OnClick="btnFindStd_Click" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 415px" colspan="2">
                                                        <asp:GridView ID="gvSelection" runat="server" AutoGenerateColumns="False"
                                                            Width="394px" OnSelectedIndexChanged="gvSelection_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkLeft" runat="server" AutoPostBack="true"
                                                                            OnCheckedChanged="chkLeft_CheckedChanged" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Roll" HeaderText="Student ID" />
                                                                <asp:BoundField HeaderText="Valid From" />
                                                                <asp:BoundField HeaderText="Valid To" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <%--<td>
                                                    
                                                    <br />
                                                    <br />
                                                    
                                                    <asp:Button ID="btnRightPass" runat="server" Width="50px" Text="&gt;" 
                                                        onclick="btnRightPass_Click" />
                                                    
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnLeftPass" runat="server" Text="&lt;" Width="50px" />
                                                    
                                                </td>
                                                <td>
                                                    <asp:GridView ID="gvAfterSelection" runat="server" AutoGenerateColumns="False" 
                                                        Width="176px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="ID" Visible="true" />
                                                            <asp:TemplateField  HeaderText="Select">
                                                                <ItemTemplate>
                                                                   <asp:CheckBox ID="chkRight" runat="server" Visible = "true"  OnCheckedChanged = "chkRight_CheckedChanged" AutoPostBack="true"/>
                                                                </ItemTemplate>
                                                           </asp:TemplateField>
                                                            <asp:BoundField DataField="StdID" HeaderText="Student ID" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>--%>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlNonSpecStd" runat="server" Width="418px"
                                            HorizontalAlign="Left">
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:Button ID="btnAddGridView" runat="server" Text="Add" Width="50px"
                                                            OnClick="btnAddGridView_Click" />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvSelectedProgram" runat="server" AutoGenerateColumns="False"
                                                            Width="412px" OnRowDeleting="gvSelectedProgram_RowDeleting"
                                                            OnRowEditing="gvSelectedProgram_RowEditing">
                                                            <Columns>
                                                                <asp:CommandField ShowEditButton="True" Visible="false"></asp:CommandField>
                                                                <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                                <asp:BoundField DataField="ProgCode" HeaderText="Program" />
                                                                <asp:BoundField DataField="ProgId" Visible="False" />
                                                                <asp:BoundField DataField="BatchNo" HeaderText="Batch No" />
                                                                <asp:BoundField DataField="BatchId" Visible="False" />
                                                                <asp:BoundField DataField="AccessValidFrom" HeaderText="Valid From" DataFormatString="{0:dd MMM, yyyy}" />
                                                                <asp:BoundField DataField="AccessValidTo" HeaderText="Valid To" DataFormatString="{0:dd MMM, yyyy}" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="td" colspan="2" align="left">
                                        <asp:CheckBox ID="chkIsActiveUser" runat="server" ForeColor="#FF6600"
                                            Text="Is Active User" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="td">
                                        <asp:Panel ID="pnlSvClos" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnSave" runat="server" BackColor="#FF6600"
                                                            BorderColor="#FF9933" Text="Save"
                                                            OnClick="btnSave_Click" CssClass="button" />
                                                    </td>
                                                    <td align="center" class="td">
                                                        <asp:Button ID="btnCancel" runat="server" BackColor="#FF6600"
                                                            BorderColor="#FF9933" Text="Cancel"
                                                            OnClick="btnCancel_Click" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

