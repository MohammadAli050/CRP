<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="FAQPage" Codebehind="FAQPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContainer" Runat="Server">
     <style type="text/css">
        .div {
            text-align: left;
        }
    </style>
    <div style="width:980px;margin:0 auto;">
        <div>
            <h2>Frequently Asked Questions</h2>
            <h1 style="color: red">Under Construction</h1>
        </div>
        <div class="div">
            Description aboute frequently asked question.
            Description aboute frequently asked question.
            Description aboute frequently asked question.
            Description aboute frequently asked question.

        </div>
        <div class="div">
            <p>
                Description about tagging here.Description about tagging hereDescription about tagging here.
                Description about tagging here.Description about tagging hereDescription about tagging here.
            </p>
            <asp:Label Style="text-align: left; width: 980px; font-size: 20px" ID="Label1" runat="server" Text="Tag "></asp:Label>
        </div>
        <div class="div">
            <asp:TextBox ID="txtTag" placeholder="Tag here" Width="980px" runat="server"></asp:TextBox>
        </div>
        <div class="div">
            <p>
                Description about key here.Description about key here.Description about key here.
                Description about key here.Description about key here.Description about key here.
            </p>
            <asp:Label Style="text-align: left; width: 980px; font-size: 20px" ID="Label2" runat="server" Text="Key "></asp:Label>
        </div>
        <div class="div">
            <asp:TextBox ID="txtKey" placeholder="Key here" Width="980px" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="div">
            <p>
                Description about Question here.Description about Question here.Description about Question here.
                Description about Question here.Description about Question here.Description about Question here.
            </p>
            <asp:Label Style="text-align: left; width: 980px; font-size: 20px" ID="Label3" runat="server" Text="Question "></asp:Label>
        </div>
        <div class="div">
            <asp:TextBox TextMode="MultiLine" placeholder="Ask here" Width="980px" ID="txtQuestion" runat="server" Text="" Height="104px"></asp:TextBox>
        </div>
        <div class="div" style="text-align: left; font-size: 14px;">
            <asp:Button Style="margin-top: 20px" ID="btnSubmit" runat="server" Text="Submit" OnClick="submitQuestion_click"></asp:Button><br />
        </div>
    </div>
</asp:Content>

