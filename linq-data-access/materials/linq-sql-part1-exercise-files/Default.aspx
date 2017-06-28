<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView runat="server" ID="_grid" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Title" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:GridView runat="server" DataSource='<%# Eval("Reviews") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </form>
</body>
</html>
