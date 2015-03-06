<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleInputsControl.ascx.cs" Inherits="DevTemplateV3.WebApp.UI.Controls.SampleInputsControl" %>

<div class="toolbar_element" style="width: 180px;">
    Year
    <br />
    <asp:ListBox ID="cmbYear" Width="160px" SelectionMode="Single" Rows="1" runat="server" AutoPostBack="false"></asp:ListBox>
</div>

<div class="toolbar_element" style="width: 180px;">
    Month
    <br />
    <asp:ListBox ID="cmbMonth" Width="160px" SelectionMode="Multiple" Rows="5" runat="server" AutoPostBack="false"></asp:ListBox>
</div>
