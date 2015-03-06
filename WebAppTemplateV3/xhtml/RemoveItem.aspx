<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveItem.aspx.cs" Inherits="WebAppTemplateV3.xhtml.RemoveItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>removes</title>
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
        }
    </script>
</head>
<body bgcolor="#F5F5F5">
    <form id="mainForm" runat="server">
    <div style="margin: 10px;">
        <div style="float: left; padding-left: 2px; width: 100%;">

            <div style="float: left; width: 50%; text-align: center;">
                <asp:Label ID="listboxtitle" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <asp:ListBox ID="content" runat="server" Height="400px" Width="250px" ></asp:ListBox>
            </div>
            <div style="float: left; width: 50%;">
            <div style="height: 390px;">
            </div>
            <div style = "text-align: center;">
                <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Button ID="Button1" runat="server" Text="remove"  OnClientClick="return ConfirmOnDelete();"
                    onclick="Button1_Click"  />
            </div>
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
