namespace June2018.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StarTrekProductionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StarTrekProductionType()
        {
            StarTrekProductions = new HashSet<StarTrekProduction>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string ProductionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StarTrekProduction> StarTrekProductions { get; set; }
    }
}
