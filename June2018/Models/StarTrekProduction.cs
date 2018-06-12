namespace June2018.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StarTrekProduction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StarTrekProduction()
        {
            StarTrekUserDatas = new HashSet<StarTrekUserData>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Episode { get; set; }

        [StringLength(50)]
        public string ProdNum { get; set; }

        [StringLength(50)]
        public string StarDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OriginalAirDate { get; set; }

        public int? ProductionTypeID { get; set; }

        public int? SeriesID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StarTrekUserData> StarTrekUserDatas { get; set; }
    }
}
