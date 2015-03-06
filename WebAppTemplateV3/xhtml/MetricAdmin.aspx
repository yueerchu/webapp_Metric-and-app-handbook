<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetricAdmin.aspx.cs" Inherits="WebAppTemplateV3.xhtml.MetricAdmin" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>metricadmin</title>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
        }

        function File1_onclick() {

        }

    </script>
    <style type="text/css">
        #File1
        {
            width: 292px;
        }
    </style>
</head>

<body bgcolor="#F5F5F5">
    <form id="mainForm" runat="server">
    <asp:Panel ID="mainpanel" runat="server" Font-Names="Calibri, Sans-Serif">
        <div style="margin: 10px;">
            <div style="float: left; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 20%;">
                    <b>Metric ID:</b>
                    <br />
                    <asp:TextBox ID="metricid" runat="server" Width="125px" ReadOnly="True" BackColor="#999999"></asp:TextBox>
                </div>
                <div style="float: left; width: 50%;">
                    <b>Metric Name:</b>
                    <br />
                    <asp:TextBox ID="metricname" runat="server" Width="400px"></asp:TextBox>
                </div>
                <div style="float: left; width: 30%;">
                    <b>Data Source:</b>
                    <br />
                    <asp:TextBox ID="datas" runat="server" Width="261px"></asp:TextBox>
                </div>
            </div>
            <br />
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 45%;">
                    <b>Definition:</b>
                    <br />
                    <asp:TextBox ID="desc" runat="server" Width="400px" Height="120px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div style="float: right; width: 45%;">
                    <b>Objective:</b>
                    <br />
                    <asp:TextBox ID="objective" runat="server" Width="390px" Height="120px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%">
                <div style="float: left; width: 25%;">
                    <b>Frequency:</b>
                    <br />
                    <asp:DropDownList ID="frequency" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 25%;">
                    <b>Metric Category:</b>
                    <br />
                    <asp:DropDownList ID="category" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 25%;">
                    <b>Business Owner ID:</b>
                    <br />
                    <asp:TextBox ID="businessid" runat="server"></asp:TextBox>
                </div>
                <div style="float: left; width: 25%;">
                    <b>Metric Owner ID:</b>
                    <br />
                    <asp:TextBox ID="ownerid" runat="server"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 20px; padding-left: 2px; width: 100%">
                <div style="float: left; width: 33%;">
                    <b>Industry Average(Benchmark):</b>
                    <br />
                    <asp:TextBox ID="indave" runat="server"></asp:TextBox>
                </div>
                <div style="float: left; width: 33%;">
                    <b>World Class(Benchmark):</b>
                    <br />
                    <asp:TextBox ID="worldc" runat="server"></asp:TextBox>
                </div>
                <div style="float: left; width: 33%;">
                    <b>CCR(Benchmark):</b>
                    <br />
                    <asp:TextBox ID="ccr" runat="server"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 45%;">
                    <b>MetaData:</b>
                    <br />
                    <asp:TextBox ID="metadatas" runat="server" Width="400px" Height="90px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div style="float: right; width: 45%;">
                    <b>Comments:</b>
                    <br />
                    <asp:TextBox ID="comment" runat="server" Width="390px" Height="90px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 90%;">
                    <b>Example:</b>
                    <br />
                    <asp:TextBox ID="example" runat="server" Width="871px" Height="120px" TextMode="MultiLine"></asp:TextBox>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
                    <asp:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="example" runat="server" OnImageUploadComplete="HtmlEditorExtender1_ImageUploadComplete">
                        <Toolbar>
                            <ajaxToolkit:Bold />
                            <ajaxToolkit:Italic />
                            <ajaxToolkit:Underline />
                            <ajaxToolkit:BackgroundColorSelector />
                            <ajaxToolkit:ForeColorSelector />
                            <ajaxToolkit:FontSizeSelector />
                            <ajaxToolkit:FontNameSelector />
                            <ajaxToolkit:RemoveFormat />
                            <ajaxToolkit:Subscript />
                            <ajaxToolkit:Superscript />
                            <ajaxToolkit:CreateLink />
                            <ajaxToolkit:UnLink />
                            <ajaxToolkit:InsertImage  />
                        </Toolbar>
                    </asp:HtmlEditorExtender>
                </div>
            </div>
            <asp:Panel ID="nextstep" runat="server">
                <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                    <div style="float: left; width: 30%;">
                        <b>Avaliable Application:</b>
                        <br />
                        <asp:ListBox ID="allmetric" runat="server" Height="270px" Width="255px"></asp:ListBox>
                    </div>
                    <div style="float: left; width: 40%;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div style="text-align: center; width: 100%;">
                            <asp:Button ID="addbutton" runat="server" Text="Add -->" OnClick="addbutton_Click"
                                OnClientClick="return ConfirmOnDelete();" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="removebutton" runat="server" Text="<-- Remove" OnClick="removebutton_Click"
                                OnClientClick="return ConfirmOnDelete();" />
                        </div>
                    </div>
                    <div style="float: left; width: 30%;">
                        <b>Related Application for this Metric:</b>
                        <br />
                        <asp:ListBox ID="related" runat="server" Height="270px" Width="255px"></asp:ListBox>
                    </div>
                </div>
                <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                    <div style="float: left; width: 30%;">
                        <b>Avaliable Metrics:</b>
                        <br />
                        <asp:ListBox ID="allmetric2" runat="server" Height="270px" Width="255px"></asp:ListBox>
                    </div>
                    <div style="float: left; width: 40%;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div style="text-align: center; width: 100%;">
                            <asp:Button ID="Button1" runat="server" Text="Add -->" OnClick="addbutton_Click2"
                                OnClientClick="return ConfirmOnDelete();" />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="Button2" runat="server" Text="<-- Remove" OnClick="removebutton_Click2"
                                OnClientClick="return ConfirmOnDelete();" />
                        </div>
                    </div>
                    <div style="float: left; width: 30%;">
                        <b>Related Metrics for this Metric:</b>
                        <br />
                        <asp:ListBox ID="related2" runat="server" Height="270px" Width="255px"></asp:ListBox>
                    </div>
                </div>
                <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                    <div style="float: left; width: 40%;">
                        <strong>Formula</strong><b>:</b>&nbsp;&nbsp;&nbsp;
                        <asp:Image ID="formula" runat="server" />
                        <br />
                        <br />
                        <div style="text-align: left; width: 100%;">
                            <b>Upload a New Formula: </b>
                            <input type="file" id="File1" name="File1" runat="server" onclick="return File1_onclick()" />
                            <br />
                            &nbsp;<br />
                            <asp:Button ID="Submit1" runat="server" OnClick="Submit1_Click" OnClientClick="return ConfirmOnDelete();"
                                Text="Upload" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="removefile" runat="server" OnClick="removefilebutton_Click" OnClientClick="return ConfirmOnDelete();"
                                Text="Remove" style="height: 26px" />
                            <br />
                            <asp:Label ID="uploadmsg" runat="server" Text="Label"></asp:Label>
                            <br />
                            <br />
                        </div>
                        <br />
                    </div>
                    <br />
                    <div style="float: left; width: 60%;">
                        <b>Formula Help Text:</b>
                        <br />
                        <asp:TextBox ID="formulahelp" runat="server" Width="510px" Height="90px" TextMode="MultiLine"></asp:TextBox>
                        
                        <asp:HtmlEditorExtender ID="HtmlEditorExtender2" TargetControlID="formulahelp" runat="server">
                        <Toolbar>
                            <ajaxToolkit:Bold />
                            <ajaxToolkit:Italic />
                            <ajaxToolkit:Underline />
                            <ajaxToolkit:BackgroundColorSelector />
                            <ajaxToolkit:ForeColorSelector />
                            <ajaxToolkit:FontSizeSelector />
                            <ajaxToolkit:FontNameSelector />
                            <ajaxToolkit:RemoveFormat />
                            <ajaxToolkit:Subscript />
                            <ajaxToolkit:Superscript />
                            <ajaxToolkit:CreateLink />
                            <ajaxToolkit:UnLink />
                        </Toolbar>
                    </asp:HtmlEditorExtender>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%; text-align: center;">
                <br />
                <asp:Button ID="nextb" runat="server" Text="Next" OnClick="nextb_Click" OnClientClick="return ConfirmOnDelete();" />&nbsp&nbsp&nbsp
                <asp:Button ID="submitb" runat="server" Text="Submit" OnClick="submitb_Click" OnClientClick="return ConfirmOnDelete();" />&nbsp&nbsp&nbsp
                <asp:Button ID="cancelb" runat="server" Text="Cancel" OnClick="cancelb_Click" OnClientClick="return ConfirmOnDelete();" />
                <br />
                <br />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="thankyou" runat="server">
        The change has been applied.
        <div class="back_button">
            <asp:HyperLink ID="goback" runat="server" ImageUrl="../img/arrow_left.png">go back</asp:HyperLink>
        </div>
        <asp:HyperLink ID="newview" runat="server">View the Page</asp:HyperLink>
    </asp:Panel>
    </form>
</body>
</html>
