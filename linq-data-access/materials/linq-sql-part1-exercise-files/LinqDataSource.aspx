<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinqDataSource.aspx.cs" Inherits="LinqDataSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="LinqDataSource1">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" 
                SortExpression="ReleaseDate" />
        </Columns>
    </asp:GridView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="MovieReviewsDataContext" TableName="Movies">
    </asp:LinqDataSource>
    </form>
</body>
</html>
