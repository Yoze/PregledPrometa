using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PregledPrometa.Model;
using PregledPrometa.Web.Models;
using System.Globalization;

namespace PregledPrometa.Web.Controllers
{
    [Authorize]
    public class PrometController : Controller
    {

        /*
         * 'Writing your own custom ASP.Net MVC [Authorize] attributes'
         https://dougrathbone.com/blog/2011/07/24/writing-your-own-custom-aspnet-mvc-authorize-attributes
             */


        // GET: DnevniPregled
        [Authorize]
        public ActionResult Dnevni()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                DateTime dateNow = DateTime.Now.Date;
                ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", dateNow);

                return View();
            }
            else return RedirectToAction("Loader", "");
            
        }


        // POST: Pregled za dan izabran iz datepicker-a -> 'datum'
        [Authorize]
        [HttpPost]        
        public ActionResult GetDnevni(string dateFrom)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(dateFrom))
                {
                    DateTime _dateFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    DPViewModel vm = new DPViewModel(_dateFrom);

                }
                else
                {
                    // TO DO
                }



                Repository r = new Repository();

                r.GetDnevniPrometList(DPViewModel.PodaciOperatera);


                // datumi za prethodne godine
                //2016
                DateTime _datumFiltera2016 = new DateTime();
                _datumFiltera2016 = r.ClarionToDate(DPViewModel.clDateFrom);
                _datumFiltera2016 = _datumFiltera2016.AddYears(-2);
                ViewBag.dateFrom2016 = string.Format("{0:dd.MM.yyyy}", _datumFiltera2016);
                ViewBag.dateTo2016 = "";


                // 2017
                DateTime _datumFiltera2017 = new DateTime();
                _datumFiltera2017 = r.ClarionToDate(DPViewModel.clDateFrom);
                _datumFiltera2017 = _datumFiltera2017.AddYears(-1);
                ViewBag.dateFrom2017 = string.Format("{0:dd.MM.yyyy}", _datumFiltera2017);
                ViewBag.dateTo2017 = "";


                // Tekući datum
                ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateFrom));
                ViewBag.dateTO = "";



                return View("_PrometPartial");
            }
            else
            {
                return RedirectToAction("Loader", "Login");
            }
            

            //if (!string.IsNullOrEmpty(dateFrom))
            //{
            //    DateTime _dateFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            //    DPViewModel vm = new DPViewModel(_dateFrom);                
                
            //}
            //else
            //{
            //    // TO DO
            //}



            //Repository r = new Repository();

            //r.GetDnevniPrometList(DPViewModel.PodaciOperatera);


            //// datumi za prethodne godine
            ////2016
            //DateTime _datumFiltera2016 = new DateTime();
            //_datumFiltera2016 = r.ClarionToDate(DPViewModel.clDateFrom);
            //_datumFiltera2016 = _datumFiltera2016.AddYears(-2);
            //ViewBag.dateFrom2016 = string.Format("{0:dd.MM.yyyy}", _datumFiltera2016);
            //ViewBag.dateTo2016 = "";


            //// 2017
            //DateTime _datumFiltera2017 = new DateTime();
            //_datumFiltera2017 = r.ClarionToDate(DPViewModel.clDateFrom);
            //_datumFiltera2017 = _datumFiltera2017.AddYears(-1);
            //ViewBag.dateFrom2017 = string.Format("{0:dd.MM.yyyy}", _datumFiltera2017);
            //ViewBag.dateTo2017 = "";


            //// Tekući datum
            //ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateFrom));
            //ViewBag.dateTO = "";



            //return View("_PrometPartial");
        }


        
        //GET: Pregled prodatih stavki za odgovarajući dan 
        [Authorize]
        public ActionResult DnevniStavke(string shpro, int idatum)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Repository r = new Repository();
                DPViewModel vm = new DPViewModel();
                vm.DateFrom = r.ClarionToDate(idatum);

                r.GetDnevniPrometStavke(shpro, idatum);

                switch (shpro)
                {
                    case "001":
                        ViewBag.PM = "SOMBOR";
                        break;
                    case "002":
                        ViewBag.PM = "APATIN";
                        break;
                    case "003":
                        ViewBag.PM = "ODŽACI";
                        break;
                    case "004":
                        ViewBag.PM = "KULA";
                        break;
                    case "005":
                        ViewBag.PM = "B.PALANKA";
                        break;
                }

                ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(idatum));
                ViewBag.dateTO = "";
                return View("_PrometDetaljPartial");
            }
            else return RedirectToAction("Loader", "Login");

            //Repository r = new Repository();
            //DPViewModel vm = new DPViewModel();
            //vm.DateFrom = r.ClarionToDate(idatum);

            //r.GetDnevniPrometStavke(shpro, idatum);

            //switch (shpro)
            //{
            //    case "001":
            //        ViewBag.PM = "SOMBOR";
            //        break;
            //    case "002":
            //        ViewBag.PM = "APATIN";
            //        break;
            //    case "003":
            //        ViewBag.PM = "ODŽACI";
            //        break;
            //    case "004":
            //        ViewBag.PM = "KULA";
            //        break;
            //    case "005":
            //        ViewBag.PM = "B.PALANKA";
            //        break;
            //}

            //ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(idatum));
            //ViewBag.dateTO = "";
            //return View("_PrometDetaljPartial");
        }


        
        // GET: Osnovni View za pregled za više dana
        [Authorize]
        public ActionResult Period()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                DateTime dateNow = DateTime.Now.Date;
                //DateTime dateFrom = dateNow.AddDays(-7); // poslednjih 7 dana
                DateTime dateFrom = new DateTime(dateNow.Year, dateNow.Month, 1); // prvi u mesecu, datum početka filtera

                ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", dateFrom);
                ViewBag.dateTo = string.Format("{0:dd.MM.yyyy}", dateNow);


                return View(new DPViewModel());
            }
            else return RedirectToAction("Loader", "Login");

            //DateTime dateNow = DateTime.Now.Date;
            ////DateTime dateFrom = dateNow.AddDays(-7); // poslednjih 7 dana
            //DateTime dateFrom = new DateTime(dateNow.Year, dateNow.Month, 1); // prvi u mesecu, datum početka filtera

            //ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", dateFrom);
            //ViewBag.dateTo = string.Format("{0:dd.MM.yyyy}", dateNow);


            //return View(new DPViewModel());
        }




        // POST: Pregled za izabrani period
        [Authorize]
        [HttpPost]
        public ActionResult GetPeriod(string dateFrom, string dateTo)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                DateTime _dateFrom = new DateTime();
                DateTime _dateTo = new DateTime();

                if (!string.IsNullOrEmpty(dateFrom))
                {
                    _dateFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    // TO DO
                }


                if (!string.IsNullOrEmpty(dateTo))
                {
                    _dateTo = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    // TO DO
                }


                DPViewModel vm = new DPViewModel(_dateFrom, _dateTo);

                Repository r = new Repository();
                r.GetPrometPoPerioduList(DPViewModel.PodaciOperatera);



                // datumi za prethodne godine
                // 2016
                DateTime dateFrom2016 = new DateTime();
                dateFrom2016 = r.ClarionToDate(DPViewModel.clDateFrom);
                dateFrom2016 = dateFrom2016.AddYears(-2);

                DateTime dateTo2016 = new DateTime();
                dateTo2016 = r.ClarionToDate(DPViewModel.clDateTo);
                dateTo2016 = dateTo2016.AddYears(-2);

                // 2017
                DateTime dateFrom2017 = new DateTime();
                dateFrom2017 = r.ClarionToDate(DPViewModel.clDateFrom);
                dateFrom2017 = dateFrom2017.AddYears(-1);

                DateTime dateTo2017 = new DateTime();
                dateTo2017 = r.ClarionToDate(DPViewModel.clDateTo);
                dateTo2017 = dateTo2017.AddYears(-1);


                // 2016
                ViewBag.dateFrom2016 = string.Format("{0:dd.MM.yyyy}", dateFrom2016);
                ViewBag.dateTo2016 = string.Format("{0:dd.MM.yyyy}", dateTo2016);

                // 2017
                ViewBag.dateFrom2017 = string.Format("{0:dd.MM.yyyy}", dateFrom2017);
                ViewBag.dateTo2017 = string.Format("{0:dd.MM.yyyy}", dateTo2017);

                // Tekuća godina
                ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateFrom));
                ViewBag.dateTO = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateTo));
                return View("_PrometPartial");
            }
            else return RedirectToAction("Loader", "Login");




            //DateTime _dateFrom = new DateTime();
            //DateTime _dateTo = new DateTime();

            //if (!string.IsNullOrEmpty(dateFrom))
            //{
            //    _dateFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);               
            //}
            //else
            //{
            //    // TO DO
            //}


            //if (!string.IsNullOrEmpty(dateTo))
            //{
            //    _dateTo = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture); 
            //}
            //else
            //{
            //    // TO DO
            //}


            //DPViewModel vm = new DPViewModel(_dateFrom, _dateTo);

            //Repository r = new Repository();
            //r.GetPrometPoPerioduList(DPViewModel.PodaciOperatera);



            //// datumi za prethodne godine
            //// 2016
            //DateTime dateFrom2016 = new DateTime();
            //dateFrom2016 = r.ClarionToDate(DPViewModel.clDateFrom);
            //dateFrom2016 = dateFrom2016.AddYears(-2);

            //DateTime dateTo2016 = new DateTime();
            //dateTo2016 = r.ClarionToDate(DPViewModel.clDateTo);
            //dateTo2016 = dateTo2016.AddYears(-2);

            //// 2017
            //DateTime dateFrom2017 = new DateTime();
            //dateFrom2017 = r.ClarionToDate(DPViewModel.clDateFrom);
            //dateFrom2017 = dateFrom2017.AddYears(-1);

            //DateTime dateTo2017 = new DateTime();
            //dateTo2017 = r.ClarionToDate(DPViewModel.clDateTo);
            //dateTo2017 = dateTo2017.AddYears(-1);


            //// 2016
            //ViewBag.dateFrom2016 = string.Format("{0:dd.MM.yyyy}", dateFrom2016);
            //ViewBag.dateTo2016 = string.Format("{0:dd.MM.yyyy}", dateTo2016);

            //// 2017
            //ViewBag.dateFrom2017 = string.Format("{0:dd.MM.yyyy}", dateFrom2017);
            //ViewBag.dateTo2017 = string.Format("{0:dd.MM.yyyy}", dateTo2017);

            //// Tekuća godina
            //ViewBag.dateFrom = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateFrom));
            //ViewBag.dateTO = string.Format("{0:dd.MM.yyyy}", r.ClarionToDate(DPViewModel.clDateTo));
            //return View("_PrometPartial");
        }








    }
}