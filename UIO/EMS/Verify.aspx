<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Common/PublicMasterPage.master" CodeBehind="Verify.aspx.cs" Inherits="EMS.miu.ucam.Verify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">Degree Completed Viewer</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />
    
    <script type="text/javascript">
        $(document).ready(function () {
        });

        function InProgress() {
            var panelProg = $get('divProgress');
            panelProg.style.display = '';
        }

        function onComplete() {
            var panelProg = $get('divProgress');
            panelProg.style.display = 'none';
        }

        function isNumber(e) {
            var charCode = (navigator.appName == 'Netscape') ? e.which : e.keyCode
            status = charCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please make sure entries are numbers only")

                return false
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" runat="Server">
    <div> 
        <div class="PageTitle"> 
            <label style=" font-family:'Microsoft JhengHei UI' !important; font-size:1.2em;">Degree can be verified by using either the</label>
            <label style=" font-weight:bold; font-family:'Microsoft JhengHei UI' !important; font-size:1.2em; color:green; font-weight:bold !important;"><em>certificate serial no.</em> or <em>student roll or ID</em> </label>
&nbsp;</div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel01" runat="server">
            <ContentTemplate>
                <div class="Message-Area">
                    <label class="msgTitle"></label>
                    <asp:Label runat="server" ID="lblMsg" Text="" Font-Names="Microsoft JhengHei UI" Font-Size="Medium" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br /> 
        <style>
            .btnLoad {
                width:217px;
                height:50px;
                text-align:center;
                float:right;
                margin-right:10px;
                margin-bottom:15px;
                font-size:20px;
                margin-top:3px;
            }
            .auto-style1 {
                text-decoration: underline;
            }
            .auto-style2 {
                font-size: 13px;
            }
            #hover:hover {
                box-shadow:0px 0px 5px #00ff90;
            }
        </style>
        <div class="personalProfile-container">
            <asp:Panel runat="server" ID="pnStudentIDZone"  
                style="padding-left:15px; display:inline-block; width:40%; vertical-align:top; margin-right:7px;">
                <div id="hover" style=" background-color:lightblue;
                         border-radius:5px 5px; height:190px; padding:5px;">
                <div class="div-margin">
                    <div class="loadedArea">
                        <label style=" height:25px; min-width:200px; margin-top:15px; color:black; vertical-align:middle; font-family:'Microsoft JhengHei UI';font-size:20px;">Student Roll / ID :</label>
                        <asp:TextBox  style=" height:25px; color:black; vertical-align:middle; font-family:'Microsoft JhengHei UI';font-size:20px; float:right; display:inline-block; margin-right:10px;" runat="server" ID="txtStudentId" />
                        <br /><br /><br />
                        <label  style=" height:25px; min-width:200px; color:black; vertical-align:middle; font-family:'Microsoft JhengHei UI';font-size:20px;">Certificate Serial Number </label>
                        <asp:TextBox   style=" height:25px; color:black; vertical-align:middle; font-family:'Microsoft JhengHei UI';font-size:20px; display:inline-block;  float:right; margin-right:10px;" runat="server" ID="txtSerialNo" />
                        <br /><br /><br />
                        <asp:Button runat="server" CssClass="btnLoad" ID="btnLoad" Text="VERIFY" OnClick="btnLoad_Click" />
                    </div>
                </div>
             </div>
                <div style="width:100%; height:300px; margin-top:5px;  background-color:snow; box-shadow:grey 3px 3px inset; border:1px solid grey;
                         border-radius:5px 5px;">
                    <div class="div-margin">
                        <div class="loadedArea" style="font-family:'Microsoft JhengHei UI';font-size:15px; text-align:justify;">
                            <label style=" height:25px; min-width:200px; margin-top:15px; color:black; vertical-align:middle; font-family:'Microsoft JhengHei UI';font-size:20px;">Contact Details</label>
                            <br />
                            <br />
                            <div style="padding-left:10px;">
                             
                                <div id="hover" style="width:46%; display:inline-block; vertical-align:top;">
                                    <span class="auto-style1"><strong>Gulshan Campus: </strong></span>
                            <br />
                                <span class="auto-style2">Plot#CEN-16, Road # 106
                                <br />
                                Gulshan-2, Dhaka-1212, Bangladesh
                                <br />
                                <strong>Phone:</strong> 029884736, 029862251, 8817525, 9893226
                                <br />
                                <strong>Fax: </strong>9862226, 01780364414 </span>
                                <br />
                                <br />
                                </div>
                                <div id="hover" style="width:46%; display:inline-block; margin-left:11px; vertical-align:top;">
                                    <span class="auto-style1"><strong>Mirpur Campus: </strong></span>
                                <br />
                                <span class="auto-style2">Plot# 1, Block# B Section #01 Zoo Road, Mirpur-1,
                                    <br />
                                    Dhaka-1216, Bangladesh<br /> <strong>Phone: </strong>029026223, 028059990, 028059991, 03894552917-18 01819245895, 01780364415 </span>
                                <br />
                                </div>
                                
                                <span class="auto-style1"><strong>E-mail:</strong></span> <a href="mailto:info@manarat.ac.bd">info@manarat.ac.bd</a>, <a href="mailto:admission@manarat.ac.bd">controller@manarat.ac.bd</a> <span class="auto-style1"><strong>
                            <br />
                            Web Site:</strong></span> <a href="http://www.manarat.ac.bd">www.manarat.ac.bd</a>
                                </div>
                        </div>
                    </div>
                    
                </div>
            </asp:Panel>
            

            <asp:Panel runat="server" ID="pnIsVisible"  style="padding-left:15px; display:inline-block; width:55%; background-color:lightblue; padding-bottom:7px; border-radius:5px 5px;">

                <div id="hover" class="div-margin" style="min-width:210px; display:inline-block; vertical-align:top; margin-top:10px;">
                    <asp:Image  runat="server" Width="200px" Height="200px" ID="imgPhoto" ImageUrl="../../Images/photoBoy.png" />
                    <asp:HiddenField runat="server" ID="hfPhotoRoll" Value="" />
                </div>

                <asp:UpdatePanel ID="UpdatePanel02" runat="server" style="display:inline-block; width:60%; margin-left:25px;">
                    <ContentTemplate >

                            <div class="information-Zone" style="display:inline-block; width:100%;">
                                <fieldset>
                                    <legend style="font-family:'Microsoft JhengHei UI'; font-size:medium; color:blue;">Student Info</legend><br />
                                     <div class="loadAreaOdd">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Name</label>
                                    <asp:TextBox runat="server" ID="txtFullName" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:uppercase;" class="margin-zero input-Size" Enabled="True" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Father's Name</label>
                                    <asp:TextBox runat="server" ID="txtFatherName" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:uppercase;" class="margin-zero input-Size"  />
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Mother's Name</label>
                                    <asp:TextBox runat="server" ID="txtMotherName" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:uppercase;" class="margin-zero input-Size"  />
                                </div>
                                </fieldset>

                               
                               
                                
                            </div>
                            <div class="cleaner"></div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <fieldset style="display:block !important; padding-bottom:7px;">
                                    <legend style="font-family:'Microsoft JhengHei UI'; font-size:medium; color:blue;">Degree Info</legend><br />
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Degree Name</label>
                                    <asp:TextBox runat="server" ID="txtDegreeName" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:16px; font-weight:bold; text-wrap:normal; text-transform:uppercase;" class="margin-zero input-Size" />
                                </div>
                                <div class="loadedAreaOdd">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">CGPA</label>
                                    <asp:TextBox runat="server" ID="txtCGPA" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:uppercase;" class="margin-zero input-Size"  />
                                </div>
                                
                                <div class="loadedAreaOdd" style="display:none;">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Earned Credit</label>
                                    <asp:TextBox runat="server" ID="txtEarnedCredit" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:uppercase;" class="margin-zero input-Size"  />
                                </div>
                                <div class="loadedAreaEven">
                                    <label class="display-inline field-Title" style="width:100%; display:block; font-size:17px;">Completion Semester</label>
                                    <asp:TextBox runat="server" ID="txtCompletionSemester" ReadOnly="true" style="width:90%; border:none; text-align:left; padding-left:20px; padding-right:10px; background-color:inherit; display:block; height:30px; font-family:'Microsoft JhengHei UI'; font-size:23px; text-transform:capitalize;" class="margin-zero input-Size"  />
                                </div>
                                </fieldset>
            </asp:Panel>
        </div>

    </div>
</asp:Content>

