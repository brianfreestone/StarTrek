<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StarTrekDefault.aspx.cs" Inherits="June2018.StarTrek.StarTrekDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootswatch/superhero/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="Label8" runat="server" Text="Watch Next:"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:Table ID="Table1" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label1" runat="server" Text="Original Air Date:"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblOrigAirDate" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label2" runat="server" Text="Type:"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblPrimaryType" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblPrimaryTitle" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="lblSecondaryType" runat="server" Text=":"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblSecondaryTitle" runat="server" Text=""></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                        <asp:Button ID="btnWatched" runat="server" Text="Mark as Watched" CssClass="btn btn-info" OnClick="btnWatched_Click" />

                    </asp:Panel>
                    <asp:Image ID="imgMain" runat="server" />

                </div>
                <div class="col-md-8">
                    <asp:Button ID="btnShowTV" runat="server" Text="Television" OnClick="btnShowTV_Click" />
                    <asp:Button ID="btnShowMovies" runat="server" Text="Movies" OnClick="btnShowMovies_Click" />

                    <asp:MultiView ID="MultiViewMain" runat="server" ActiveViewIndex="0">
                        <asp:View ID="viewTV" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:ListBox ID="lbSeries" runat="server" Height="125px" Width="234px" AutoPostBack="True" OnTextChanged="lbSeries_TextChanged" Font-Names="Tw Cen MT" OnSelectedIndexChanged="lbSeries_SelectedIndexChanged"></asp:ListBox>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT StarTrekProductions.Title, StarTrekProductions.Episode, CONVERT (VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, StarTrekUserData.DateWatched, StarTrekProductions.Season FROM StarTrekProductions LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID = @UserID) OR (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID IS NULL) AND (StarTrekUserData.DateWatched IS NULL)">
                                        <SelectParameters>
                                            <asp:Parameter Name="seriesID" />
                                            <asp:Parameter Name="userID" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div class="col-md-10">
                                    <asp:GridView ID="gvSeriesInfo" runat="server" CssClass="table table-striped table-hover" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                            <asp:BoundField DataField="Season" HeaderText="Season" SortExpression="Season" />
                                            <asp:BoundField DataField="Episode" HeaderText="Episode" SortExpression="Episode" />
                                            <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Ai rDate" ReadOnly="True" SortExpression="OriginalAirDate" />
                                            <asp:BoundField DataField="DateWatched" HeaderText="Date Watched" SortExpression="DateWatched" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EntityDataSourceSeriesInfo" runat="server">
                                    </asp:EntityDataSource>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="viewFilms" runat="server">
                            <asp:GridView ID="gvFilms" runat="server" CssClass="table table-striped table-hover" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Air Date" ReadOnly="True" SortExpression="OriginalAirDate" />
                                    <asp:BoundField DataField="DateWatched" HeaderText="Date Watched" SortExpression="DateWatched" />
                                    <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" Visible="False" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT StarTrekProductions.Title, CONVERT (VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, StarTrekUserData.DateWatched, StarTrekUserData.UserID FROM StarTrekProductions LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekProductions.ProductionTypeID = 2) AND (StarTrekUserData.UserID = @UserID OR StarTrekUserData.UserID IS NULL)">
                                <SelectParameters>
                                    <asp:Parameter Name="UserID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>

        </div>
    </form>
</body>
</html>
