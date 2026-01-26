<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" CodeBehind="RptStudentCertificate.aspx.cs" Inherits="EMS.Module.student.report.RptStudentCertificate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ProgramUserControl.ascx" TagPrefix="uc1" TagName="ProgramUserControl" %>
<%@ Register Src="~/UserControls/BatchUserControl.ascx" TagPrefix="uc1" TagName="BatchUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Certificate
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
    
    
    
      <style type="text/css">
        .auto-style3 {
            width: 100px;
        }

        table {
            border-collapse: collapse;
        }

        .tbl-width-lbl {
            width: 100px;
            padding: 5px;
        }

        .tbl-width {
            width: 150px;
            padding: 5px;
        }
    </style>


    <%-- <script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.7.1/jquery.min.js" integrity="sha512-BkBgWiL0N/EFIbLZYGTgbksKG5bS6PtwnWvVk3gccv+KhtK/4wkLxCRGh+kelKiXx7Ey4jfTabLg3AEIPC7ENA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="../../../JavaScript/jspdf.debug.js"></script>
    <script src="../../../JavaScript/JSBarcode.js"></script>
    <script type="text/javascript">



        function IsProvisionalChecked() {
            return $('#<%=chkProvisional.ClientID %>').is(':checked');
        }

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

        function downloadPDF(htmlString) {
            //console.log(htmlString);
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };

            //var htmlString = "<h3>Sample h3 tag</h3><p>Sample pararaph</p>";
            //htmlString += "<h3>Sample h3 tag</h3><p>Sample pararaph</p>";
            doc.fromHTML(htmlString, 15, 15, {
                'width': 100,
                'elementHandlers': specialElementHandlers
            });
            doc.save('sample-file.pdf');
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 10px;
        }

        .auto-style2 {
            width: 140px;
        }

        .divOuter {
            display: inline;
            text-align: center;
        }

        .divInner1 {
            float: left;
            width: 28%;
            height: 600px;
            overflow: scroll;
        }

        .divInner2 {
            float: left;
            width: 70%;
            height: 100%;
        }

        @font-face {
            font-family: "lucida calligraphy";
            src: url("../../../Content/font/lucida calligraphy italic.ttf") format("truetype");
        }
    </style>
     <script type="text/javascript">

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="stdcertificatejs.js?v=1.13" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="server">
    <div class="container-fluid">

        <div class="row">

            <div class="col-sm-12 col-md-12 col-sm-12">
                <label><b style="color: black; font-size: 20px; margin-top:5px">Student Certificate</b></label>

            </div>
        </div>

    <div id="divProgress" style="display: none; z-index: 1000; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%);">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Loading_Animation.gif" Height="150px" Width="150px" />
        </div>

     <div style="height:auto; width:100%; margin-bottom: 20px;">
         <div class="Message-Area">
            <div style="padding: 5px; margin: 5px; width: 900px;">
                <table style="padding: 1px; width: 900px;">
                    <tr>
                        <td class="tbl-width-lbl"><b>Program</b></td>
                        <td>
                            <uc1:ProgramUserControl runat="server" ID="ucProgram" OnProgramSelectedIndexChanged="OnProgramSelectedIndexChanged" />
                        </td>
                        <td class="tbl-width-lbl"><b>Batch</b></td>
                        <td>
                            <uc1:BatchUserControl runat="server" ID="ucBatch" OnBatchSelectedIndexChanged="OnBatchSelectedIndexChanged" />
                        </td>
                        <td class="tbl-width-lbl">
                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true" Text="Student ID :"></asp:Label></td>
                        <td class="tbl-width">
                            <asp:TextBox ID="txtStudent" runat="server" Text=""></asp:TextBox>
                        </td>  
                                  <td class="tbl-width-lbl">
                                    <asp:Label ID="Label4" runat="server" Text="Provisional" Font-Bold="true"></asp:Label>
                                </td>     
                          <td>
                                    <%-- <input type="checkbox" value="Provisional" id="chkProvisional" >--%>
                                    <asp:CheckBox ID="chkProvisional" runat="server" AutoPostBack="true" OnCheckedChanged="chkProvisional_CheckedChanged" />
                         </td>

                               
                                        
                       
                        <td class="tbl-width-lbl">
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                        </td>

                       

                       

                        <td class="tbl-width-lbl">
                             <button id="Button5" runat="server" style="width: 150px; height: 30px; border-radius: 5px; background-color: #39b54a; color: white;" onclick="DownloadPDFForPreview()">Draft Copy</button>
                        </td>
                         
                          <td class="tbl-width-lbl">
                               <button id="Button2" runat="server" style="width: 100px; height: 30px; border-radius: 5px; background-color: #39b54a; color: white;" onclick="DownloadPDF()">Download</button>
                        </td>


                         <asp:Label ID="lblStudentRoll" runat="server" Text="" Visible="true" Style="display: none;"></asp:Label>

                               <%--   <td class="auto-style1">&nbsp</td>
                                <td>
                                    <asp:Button ID="btnApprove" runat="server" Text="Approval" Style="height: 30px; border-radius: 5px; background-color: firebrick; color: white;" OnClick="btnApprove_Click" />
                                </td>--%>


                        
                    </tr>

                    <tr>


                        
                                 <td class="tbl-width-lbl">
                                    <asp:Label ID="Label1" runat="server" Text="Issue Date" Font-Bold="true"></asp:Label>
                                </td>   


                                <td>
                                    <div class="controls">
                                        <asp:TextBox runat="server" ID="txtIssuedDate" Width="113px" class="margin-zero input-Size datepicker" placeholder="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIssuedDate" Format="dd/MM/yyyy" />
                                       <%-- &nbsp;&nbsp;&nbsp;--%>
                            
                                    </div>
                                </td>

                    </tr>



                </table>
            </div>
        </div>
         </div>

        <div class="Message-Area">
            <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="true">
                        <asp:Label ID="Label2" runat="server" Text="Message : "></asp:Label>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>

              

            </asp:UpdatePanel>
        </div>
    </div>
    <div style="visibility: hidden">

            <img id='itf' />
            <img id='itf2' />
            <img id='vcimg' src="../../../Images/VC.JPEG" />
        </div>
    <asp:GridView ID="GvStudent" runat="server" AutoGenerateColumns="False" CssClass="table-bordered"
        EmptyDataText="No data found." CellPadding="4"
        OnRowCommand="GvStudent_RowCommand">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="StudentId" Visible="false" HeaderText="Id">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="SI." ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><b><%# Container.DataItemIndex + 1 %></b></ItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <headertemplate>Registraion No<br />& Session</headertemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRegistrationSession" Text='<%#Eval("Attribute1") + "<br />" + Eval("Attribute2") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="120px" />
            </asp:TemplateField>

            <%--<asp:BoundField DataField="Attribute1" HeaderText="Registraion No">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:BoundField>

            <asp:BoundField DataField="Attribute2" HeaderText="Session">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="100px" />
            </asp:BoundField>--%>

          <%--  <asp:TemplateField HeaderText="Photo">
                <ItemTemplate>
                    <div style="text-align: center">
                        <span class="action-container" style="display: inline-block; width: 64px; height: 64px; overflow: hidden;">
                            <img style="width: 64px; height: auto;" src='<%#Eval("Attribute3")%>' />
                        </span>
                    </div>
                </ItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>--%>


                    <asp:TemplateField HeaderText="Student ID">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblRoll" Font-Bold="true" Text='<%# Eval("Roll") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                 <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblName" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>


        <%--    <asp:BoundField DataField="Roll" HeaderText="Student Roll">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="80px" />
            </asp:BoundField>--%>

        <%--    <asp:BoundField DataField="Name" HeaderText="Student Name">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle Width="200px" />
            </asp:BoundField>--%>

       <%--     <asp:BoundField DataField="Gender" HeaderText="Gender">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle Width="50px" />
            </asp:BoundField>--%>

            <%--<asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPhoneNumber" Text='<%#Eval("BasicInfo.Phone") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="150px" />
            </asp:TemplateField>--%>

         <%--   <asp:TemplateField>
                <headertemplate>Phone<br />& Email</headertemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPhoneEmail" Text='<%#Eval("BasicInfo.Phone") + "<br />" + Eval("BasicInfo.Email") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="150px" />
            </asp:TemplateField>--%>

           <%-- <asp:TemplateField>
                <headertemplate>Father<br /> & Mother</headertemplate>
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFatherMother" Text='<%#Eval("BasicInfo.FatherName") + "<br />" + Eval("BasicInfo.MotherName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="150px" />
            </asp:TemplateField>--%>

                            
           <%-- <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="EditButton" CommandName="StudentEdit" Text="Edit" ToolTip="Edit Student" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="40px"></HeaderStyle>
                <ItemStyle CssClass="center" HorizontalAlign="Center"/>
            </asp:TemplateField>--%>

                         <asp:TemplateField HeaderText="Download Count">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDownloadCount" Font-Bold="true" ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"   
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox runat="server" ID="ChkSelect" OnCheckedChanged="chkNotSet_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                            <asp:HiddenField ID="HiddenField1" Value='<%# Eval("Roll") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>

                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>

            <%--OnCheckedChanged="chkSelectAll_CheckedChanged"--%>
            <%--OnCheckedChanged="chkNotSet_CheckedChanged"--%>
        <%--    Text='<%#Eval("DownLoadCount") %>'--%>

            <%--<asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="DeleteButton" CommandName="StudentDelete" Enabled="false" Visible="false" Text="Delete" ToolTip="Delete Student" CommandArgument='<%# Bind("StudentId") %>' runat="server"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="80px"></HeaderStyle>
                <ItemStyle CssClass="center" />
            </asp:TemplateField>--%>
        </Columns>
        <EditRowStyle BackColor="#00CC00" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SeaGreen" Font-Bold="True" ForeColor="White" />
        <%--<PagerStyle BackColor="#F7F6F3" ForeColor="#5D7B9D" HorizontalAlign="left" cssclass="gridview">--%>
        <PagerStyle BackColor="#F7F6F3" ForeColor="#00CC00" HorizontalAlign="left" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

      
   

</asp:Content>
