using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace June2018.Models.Image
{
    public partial class StarTrekShowImage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("stills")]
        public List<Still> Stills { get; set; }
    }

    public partial class Still
    {
        [JsonProperty("aspect_ratio")]
        public double AspectRatio { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("iso_639_1")]
        public object Iso639_1 { get; set; }

        [JsonProperty("vote_average")]
        public long VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class StarTrekShowImage
    {
        public static StarTrekShowImage FromJson(string json) => JsonConvert.DeserializeObject<StarTrekShowImage>(json, June2018.Models.Image.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this StarTrekShowImage self) => JsonConvert.SerializeObject(self, June2018.Models.Image.Converter.Settings);
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