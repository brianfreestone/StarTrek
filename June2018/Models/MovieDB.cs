using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;

namespace June2018.Models
{
    public class MovieDB
    {

        public string base_url { get; set;}
        public List<string> listBackdrop_sizes { get; set; }
        public List<string> listLogo_sizes { get; set; }
        public List<string> listPoster_sizes { get; set; }
        public List<string> listProfile_sizes { get; set; }
        public List<string> listStill_sizes { get; set; }

        public string json { get; set; }

        public MovieDB()
        {
            // get data from API
            string appId = "307312fdc6a58dfff8694e85a59f3f29";
            string url;

            url = string.Format("https://api.themoviedb.org/3/configuration?api_key=" + appId);

            //url = string.Format("https://api.themoviedb.org/3/search/movie?api_key=" + appId + " &query=Jack+Reacher");

            using (WebClient client = new WebClient())
            {
                json = client.DownloadString(url);
                Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                dynamic jsonObj = Json.Decode(json);

                dynamic images = jsonObj.images.base_url;


                var jo = JObject.Parse(json);
                var foo = jo["images"]["base_url"];
                var bar = jo["images"]["backdrop_sizes"];
                JToken base_url = jo["images"]["base_url"];
                JToken backdrop_sizes = jo["images"]["backdrop_sizes"];
                foreach (var item in backdrop_sizes)
                {
                    listBackdrop_sizes.Add(item.Value);
                }
                var l = backdrop_sizes.ToList();
            }
        }    
    }
}