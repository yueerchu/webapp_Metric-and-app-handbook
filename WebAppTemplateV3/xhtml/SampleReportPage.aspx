<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="SampleReportPage.aspx.cs" Inherits="DevTemplateV3.WebApp.UI.Pages.SampleReportPage" %>
<%@ Register Src="SampleInputsControl.ascx" TagName="InputSelection" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>


<asp:Content ID="Content" ContentPlaceHolderID="PlaceHolderForContentSection" runat="server">
  <uc1:CollapsiblePanelExtender runat="server"
            ID="CollapsiblePanelExtender" 
            TargetControlID="ExpandedToolbarPanel"
            Collapsed="false"
            CollapsedSize="0"
            ExpandControlID="CollapsedToolbarPanel"                            
            CollapseControlID="CollapsedToolbarPanel"
            ExpandDirection="Vertical"
            ImageControlID="ExpandImage"
            ExpandedImage="../img/barmin.png"
            CollapsedImage="../img/barmax.png"
    />
    <div class="title_block"><asp:Image ID="TitleImage" runat="server" ImageUrl="../img/cocacola.png" style="vertical-align: bottom;" /> Sample Report</div>
<div id="toolbar_top">
        <asp:Panel ID="CollapsedToolbarPanel" runat="server" CssClass="Collapsible_Header" >
            <asp:Image ImageUrl="../img/barmin.png" ID="ExpandImage" runat="server" />
        </asp:Panel>
    </div>

	<asp:Panel runat="server" ID="ExpandedToolbarPanel"  >
        <div id="toolbar">

            <%/* TOOLBAR GOES HERE */%>

            <uc1:InputSelection ID="InputChoice" runat="server" />

            <div class="toolbar_element" style="width: 550px;">
                <br />
            </div>

            <div class="toolbar_element">
	            <br />
	            <asp:ImageButton ID="cmdRefresh" runat="server" ToolTip="Refresh Report" OnClick="cmdRefresh_Click" OnClientClick="Common_WaitingTimer();" ImageUrl="../img/FreeStyle_Refresh.png" Width="46px" Height="46px" ImageAlign="AbsMiddle" />
	            <br />
	            <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
	        </div>
        </div>
        <div id="toolbar_bottom"></div>
    </asp:Panel>
    <div class="content_section">
        
        <div class="padded_content_block">
            
            <asp:Panel runat="server" ID="SpacerPanel" Width="1170" Height="300"></asp:Panel>

            <asp:Panel runat="server" ID="ReportPanel">
                I AM A SAMPLE REPORT
            </asp:Panel>

        </div>
    </div>
</asp:Content>
