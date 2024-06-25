using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace June2018.Models.MovieDBMovie
{
    public partial class MovieDetails
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("genre_ids")]
        public List<long> GenreIds { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }
    }

    public partial class MovieDetails
    {
        public static MovieDetails FromJson(string json) => JsonConvert.DeserializeObject<MovieDetails>(json, June2018.Models.MovieDBMovie.Converter.Settings);
    }
    
    public static class Serialize
    {
        public static string ToJson(this MovieDetails self) => JsonConvert.SerializeObject(self, June2018.Models.MovieDBMovie.Converter.Settings);
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