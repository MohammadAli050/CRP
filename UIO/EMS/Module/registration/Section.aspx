<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="StudentRegistration_Section" Codebehind="Section.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Section</title>
    <script src="../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">

        function countdown(count) {

            $("#countDownNum").html(count);

            count -= 1;

            if (count > 0) {
                setTimeout("countdown(" + count + ")", 1000);
            }
            else {
                setTimeout("self.close()", 1000)
            }
        }

        $(function () {
            countdown(30);
        });

        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
    </script>
    <%--<style type="text/css">
        #countDownNum
        {
            position: absolute;
            left: 800px;
            color: red;
            font: bold 40px arial;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin: 10px;">
                <div style="float: left; color: black; font: bold 40px arial;">Close In... &nbsp</div>
                <div style="float: left; color: red; font: bold 40px arial;" id="countDownNum"></div>
            </div>

            <div style="clear: both"></div>
            <div style="margin: 10px;">
                <asp:Label ID="lblMessage" ForeColor="Blue" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
            </div>
            <div style="clear: both"></div>
            <div>
                <asp:GridView ID="GridViewSection" runat="server" DataKeyNames="AcaCalSectionID"
                    AutoGenerateColumns="False"
                    OnSelectedIndexChanged="GridViewSection_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ButtonType="Link" ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="Id" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("AcaCalSectionID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Bind("SectionName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AcaCal_SectionID" HeaderText="AcaCal_SectionID"
                            Visible="False" />
                        <asp:TemplateField HeaderText="Time Slot1">
                            <ItemTemplate>
                                <asp:Label ID="lblTimeSlot1" runat="server" Text='<%# Bind("TimeSlot1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day One">
                            <ItemTemplate>
                                <asp:Label ID="lblDayOne" runat="server" Text='<%# Bind("DayOne") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time Slot2">
                            <ItemTemplate>
                                <asp:Label ID="lblTimeSlot2" runat="server" Text='<%# Bind("TimeSlot2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day Two">
                            <ItemTemplate>
                                <asp:Label ID="lblDayTwo" runat="server" Text='<%# Bind("DayTwo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Faculty1" HeaderText="Faculty (1)" Visible="False" />
                        <asp:BoundField DataField="Faculty2" HeaderText="Faculty (2)" Visible="False" />
                        <asp:BoundField DataField="RoomNo1" HeaderText="RoomNo (1)" Visible="False" />
                        <asp:BoundField DataField="RoomNo2" HeaderText="RoomNo (2)" Visible="False" />
                        <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                        <asp:BoundField DataField="Occupied" HeaderText="Occupied" />

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>


