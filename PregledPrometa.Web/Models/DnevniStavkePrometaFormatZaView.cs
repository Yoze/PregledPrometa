using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PregledPrometa.Model;

namespace PregledPrometa.Web.Models
{
    public class DnevniStavkePrometaFormatZaView : DNEVNIPROMETzaWEBApp
    {

        public DateTime DatumRegular { get; set; }
        public DateTime VremeRegular { get; set; }
        public double? RucProcentualno { get; set; }


    }
}