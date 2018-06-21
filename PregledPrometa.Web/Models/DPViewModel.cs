using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PregledPrometa.Model;
using System.ComponentModel.DataAnnotations;
using PregledPrometa.Web.Models;
using System.Globalization;
using System.Configuration;
using System.Web.Configuration;

namespace PregledPrometa.Web.Models
{
    public class DPViewModel
    {

        // generisan iz pogleda DNEVNIPROMETM sa servera
        private List<DNEVNIPROMETM> PregledPrometaList { get; set; }

        // nasleđen DNEVNIPROMETM sa dodatim poljem ukruc zbog računanja ruc-a
        //2016
        public static List<DnevniPrometWruc> DnevniPrometWithRUC2016 { get; set; }
        //2017
        public static List<DnevniPrometWruc> DnevniPrometWithRUC2017 { get; set; }
        //2018
        public static List<DnevniPrometWruc> DnevniPrometWithRUC2018 { get; set; }


        // pogled DNEVNIPROMETzaWEBApp na serveru objedinjuje podatke za prikaz stavki u dn. pregledu.
        private List<DNEVNIPROMETzaWEBApp> DnevniPrometDetalj { get; set; }

        // lista stavki prometa sa konvertovanim Clarion datumom i vremenom, koristi se za prikaz
        public static List<DnevniStavkePrometaFormatZaView> DnevniStavkeFormatted { get; set; }




        public static List<string> PodaciOperatera { get; set; }



        public string Pwd;

        public static int clDateFrom { get; set; } // CLarion format datuma
        public static int clDateTo { get; set; } // CLarion format datuma

        Repository r = null;



        private DateTime _DateFrom;
        public DateTime DateFrom
        {
            get
            {
                //DateTimeFormatInfo fmt = (new CultureInfo("sr-SP-Latn")).DateTimeFormat;
                return _DateFrom;
            }
            set
            {
                clDateFrom = r.DateToClarion(value);
                _DateFrom = value;
            }
        }



        private DateTime _DateTo;
        public DateTime DateTo
        {
            get
            {
                return _DateTo;
            }
            set
            {
                clDateTo = r.DateToClarion(value);
                _DateTo = value;
            }
        }



        public DPViewModel()
        {
            r = new Repository();
           // Pwd = WebConfigurationManager.AppSettings["pwd"];
        }




        public DPViewModel(DateTime datefrom)
        {
            r = new Repository();
            DateFrom = datefrom;
        }

        public DPViewModel(DateTime dateFrom, DateTime dateTo)
        {
            r = new Repository();
            DateFrom = dateFrom;
            DateTo = dateTo;
        }




    }
}