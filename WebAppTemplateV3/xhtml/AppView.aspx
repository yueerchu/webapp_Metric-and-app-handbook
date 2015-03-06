<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppView.aspx.cs" Inherits="WebAppTemplateV3.xhtml.AppView" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="NavigationBarControl.ascx" TagName="NavigationBarControl" TagPrefix="uc1" %>
<%@ Register Src="BroadcastMessageControl.ascx" TagName="BroadcastMessageControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="CoeHeader" Namespace="PMCOE.Common.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=8" />
<head>
    <title>Web App Template</title>
    <link rel="stylesheet" type="text/css" href="../css/core.css" />
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" />
    <script language="javascript" type="text/javascript" src="../jscript/jquery-1.8.3.min.js"></script>
    <script language="javascript" type="text/javascript" src="../jscript/common.js"></script>
</head>
<body onload="scrollWin()">
    <form id="mainForm" runat="server" defaultbutton="UselessDefaultButton">
    <script type="text/javascript">
        function GoB() {
            javascript: history.go(-1);
        }

        function scrollWin() {
            window.scrollTo(0, 0);
        }

    </script>
    <div id="outline" style="width: auto; border: 0px;">
        <div id="middle" style="background-color: white;">
            <%/* --- REQUIRED FOR AJAX CONTROLS --- */%>
            <uc1:ToolkitScriptManager ID="ScriptManagerForPopup" runat="server" />
            <asp:HiddenField ID="hdnField" runat="server" />
            <div id="content" style="width: auto; padding: 0px;">
                <asp:Panel ID="LoadingPanel" runat="server" CssClass="Loading_Hidden">
                    <asp:Image runat="server" ID="LoadingIcon" ImageUrl="../img/loading_timer.gif" />
                </asp:Panel>
                <div class="content_section" style="width: auto;">
                    <div class="title_block">
                        <asp:Image ID="TitleImage" runat="server"  Style="vertical-align: bottom;
                            padding-right: 5px;" /><asp:Label ID="Title" runat="server" Text="Label"></asp:Label></div>
                    <div class="padded_content_block">
                        <div class="top_section" style="width: 100%;">
                            <div class="select_section" style="width: 100%;">
                                <div class="titlebutton">
                                    <div class="subtitle" style="text-align: left; margin-left: 3%;  height: 100%;
                                        font-size: 29px; width: 40%; float: left; line-height: 70px;">
                                        <asp:Label ID="Label1" runat="server" Text="App Title"></asp:Label>
                                    </div>
                                    <div class="toolbarbuttons">
                                        <br />
                                        <div class="functionbutton">
                                            <asp:ImageButton ID="supportcenter" runat="server" ToolTip="Print" ImageUrl="../img/help.png"
                                                OnClick="cmdSupport_Click" Width="36px" Height="36px" ImageAlign="AbsMiddle" />
                                        </div>
                                        <div class="functionbutton">
                                            <asp:ImageButton ID="cmdExcel" runat="server" ToolTip="Export To Excel" ImageUrl="../img/page_white_excel.png"
                                                OnClick="cmdExcel_Click" Width="36px" Height="36px" ImageAlign="AbsMiddle" />
                                        </div>
                                        <div class="functionbutton">
                                            <asp:ImageButton ID="cmbAdobe" runat="server" ToolTip="Export To Adobe" ImageUrl="../img/page_white_acrobat.png"
                                                OnClick="cmdAdobe_Click" Width="36px" Height="36px" ImageAlign="AbsMiddle" />
                                        </div>
                                        <div class="functionbutton">
                                            <asp:ImageButton ID="cmdPrint" runat="server" ToolTip="Print" ImageUrl="../img/printer.png"
                                                Width="36px" Height="36px" ImageAlign="AbsMiddle" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <div class="app_Details">
                                    <div class="section_title" style="text-align: left;">
                                        Purpose
                                    </div>
                                    <div class="section_content">
                                        <asp:Label ID="purpose" runat="server" Text="The purpose of this app"></asp:Label>
                                    </div>
                                    <div class="section_title">
                                        Supporting Files
                                    </div>
                                    <div class="section_content" style = "height:110px; overflow: auto;">
                                        <asp:Label ID="MSG" runat="server" Text="No supporting files for this app"></asp:Label>
                                        <asp:Table ID="supportfiles" runat="server" CssClass="sfiles">
                                        </asp:Table>
                                    </div>
                                    <div class="section_title">
                                        Metrics
                                    </div>
                                    <br />
                                    <div class="section_content" style="padding: 0px; width: 100%;">
                                        <div class="ReportTable" style="margin: 0px; width: 100%;" >
                                            <asp:Table runat="server" ID="MetricTable" CssClass="DataTable" />
                                        </div>
                                    </div>
                                </div>
                                <div class="back_button" style="margin-left:10%;">
                                        <asp:ImageButton ID="gobacknow" runat="server" ImageUrl="../img/arrow_left.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button runat="server" ID="UselessDefaultButton" Visible="true" Width="0" Height="0"
        BorderWidth="0" BackColor="White" ForeColor="White" OnClientClick="return false;" />
    </form>
</body>
</html>
