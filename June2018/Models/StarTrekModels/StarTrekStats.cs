using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static June2018.Models.WatchNext;

namespace June2018.Models.StarTrekModels
{
    public class StarTrekStats
    {
        public MEDIA_TYPE MediaType { get; set; }
        public int SeriesID { get; set; }
        public string StatTitle { get; set; }
        public int TotalCount { get; set; }
        public int TotalWatched { get; set; }
        public double PercentageComplete { get; set; }
        public TV_SERIES TVSeries {get;set;}
        
        //public StarTrekStats(string _statTitle, int _totalCount, int _totalWatched)
        //{
        //    StatTitle = _statTitle;
        //    TotalCount = _totalCount;
        //    TotalWatched = _totalWatched;
        //    PercentageComplete = TotalCount / TotalWatched;
        //}

        public static List<StarTrekStats> GetListStats(int userID)
        {
            List<StarTrekStats> listStats = new List<StarTrekStats>();


            List<StarTrekStats> listTV = GetListTV(userID);
            List<StarTrekStats> listFilms = GetListFilms(userID);

            listStats.AddRange(listTV);
            listStats.AddRange(listFilms);

            
            // combine listTV and listFilms

            return listStats;
        }

        private static List<StarTrekStats> GetListFilms(int userID)
        {

            List<StarTrekStats> listMovieStats = new List<StarTrekStats>();

            int total;
            int watched;

            string cmdText = "SELECT COUNT(StarTrekProductions.ID) AS Count FROM StarTrekProductions INNER JOIN " +
                             "StarTrekProductionTypes on StarTrekProductionTypes.ID = StarTrekProductions.ProductionTypeID " +
                             "WHERE StarTrekProductionTypes.ProductionType = 'Film'";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdText, con);
                total = (int)cmd.ExecuteScalar();    
            }


            cmdText = "SELECT COUNT( StarTrekProductions.ID) " +
                      "FROM StarTrekProductions INNER JOIN " +
                      "StarTrekProductionTypes ON StarTrekProductionTypes.ID = StarTrekProductions.ProductionTypeID INNER JOIN " +
                      "StarTrekUserData ON StarTrekUserData.ProductionID = StarTrekProductions.ID " +
                      "WHERE(StarTrekProductionTypes.ProductionType = 'Film') AND(StarTrekUserData.UserID = @UserID OR " +
                      "StarTrekUserData.UserID IS NULL)";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                watched = (int)cmd.ExecuteScalar();
            }

            StarTrekStats movieStat = new StarTrekStats
            {
                MediaType = MEDIA_TYPE.FILM,
                StatTitle = "Film",
                TotalCount = total,
                TotalWatched = watched,
                PercentageComplete = Math.Round((watched * 100.0 / total), 2)
        };

            listMovieStats.Add(movieStat);

            return listMovieStats;
        }



        private static List<StarTrekStats> GetListTV( int userID)
        {
            StarTrekProductionModel DBContext = new StarTrekProductionModel();
            List<StarTrekSeriesName> listStarTrekSeries = DBContext.StarTrekSeriesNames.ToList();          

            List<StarTrekStats> listStats = new List<StarTrekStats>(); 

            foreach (StarTrekSeriesName series in listStarTrekSeries)
            {
                StarTrekStats newStat = new StarTrekStats
                {
                    SeriesID = series.ID,
                    MediaType = MEDIA_TYPE.TELEVISION,
                    StatTitle = series.SeriesName,
                    TVSeries = WatchNext.GetEnumTVSeriesType(series.SeriesName)
                };

                listStats.Add(newStat);
            }

            Dictionary<string, int> dictTotalCount = GetTotalSeriesCounts();
            Dictionary<string, int> dictWatchCount = GetTotalWatchedSeriesByUserID(userID);

            foreach (StarTrekStats stat in listStats)
            {
                stat.TotalCount = dictTotalCount[stat.StatTitle];
                if (dictWatchCount.ContainsKey(stat.StatTitle))
                {
                    stat.TotalWatched = dictWatchCount[stat.StatTitle];
                    stat.PercentageComplete = Math.Round((stat.TotalWatched * 100.0 / stat.TotalCount), 2);
                }  
            }

            return listStats;
        }


        private static Dictionary<string,int> GetTotalSeriesCounts()
        {
            Dictionary<string, int> dictTotalCount = new Dictionary<string, int>();

            string cmdText = "SELECT COUNT(StarTrekProductions.ID) AS Count, StarTrekSeriesNames.SeriesName " +
                             "FROM StarTrekSeriesNames INNER JOIN " +
                             "StarTrekProductions ON StarTrekSeriesNames.ID = StarTrekProductions.SeriesID LEFT OUTER JOIN " +
                             "StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID " +
                             "GROUP BY StarTrekSeriesNames.SeriesName";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dictTotalCount.Add(rdr["SeriesName"].ToString(), (int)rdr["Count"]);
                }
            }

            return dictTotalCount;
        }

        private static Dictionary<string, int> GetTotalWatchedSeriesByUserID(int UserID)
        {

            Dictionary<string, int> dictTotals = new Dictionary<string, int>();

            string cmdText = "SELECT COUNT(StarTrekProductions.ID) AS Count, StarTrekSeriesNames.SeriesName " + 
                             "FROM StarTrekSeriesNames INNER JOIN " +
                             "StarTrekProductions ON StarTrekSeriesNames.ID = StarTrekProductions.SeriesID INNER JOIN " +
                             "StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID " +
                             "GROUP BY StarTrekSeriesNames.SeriesName, StarTrekUserData.UserID " +
                             "HAVING(StarTrekUserData.UserID = @UserID)";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dictTotals.Add(rdr["Seriesname"].ToString(), (int)rdr["Count"]);
                }
            }
            return dictTotals;
        }

        private static int GetTotalFilmCount()
        {
            string cmdText = "SELECT COUNT(StarTrekProductions.ID) AS Count " +
                             "FROM StarTrekProductions LEFT OUTER JOIN " +
                             "StarTrekProductionTypes ON StarTrekProductions.ProductionTypeID = StarTrekProductionTypes.ID " +
                             "GROUP BY StarTrekProductionTypes.ProductionType " +
                             "HAVING(StarTrekProductionTypes.ProductionType = 'Film')";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                return (int)cmd.ExecuteScalar();
            }
        }

        private static int GetTotalFilmsWatchedByUserID(int UserID)
        {
            string cmdText = "SELECT COUNT(StarTrekProductions.ID) AS Count" +
                             "FROM StarTrekProductions INNER JOIN " +
                             "StarTrekUserData ON StarTrekProductions.ID = StarTrekUserData.ProductionID INNER JOIN " +
                             "StarTrekProductionTypes ON StarTrekProductions.ProductionTypeID = StarTrekProductionTypes.ID " +
                             "GROUP BY StarTrekUserData.UserID, StarTrekProductionTypes.ProductionType " +
                             "HAVING(StarTrekUserData.UserID = @UserID) AND(StarTrekProductionTypes.ProductionType = 'Film')";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dboMasterConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, con);
                return (int)cmd.ExecuteScalar();
            }
        }


        // TODO refactor title
    }
}