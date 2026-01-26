<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="Registration_DeptSetUp" CodeBehind="DeptSetUp.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.2, Version=9.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }

        /*table, tr, th,td {
            border: 1px solid #008080;
        }*/

        .style1 {
            width: 60px;
        }

        .style5 {
            width: 330px;
        }

        .style6 {
            width: 101px;
        }

        .dxeButtonEdit {
            background-color: white;
            border: solid 1px #9F9F9F;
            width: 170px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContainer" runat="Server">

    <div class="PageTitle">
        <label>Dept Parameter SetUp</label>
    </div>

    <div class="Message-Area">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                    <asp:Label ID="Label16" runat="server" Text="Message : "></asp:Label>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <%-- <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Text="Department SetUp"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label1" runat="server" Text="Program Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPrograms" runat="server" Height="24px" Width="142px"
                                        TabIndex="1" OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlDetail" runat="server">
                            <table style="text-align: center;" cellpadding="8"
                                cellspacing="6">
                                <tr style="background: SeaGreen; color: white;">
                                    <td class="locname" colspan="2" align="center" style="text-align: center">
                                        <asp:Label ID="lblLocal" runat="server" Text="Course List Open Def."></asp:Label>
                                    </td>
                                    <td style="background:white;">&nbsp;</td>
                                    <td class="manname" colspan="3">
                                        <asp:Label ID="lblMan" runat="server" Text="Mandatory Assignment Def."></asp:Label>
                                    </td>
                                    <td class="creditlimit" style="background:white;">&nbsp;</td>
                                    <td class="maxname" colspan="2">
                                        <asp:Label ID="lblMax" runat="server" Text="Maximum Limit Def."></asp:Label>
                                    </td>
                                </tr>
                                <tr style="background: #04B45F; color: white;">
                                    <td class="loccgpa">
                                        <asp:Label ID="Label3" runat="server" Text="Max. CGPA"></asp:Label>
                                    </td>
                                    <td class="loccreditlimit">
                                        <asp:Label ID="Label4" runat="server" Text="Credit Limt"></asp:Label>
                                    </td>
                                    <td align="center" style="background:white;">&nbsp;</td>
                                    <td class="mancgpa">
                                        <asp:Label ID="Label6" runat="server" Text="Max. CGPA"></asp:Label>
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:Label ID="Label8" runat="server" Text="Credit Limit"></asp:Label>
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:Label ID="Label9" runat="server" Text="Old grade to improve"></asp:Label>
                                    </td>
                                    <td class="creditlimit" style="background:white;">&nbsp;</td>
                                    <td class="maxcgpa">
                                        <asp:Label ID="Label7" runat="server" Text="Max. CGPA"></asp:Label>
                                    </td>
                                    <td class="maxcreditlimit">
                                        <asp:Label ID="Label5" runat="server" Text="Credit Limit"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="loccgpa" align="center" style="text-align: center">
                                        <dxe:ASPxSpinEdit ID="spnLocalCGPA1" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" TabIndex="2" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="loccreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnLocalCredit1" runat="server" Height="21px" Number="0"
                                            TabIndex="3" Width="50px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit"></td>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMANCGPA1" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMANCredit1" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:DropDownList ID="ddlMinGrade1" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="creditlimit"></td>
                                    <td class="maxcgpa">
                                        <dxe:ASPxSpinEdit ID="spnMAXCGPA1" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="maxcreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMAXCredit1" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="loccgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnLocalCGPA2" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="loccreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnLocalCredit2" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit">&nbsp;</td>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMANCGPA2" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMANCredit2" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:DropDownList ID="ddlMinGrade2" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="creditlimit">&nbsp;</td>
                                    <td class="maxcgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMAXCGPA2" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="maxcreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMAXCredit2" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="loccgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnLocalCGPA3" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="loccreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnLocalCredit3" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit">&nbsp;</td>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMANCGPA3" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMANCredit3" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:DropDownList ID="ddlMinGrade3" runat="server" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="creditlimit">&nbsp;</td>
                                    <td class="maxcgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMAXCGPA3" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="maxcreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMAXCredit3" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9"></td>
                                </tr>
                                <tr style="background: SeaGreen; color: white;">
                                    <td class="manname" colspan="2">
                                        <asp:Label ID="Label2" runat="server" Text="Pre-Advising Def."></asp:Label>
                                    </td>

                                    <td style="background:white;">&nbsp;</td>

                                    <td class="locname" colspan="2" align="center" style="text-align: center">
                                        <asp:Label ID="lbl" runat="server" Text="Project/Thesis Def."></asp:Label>
                                    </td>
                                    <td class="creditlimit" colspan="2" style="background:white;" >&nbsp;</td>
                                    <td class="manname" colspan="2">
                                        <asp:Label ID="Label11" runat="server" Text="Major Declare Def."></asp:Label>
                                    </td>

                                </tr>
                                <tr style="background: #04B45F; color: white;">
                                    <td class="mancgpa">
                                        <asp:Label ID="Label14" runat="server" Text="Min. CGPA"></asp:Label>
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:Label ID="Label15" runat="server" Text="Credit Limit"></asp:Label>
                                    </td>



                                    <td align="center" style="background:white;">&nbsp;</td>
                                    <td class="loccgpa">
                                        <asp:Label ID="Label12" runat="server" Text="Projec/Internship: Min. CGPA"></asp:Label>
                                    </td>
                                    <td class="loccreditlimit">
                                        <asp:Label ID="Label13" runat="server" Text="Credit Limt"></asp:Label>
                                    </td>
                                    <td class="creditlimit" colspan="2" style="background:white;">&nbsp;</td>
                                    <td class="mancgpa">
                                        <asp:Label ID="Label18" runat="server" Text="Min. CGPA"></asp:Label>
                                    </td>
                                    <td class="mancreditlimit">
                                        <asp:Label ID="Label21" runat="server" Text="Credit Limit"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCGPA1" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCredit1" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit"></td>
                                    <td class="loccgpa" align="center" style="text-align: center">
                                        <dxe:ASPxSpinEdit ID="spnProjectCGPA" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" TabIndex="2" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="loccreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnProjectCrLimit" runat="server" Height="21px" Number="0"
                                            TabIndex="3" Width="50px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit" colspan="2"></td>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnMajorCGPA" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                        &nbsp;</td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnMajorCrLimit" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />

                                    </td>
                                </tr>
                                <tr>
                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCGPA2" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCredit2" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td></td>

                                    <td class="loccgpa">
                                        <asp:Label ID="Label19" runat="server" Text="Thesis: Min. CGPA"></asp:Label>
                                    </td>
                                    <td class="loccreditlimit">
                                        <asp:Label ID="Label20" runat="server" Text="Credit Limt"></asp:Label>
                                    </td>
                                    <td align="center" colspan="4">&nbsp;</td>
                                </tr>
                                <tr>

                                    <td class="mancgpa" align="center">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCGPA3" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="mancreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnAutoPreRegCredit3" runat="server" Height="21px" Number="0"
                                            Width="50px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />
                                    </td>

                                    <td class="creditlimit"></td>
                                    <td class="loccgpa" align="center" style="text-align: center">
                                        <dxe:ASPxSpinEdit ID="spnThesisCGPA" runat="server"
                                            AutoResizeWithContainer="True" Height="21px" Increment="0.1" LargeIncrement="1"
                                            Number="0" TabIndex="2" Width="100px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center">
                                            <LargeIncrementButtonStyle Wrap="True">
                                            </LargeIncrementButtonStyle>
                                            <IncrementButtonStyle Wrap="True">
                                            </IncrementButtonStyle>
                                            <SpinButtons ShowLargeIncrementButtons="True">
                                            </SpinButtons>
                                            <DecrementButtonStyle Wrap="True">
                                            </DecrementButtonStyle>
                                            <LargeDecrementButtonStyle Wrap="True">
                                            </LargeDecrementButtonStyle>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td class="loccreditlimit">
                                        <dxe:ASPxSpinEdit ID="spnThesisCrLimit" runat="server" Height="21px" Number="0"
                                            TabIndex="3" Width="50px" DecimalPlaces="2" MaxLength="7"
                                            MaxValue="200" CssClass="spiners" HorizontalAlign="Center" />
                                    </td>
                                    <td class="creditlimit"></td>
                                    <td class="creditlimit"></td>

                                    <td class="creditlimit" colspan="2"></td>

                                </tr>
                                <tr>
                                    <td colspan="9"></td>
                                </tr>
                                <tr>
                                    <td align="center" class="retakeLimit" colspan="3">
                                        <asp:Label ID="Label22" runat="server"
                                            Text="Pre Registration Lock Def. for Probation:"></asp:Label></td>
                                    <td class="retakeLimit" colspan="6">
                                        <dxe:ASPxSpinEdit ID="spnProbationLock" runat="server" Height="21px" Number="0"
                                            Width="70px" DecimalPlaces="2" MaxLength="7" MaxValue="200"
                                            CssClass="spiners" HorizontalAlign="Center" />

                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" class="retakeLimit" colspan="3">
                                        <asp:Label ID="Label10" runat="server"
                                            Text="Retake limit per course for a student:"></asp:Label></td>
                                    <td class="retakeLimit" colspan="6">
                                        <dxe:ASPxSpinEdit ID="spnRetakelimit" runat="server" Height="21px" Number="0"
                                            Width="50px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlCtl" runat="server">
                            <table width="100%">
                                <tr>
                                    <td class="style1">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button"
                                            OnClick="btnSave_Click" />
                                    </td>
                                    <td class="style1">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="valsumDepsetUp" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

