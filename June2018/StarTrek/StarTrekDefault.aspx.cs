using June2018.Models;
using June2018.Models.MovieDB;
using June2018.Models.StarTrekModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace June2018.StarTrek
{
    public partial class StarTrekDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate TV ListBox
                PopulateTVListBox();

                // create temp session variable for UserID
                //Session["UserID"] = 1;

                // find next available item to watch
                ShowNextAvailableItem();

                // populate the tv gridview with the appropriate tv series

            }
        }

        private void ShowNextAvailableItem()
        {
            //StarTrekUserModel dbContext = new StarTrekUserModel();

            //var listInfo = dbContext.StarTrekUserDatas.Where(x => x.UserID == 1).ToList();

            string cmdText = "SELECT TOP (1) StarTrekProductions.ID, StarTrekProductions.OriginalAirDate, StarTrekProductions.ProductionTypeID, StarTrekProductions.Title, StarTrekSeriesNames.SeriesName, StarTrekProductions.Season, StarTrekProductions.Episode " +
                             "FROM StarTrekProductions INNER JOIN " +
                             "StarTrekProductionTypes ON StarTrekProductionTypes.ID = StarTrekProductions.ProductionTypeID LEFT JOIN " +
                             "StarTrekSeriesNames ON StarTrekProductions.SeriesID = StarTrekSeriesNames.ID " +
                             "WHERE StarTrekProductions.ID NOT IN " +
                             "(" +
                             "SELECT  STP.ID " +
                             "FROM StarTrekProductions AS STP LEFT OUTER JOIN " +
                             "StarTrekUserData ON STP.ID = StarTrekUserData.ProductionID INNER JOIN " +
                             "StarTrekProductionTypes AS STPT ON STP.ProductionTypeID = STPT.ID " +
                             "WHERE(StarTrekUserData.UserID = 1) OR " +
                             "(StarTrekUserData.UserID = 1) AND (StarTrekProductions.SeriesID IS NULL) " +
                             ") " +
                             "ORDER BY StarTrekProductions.OriginalAirDate";


            //enforce movie
            //cmdText = "SELECT StarTrekProductions.ID, StarTrekProductions.OriginalAirDate, StarTrekProductions.ProductionTypeID, StarTrekProductions.Title, StarTrekSeriesNames.SeriesName, StarTrekProductions.Season, StarTrekProductions.Episode " +
            //          "FROM StarTrekProductions INNER JOIN " +
            //          "StarTrekProductionTypes ON StarTrekProductionTypes.ID = StarTrekProductions.ProductionTypeID LEFT JOIN " +
            //          "StarTrekSeriesNames ON StarTrekProductions.SeriesID = StarTrekSeriesNames.ID " +
            //          "WHERE StarTrekProductions.ID = 7";

            WatchNext watchNext = new WatchNext();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);

                cmd.Parameters.AddWithValue("@UserID", 1);
                con.Open();

                int seasonNum = 0;
                int episodeNum = 0;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    watchNext = new WatchNext();

                    watchNext.ID = Convert.ToInt16(rdr[0]);

                    Session["NextID"] = watchNext.ID;

                    watchNext.OriginalAirDate = Convert.ToDateTime(rdr[1]).ToShortDateString();
                    watchNext.enumMediaType = (WatchNext.MEDIA_TYPE)rdr[2];
                    watchNext.Title = rdr[3].ToString();
                    watchNext.SeriesName = rdr[4].ToString();
                    watchNext.SeasonNum = rdr[5].ToString();
                    watchNext.EpisodeNum = rdr[6].ToString();

                }



                // get appropriate image(tv or movie)
                MovieDB mdb = new MovieDB();
                mdb = mdb.GetShowDetails(watchNext.enumMediaType, watchNext.Title, watchNext.SeriesName, watchNext.SeasonNum, watchNext.EpisodeNum);
                lblDescription.Text = mdb.Details;
                imgMain.ImageUrl = mdb.Image;

            }

            //lblOrigAirDate.Text = watchNext.OriginalAirDate;

            switch (watchNext.enumMediaType)
            {
                case WatchNext.MEDIA_TYPE.TELEVISION:
                    lblType.Text = "Television";
                    lblPrimaryType.Text = "Series:";
                    lblPrimaryTitle.Text = watchNext.SeriesName;
                    lblSecondaryType.Text = "Title:";
                    lblSecondaryTitle.Text = watchNext.Title;
                    lblTertiaryType.Text = "Season:";
                    lblTertiaryTitle.Text = watchNext.SeasonNum;
                    lblQuaternaryType.Text = "Episode:";
                    lblQuaternaryTitle.Text = watchNext.EpisodeNum;
                    lbl5Type.Text = "Aired Date: ";
                    lbl5Title.Text = watchNext.OriginalAirDate;

                    break;
                case WatchNext.MEDIA_TYPE.FILM:
                    lblType.Text = "Movie";
                    lblPrimaryType.Text = "Movie:";
                    lblPrimaryTitle.Text = watchNext.Title;
                    break;
                default:
                    break;
            }

        }

        private void PopulateTVListBox()
        {
            StarTrekProductionModel dbContext = new StarTrekProductionModel();

            List<StarTrekSeriesName> listSeries = dbContext.StarTrekSeriesNames.ToList();

            lbSeries.DataTextField = "SeriesName";
            lbSeries.DataValueField = "ID";

            lbSeries.DataSource = listSeries;
            lbSeries.DataBind();
        }

        //

        protected void btnWatched_Click(object sender, EventArgs e)
        {
            // add datewatched to shown item
            StarTrekUserModel DBContext = new StarTrekUserModel();
            StarTrekUserData STUD = new StarTrekUserData();

            STUD.UserID = 1;// (int)Session["UserId"];
            STUD.ProductionID = (int)Session["NextID"];
            STUD.DateWatched = DateTime.Now;

            DBContext.StarTrekUserDatas.Add(STUD);
            DBContext.SaveChanges();

            ShowNextAvailableItem();

            PopulateMovieInfoGridView();

            GenerateCharts();


        }


        // buttons the change the pages of the multiview
        protected void btnShowTV_Click(object sender, EventArgs e)
        {
            MultiViewMain.SetActiveView(viewTV);
            PopulateSeriesInfoGridView(lbSeries.SelectedValue);
        }

        protected void btnShowMovies_Click(object sender, EventArgs e)
        {
            MultiViewMain.SetActiveView(viewFilms);

            PopulateMovieInfoGridView();

        }

        protected void btnStats_Click(object sender, EventArgs e)
        {
            GenerateCharts();

        }

        private void GenerateCharts()
        {

            // TODO show date chart. date that the episode was watched  

            // set tool tips on charts
            ChartTotals.Series[0].ToolTip = "#AXISLABEL: #VALY %";
            PieChartPercentageWatched.Series[0].ToolTip = "#AXISLABEL: #VALY{0%}";
            ChartPieTotals.Series[0].ToolTip = "#AXISLABEL: #VALY{0%}";
            chartMultiBar.Series[0].ToolTip = "#AXISLABEL Count: #VALY";
            chartMultiBar.Series[1].ToolTip = "#AXISLABEL Watched: #VALY";

            MultiViewMain.SetActiveView(viewStats);

            int userID = 1;// (int)Session["UserID"];

            // get stats and show in charts
            List<StarTrekStats> listStats = StarTrekStats.GetListStats(userID);

            //StarTrekStats s = (StarTrekStats)listStats.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ORIGINAL_SERIES);

            // display data in charts
            //lblOriginalPercentage.Text = s.PercentageComplete.ToString();

            var listTV = listStats.Where(x => x.MediaType == WatchNext.MEDIA_TYPE.TELEVISION).ToList();

            // THE ORIGINAL SERIIES
            lblOriginalCount.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ORIGINAL_SERIES).TotalCount.ToString();
            lblOriginalWatched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ORIGINAL_SERIES).TotalWatched.ToString();
            progBarOriginalSeries.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ORIGINAL_SERIES).PercentageComplete.ToString() + "%";
            progBarOriginalSeries.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ORIGINAL_SERIES).PercentageComplete.ToString() + "%";

            //THE ANIMATED SERIES
            lblAnimatedCount.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ANIMATED_SERIES).TotalCount.ToString();
            lblAnimatedWatched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ANIMATED_SERIES).TotalWatched.ToString();
            progBarAnimatedSeries.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ANIMATED_SERIES).PercentageComplete.ToString() + "%";
            progBarAnimatedSeries.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_ANIMATED_SERIES).PercentageComplete.ToString() + "%";

            // THE NEXT GENERATION
            lblNextGenCount.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_NEXT_GENERATION).TotalCount.ToString();
            lblNextGenWatched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_NEXT_GENERATION).TotalWatched.ToString();
            progBarNextGen.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_NEXT_GENERATION).PercentageComplete.ToString() + "%";
            progBarNextGen.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.THE_NEXT_GENERATION).PercentageComplete.ToString() + "%";

            // DEEP SPACE NINE
            lblDS9Count.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.DEEP_SPACE_NINE).TotalCount.ToString();
            lblDS9Watched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.DEEP_SPACE_NINE).TotalWatched.ToString();
            progBarDeepSpace.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.DEEP_SPACE_NINE).PercentageComplete.ToString() + "%";
            progBarDeepSpace.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.DEEP_SPACE_NINE).PercentageComplete.ToString() + "%";

            // VOYAGER
            lblVoyagerCount.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.VOYAGER).TotalCount.ToString();
            lblVoyagerWatched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.VOYAGER).TotalWatched.ToString();
            progBarVoayger.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.VOYAGER).PercentageComplete.ToString() + "%";
            progBarVoayger.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.VOYAGER).PercentageComplete.ToString() + "%";

            // ENTERPRISE
            lblEnterpriseCount.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.ENTERPRISE).TotalCount.ToString();
            lblEnterpriseWatched.Text = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.ENTERPRISE).TotalWatched.ToString();
            progBarEnterprise.Style["width"] = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.ENTERPRISE).PercentageComplete.ToString() + "%";
            progBarEnterprise.InnerText = listTV.Find(x => x.TVSeries == WatchNext.TV_SERIES.ENTERPRISE).PercentageComplete.ToString() + "%";


            // FILMS
            StarTrekStats films = (StarTrekStats)listStats.Find(x => x.MediaType == WatchNext.MEDIA_TYPE.FILM);

            lblFilmCount.Text = films.TotalCount.ToString();
            lblFilmWatched.Text = films.TotalWatched.ToString();
            progBarFilms.Style["width"] = films.PercentageComplete.ToString() + "%";
            progBarFilms.InnerText = films.PercentageComplete.ToString() + "%";




            // TOTALS CHART
            int totalsCount = listStats.Select(x => x.TotalCount).Sum();
            int totalsWatched = listStats.Select(x => x.TotalWatched).Sum();
            double totalPercentage = Math.Round(((double)totalsWatched / totalsCount) * 100, 2);

            lblTotalCount.Text = totalsCount.ToString();
            lblTotalWatched.Text = totalsWatched.ToString();
            progBarTotals.Style["width"] = totalPercentage.ToString() + "%";
            progBarTotals.InnerText = totalPercentage.ToString() + "%";

            List<string> xValuesTotal = listStats.Select(x => x.StatTitle.Replace("Star Trek: ", "")).ToList();

            List<int> yValuesTotal = listStats.Select(x => x.TotalCount).ToList();
            List<double> yValuesPercentage = listStats.Select(x => x.PercentageComplete).ToList();
            List<int> yValuesWatched = listStats.Select(x => x.TotalWatched).ToList();


            var foo = listStats.Select(x => Math.Round(((double)x.TotalWatched / totalsWatched), 2)).ToList();
            ChartPieTotals.Series[0].Points.DataBindXY(xValuesTotal, foo);

            List<double> yValuesPercentages = new List<double>();

            foreach (var item in yValuesTotal)
            {
                yValuesPercentages.Add(Math.Round(((double)item / totalsCount), 2));
            }

            PieChartPercentageWatched.Series[0].Points.DataBindXY(xValuesTotal, yValuesPercentages);

            xValuesTotal.Add("Total");
            yValuesTotal.Add(totalsCount);
            yValuesWatched.Add(totalsWatched);
            yValuesPercentage.Add(totalPercentage);

            chartMultiBar.Series[0].Points.DataBindXY(xValuesTotal, yValuesTotal);
            chartMultiBar.Series[1].Points.DataBindXY(xValuesTotal, yValuesWatched);

            ChartTotals.Series[0].Points.DataBindXY(xValuesTotal, yValuesPercentage);

            List<int> listMonthlyCount = new List<int>();

            List<string> listMonths = GetMonths(ref listMonthlyCount);

            chartMonthly.Series[0].Points.DataBindXY(listMonths, listMonthlyCount);


        }

        private List<string> GetMonths(ref List<int> listMonthlyCount)
        {

            int userID = 1;// (int)Session["UserID"];
            List<string> listMonths = new List<string>();
            DateTime currentMonth = DateTime.Today;

            for (int i = 11; i > -1; i--)
            {
                listMonths.Add(currentMonth.AddMonths(-i).ToString("MMMM"));
                listMonthlyCount.Add(GetMonthlyCount(currentMonth.AddMonths(-i), userID));
            }


            //for (int i =0; i < 12; i++)
            //{
            //    listMonths.Add(currentMonth.AddMonths(-i).ToString("MMMM"));
            //    listMonthlyCount.Add(GetMonthlyCount(currentMonth.AddMonths(-i), userID));
            //}

            return listMonths;
        }

        private int GetMonthlyCount(DateTime currentMonth, int userID)
        {
            string cmdText = "SELECT COUNT(ID) AS TotalCount FROM StarTrekUserData WHERE (UserID = @UserID) AND (DateWatched BETWEEN @StartDate AND @EndDate)";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@StartDate", GetFirstDayOfMonth(currentMonth));
                cmd.Parameters.AddWithValue("@EndDate", GetLastDayOfMonth(currentMonth));
                con.Open();
                return (int)cmd.ExecuteScalar();
            }

        }

        private DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private DateTime GetLastDayOfMonth(DateTime date)
        {
            DateTime firstDay = new DateTime(date.Year, date.Month, 1);
            return firstDay.AddMonths(1).AddDays(-1);
        }



        protected void lbSeries_TextChanged(object sender, EventArgs e)
        {




        }

        protected void lbSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate Series based on item
            PopulateSeriesInfoGridView(lbSeries.SelectedValue);
        }

        private void PopulateMovieInfoGridView()
        {
            //int userID = (int)Session["UserID"];
            int userID = 1;

            string cmdText = "SELECT StarTrekProductions.Title, CONVERT(VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, " +
                             "StarTrekUserData.DateWatched, StarTrekUserData.UserID " +
                             "FROM StarTrekProductions LEFT OUTER JOIN " +
                             "StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID " +
                             "WHERE(StarTrekProductions.ProductionTypeID = 2) AND(StarTrekUserData.UserID = @UserID OR StarTrekUserData.UserID IS NULL)";


            //StarTrekUserModel dbContext = new StarTrekUserModel();

            ////List<StarTrekUserData> listMoviesInfo = dbContext.StarTrekUserDatas.(x => x.UserID == userID && x.StarTrekProduction.ProductionTypeID == 2);
            //List<StarTrekUserData> listMoviesInfo = dbContext.StarTrekUserDatas.Where(x => x.UserID == userID &&  x.StarTrekProduction.ProductionTypeID==2).DefaultIfEmpty().ToList();

            DataSet listMoviesInfo = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmdText, con);
                da.SelectCommand.Parameters.AddWithValue("@UserID", userID);
                da.Fill(listMoviesInfo);
            }

            gvFilms.DataSource = listMoviesInfo;
            gvFilms.DataBind();
        }

        private void PopulateSeriesInfoGridView(string selectedValue)
        {

            int seriesID;
            try
            {
                seriesID = Convert.ToInt32(selectedValue);

                int userID = 1;// (int)Session["UserID"];

                DataSet listSeriesInfo = new DataSet();

                string cmdText = "SELECT StarTrekProductions.Title, StarTrekProductions.Episode, CONVERT(VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, " +
                                 "StarTrekUserData.DateWatched, StarTrekProductions.Season FROM StarTrekProductions " +
                                 "LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID " +
                                 "WHERE (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID = @UserID OR StarTrekUserData.UserID IS NULL AND StarTrekUserData.DateWatched IS NULL) ORDER BY season, convert(int, episode)"; // +
                                                                                                                                                                                                                                                //"OR (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.DateWatched IS NULL)";

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
                {
                    //SqlCommand cmd = new SqlCommand(cmdText, con);

                    SqlDataAdapter da = new SqlDataAdapter(cmdText, con);
                    da.SelectCommand.Parameters.AddWithValue("@SeriesID", seriesID);
                    da.SelectCommand.Parameters.AddWithValue("@UserID", userID);


                    da.Fill(listSeriesInfo);

                }


                //StarTrekUserModel dbContext = new StarTrekUserModel();

                //var query = dbContext.StarTrekUserDatas.GroupJoin(dbContext.StarTrekProductions, x => x.ProductionID, y => y.ProductionTypeID, (x, y) => new { x, y }).SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new { x.x.DateWatched, y.Title }).ToList();

                //List < StarTrekUserData > listSeriesInfo = dbContext.StarTrekUserDatas.Where(x => x.UserID == userID && x.StarTrekProduction.SeriesID == seriesID).DefaultIfEmpty().ToList();


                gvSeriesInfo.DataSource = listSeriesInfo.Tables[0];
                gvSeriesInfo.DataBind();


            }
            catch (Exception e)
            {


            }


        }

        protected void gvSeriesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // change date to short date
                LinkButton selectedLinkButton = (LinkButton)e.Row.Cells[4].Controls[0];
                GridViewRow selectedRow = (GridViewRow)(((LinkButton)e.Row.Cells[4].Controls[0]).NamingContainer);
                //lblError2.Text = e.Row.Cells[6].Text;
                string jsSingle = ClientScript.GetPostBackClientHyperlink(selectedRow.NamingContainer, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["onclick"] = jsSingle;
            }
        }

        protected void gvSeriesInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
         


                GridViewRow selectedRow = gvSeriesInfo.SelectedRow;

                var seasonNum = selectedRow.Cells[1].Text;
                var episodeNum = selectedRow.Cells[2].Text;
                var title = selectedRow.Cells[0].Text;
                var seriesName = lbSeries.SelectedItem.Text;

                WatchNext watchNext = new WatchNext
                {
                    enumMediaType = WatchNext.MEDIA_TYPE.TELEVISION,
                    Title = title,
                    SeriesName = seriesName,
                    SeasonNum = seasonNum,
                    EpisodeNum = episodeNum
                };
                // get appropriate image(tv or movie)
                MovieDB mdb = new MovieDB();
                mdb = mdb.GetShowDetails(watchNext.enumMediaType, watchNext.Title, watchNext.SeriesName, watchNext.SeasonNum, watchNext.EpisodeNum);

                detailsPanel.Visible = true;
                lblSelectedSeries.Text = seriesName;
                lblSelectedTitle.Text = title;
                lblSelectedSeason.Text = seasonNum;
                lblSelectedEpisode.Text = episodeNum;
                lblSelectedAirDate.Text = mdb.details.AirDate.Date.ToShortDateString();
                gvCrew.DataSource = mdb.details.Crew;
                gvCrew.DataBind();
                txtSelectedSynopsis.Text = mdb.Details;
                seletedEpisodeImage.ImageUrl = mdb.Image;
            //foreach (GridViewRow row in gvSeriesInfo.Rows)
            //{




         
        



            //    var foo = gvSeriesInfo.DataKeys[selectedRow.Row=Index].Values[0].ToString();
            //}
        }

        protected void gvSeriesInfo_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ffff00'");

                // when mouse leaves the row, change the bg color to its original value   
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
        }

        protected void gvSeriesInfo_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvSeriesInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectRow")
            {
                GridViewRow selectedRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                var panel = detailsPanel;
                panel.Visible = true;
                lblSelectedTitle.Text = "";
            }
        }

        protected void gvCrew_DataBound(object sender, EventArgs e)
        {
          
        }

        protected void gvCrew_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string helper = string.Format("DisplayImage.aspx?id={0}", HttpUtility.UrlEncode(e.Row.Cells[5].Text));
                //e.Row.ToolTip = helper;

            }
        }

        protected void gvCrew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text != "" && e.Row.Cells[5].Text != "&nbsp;")
                {
                    string helper = string.Format("DisplayImage.aspx?src='https://image.tmdb.org/t/p/w342{0}'", HttpUtility.UrlEncode(e.Row.Cells[5].Text));
                    string toolTip = string.Format("<img src=DisplayImage.aspx?source={0}", e.Row.Cells[5].Text) + " />";
                    e.Row.ToolTip = helper;
                    e.Row.Attributes.Add("onmouseover", "DisplayImageToolTip('" + toolTip + "');");
                    e.Row.Attributes.Add("onmouseout", "DisplayImageToolTip('');");
                }
           

            }
        }
    }
}