<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppAdmin.aspx.cs" Inherits="WebAppTemplateV3.xhtml.AppAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>appadmin</title>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
        }


    </script>
    <style type="text/css">
        #File1
        {
            width: 220px;
        }
    </style>
</head>
<body bgcolor="#F5F5F5">
    <form id="mainForm" runat="server">
    <asp:Panel ID="mainpanel" runat="server" Font-Names="Calibri, Sans-Serif">
        <div style="margin: 10px;">
            <div style="float: left; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 20%;">
                    <b>Application ID:</b>
                    <br />
                    <asp:TextBox ID="appid" runat="server" Width="125px" ReadOnly="True" BackColor="#999999"></asp:TextBox>
                </div>
                <div style="float: left; width: 50%;">
                    <b>Application Name:</b>
                    <br />
                    <asp:TextBox ID="appname" runat="server" Width="400px"></asp:TextBox>
                </div>
                <div style="float: left; width: 30%;">
                    <b>Key Word:</b>
                    <br />
                    <asp:TextBox ID="keyword" runat="server" Width="261px"></asp:TextBox>
                </div>
            </div>
            <br />
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 45%;">
                    <b>Description:</b>
                    <br />
                    <asp:TextBox ID="desc" runat="server" Width="400px" Height="120px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div style="float: right; width: 45%;">
                    <b>MetaData:</b>
                    <br />
                    <asp:TextBox ID="metadata" runat="server" Width="390px" Height="120px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%">
                <div style="float: left; width: 33%;">
                    <b>Pillar:</b>
                    <br />
                    <asp:DropDownList ID="pillar" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 45%;">
                    <b>Process:</b>
                    <br />
                    <asp:DropDownList ID="process" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 21%;">
                    <b>Classification:</b>
                    <br />
                    <asp:DropDownList ID="classification" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%">
                <div style="float: left; width: 33%;">
                    <b>Frequency:</b>
                    <br />
                    <asp:DropDownList ID="frequency" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 45%;">
                    <b>Audience:</b>
                    <br />
                    <asp:DropDownList ID="audience" runat="server">
                    </asp:DropDownList>
                </div>
                <div style="float: left; width: 21%;">
                    <b>Format:</b>
                    <br />
                    <asp:DropDownList ID="format" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="float: left; padding-top: 20px; padding-left: 2px; width: 100%">
                <div style="float: left; width: 33%;">
                    <b>Business Owner ID:</b>
                    <br />
                    <asp:TextBox ID="businessid" runat="server"></asp:TextBox>
                </div>
                <div style="float: left; width: 33%;">
                    <b>Owner User ID:</b>
                    <br />
                    <asp:TextBox ID="ownerid" runat="server"></asp:TextBox>
                </div>
                <div style="float: left; width: 33%;">
                    <b>Support Owner ID:</b>
                    <br />
                    <asp:TextBox ID="supportid" runat="server"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 50%;">
                    <b>Application URL:</b>
                    <br />
                    <asp:TextBox ID="appurl" runat="server" Width="400px"></asp:TextBox>
                </div>
                <div style="float: left; width: 50%;">
                    <b>Image URL:</b>
                    <br />
                    <asp:TextBox ID="imageurl" runat="server" Width="430px"></asp:TextBox>
                </div>
            </div>
            <asp:Panel ID="nextstep" runat="server">
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 30%;">
                    <b>Avaliable Metric:</b>
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
                    <b>Related Metric for this App:</b>
                    <br />
                    <asp:ListBox ID="related" runat="server" Height="270px" Width="255px"></asp:ListBox>
                </div>
            </div>
            <div style="float: left; padding-top: 15px; padding-left: 2px; width: 100%;">
                <div style="float: left; width: 40%;">
                    <b>All Supporting Files:</b>
                    <br />
                    <asp:ListBox ID="files" runat="server" Height="270px" Width="255px"></asp:ListBox>
                </div>
                <div style="float: left; width: 60%;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                    <div style="text-align: center; width: 100%;">
                        <b>Upload a File: </b> <input type="file" id="File1" name="File1" runat="server" />
                        <asp:Button ID="Submit1" runat="server" Text="Upload" OnClick="Submit1_Click" OnClientClick="return ConfirmOnDelete();" />
                        <br />
                        <asp:Label ID="uploadmsg" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br /> 
                        <asp:Button ID="removefile" runat="server" Text="Remove" OnClick="removefilebutton_Click"
                            OnClientClick="return ConfirmOnDelete();" />
                    </div>
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
