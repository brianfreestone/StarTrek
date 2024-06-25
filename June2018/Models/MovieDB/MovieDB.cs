using June2018.Models.Image;
using June2018.Models.MovieDBMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace June2018.Models.MovieDB
{
    public class MovieDB
    {
        public string Details;
        public string Image;
        public const string appId = "307312fdc6a58dfff8694e85a59f3f29";
        public StarTrekShowDetails details;


        public MovieDB()
        {

        }

        public string GetJson(string url)
        {
            using (WebClient client = new WebClient())
            {

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                string json;
                json = client.DownloadString(url);
                

                return json;
            }
        }

        public long GetShowID(string ShowTitle)
        {
            long ID;

            // search TV Show

            string url;
            string json;
            string appId = "307312fdc6a58dfff8694e85a59f3f29";
            DateTime airDate;

            url = "https://api.themoviedb.org/3/search/tv?api_key="+ appId + "&language=en-US &query=" + ShowTitle + " &page=1";
            json = GetJson(url);

  
            StarTrekObject starTrekObject = StarTrekObject.FromJson(json);

 
            ID = starTrekObject.Results[0].Id;
            return ID;
        }


        /// <summary>
        /// This method 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <param name="title"></param>
        /// <param name="tvTitle"></param>optional if it is a tv series
        /// <param name="tvSeason"></param>optional if it is a tv series
        /// <param name="tvEpisode"></param>optional if it is a tv series
        /// <returns>None</returns>
        public MovieDB GetShowDetails(WatchNext.MEDIA_TYPE mediaType, string title, string tvTitle, string tvSeason, string tvEpisode)
        {
            string json;
            string url;
            string base_url;
            string file_size;
            string stillsPath = "";
            long ID;

            // get configuration
            url = "https://api.themoviedb.org/3/configuration?api_key=" + appId;
            json = GetJson(url);

            MovieDBConfiguration movieDBConfiguration = MovieDBConfiguration.FromJson(json);
            base_url = movieDBConfiguration.Images.BaseUrl;
            file_size = movieDBConfiguration.Images.PosterSizes[3]; // w500

            MovieDB movieDB = new MovieDB();

            switch (mediaType)
            {
                case WatchNext.MEDIA_TYPE.FILM:

                    // get Movie Details
                    url = "https://api.themoviedb.org/3/search/movie?api_key=" + appId + "&language=en-US&query=" + title + "&page=1&include_adult=false";
                    json = GetJson(url);

                    MovieDetails movieDetails = MovieDetails.FromJson(json);
                    movieDB.Details = movieDetails.Results[0].Overview;
                    stillsPath = movieDetails.Results[0].PosterPath;


                    break;
                case WatchNext.MEDIA_TYPE.TELEVISION:

                    // get TV Details
                    ID = GetShowID(tvTitle);
                    url = "https://api.themoviedb.org/3/tv/" + ID + "/season/" + tvSeason + "/episode/" + tvEpisode + "?api_key=" + appId + "&language=en-US";
                    json = GetJson(url);
                    StarTrekShowDetails starTrekShowDetails = StarTrekShowDetails.FromJson(json);

                    movieDB.Details = starTrekShowDetails.Overview;
                    stillsPath = starTrekShowDetails.StillPath;
                    movieDB.details = starTrekShowDetails;

                    break;
  
            }

            movieDB.Image = base_url + file_size + stillsPath;

            return movieDB;
        }

    }


}