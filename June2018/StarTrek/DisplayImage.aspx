<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayImage.aspx.cs" Inherits="June2018.StarTrek.DisplayImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image AlternateText="" ID="img" Width="200" Height="200" runat="server" CssClass="img-thumbnail"/>
        <asp:Image AlternateText="" ID="Image1" Width="200" Height="200" runat="server" CssClass="img-thumbnail" ImageUrl="https://image.tmdb.org/t/p/w342/lbjakiOeVrnLiez92x62CGEFGBo.jpg"/>

    </form> 
</body>
</html>
