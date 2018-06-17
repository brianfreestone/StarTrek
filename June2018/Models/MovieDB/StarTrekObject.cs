using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;


public partial class StarTrekObject
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
    [JsonProperty("original_name")]
    public string OriginalName { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("vote_count")]
    public long VoteCount { get; set; }

    [JsonProperty("vote_average")]
    public double VoteAverage { get; set; }

    [JsonProperty("poster_path")]
    public string PosterPath { get; set; }

    [JsonProperty("first_air_date")]
    public DateTimeOffset FirstAirDate { get; set; }

    [JsonProperty("popularity")]
    public double Popularity { get; set; }

    [JsonProperty("genre_ids")]
    public List<long> GenreIds { get; set; }

    [JsonProperty("original_language")]
    public string OriginalLanguage { get; set; }

    [JsonProperty("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonProperty("overview")]
    public string Overview { get; set; }

    [JsonProperty("origin_country")]
    public List<string> OriginCountry { get; set; }
}

public partial class StarTrekObject
{
    public static StarTrekObject FromJson(string json) => JsonConvert.DeserializeObject<StarTrekObject>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this StarTrekObject self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
