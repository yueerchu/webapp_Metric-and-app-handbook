<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true"
    CodeBehind="LauncherPage.aspx.cs" Inherits="DevTemplateV3.WebApp.UI.Pages.LauncherPage" %>

<asp:Content ID="ContentSection" ContentPlaceHolderID="PlaceHolderForContentSection"
    runat="server">
    <script type="text/javascript">
        function Navigate(url) {
            javascript: window.open(url, '_blank', 'location=0,menubar=0,status=0,resizable=1,scrollbars=1, top=120px, left = 200px, height = 660px, width = 1160px,');
        }
    

    </script>
    <div class="content_section">
        <div class="title_block">
            <asp:Image ID="TitleImage" runat="server" ImageUrl="../img/book.png" Style="vertical-align: bottom;
                padding-right: 5px;" />Search</div>
        <div class="padded_content_block">
            <div class="top_section">
                <div class="select_section">
                    <div class="filter_section">
                        <div class="filter">
                            Search Keywords:
                            <br />
                            <asp:TextBox ID="TextBox1" runat="server" Width="320px" Height="20px" OnTextChanged="RefreshButton_Click"
                                AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="filter">
                            Pillar
                            <br />
                            <asp:DropDownList ID="pillars" Width="150px" SelectionMode="Single" Rows="1" runat="server"
                                CssClass="filter_font" OnTextChanged="pillar_click" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="filter">
                            Applications
                            <br />
                            <asp:DropDownList ID="appss" Width="150px" SelectionMode="Single" Rows="1" runat="server"
                                OnTextChanged="app_click" CssClass="filter_font" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="filter">
                            Metrics
                            <br />
                            <asp:DropDownList ID="metrics" Width="150px" SelectionMode="Single" Rows="1" runat="server"
                                OnTextChanged="metric_click" CssClass="filter_font" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="upper_right">
                    <div class="search_button">
                        <br />
                        <asp:ImageButton ID="cmdRefresh" runat="server" OnClick="RefreshButton_Click" AlternateText="Search"
                            ToolTip="Click to Search" ImageUrl="../img/arrow_refresh.png" Width="32px" Height="32px" />
                        <br />
                        <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:Panel ID="resultsPanel" runat="server">
                <hr id="divLine" runat="server" visible="false" style="width: 90%; left: 5%" />
                <div class="result">
                    <div class="subtitle">
                        Application Results
                    </div>
                    <br />
                    <div class="ReportTable">
                        <asp:Table runat="server" ID="AppTable" CssClass="DataTable" />
                        <asp:Label ID="AppTableNoData" runat="server" Visible="false" CssClass="no_data_found" Text="No Data Found"></asp:Label>
                    </div>
                </div>
                <div class="result" style="padding-top: 0px;">
                    <div class="subtitle">
                        Metric Results
                    </div>
                    <br />
                    <div class="ReportTable">
                        <asp:Table runat="server" ID="MetricTable" CssClass="DataTable" />
                        <asp:Label ID="MetricTableNoData" runat="server" Visible="false" CssClass="no_data_found" Text="No Data Found"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
