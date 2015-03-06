<%@ Page Title="" Language="C#" MasterPageFile="~/xhtml/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="AdminPage.aspx.cs" Inherits="WebAppTemplateV3.xhtml.AdminPage" %>

<asp:Content ID="ContentSection" ContentPlaceHolderID="PlaceHolderForContentSection"
    runat="server">
    <div class="title_block">
        <asp:Image ID="TitleImage" runat="server" ImageUrl="../img/cocacola.png" Style="vertical-align: bottom;" />
        Admin
    </div>
    <div id="toolbar">
        <div class="toolbar_b">
            <div class="toolbar_img">
                <asp:HyperLink ID="aag" runat="server" ImageUrl="../img/add.png" NavigateUrl = "AppAdmin.aspx" Target="operation" CssClass = "button_hyperlinks"/>
            </div>
            <asp:HyperLink ID="aat" runat="server" NavigateUrl = "AppAdmin.aspx" Target="operation" CssClass = "name_hyperlinks" Text="Add Application" Font-Size="Medium" />
        </div>
        <div class="toolbar_b">
            <div class="toolbar_img">
                <asp:HyperLink ID="ami" runat="server" ImageUrl="../img/add.png" NavigateUrl = "MetricAdmin.aspx" Target="operation" CssClass = "button_hyperlinks"/>
            </div>
            <asp:HyperLink ID="amt" runat="server" NavigateUrl = "MetricAdmin.aspx" Target="operation" Font-Size="Medium" CssClass="name_hyperlinks">Add Metric</asp:HyperLink>
        </div>
        <div class="toolbar_b">
            <div class="toolbar_img">
                <asp:HyperLink ID="dmi" runat="server" ImageUrl="../img/delete.png" NavigateUrl = "RemoveItem.aspx?page=metrics" Target="operation" CssClass = "button_hyperlinks"/>
            </div>
            <asp:HyperLink ID="dmt" runat="server" NavigateUrl = "RemoveItem.aspx?page=metrics" Target="operation" Font-Size="Medium" CssClass="name_hyperlinks">Remove Metric</asp:HyperLink>
        </div>
    </div>
    <div class="content_section">
        <div class="padded_content_block" style="padding: 0px;">
            <div class="admin_left">
                <asp:Panel runat="server" ID="treeviews" Font-Size="Medium" Font-Names="Calibri, Sans-Serif" style="padding:7px;">
                    <asp:TreeView ID="TreeView1" runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate">
                        <Nodes>
                            <asp:TreeNode PopulateOnDemand="True" Text="Applications" Value="Application" Expanded="False"
                                SelectAction="Expand"></asp:TreeNode>
                        </Nodes>
                    </asp:TreeView>
                    <asp:TreeView ID="TreeView2" runat="server" OnTreeNodePopulate="TreeView2_TreeNodePopulate">
                        <Nodes>
                            <asp:TreeNode PopulateOnDemand="True" Text="Metrics" Value="Metrics" SelectAction="Expand">
                            </asp:TreeNode>
                        </Nodes>
                    </asp:TreeView>
                </asp:Panel>
            </div>
            <div class="admin_right">
                <iframe height="100%" width="100%" runat="server" name="operation"></iframe>
            </div>
        </div>
    </div>
</asp:Content>
