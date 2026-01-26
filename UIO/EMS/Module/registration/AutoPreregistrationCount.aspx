<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="StudentRegistration_AutoPreregistrationCount" Codebehind="AutoPreregistrationCount.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="AutoPreregistrationCount">
        <div>
            <label class="labelDisplay" for="ddlProgram" style="width: 55px;">Program</label>
            <asp:DropDownList runat="server" class="dropdownList-size" ID="ddlProgram" DataValueField="ProgramId" DataTextField="ShortName"/>

            <asp:Button runat ="server" CssClass="button-css" ID="btnLoad" OnClick="btnLoad_Click" Text="Load" />
        </div>
        <div>
            <div style="width: 960px;">
                <div style="border-bottom: 1px solid gray; font-weight:bold; display:inline-block; padding: 1px; width: 51px;">Select</div>
                <div style="border-bottom: 1px solid gray; font-weight:bold; display:inline-block; padding: 1px; width: 202px;">Course Code</div>
                <div style="border-bottom: 1px solid gray; font-weight:bold; display:inline-block; padding: 1px; width: 562px;">Course Name</div>
                <div style="border-bottom: 1px solid gray; font-weight:bold; display:inline-block; padding: 1px; width: 61px;">Offered</div>
                <div style="border-bottom: 1px solid gray; font-weight:bold; display:inline-block; padding: 1px; width: 61px;">Taken</div>
            </div>
            <asp:Panel ID="pnCousreCount" runat="server" Width="980px" Height="400px" ScrollBars="Vertical" Wrap="False" style="display: inline-block;">                                              
                <asp:gridview ID="gvCourseCount" runat="server" AutoGenerateColumns="False" Height="80px" TabIndex="6" Width="960px" ShowHeader="false" style="border: 1px solid gray;">
				    <RowStyle Height="24px" />
					    <Columns>
                            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOfferedCourse" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" Width="50px" />
                            </asp:TemplateField>

                            <asp:boundfield DataField="CourseID" HeaderText="CourseID" Visible="false" HeaderStyle-HorizontalAlign="Left" ><%--ItemStyle-Width="100px"--%>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:boundfield>
                            <asp:boundfield DataField="VersionID" HeaderText="VersionID" Visible="false" HeaderStyle-HorizontalAlign="Left" ><%--ItemStyle-Width="100px"--%>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:boundfield>

                            <asp:boundfield DataField="FormalCode" HeaderText="Coure Code" ItemStyle-Width="50px">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="200px" />
                            </asp:boundfield>

                            <asp:boundfield DataField="CourseTitle" HeaderText="Course Name" HeaderStyle-HorizontalAlign="Left" ><%--ItemStyle-Width="100px"--%>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="550px" />
                            </asp:boundfield>

                            <asp:boundfield DataField="AutoOpen" HeaderText="Offered" HeaderStyle-HorizontalAlign="Left"><%-- ItemStyle-Width="300px"--%>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" />
                            </asp:boundfield>

                            <asp:boundfield DataField="AutoAssign" HeaderText="Taken" HeaderStyle-HorizontalAlign="Left" ><%--ItemStyle-Width="100px"--%>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" />
                            </asp:boundfield>                                        
					    </Columns>
				    <SelectedRowStyle Height="24px" />
				    <HeaderStyle Height="24px" />
			    </asp:gridview>
            </asp:Panel>
        </div>
    </div>

    <br />
    <br />
    <asp:Button runat="server" ID="btnCourseOffer" Text="Course Offer" OnClick="btnCourseOffer_Click" />
</asp:Content>

