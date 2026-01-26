<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Common/CommonMasterPage.master" AutoEventWireup="true" Inherits="FAQAnswer" Codebehind="FAQAnswer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContainer" runat="Server">
    &nbsp;<style type="text/css">
              .div
              {
                  text-align: left;
              }
          </style>
    <div style=" width:980px; margin: 0 auto;">
        <div class="div">

            <h2 style="color: red"><span class="mw-headline" id="Questions">Questions Section</span></h2>
            <h3><span class="mw-headline" id="Most_Frequently_Asked_Questions">Most Frequently Asked Questions</span></h3>
        </div>
        <div>
            <div class="div">
                <ol>
                    <li><a href="#id1">Frequently asked question number 1?</a>
                    </li>
                    <li><a href="#id2">Frequently asked question number 2?Frequently asked question number 2?</a>
                    </li>
                    <li><a href="#id3">Frequently asked question number 3?Frequently asked question number 3?Frequently asked question number 3?</a>
                    </li>
                    <li><a href="#id4">Frequently asked question number 4?Frequently asked question number 4?Frequently asked question number 4?Frequently asked question number 4? extra large question of frequently asked question</a>
                    </li>
                </ol>
                <h3><span class="mw-headline" id="Question category 2">Question category 2</span></h3>
                <ol>
                    <li><a href="#id5">question 1 of Category 2?</a>
                    </li>
                    <li><a href="#id6">question 2 of Category 2?question 2 of Category 2?</a>
                    </li>
                    <li><a href="#id7">question 3 of Category 2?question 3 of Category 2?question 3 of Category 2?question 3 of Category 2?</a>
                    </li>
                    <li><a href="#id8">question 4 of Category 2?question 4 of Category 2?question 4 of Category 2?question 4 of Category 2?question 4 of Category 2?question 4 of Category 2? extra large question</a>
                    </li>
                </ol>


            </div>
        </div>
        <h4 class="div" style="font-size: 20px; color: green">Anser Section</h4>
        <div>
            <div class="div">
                <h4><span class="mw-headline" id="id1">Frequently asked question 1?</span></h4>
                <p>anser of the question of frequently asked question number 1</p>
                <p>anser of the question of frequently asked question number 1.anser of the question of frequently asked question number 1.</p>
                <p>anser of the question of frequently asked question number 1.anser of the question of frequently asked question number 1.anser of the question of frequently asked question number 1.anser of the question of frequently asked question number 1</p>
            </div>

            <div class="div">
                <h4><span class="mw-headline" id="id2">Frequently asked question 2?</span></h4>
                <p>anser of the question of frequently asked question number 2</p>
                <p>anser of the question of frequently asked question number 2.anser of the question of frequently asked question number 2.</p>
                <p>anser of the question of frequently asked question number 2.anser of the question of frequently asked question number 2.anser of the question of frequently asked question number 2.anser of the question of frequently asked question number 2</p>
            </div>
            <div class="div">
                <h4><span class="mw-headline" id="id5">Question category 2?</span></h4>
                <p>anser of the question of 1 of Question category 2 </p>
                <p>anser of the question of 1 of Question category 2 .anser of the question of 1 of Question category 2 .</p>
                <p>anser of the question of 1 of Question category 2 .anser of the question of 1 of Question category 2 .anser of the question of 1 of Question category 2 .anser of the question of 1 of Question category 2 .anser of the question of 1 of Question category 2 .</p>
            </div>
            <div class="div">
                <h4><span class="mw-headline" id="id6">Question category 2?</span></h4>
                <p>anser of the question of 2 of Question category 2 .</p>
                <p>anser of the question of 2 of Question category 2 .anser of the question of 2 of Question category 2 .</p>
                <p>anser of the question of 2 of Question category 2 .anser of the question of 2 of Question category 2 .anser of the question of 2 of Question category 2 .anser of the question of 2 of Question category 2 .</p>
            </div>
        </div>

    </div>
</asp:Content>

