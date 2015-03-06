<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBarControl.ascx.cs" Inherits="DevTemplateV3.WebApp.UI.Controls.NavigationBarControl" %>

<asp:Panel CssClass="navigation_inactive" runat="server" ID="HomeLinkPanel">
    <asp:LinkButton ID="HomeLink" runat="server" onclick="HomeLink_Click">Search</asp:LinkButton>
</asp:Panel>

<asp:Panel CssClass="navigation_inactive" runat="server" ID="AdminPanel">
    <asp:LinkButton ID="SampleReportLink" runat="server" onclick="AdminLink_Click">Admin</asp:LinkButton>
</asp:Panel>
