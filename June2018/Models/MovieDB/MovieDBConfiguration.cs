using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

namespace June2018.Models


{

    public partial class MovieDBConfiguration
    {

        public string json;
        public const string appId = "307312fdc6a58dfff8694e85a59f3f29";
        public string url;

        public MovieDBConfiguration()
        {
            //url = "https://api.themoviedb.org/3/configuration?api_key=" + appId;
            //using (WebClient client = new WebClient())
            //{
            //    json = client.DownloadString(url);

            //    var ConfigDetails = MovieDBConfiguration.FromJson(json);

               
            //}

        }
    }


    public partial class MovieDBConfiguration
    {
        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("change_keys")]
        public List<string> ChangeKeys { get; set; }
    }

    public partial class Images
    {
        [JsonProperty("base_url")]
        public string BaseUrl { get; set; }

        [JsonProperty("secure_base_url")]
        public string SecureBaseUrl { get; set; }

        [JsonProperty("backdrop_sizes")]
        public List<string> BackdropSizes { get; set; }

        [JsonProperty("logo_sizes")]
        public List<string> LogoSizes { get; set; }

        [JsonProperty("poster_sizes")]
        public List<string> PosterSizes { get; set; }

        [JsonProperty("profile_sizes")]
        public List<string> ProfileSizes { get; set; }

        [JsonProperty("still_sizes")]
        public List<string> StillSizes { get; set; }
    }

    public partial class MovieDBConfiguration
    {

        public static MovieDBConfiguration FromJson(string json) => JsonConvert.DeserializeObject<MovieDBConfiguration>(json, June2018.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MovieDBConfiguration self) => JsonConvert.SerializeObject(self, June2018.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}