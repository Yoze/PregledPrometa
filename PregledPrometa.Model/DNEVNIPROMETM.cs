namespace PregledPrometa.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
        

    [Table("DNEVNIPROMETM")]
    public partial class DNEVNIPROMETM
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string SHPRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATUM { get; set; }

        public int? UKFI { get; set; }

        public double? UKIZNOS { get; set; }

        public double? UKKES { get; set; }

        public double? UKVIRMAN { get; set; }

        public int? UKSTAVKI { get; set; }

        public double? UKKOLICINA { get; set; }

        public double? UKRUC { get; set; }
    }
}
