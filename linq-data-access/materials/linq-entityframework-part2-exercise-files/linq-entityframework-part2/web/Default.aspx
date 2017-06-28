<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Title: <asp:TextBox runat="server" ID="_titleInput"></asp:TextBox>
        <br />
        ReleaseDate: <asp:TextBox runat="server" ID="_releaseDateInput"></asp:TextBox>
        <br />
        <asp:Button runat="server" Text="Submit" ID="_post" onclick="_post_Click" />
    </div>
    </form>
</body>
</html>
