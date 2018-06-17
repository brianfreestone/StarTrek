
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace June2018.Models
{
    public class WatchNext
    {
        public enum MEDIA_TYPE
        {

            TELEVISION = 1,
            FILM = 2
        }

        public enum TV_SERIES
        {
            THE_ORIGINAL_SERIES = 1,
            THE_ANIMATED_SERIES = 2,
            THE_NEXT_GENERATION = 3,
            DEEP_SPACE_NINE = 4,
            VOYAGER = 5,
            ENTERPRISE = 6
        }

        public enum SERIES_EPISODE
        {
            SERIES,
            EPISODE
        }

        public int ID { get; set; }
        public TV_SERIES enumTVSeries { get; set; }
        public string SeriesNum { get; set; }
        public string EpisodeNum { get; set; }
        public string Title { get; set; }
        public string OriginalAirDate { get; set; }
        public string ProductionType { get; set; }
        public MEDIA_TYPE enumMediaType { get; set; }
        public string SeriesName { get; set; }

        public static MEDIA_TYPE GetEnumMediaType(string mType)
        {

            MEDIA_TYPE medType = new MEDIA_TYPE();
            switch (mType)
            {
                case "Television":
                    medType = MEDIA_TYPE.TELEVISION;
                    break;
                case "Film":
                    medType = MEDIA_TYPE.FILM;
                    break;
            }

            return medType;
        }

        public static TV_SERIES GetEnumTVSeriesType(string tvSeries)
        {

            TV_SERIES medType = new TV_SERIES();
            switch (tvSeries)
            {

                case "Star Trek: The Original Series":
                    medType = TV_SERIES.THE_ORIGINAL_SERIES;
                    break;
                case "Star Trek: The Animated Series":
                    medType = TV_SERIES.THE_ANIMATED_SERIES;
                    break;
                case "Star Trek: The Next Generation":
                    medType = TV_SERIES.THE_NEXT_GENERATION;
                    break;
                case "Star Trek: Deep Space Nine":
                    medType = TV_SERIES.DEEP_SPACE_NINE;
                    break;
                case "Star Trek: Voyager":
                    medType = TV_SERIES.VOYAGER;
                    break;
                case "Star Trek: Enterprise":
                    medType = TV_SERIES.ENTERPRISE;
                    break;
            }

            return medType;
        }
    }
}