<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RoomInfoSetup.aspx.cs" Inherits="RoomInfoSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Room Information</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Setup :: Room Information</label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle">Message: </label>
                    <asp:Label runat="server" ID="lblMsg" Text="" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel02" runat="server">
            <ContentTemplate>
                <div class="RoomInfoSetup-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <asp:HiddenField runat="server" ID="hfRoomInfoID" Value="" />

                            <label class="display-inline field-Title">Room Number</label>
                            <asp:TextBox runat="server" ID="txtRoomNumber" class="margin-zero input-Size" />

                            <label class="display-inline field-Title1">Room Name</label>
                            <asp:TextBox runat="server" ID="txtRoomName" class="margin-zero input-Size" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Room Type</label>
                            <asp:DropDownList runat="server" ID="ddlRoomType" class="margin-zero dropDownList" />

                            <label class="display-inline field-Title1">Campus Name</label>
                            <asp:DropDownList runat="server" ID="ddlCampus" class="margin-zero dropDownList" AutoPostBack="true" OnSelectedIndexChanged="Campus_Changed" />

                            <label class="display-inline field-Title1">Building Name</label>
                            <asp:DropDownList runat="server" ID="ddlBuilding" class="margin-zero dropDownList" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title2">Class Capacity</label>
                            <asp:TextBox runat="server" ID="txtClassCapacity" class="margin-zero input-Size1" />

                            <label class="display-inline field-Title3">Exam Capacity</label>
                            <asp:TextBox runat="server" ID="txtExamCapacity" class="margin-zero input-Size1" />

                            <label class="display-inline field-Title3">Total Rows</label>
                            <asp:TextBox runat="server" ID="txtTotalRows" class="margin-zero input-Size1" />

                            <label class="display-inline field-Title3">Total Columns</label>
                            <asp:TextBox runat="server" ID="txtTotalColumns" class="margin-zero input-Size1" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title"></label>
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" class="button-margin btn-size" />
                            <asp:Button runat="server" ID="btnLoad" Text="Load" OnClick="btnLoad_Click" class="button-margin btn-size1" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="RoomInfoSetup-container">
                    <asp:Panel ID="PnlRoomInfoSetup" runat="server" Wrap="False">
                        <asp:gridview ID="gvRoomInfoSetup" runat="server" AutoGenerateColumns="False" class="mainTable">
                            <RowStyle Height="24px" />
                            <AlternatingRowStyle BackColor="#f5fbfb" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblType" Font-Bold="False" Text='<%#Eval("RomeTypeName") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Campus" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCampus" Font-Bold="False" Text='<%#Eval("CampusName") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Building" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblBuilding" Font-Bold="False" Text='<%#Eval("BuildingName") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Room Name" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblRoomName" Font-Bold="False" Text='<%#Eval("RoomName") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capacity" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblCapacity" Font-Bold="False" Text='<%#Eval("Capacity") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rows" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblRows" Font-Bold="False" Text='<%#Eval("Rows") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Columns" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate><asp:Label runat="server" ID="lblColumns" Font-Bold="False" Text='<%#Eval("Columns") %>' /></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Edit" ID="lbEdit" CommandArgument='<%#Eval("RoomInfoID") %>' OnClick="lbEdit_Click">
                                            <span class="action-container"><img src="../../../Images/2.edit.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ToolTip="Delete" ID="lbDelete" CommandArgument='<%#Eval("RoomInfoID") %>' OnClick="lbDelete_Click" OnClientClick="return confirm('Are you sure to Delete ?')">
                                            <span class="action-container"><img src="../../../Images/3.delete.png" class="img-action" /></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="45px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data Found !!
                            </EmptyDataTemplate>
                            <RowStyle CssClass="rowCss" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:gridview>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>