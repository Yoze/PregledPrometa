using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PregledPrometa.Web.Models;
using PregledPrometa.Model;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace PregledPrometa.Web.Models
{
    public class Repository
    {
        //public static string ELBS_ConnString = Properties.Settings.Default.ELBS_ConnString;
        //public static SqlConnection ELBS_Connection;
        public static List<string> PodaciOperatera; // generička kolekcija sadrži shpro, naziv operatera

        public HttpCookie Cookie { get; set; }
        public string userName { get; set; }

        public string password { get; set; }


        public Repository()
        {
            Cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Cookie.Value);
            userName = ticket.UserData;

            string[] userAndPassFromCookie = userName.Split(';');

            userName = userAndPassFromCookie[0];
            password = userAndPassFromCookie[1];

        }

        //// generiše prikaz za navedeni broj godina unazad npr. 3 će prikazati tekuću godinu + dve prethodne godine
        //int prikaziGodinaUnazad = 3;


        // D N E V N I
        // Pregled dnevnog prometa po radnjama





        //public void GetDnevniPrometZaSvaProdajnaMesta(string godina)
        //{

        //    int tekucaGodina = DateTime.Now.Year;



        //    // konekcioni string prema ELBS bazi, uzima se iz WebConnString.config
        //    string webConnStringConfigName = "name=PregledPrometaConn" + godina;

        //    GetDnevniPrometZaProdajnoMesto("001", tekucaGodina);


        //    //using (PregledDnevnogPrometaContext db = new PregledDnevnogPrometaContext(webConnStringConfigName))
        //    //{
        //    //    List<DNEVNIPROMETM> dnevniPromet = new List<DNEVNIPROMETM>();

        //    //    dnevniPromet = db.DNEVNIPROMETM.ToList();
        //    //}





        //}



        //private void GetDnevniPrometZaProdajnoMesto(string shpro, int godina)
        //{
        //    string webConnStringConfigName = "name=PregledPrometaConn" + godina;

        //    using (PregledDnevnogPrometaContext db = new PregledDnevnogPrometaContext(webConnStringConfigName))
        //    {
        //        List<DNEVNIPROMETM> dnevniPromet = new List<DNEVNIPROMETM>();

        //        dnevniPromet = db.DNEVNIPROMETM.ToList();
        //    }



        //    using (PregledPrometaContext2016 db = new PregledPrometaContext2016())
        //    {
        //        List<DNEVNIPROMETM> tempList2016 = new List<DNEVNIPROMETM>();

        //        // clarion datum filtera za dnevni promet minus 2 god
        //        DateTime _datumFiltera = new DateTime();
        //        _datumFiltera = ClarionToDate(DPViewModel.clDateFrom);
        //        _datumFiltera = _datumFiltera.AddYears(-2);

        //        int clDate2016MinusYear = DateToClarion(_datumFiltera);




        //        tempList2016 = db.DNEVNIPROMETM
        //            .Where(p => p.DATUM.Equals(clDate2016MinusYear))
        //            .Where(x => x.SHPRO == shpro)
        //            .OrderBy(c => c.SHPRO)
        //            .ToList();

        //        List<DNEVNIPROMETM> tempList2016OrderBySHPRO = new List<DNEVNIPROMETM>();
        //        tempList2016OrderBySHPRO = tempList2016.OrderBy(x => x.SHPRO).ToList();

        //        DPViewModel.DnevniPrometWithRUC2016 = tempList2016OrderBySHPRO
        //         .Select(i => new DnevniPrometWruc()
        //         {
        //             SHPRO = i.SHPRO,
        //             DATUM = i.DATUM,
        //             UKFI = i.UKFI,
        //             UKIZNOS = i.UKIZNOS,
        //             UKKES = i.UKKES,
        //             UKVIRMAN = i.UKVIRMAN,
        //             UKSTAVKI = i.UKSTAVKI,
        //             UKKOLICINA = i.UKKOLICINA,
        //             UKRUC = i.UKRUC,
        //             RUCProc = (double)(((i.UKRUC) / (i.UKIZNOS)) * 100),
        //         }).ToList();
        //    }
        //}




        public void GetDnevniPrometList(List<string> podaciOperatera)
        {
            


            /** 2 0 1 6 */
            using (PregledPrometaContext2016 db = new PregledPrometaContext2016())
            {
                List<DNEVNIPROMETM> tempList2016 = new List<DNEVNIPROMETM>();

                // clarion datum filtera za dnevni promet minus 2 god
                DateTime _datumFiltera = new DateTime();
                _datumFiltera = ClarionToDate(DPViewModel.clDateFrom);
                _datumFiltera = _datumFiltera.AddYears(-2);

                int clDate2016MinusYear = DateToClarion(_datumFiltera);


                // filtrira prikaz svih prodavnica za korisnika 'elbraco' ili prikaz samo prodavnice za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco")) // <== throw exception  here !!!
                {
                    tempList2016 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM.Equals(clDate2016MinusYear))
                    .OrderBy(c => c.SHPRO)
                    .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2016 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM.Equals(clDate2016MinusYear))
                   .Where(x => x.SHPRO == shpro)
                   .OrderBy(c => c.SHPRO)
                   .ToList();
                }
               

                List<DNEVNIPROMETM> tempList2016OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2016OrderBySHPRO = tempList2016.OrderBy(x => x.SHPRO).ToList();

                DPViewModel.DnevniPrometWithRUC2016 = tempList2016OrderBySHPRO
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.SHPRO,
                     DATUM = i.DATUM,
                     UKFI = i.UKFI,
                     UKIZNOS = i.UKIZNOS,
                     UKKES = i.UKKES,
                     UKVIRMAN = i.UKVIRMAN,
                     UKSTAVKI = i.UKSTAVKI,
                     UKKOLICINA = i.UKKOLICINA,
                     UKRUC = i.UKRUC,
                     RUCProc = (double)(((i.UKRUC) / (i.UKIZNOS)) * 100),
                 }).ToList();
            }


            /** 2 0 1 7 */
            using (PregledPrometaContext2017 db = new PregledPrometaContext2017())
            {
                List<DNEVNIPROMETM> tempList2017 = new List<DNEVNIPROMETM>();

                // clarion datum filtera za dnevni promet minus 1 god
                DateTime _datumFiltera = new DateTime();
                _datumFiltera = ClarionToDate(DPViewModel.clDateFrom);
                _datumFiltera = _datumFiltera.AddYears(-1);

                int clDate2017MinusYear = DateToClarion(_datumFiltera);


                // filtrira prikaz svih prodavnica za korisnika 'elbraco' ili prikaz samo prodavnice za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco"))
                {
                    tempList2017 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM.Equals(clDate2017MinusYear))
                    .OrderBy(c => c.SHPRO)
                    .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2017 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM.Equals(clDate2017MinusYear))
                   .Where(x => x.SHPRO == shpro)
                   .OrderBy(c => c.SHPRO)
                   .ToList();
                }



                List<DNEVNIPROMETM> tempList2017OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2017OrderBySHPRO = tempList2017.OrderBy(x => x.SHPRO).ToList();


                DPViewModel.DnevniPrometWithRUC2017 = tempList2017OrderBySHPRO
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.SHPRO,
                     DATUM = i.DATUM,
                     UKFI = i.UKFI,
                     UKIZNOS = i.UKIZNOS,
                     UKKES = i.UKKES,
                     UKVIRMAN = i.UKVIRMAN,
                     UKSTAVKI = i.UKSTAVKI,
                     UKKOLICINA = i.UKKOLICINA,
                     UKRUC = i.UKRUC,
                     RUCProc = (double)(((i.UKRUC) / (i.UKIZNOS)) * 100),
                 }).ToList();


            }




            /** 2 0 1 8 */
            using (PregledPrometaContext2018 db = new PregledPrometaContext2018())
            {
                List<DNEVNIPROMETM> tempList2018 = new List<DNEVNIPROMETM>();

                //tempList2018 = db.DNEVNIPROMETM
                //    .Where(p => p.DATUM.Equals(DPViewModel.clDateFrom))
                //    .OrderBy(c => c.SHPRO)
                //    .ToList();



                // filtrira prikaz svih prodavnica za korisnika 'elbraco' ili prikaz samo prodavnice za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco"))
                {
                    tempList2018 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM.Equals(DPViewModel.clDateFrom))
                    .OrderBy(c => c.SHPRO)
                    .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2018 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM.Equals(DPViewModel.clDateFrom))
                   .Where(x => x.SHPRO == shpro)
                   .OrderBy(c => c.SHPRO)
                   .ToList();
                }




                List<DNEVNIPROMETM> tempList2018OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2018OrderBySHPRO = tempList2018.OrderBy(x => x.SHPRO).ToList();


                DPViewModel.DnevniPrometWithRUC2018 = tempList2018OrderBySHPRO
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.SHPRO,
                     DATUM = i.DATUM,
                     UKFI = i.UKFI,
                     UKIZNOS = i.UKIZNOS,
                     UKKES = i.UKKES,
                     UKVIRMAN = i.UKVIRMAN,
                     UKSTAVKI = i.UKSTAVKI,
                     UKKOLICINA = i.UKKOLICINA,
                     UKRUC = i.UKRUC,
                     RUCProc = (double)(((i.UKRUC) / (i.UKIZNOS)) * 100),
                 }).ToList();
            }

        }


        // Pregled stavki prometa po radnji za zadati datum
        public void GetDnevniPrometStavke(string shpro, int idatum)
        {

            using (PregledPrometaContext2018 db = new PregledPrometaContext2018())
            {
                List<DNEVNIPROMETzaWEBApp> tempListStavkePrometa = new List<DNEVNIPROMETzaWEBApp>();


                tempListStavkePrometa = db.DNEVNIPROMETzaWEBApp
                    .Where(p => p.SHPRO.Equals(shpro) && p.DATUM.Equals(idatum))
                    .ToList();

                // tempList.OrderBy(p => p.VREME2);


                DPViewModel.DnevniStavkeFormatted = tempListStavkePrometa
                    .Select(i => new DnevniStavkePrometaFormatZaView()
                    {
                        SHPRO = i.SHPRO,
                        DatumRegular = ClarionToDate(i.DATUM),
                        VremeRegular = ClarionTimeToRegularTime(i.VREME2, ClarionToDate(idatum)),
                        BROJFI = i.BROJFI,
                        Operater = i.Operater,
                        //BARCODE = i.BARCODE,
                        SifraArtikla = i.SifraArtikla,
                        NazivArtikla = i.NazivArtikla,
                        CENAM = i.CENAM,
                        KOLICINA = i.KOLICINA,
                        IZNOS = i.IZNOS,
                        UKRUC = i.UKRUC,
                        RucProcentualno = ((i.UKRUC) / ((Convert.ToDouble(i.IZNOS) / 1.2) - i.UKRUC)) * 100
                    }).ToList();
            }
        }




        // P E R I O D
        // Pregled prometa po radnjama za zadati period
        public void GetPrometPoPerioduList(List<string> podaciOperatera)
        {
            // 2 0 1 6
            using (PregledPrometaContext2016 db = new PregledPrometaContext2016())
            {
                List<DNEVNIPROMETM> tempList2016 = new List<DNEVNIPROMETM>();

                // početni datum
                DateTime _dateFromMinusYEAR = new DateTime();
                _dateFromMinusYEAR = ClarionToDate(DPViewModel.clDateFrom);
                _dateFromMinusYEAR = _dateFromMinusYEAR.AddYears(-2);
                int dateFromMinusYEAR = DateToClarion(_dateFromMinusYEAR);

                // krajnji datum
                DateTime _dateToMinusYEAR = new DateTime();
                _dateToMinusYEAR = ClarionToDate(DPViewModel.clDateTo);
                _dateToMinusYEAR = _dateToMinusYEAR.AddYears(-2);
                int dateToMinusYEAR = DateToClarion(_dateToMinusYEAR);

                // provera prestupne godine i 29.02. - 78590
                if (dateToMinusYEAR == 78589)
                {
                    dateToMinusYEAR = 78590;
                }




                // provera ulogovanog korisnika, ako je 'elbraco' prikazju se sve prodavnice. u suprotnom samo prodavnica za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco"))
                {
                    tempList2016 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM >= dateFromMinusYEAR && p.DATUM <= dateToMinusYEAR)
                   .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2016 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM >= dateFromMinusYEAR && p.DATUM <= dateToMinusYEAR && p.SHPRO == shpro)
                    .ToList();
                }

                               

                List<DNEVNIPROMETM> tempList2016OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2016OrderBySHPRO = tempList2016.OrderBy(x => x.SHPRO).ToList();


                // grupisanje po prodavnicama i agregacija podataka
                DPViewModel.DnevniPrometWithRUC2016 = tempList2016OrderBySHPRO
                 .GroupBy(l => l.SHPRO)
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.First().SHPRO,
                     UKFI = i.Sum(c => c.UKFI),
                     UKIZNOS = i.Sum(c => c.UKIZNOS),
                     UKKES = i.Sum(c => c.UKKES),
                     UKVIRMAN = i.Sum(c => c.UKVIRMAN),
                     UKSTAVKI = i.Sum(c => c.UKSTAVKI),
                     UKKOLICINA = i.Sum(c => c.UKKOLICINA),
                     UKRUC = i.Sum(c => c.UKRUC),
                     RUCProc = (double)(((i.Sum(j => j.UKRUC) / (i.Sum(d => d.UKIZNOS)) * 100))),
                 }).ToList();
            }



            // 2 0 1 7
            using (PregledPrometaContext2017 db = new PregledPrometaContext2017())
            {
                List<DNEVNIPROMETM> tempList2017 = new List<DNEVNIPROMETM>();

                // početni datum
                DateTime _dateFromMinusYEAR = new DateTime();
                _dateFromMinusYEAR = ClarionToDate(DPViewModel.clDateFrom);
                _dateFromMinusYEAR = _dateFromMinusYEAR.AddYears(-1);
                int dateFromMinusYEAR = DateToClarion(_dateFromMinusYEAR);

                // krajnji datum
                DateTime _dateToMinusYEAR = new DateTime();
                _dateToMinusYEAR = ClarionToDate(DPViewModel.clDateTo);
                _dateToMinusYEAR = _dateToMinusYEAR.AddYears(-1);
                int dateToMinusYEAR = DateToClarion(_dateToMinusYEAR);

                //// provera prestupne godine i 29.02. - 78590
                //if (dateToMinusYEAR == 78589)
                //{
                //    dateToMinusYEAR = 78590;
                //}


                //tempList2017 = db.DNEVNIPROMETM
                //    .Where(p => p.DATUM >= dateFromMinusYEAR && p.DATUM <= dateToMinusYEAR)
                //    .ToList();



                // provera ulogovanog korisnika, ako je 'elbraco' prikazju se sve prodavnice. u suprotnom samo prodavnica za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco"))
                {
                    tempList2017 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM >= dateFromMinusYEAR && p.DATUM <= dateToMinusYEAR)
                   .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2017 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM >= dateFromMinusYEAR && p.DATUM <= dateToMinusYEAR && p.SHPRO == shpro)
                    .ToList();
                }



                List<DNEVNIPROMETM> tempList2017OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2017OrderBySHPRO = tempList2017.OrderBy(x => x.SHPRO).ToList();


                // grupisanje po prodavnicama i agregacija podataka
                DPViewModel.DnevniPrometWithRUC2017 = tempList2017OrderBySHPRO
                 .GroupBy(l => l.SHPRO)
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.First().SHPRO,
                     UKFI = i.Sum(c => c.UKFI),
                     UKIZNOS = i.Sum(c => c.UKIZNOS),
                     UKKES = i.Sum(c => c.UKKES),
                     UKVIRMAN = i.Sum(c => c.UKVIRMAN),
                     UKSTAVKI = i.Sum(c => c.UKSTAVKI),
                     UKKOLICINA = i.Sum(c => c.UKKOLICINA),
                     UKRUC = i.Sum(c => c.UKRUC),
                     RUCProc = (double)(((i.Sum(j => j.UKRUC) / (i.Sum(d => d.UKIZNOS)) * 100))),
                 }).ToList();
            }



            // 2 0 1 8
            using (PregledPrometaContext2018 db = new PregledPrometaContext2018())
            {
                List<DNEVNIPROMETM> tempList2018 = new List<DNEVNIPROMETM>();

                //tempList2018 = db.DNEVNIPROMETM
                //    .Where(p => p.DATUM >= DPViewModel.clDateFrom && p.DATUM <= DPViewModel.clDateTo)
                //    .ToList();


                // provera ulogovanog korisnika, ako je 'elbraco' prikazju se sve prodavnice. u suprotnom samo prodavnica za ulogovanog korisnika
                if (podaciOperatera[2].Equals("elbraco"))
                {
                    tempList2018 = db.DNEVNIPROMETM
                   .Where(p => p.DATUM >= DPViewModel.clDateFrom && p.DATUM <= DPViewModel.clDateTo)
                   .ToList();
                }
                else
                {
                    string shpro = podaciOperatera[0];

                    tempList2018 = db.DNEVNIPROMETM
                    .Where(p => p.DATUM >= DPViewModel.clDateFrom && p.DATUM <= DPViewModel.clDateTo && p.SHPRO == shpro)
                    .ToList();
                }

                

                List<DNEVNIPROMETM> tempList2018OrderBySHPRO = new List<DNEVNIPROMETM>();
                tempList2018OrderBySHPRO = tempList2018.OrderBy(x => x.SHPRO).ToList();

                // grupisanje po prodavnicama i agregacija podataka
                DPViewModel.DnevniPrometWithRUC2018 = tempList2018OrderBySHPRO
                 .GroupBy(l => l.SHPRO)
                 .Select(i => new DnevniPrometWruc()
                 {
                     SHPRO = i.First().SHPRO,
                     UKFI = i.Sum(c => c.UKFI),
                     UKIZNOS = i.Sum(c => c.UKIZNOS),
                     UKKES = i.Sum(c => c.UKKES),
                     UKVIRMAN = i.Sum(c => c.UKVIRMAN),
                     UKSTAVKI = i.Sum(c => c.UKSTAVKI),
                     UKKOLICINA = i.Sum(c => c.UKKOLICINA),
                     UKRUC = i.Sum(c => c.UKRUC),
                     RUCProc = (double)(((i.Sum(j => j.UKRUC) / (i.Sum(d => d.UKIZNOS)) * 100))),
                 }).ToList();
            }




        }





        /* CLARION KONVERZIJE ZA DATUM I VREME */

        public DateTime ClarionToDate(int clarionDatum)
        {
            // KONVERZIJA Clarion(int) TO DateTime
            var date = new DateTime(1800, 12, 28, 0, 0, 0);
            var theDate = date.AddDays(clarionDatum);
            return theDate;
        }


        public int DateToClarion(DateTime datumToClarion)
        {
            //KONVERZIJA DateTime TO Clarion(int)            
            var date = new DateTime(1800, 12, 28, 0, 0, 0);
            var daysSince = (datumToClarion - date).Days;
            return daysSince;
        }


        public DateTime ClarionTimeToRegularTime(int clarionTime, DateTime baseDate)
        {
            // KONVERZIJA ClarionTIME u regularno vreme DateTime
            // baseDate je datum za dan za koji se računa vreme npr. izračunati vreme za 20.10.2016 - baseDate je 20.10.2016            

            int lHours = Convert.ToInt32(clarionTime / 360000);
            int lMinutes = Convert.ToInt32((clarionTime - (lHours * 360000)) / 6000);
            int lSeconds = Convert.ToInt32((clarionTime - (lHours * 360000) - (lMinutes * 6000)) / 100);
            int lMiliseconds = (clarionTime - (lHours * 360000) - (lMinutes * 6000) - (lSeconds * 100)) * 10;

            DateTime then = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, lHours, lMinutes, lSeconds);
            return then;
        }


        private int idkase(short _idk)
        {
            int idk;
            idk = Convert.ToInt32(_idk);

            return idk;
        }



        ///** 
        // * LOGIN Helpers 
        // */

        //public List<string> LogInUserAndGetPodaciOperatera(string txtPwd, string txtUser)
        //{
        //    List<string> podaciOperatera = new List<string>();

        //    using (ELBS_Connection = CreateElbsSqlConnection(txtUser, txtPwd))
        //    {
        //        try
        //        {
        //            ELBS_Connection.Open();
        //            if (ELBS_Connection.State == System.Data.ConnectionState.Open)
        //            {
        //                podaciOperatera = GetPodaciOperatera(txtUser);
        //                return podaciOperatera;
        //            }
        //            else return null;
                    
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }

        //}
        


        //private List<string> GetPodaciOperatera(string txtUser)
        //{
        //    List<string> podaciOperatera = new List<string>();

        //    try
        //    {
        //        string selectLoggedUser = "SELECT shpro, naziv, userid FROM operater WHERE userid='" + txtUser + "'";

        //        SqlCommand komanda = new SqlCommand(selectLoggedUser, ELBS_Connection);
        //        SqlDataReader dr = komanda.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            podaciOperatera.Add(dr[0].ToString());
        //            podaciOperatera.Add(dr[1].ToString());
        //            podaciOperatera.Add(dr[2].ToString());
        //        }
        //        dr.Close();

        //        return podaciOperatera;
        //    }
        //    catch (Exception ex)
        //    {                
        //        return null;
        //    }
        //}


        //private SqlConnection CreateElbsSqlConnection(string txtUser, string txtPwd)
        //{
        //    ELBS_ConnString = ELBS_ConnString + ";User ID=" + txtUser + ";Password=" + txtPwd;

        //    SqlConnection elbsConnection = new SqlConnection(ELBS_ConnString);

        //    return elbsConnection;
        //}

    }
}