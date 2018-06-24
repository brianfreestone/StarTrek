<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StarTrekDefault.aspx.cs" Inherits="June2018.StarTrek.StarTrekDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootswatch/superhero/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="Label8" runat="server" Text="Watch Next:"></asp:Label>
                    <div style="border: solid 1px black; padding: 10px 5px; border-radius: 25px; background-color: antiquewhite;">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Table ID="Table1" runat="server">

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label2" runat="server" Text="Type:" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label1" runat="server" Text="Original Air Date:" Font-Size="Medium" ForeColor="Black"> </asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblOrigAirDate" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPrimaryType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPrimaryTitle" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblSecondaryType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblSecondaryTitle" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                    </div>
                    <br />

                    <asp:Label ID="lblDescription" runat="server" Text="" Font-Size="Medium" CssClass="gl"></asp:Label>
                    <br />
                    <div class="row">
                        <asp:Image ID="imgMain" runat="server" CssClass="img-thumbnail" />
                    </div>
                    <br />
                    <div>
                        <asp:Button ID="btnWatched" runat="server" Text="Mark as Watched" CssClass="btn btn-info" OnClick="btnWatched_Click" />
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <asp:Button ID="btnShowTV" runat="server" Text="Television" OnClick="btnShowTV_Click" />
                        <asp:Button ID="btnShowMovies" runat="server" Text="Movies" OnClick="btnShowMovies_Click" />
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiViewMain" runat="server" ActiveViewIndex="0">
                            <asp:View ID="viewTV" runat="server">
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:ListBox ID="lbSeries" runat="server" Height="125px" Width="234px" AutoPostBack="True" OnTextChanged="lbSeries_TextChanged" Font-Names="Tw Cen MT" OnSelectedIndexChanged="lbSeries_SelectedIndexChanged"></asp:ListBox>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT StarTrekProductions.Title, StarTrekProductions.Episode, CONVERT (VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, StarTrekUserData.DateWatched, StarTrekProductions.Season FROM StarTrekProductions LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID = @UserID) OR (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID IS NULL) AND (StarTrekUserData.DateWatched IS NULL)">
                                            <SelectParameters>
                                                <asp:Parameter Name="seriesID" />
                                                <asp:Parameter Name="userID" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:GridView ID="gvSeriesInfo" runat="server"  ShowHeaderWhenEmpty="True"
                                            CellPadding="4" ForeColor="Black" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px">
                                            <Columns>
                                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                <asp:BoundField DataField="Season" HeaderText="Season" SortExpression="Season" />
                                                <asp:BoundField DataField="Episode" HeaderText="Episode" SortExpression="Episode" />
                                                <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Air Date" ReadOnly="True" SortExpression="OriginalAirDate" />
                                                <asp:BoundField DataField="DateWatched" HeaderText="Date Watched" SortExpression="DateWatched" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View ID="viewFilms" runat="server">
                                <asp:GridView ID="gvFilms" runat="server"  ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                    <Columns>
                                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Air Date" ReadOnly="True" SortExpression="OriginalAirDate" />
                                        <asp:BoundField DataField="DateWatched" HeaderText="Date Watched" SortExpression="DateWatched" />
                                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" Visible="False" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT StarTrekProductions.Title, CONVERT (VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, StarTrekUserData.DateWatched, StarTrekUserData.UserID FROM StarTrekProductions LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekProductions.ProductionTypeID = 2) AND (StarTrekUserData.UserID = @UserID OR StarTrekUserData.UserID IS NULL)">
                                    <SelectParameters>
                                        <asp:Parameter Name="UserID" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString='<%$ ConnectionStrings:dboMasterConnectionString %>' SelectCommand="SELECT StarTrekProductions.Season, COUNT(StarTrekProductions.Episode) AS Count, StarTrekProductions.SeriesID FROM StarTrekProductions INNER JOIN StarTrekSeriesNames ON StarTrekProductions.SeriesID = StarTrekSeriesNames.ID INNER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekUserData.UserID = 1) GROUP BY StarTrekProductions.Episode, StarTrekProductions.Season, StarTrekProductions.SeriesID"></asp:SqlDataSource>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
