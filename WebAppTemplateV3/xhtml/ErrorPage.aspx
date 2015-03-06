<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="DevTemplateV3.WebApp.UI.Pages.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderForToolbarSection" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderForContentSection" runat="server">
    <div class="content_section">
        <div class="title_block"><asp:Image runat="server" ID="ErrorMessageIcon" ImageUrl="../img/error.png" style="vertical-align: bottom;" /> Error</div>
        <div class="padded_content_block">
            <div style="padding:10px;">
                <asp:Label runat="server" ID="ErrorMessageLabel" Font-Bold="true" Font-Size="Large" />
            </div>
            <br />
            <br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="width: 32px;">
                        <asp:ImageButton runat="server" ID="BackButton" ImageUrl="../img/arrow_left.png"  OnClick="BackButton_Click"/>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
