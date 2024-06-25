<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StarTrekDefault.aspx.cs" Inherits="June2018.StarTrek.StarTrekDefault" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootswatch/superhero/bootstrap.css" rel="stylesheet" />
    <link href="../Content/CSS/style.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
     <style type="text/css">
        .fadingTooltip
        {
            border-right: darkgray 1px outset;
            border-top: darkgray 1px outset;
            font-size: 12pt;
            border-left: darkgray 1px outset;
            width: auto;
            color: black;
            border-bottom: darkgray 1px outset;
            height: auto;
            background-color: lemonchiffon;
            margin: 3px 3px 3px 3px;
            padding: 3px 3px 3px 3px;
            borderbottomwidth: 3px 3px 3px 3px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            height: 22px;
        }
        
        
    </style>
    <script type="text/javascript">
        var fadingTooltip;
        var wnd_height, wnd_width;
        var tooltip_height, tooltip_width;
        var tooltip_shown = false;
        var transparency = 100;
        var timer_id = 1;
        var tooltiptext;

        // override events
        window.onload = WindowLoading;
        window.onresize = UpdateWindowSize;
        document.onmousemove = AdjustToolTipPosition;

        function DisplayImageToolTip(tooltip_text) {
            console.log(tooltip_text)
            fadingTooltip.innerHTML = tooltip_text;
            tooltip_shown = (tooltip_text != "") ? true : false;
            if (tooltip_text != "") {
                // Get tooltip window height
                tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;
                transparency = 0;
                ToolTipFading();
            }
            else {
                clearTimeout(timer_id);
                fadingTooltip.style.visibility = "hidden";
            }
        }

        function AdjustToolTipPosition(e) {
            if (tooltip_shown) {
                // Depending on IE/Firefox, find out what object to use to find mouse position
                var ev;
                if (e)
                    ev = e;
                else
                    ev = event;

                fadingTooltip.style.visibility = "visible";
                offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? -15 - tooltip_height : 20;
                fadingTooltip.style.left = Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
                fadingTooltip.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
            }
        }

        function WindowLoading() {
            fadingTooltip = document.getElementById('fadingTooltip');

            // Get tooltip  window width				
            tooltip_width = (fadingTooltip.style.pixelWidth) ? fadingTooltip.style.pixelWidth : fadingTooltip.offsetWidth;

            // Get tooltip window height
            tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;

            UpdateWindowSize();
        }

        function ToolTipFading() {
            if (transparency <= 100) {
                fadingTooltip.style.filter = "alpha(opacity=" + transparency + ")";
                fadingTooltip.style.opacity = transparency / 100;
                transparency += 5;
                timer_id = setTimeout('ToolTipFading()', 35);
            }
        }

        function UpdateWindowSize() {
            wnd_height = document.body.clientHeight;
            wnd_width = document.body.clientWidth;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
        position: absolute">

                <image runat="server" id="picture" />
    </div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <a href="../../Default.aspx">Back to brianfreestone.com</a>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label8" runat="server" Text="Watch Next:"></asp:Label>
                            <div style="border: solid 1px black; padding: 10px 5px; border-radius: 25px; background-color: antiquewhite;">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Table ID="Table1" runat="server" CssClass="table table-condensed">

                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label2" runat="server" Text="Type:" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <%--            <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label1" runat="server" Text="Original Air Date:" Font-Size="Medium" ForeColor="Black"> </asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblOrigAirDate" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>--%>
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
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblTertiaryType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblTertiaryTitle" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblQuaternaryType" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblQuaternaryTitle" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>

                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl5Type" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl5Title" runat="server" Text="" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                            </asp:TableCell>

                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:Panel>

                            </div>
                            <br />

                            <asp:Label ID="lblDescription" runat="server" Text="" Font-Size="Medium" CssClass="gl"></asp:Label>
                            <br />
                            <br />
                            <div class="row">
                                <asp:Image ID="imgMain" runat="server" CssClass="img-thumbnail " />
                            </div>
                            <br />
                            <div>
                                <asp:Button ID="btnWatched" runat="server" Text="Mark as Watched" CssClass="btn btn-info" OnClick="btnWatched_Click" />
                            </div>
                            <br />
                            <asp:Panel ID="detailsPanel" runat="server" Visible="false">
                                <asp:Table ID="Table3" runat="server" CssClass="table table-condensed">

                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label1" runat="server" Text="Series"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblSelectedSeries" runat="server" Text="Series"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label3" runat="server" Text="Season"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblSelectedSeason" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label4" runat="server" Text="Episode"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblSelectedEpisode" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label5" runat="server" Text="Title"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblSelectedTitle" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label9" runat="server" Text="Title"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="lblSelectedAirDate" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label6" runat="server" Text="Synopsis"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox  ID="txtSelectedSynopsis" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    
                                </asp:Table>
                                            <asp:Label ID="Label7" runat="server" Text="Crew"></asp:Label>
                                            <asp:GridView ID="gvCrew" runat="server" CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnDataBound="gvCrew_DataBound" OnRowCreated="gvCrew_RowCreated" OnRowDataBound="gvCrew_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="CreditId" HeaderText="Dept"  Visible="false"/>
                                                    <asp:BoundField DataField="GenderId" HeaderText="Dept"  Visible="false"/>


                                                    <asp:BoundField DataField="Department" HeaderText="Dept" />
                                                    <asp:BoundField DataField="Job" HeaderText="Job" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                    <asp:BoundField DataField="ProfilePath" HeaderText="Profile" />
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
          
                                <asp:Image ID="seletedEpisodeImage" runat="server" />
                            </asp:Panel>

                        </div>
                        <div class="col-md-8">
                            <div class="row">
                                <asp:Button ID="btnShowTV" runat="server" Text="Television" OnClick="btnShowTV_Click" CssClass="btn btn-warning" ToolTip="Click to show the items in the TV Series" />
                                <asp:Button ID="btnShowMovies" runat="server" Text="Movies" OnClick="btnShowMovies_Click" CssClass="btn btn-warning" ToolTip="Click to show the Items in Movies" />
                                <asp:Button ID="btnStats" runat="server" Text="Stats" OnClick="btnStats_Click" CssClass="btn btn-warning" ToolTip="Click to show charts and more data" />
                            </div>
                            <br />
                            <div class="row">
                                <asp:MultiView ID="MultiViewMain" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="viewTV" runat="server">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:ListBox ID="lbSeries" runat="server" Height="125px" Width="234px" AutoPostBack="True" CssClass="form-control"
                                                        OnTextChanged="lbSeries_TextChanged" Font-Names="Tw Cen MT" OnSelectedIndexChanged="lbSeries_SelectedIndexChanged" ToolTip="Click to show viewing results"></asp:ListBox>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT StarTrekProductions.Title, StarTrekProductions.Episode, CONVERT (VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, StarTrekUserData.DateWatched, StarTrekProductions.Season FROM StarTrekProductions LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID WHERE (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID = @UserID) OR (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID IS NULL) AND (StarTrekUserData.DateWatched IS NULL)">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="seriesID" />
                                                            <asp:Parameter Name="userID" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:GridView ID="gvSeriesInfo" runat="server" ShowHeaderWhenEmpty="True" GridLines="Horizontal"
                                                        CellPadding="4" ForeColor="Black" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                                        BorderStyle="None" BorderWidth="1px" CssClass="table table-condensed" DataKeyNames="Season,Episode" OnRowCreated="gvSeriesInfo_RowCreated" OnSelectedIndexChanged="gvSeriesInfo_SelectedIndexChanged" OnDataBound="gvSeriesInfo_DataBound" OnRowDataBound="gvSeriesInfo_RowDataBound" OnRowCommand="gvSeriesInfo_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                            <asp:BoundField DataField="Season" HeaderText="Season" SortExpression="Season" />
                                                            <asp:BoundField DataField="Episode" HeaderText="Episode" SortExpression="Episode" />
                                                            <%-- <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Air Date" ReadOnly="True" SortExpression="OriginalAirDate" />--%>
                                                            <asp:BoundField DataField="DateWatched" HeaderText="Date Watched" SortExpression="DateWatched"></asp:BoundField>
                                                            <asp:ButtonField CommandName="Select" />
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
                                        </div>
                                    </asp:View>
                                    <asp:View ID="viewFilms" runat="server">
                                        <asp:GridView ID="gvFilms" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="Black"
                                            GridLines="Horizontal" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                            BorderWidth="1px" CssClass="table table-condensed">
                                            <Columns>
                                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                                <%--          <asp:BoundField DataField="OriginalAirDate" HeaderText="Original Air Date" ReadOnly="True" SortExpression="OriginalAirDate" />--%>
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
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT COUNT(StarTrekProductions.ID) AS Count FROM StarTrekProductions LEFT OUTER JOIN StarTrekProductionTypes ON StarTrekProductions.ProductionTypeID = StarTrekProductionTypes.ID GROUP BY StarTrekProductionTypes.ProductionType HAVING (StarTrekProductionTypes.ProductionType = 'Film')"></asp:SqlDataSource>
                                    </asp:View>
                                    <asp:View ID="viewStats" runat="server">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="table-responsive">
                                                        <asp:Table ID="Table2" runat="server" CssClass="table table-bordered table-condensed">
                                                            <asp:TableHeaderRow>
                                                                <asp:TableHeaderCell>
                                                    
                                                                </asp:TableHeaderCell>
                                                                <asp:TableHeaderCell>
                                                    <asp:Label runat="server">Total Count</asp:Label>
                                                                </asp:TableHeaderCell>
                                                                <asp:TableHeaderCell>
                                                    <asp:Label runat="server">Num Watched</asp:Label>
                                                                </asp:TableHeaderCell>
                                                                <asp:TableHeaderCell>
                                                    <asp:Label runat="server">Percentage Complete</asp:Label>
                                                                </asp:TableHeaderCell>
                                                            </asp:TableHeaderRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">The Original Series</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblOriginalCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblOriginalWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarOriginalSeries" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">The Animated Series</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblAnimatedCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblAnimatedWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarAnimatedSeries" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">The Next Generation</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblNextGenCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblNextGenWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarNextGen" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">Deep Space Nine</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblDS9Count" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblDS9Watched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarDeepSpace" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">Voyager</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblVoyagerCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblVoyagerWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarVoayger" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">Enterprise</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblEnterpriseCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblEnterpriseWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarEnterprise" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                    <asp:Label runat="server">Films</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblFilmCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblFilmWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarFilms" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow>
                                                                <asp:TableCell>
                                                            <asp:Label runat="server">Totals</asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblTotalCount" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <asp:Label ID="lblTotalWatched" runat="server"></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <div class="progress">
                                                                        <div class="progress-bar" id="progBarTotals" runat="server" role="progressbar">
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <%--<div class="row">--%>
                                                    <asp:Chart ID="ChartTotals" runat="server" Palette="EarthTones" Width="400px">
                                                        <Series>
                                                            <asp:Series Name="Series1" IsValueShownAsLabel="False" ChartArea="ChartArea1"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <AxisY Title="Percent Completed" Maximum="100">
                                                                    <MajorGrid Enabled="False" />
                                                                    <MajorTickMark Enabled="False" />
                                                                </AxisY>
                                                                <AxisX Title="Category" IsLabelAutoFit="True" LabelAutoFitMinFontSize="6">
                                                                    <MajorGrid Enabled="False" />
                                                                    <MajorTickMark Enabled="False" />

                                                                </AxisX>
                                                                <Area3DStyle Enable3D="True" />
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                        <BorderSkin BackColor="InactiveCaption" SkinStyle="Emboss" />
                                                    </asp:Chart>
                                                    <%--</div>--%>
                                                    <asp:SqlDataSource ID="SqlDataSource5" runat="server"></asp:SqlDataSource>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Chart ID="ChartPieTotals" runat="server" Width="400px">
                                                        <Series>
                                                            <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" LabelFormat="#%" CustomProperties="PieLabelStyle=Outside" ToolTip="#AXISLABEL">
                                                            </asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="true" />
                                                                <AxisX>
                                                                </AxisX>
                                                                <AxisY>
                                                                </AxisY>
                                                            </asp:ChartArea>

                                                        </ChartAreas>
                                                        <Legends>
                                                            <asp:Legend Name="Legend1">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <Titles>
                                                            <asp:Title Name="Title1" Text="Percentage Viewed">
                                                            </asp:Title>
                                                        </Titles>
                                                        <BorderSkin SkinStyle="Emboss" />
                                                    </asp:Chart>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Chart ID="chartMultiBar" runat="server" Width="400px">
                                                        <Series>
                                                            <asp:Series Name="Count" Legend="Legend1"></asp:Series>
                                                            <asp:Series ChartArea="ChartArea1" Name="Watched" Legend="Legend1">
                                                            </asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="false" />

                                                                <AxisY Title="Count">
                                                                </AxisY>
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                        <Legends>
                                                            <asp:Legend Name="Legend1">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <Titles>
                                                            <asp:Title Name="Title1" Text="Count/Watched Comparison">
                                                            </asp:Title>
                                                        </Titles>
                                                        <BorderSkin SkinStyle="Emboss" />
                                                    </asp:Chart>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Chart ID="PieChartPercentageWatched" runat="server" Width="400px">
                                                        <Series>
                                                            <asp:Series Name="Series1" ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" LabelFormat="#%" CustomProperties="CollectedSliceExploded=True, PieLabelStyle=Outside"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <Area3DStyle Enable3D="true" />
                                                            </asp:ChartArea>

                                                        </ChartAreas>
                                                        <Legends>
                                                            <asp:Legend Name="Legend1">
                                                            </asp:Legend>
                                                        </Legends>
                                                        <Titles>
                                                            <asp:Title Name="Title1" Text="Percentage Breakdown of All Titles">
                                                            </asp:Title>
                                                        </Titles>
                                                        <BorderSkin SkinStyle="Emboss" />
                                                    </asp:Chart>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Chart ID="chartMonthly" runat="server" Width="400px">
                                                        <Series>
                                                            <asp:Series Name="Series1" ChartType="Line" IsValueShownAsLabel="True"></asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                                <AxisY Title="Number Viewed">
                                                                    <MajorGrid Enabled="False" />
                                                                </AxisY>
                                                                <AxisX Title="Month" Interval="1">
                                                                    <MajorGrid Enabled="False" />
                                                                </AxisX>
                                                                <Area3DStyle Enable3D="false" />
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                        <Titles>
                                                            <asp:Title Name="Title1" Text="12 Month Viewing History">
                                                            </asp:Title>
                                                        </Titles>
                                                        <BorderSkin SkinStyle="Emboss" />
                                                    </asp:Chart>

                                                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:dboMasterConnectionString %>" SelectCommand="SELECT COUNT(ID) AS TotalCount FROM StarTrekUserData WHERE (UserID = @UserID) AND (DateWatched BETWEEN @StartDate AND @EndDate)">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="UserID" />
                                                            <asp:Parameter Name="StartDate" />
                                                            <asp:Parameter Name="EndDate" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>

                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnWatched" />
                <%--        <asp:AsyncPostBackTrigger ControlID="btnWatched" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
