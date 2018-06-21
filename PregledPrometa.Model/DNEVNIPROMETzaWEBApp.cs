namespace PregledPrometa.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("DNEVNIPROMETzaWEBApp")]
    public class DNEVNIPROMETzaWEBApp
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string SHPRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DATUM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VREME2 { get; set; }

        [Column(Order =3)]
        public Int16 IDKASE { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BROJFI { get; set; }

        [StringLength(30)]
        public string Operater { get; set; }

        //[StringLength(13)]
        //public string BARCODE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(7)]
        public string SifraArtikla { get; set; }

        [StringLength(30)]
        public string NazivArtikla { get; set; }

        public double? CENAM { get; set; }

        public double? KOLICINA { get; set; }

        public decimal? IZNOS { get; set; }

        public double? UKRUC { get; set; }

        
    }
}
