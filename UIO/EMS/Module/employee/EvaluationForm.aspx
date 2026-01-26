<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="ServayByStudent_EvaluationForm" Codebehind="EvaluationForm.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">Evaluation</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">

    <link href="../../ContentCSS/CSS/ChildSiteMaster.CSS" rel="stylesheet" />

    <!--<link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.3.0/pure-min.css" />-->

    <script type="text/javascript">
        $(document).ready(function () {
            //$('#MainContainer_pnMessage').hide();
        });
        function jScript() {
            $('.rdbOpinion label').after('<br />');
            $('.evaluationTable tr:nth-child(odd)').css("background-color", "#f5fbfb");
            //$('#MainContainer_pnMessage').hide();
        }
        function btnTheorySubmit() {
            return true;
        }
        function btnLabSubmit() {
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContainer" Runat="Server">
    <div>
        <div class="PageTitle">
            <label>Course Evaluation Form</label>
        </div>

        <asp:UpdatePanel runat="server" ID="UpdatePanel01">
            <ContentTemplate>
                <script type="text/javascript">Sys.Application.add_load(jScript);</script>
                <asp:Panel runat="server" ID="pnMessage">
                    <div class="Message-Area">
                        <label class="msgTitle">Message: </label>
                        <asp:Label runat="server" ID="lblMsg" Text="" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel02">
            <ContentTemplate>
                <div class="EvaluationFormTheory-container">
                    <div class="div-margin">
                        <div class="loadArea">
                            <label class="display-inline field-Title">Course</label>
                            <asp:DropDownList runat="server" ID="ddlAcaCalSection" class="margin-zero dropDownList" OnSelectedIndexChanged="ddlAcaCalSection_Change" AutoPostBack="true" />
                        </div>
                        <div class="loadedArea">
                            <label class="display-inline field-Title">Faculty</label>
                            <asp:Label runat="server" ID="lblFacultyName" class="display-inline field-Title-Fix" Text="............................................................" />

                            <label class="display-inline field-Title2">Semester</label>
                            <asp:Label runat="server" ID="lblSemester" class="display-inline field-Title-Fix2" Font-Bold="true" Text="" />
                            <asp:HiddenField runat="server" ID="hfSemester" />

                            <label class="display-inline field-Title1">Expected Grade</label>
                            <asp:DropDownList runat="server" ID="ddlExpectedGrade" class="margin-zero dropDownList2" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel runat="server" ID="UpdatePanel03">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnTheoryCourse">
                    <div class="EvaluationFormTheory-container">
                        <div class="div-margin1">
                            <b>
                                We seek your objective and unbiased response to our inquiries, Your Feedback will help us to improve the quality of teaching.<br />
                                 Please circle one number in response to each inquiry.
                            </b>
                        </div>
                        <div>
                            <table class="pure-table pure-table-bordered evaluationTable">
                                <tr>
                                    <th>#</th>
                                    <th class="head-title">Inquires</th>
                                    <th class="head-title">Action</th>
                                </tr>
                                <tr>
                                    <td>1.</td>
                                    <td class="inquiresTitle">The course was well designed and organized</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2.</td>
                                    <td class="inquiresTitle">Materials mentioned in the course outline were well covered in the class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>3.</td>
                                    <td class="inquiresTitle">You learned a great deal from the course</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4.</td>
                                    <td class="inquiresTitle">The materials covered in this course were reasonable for one semester</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5.</td>
                                    <td class="inquiresTitle">The course was interesting and useful</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>6.</td>
                                    <td class="inquiresTitle">The teacher was adequately prepared for the class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>7.</td>
                                    <td class="inquiresTitle">The teacher gave clear explanation</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>8.</td>
                                    <td class="inquiresTitle">The teacher encouraged class participation and responded to questions</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>9.</td>
                                    <td class="inquiresTitle">The teacher's teaching methods were effective</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>10.</td>
                                    <td class="inquiresTitle">Students were kept informed about their progress</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>11.</td>
                                    <td class="inquiresTitle">The teacher stimulated independent thinking</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>12.</td>
                                    <td class="inquiresTitle">The teacher used class time effectively</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory12" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>13.</td>
                                    <td class="inquiresTitle">The teacher was available beyond class hours and was helpful</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory13" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>14.</td>
                                    <td class="inquiresTitle">Class started and ended on time</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory14" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>15.</td>
                                    <td class="inquiresTitle">The Teacher was fair to all students</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory15" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>16.</td>
                                    <td class="inquiresTitle">The Teacher's English communication and delivery skills were very good</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory16" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>17.</td>
                                    <td class="inquiresTitle">The Teacher is excellent</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtTheory17" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>18.</td>
                                    <td class="inquiresTitle">Any other comment </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="300" Height="75"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="pure-form pure-form-stacked div-margin">
                                <asp:Button class="pure-button pure-button-active" ID="btnTheorySubmit" runat="server" Text="Save" OnClick="btnTheorySubmit_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnLabCourse">
                    <div class="EvaluationFormTheory-container">
                        <div class="div-margin1">
                            <b>
                                We seek your objective and unbiased response to our inquiries, Your Feedback will help us to improve the quality of teaching. <br />
                                Please circle one number in response to each inquiry.

                            </b>
                        </div>
                        <div>
                            <table class="pure-table pure-table-bordered evaluationTable">
                                <tr>
                                    <th>#</th>
                                    <th class="head-title">Inquires</th>
                                    <th class="head-title">Action</th>
                                </tr>
                                <tr>
                                    <td>1.</td>
                                    <td class="inquiresTitle">The lab classes were</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Boring</asp:ListItem>
                                            <asp:ListItem Value="2">Enjoyable</asp:ListItem>
                                            <asp:ListItem Value="3">Frightening</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2.</td>
                                    <td class="inquiresTitle">The lab course was</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Easy</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Difficult</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>3.</td>
                                    <td class="inquiresTitle">You have learned a lot to solve a problem</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4.</td>
                                    <td class="inquiresTitle">You could clearly understand the problem matters given in the class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5.</td>
                                    <td class="inquiresTitle">You have found your teacher during full lab hour</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>6.</td>
                                    <td class="inquiresTitle">You have received full and properly prepared lab sheet</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>7.</td>
                                    <td class="inquiresTitle">The teacher gave attention in solving problems for students</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>8.</td>
                                    <td class="inquiresTitle">Teacher provided hands-on training</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>9.</td>
                                    <td class="inquiresTitle">Teacher provided lecture</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>10.</td>
                                    <td class="inquiresTitle">Teacher provided assignment/experiments/quiz</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">Less</asp:ListItem>
                                            <asp:ListItem Value="2">Moderate</asp:ListItem>
                                            <asp:ListItem Value="3">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="4">Too Much</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>11.</td>
                                    <td class="inquiresTitle">Lab facilities were</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtLab11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">More than enough</asp:ListItem>
                                            <asp:ListItem Value="4">Sufficient</asp:ListItem>
                                            <asp:ListItem Value="3">Equipments were older</asp:ListItem>
                                            <asp:ListItem Value="2">Equipments are modern</asp:ListItem>
                                            <asp:ListItem Value="1">Number of equipment was few</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <div class="pure-form pure-form-stacked div-margin">
                                <asp:Button class="pure-button pure-button-active" ID="btnLabSubmit" runat="server" Text="Save" OnClick="btnLabSubmit_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnBussiness">
                    <div class="EvaluationFormTheory-container">
                        <div class="div-margin1">
                            <b>
                               We seek your objective and unbiased response to our inquiries, Your Feedback will help us to improve the quality of teaching. <br />
                               Please circle one number in response to each inquiry.

                            </b>
                        </div>
                        <div>
                            <table class="pure-table pure-table-bordered evaluationTable">
                                <tr>
                                    <th>#</th>
                                    <th class="head-title">Inquires</th>
                                    <th class="head-title">Action</th>
                                </tr>
                                <tr>
                                    <td>1.</td>
                                    <td class="inquiresTitle">The teacher provided the course - outline in the 1<sup>st</sup> day of the class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2.</td>
                                    <td class="inquiresTitle">Materials mentioned in the course - outline were well covered</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>3.</td>
                                    <td class="inquiresTitle">The course was usefull and I learned a great deal from the course</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>4.</td>
                                    <td class="inquiresTitle">The teacher was adequately prepared for each class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>5.</td>
                                    <td class="inquiresTitle">The teacher encouraged class participation and responded to questions asked</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>6.</td>
                                    <td class="inquiresTitle">The teacher returned test results within one week</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>7.</td>
                                    <td class="inquiresTitle">The teacher used class time effectively</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>8.</td>
                                    <td class="inquiresTitle">The teacher was available during counseling hours</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>9.</td>
                                    <td class="inquiresTitle">Classes began and ended on time</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>10.</td>
                                    <td class="inquiresTitle">The teacher was reasonably fair to all students</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>11.</td>
                                    <td class="inquiresTitle">The teacher always delivers lecture in English in the class</td>
                                    <td>
                                        <asp:RadioButtonList Class="rdbOpinion" ID="rbtBus11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="5">Strongly Agree</asp:ListItem>
                                            <asp:ListItem Value="4">Agree</asp:ListItem>
                                            <asp:ListItem Value="3">Neither Agree Nor Disagree</asp:ListItem>
                                            <asp:ListItem Value="2">Disagree</asp:ListItem>
                                            <asp:ListItem Value="1">Strongly Disagree</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>12.</td>
                                    <td class="inquiresTitle">Any other comment </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtBusComments" TextMode="MultiLine" Width="300" Height="75"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="pure-form pure-form-stacked div-margin">
                                <asp:Button class="pure-button pure-button-active" ID="btnBusSubmit" runat="server" Text="Save" OnClick="btnBusSubmit_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>