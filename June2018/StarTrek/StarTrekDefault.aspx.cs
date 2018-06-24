using June2018.Models;
using June2018.Models.MovieDB;
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
                Session["UserID"] = 1;

                // find next available item to watch
                ShowNextAvailableItem();

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


            // enforce movie
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
                    watchNext.SeriesNum = rdr[5].ToString();
                    watchNext.EpisodeNum = rdr[6].ToString();
                }

                // todo get appropriate image(tv or movie)
                MovieDB mdb = new MovieDB();
                mdb = mdb.GetShowDetails(watchNext.enumMediaType, watchNext.Title, watchNext.SeriesName, watchNext.SeriesNum, watchNext.EpisodeNum);
                lblDescription.Text = mdb.Details;
                imgMain.ImageUrl = mdb.Image;
                
            }

            lblOrigAirDate.Text = watchNext.OriginalAirDate;

            switch (watchNext.enumMediaType)
            {
                case WatchNext.MEDIA_TYPE.TELEVISION:
                    lblType.Text = "Television";
                    lblPrimaryType.Text = "Series:";
                    lblPrimaryTitle.Text = watchNext.SeriesName;
                    lblSecondaryType.Text = "Title:";
                    lblSecondaryTitle.Text = watchNext.Title;
                    break;
                case WatchNext.MEDIA_TYPE.FILM:
                    lblType.Text = "Movie";
                    lblPrimaryType.Text = "Movie:";
                    lblPrimaryTitle.Text = watchNext.Title;
                    break;
                default:
                    break;
            }

            //if (watchNext.enumMediaType == WatchNext.MEDIA_TYPE.FILM)
            //{
            //    lblType.Text = "Movie";
            //    lblPrimaryType.Text = "Movie:";
            //    lblPrimaryTitle.Text = watchNext.Title;
            //}
            //else
            //{
            //    lblType.Text = "Television";
            //    lblPrimaryType.Text = "Series:";
            //    lblPrimaryTitle.Text = watchNext.SeriesName;
            //    lblSecondaryType.Text = "Title:";
            //    lblSecondaryTitle.Text = watchNext.Title;

            //}
            

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

            STUD.UserID = (int)Session["UserId"];
            STUD.ProductionID = (int)Session["NextID"];
            STUD.DateWatched = DateTime.Now;

            DBContext.StarTrekUserDatas.Add(STUD);
            DBContext.SaveChanges();

            ShowNextAvailableItem();

            PopulateMovieInfoGridView();


        }

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
            int userID = (int)Session["UserID"];

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

                int userID = (int)Session["UserID"];

                DataSet listSeriesInfo = new DataSet();

                string cmdText = "SELECT StarTrekProductions.Title, StarTrekProductions.Episode, CONVERT(VARCHAR(50), StarTrekProductions.OriginalAirDate, 101) AS OriginalAirDate, " +
                                 "StarTrekUserData.DateWatched, StarTrekProductions.Season FROM StarTrekProductions " +
                                 "LEFT OUTER JOIN StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID " +
                                 "WHERE (StarTrekProductions.SeriesID = @SeriesID) AND (StarTrekUserData.UserID = @UserID OR StarTrekUserData.UserID IS NULL AND StarTrekUserData.DateWatched IS NULL) "; // +
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
            catch (Exception)
            {

               
            }
            
           
        }

        protected void gvSeriesInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // change date to short date
            }
        }


    }
}