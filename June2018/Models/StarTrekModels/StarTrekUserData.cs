namespace June2018.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StarTrekUserData")]
    public partial class StarTrekUserData
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        public int? ProductionID { get; set; }

        public DateTime? DateWatched { get; set; }

        public virtual StarTrekProduction StarTrekProduction { get; set; }

        public virtual User User { get; set; }
    }
}
