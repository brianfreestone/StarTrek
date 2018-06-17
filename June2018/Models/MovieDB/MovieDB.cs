using June2018.Models.Image;
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



        public MovieDB()
        {

        }

        public string GetJson(string url)
        {
            using (WebClient client = new WebClient())
            {
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

            url = "https://api.themoviedb.org/3/search/tv?api_key="+ appId + "&language=en-US &query=" + ShowTitle + " &page=1";
            json = GetJson(url);

  
            StarTrekObject starTrekObject = StarTrekObject.FromJson(json);

 
            ID = starTrekObject.Results[0].Id;
            return ID;
        }

        public MovieDB GetShowDetails(string ShowTitle, string Season, string Episode)
        {
            string json;
            string url;
            string base_url;
            string file_size;
            string stillsPath;

            // get configuration
            url = "https://api.themoviedb.org/3/configuration?api_key=" + appId;
            json = GetJson(url);
            MovieDBConfiguration movieDBConfiguration = MovieDBConfiguration.FromJson(json);
            base_url = movieDBConfiguration.Images.BaseUrl;
            file_size = movieDBConfiguration.Images.PosterSizes[4]; // w500

            // get TV Details
            long TVID = GetShowID(ShowTitle);
            url = "https://api.themoviedb.org/3/tv/" + TVID + "/season/" + Season + "/episode/" + Episode + "?api_key=" + appId + "&language=en-US";
            json = GetJson(url);
            StarTrekShowDetails starTrekShowDetails = StarTrekShowDetails.FromJson(json);


            MovieDB movieDB = new MovieDB();
            movieDB.Details = starTrekShowDetails.Overview;
            stillsPath = starTrekShowDetails.StillPath;


            //// get TV Image
            //url = "https://api.themoviedb.org/3/tv/" + TVID + "/season/" + Season + "/episode/" + Episode + "/images?api_key=" + appId;

            //json = GetJson(url);
            //StarTrekShowImage starTrekShowImage = StarTrekShowImage.FromJson(json);
            ////movieDB.Image = starTrekShowImage.Stills;
            //stillsPath = starTrekShowImage.Stills[0].FilePath;

            movieDB.Image = base_url + file_size + stillsPath;

            return movieDB;
        }

    }


}