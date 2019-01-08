using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PregledPrometa.Web.Models;

namespace PregledPrometa.Web.Controllers
{
    public class LoginController : Controller
    {

        private HttpCookie authCookie { get; set; }

        // GET: Login
        public ActionResult Loader()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkuser(string txtPwd, string txtUser)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(txtPwd))
                {
                    return RedirectToAction("Loader");
                }
                else
                {
                    //Repository repository = new Repository();
                    LoginHelper loginHelper = new LoginHelper();
                    List<string> podaciOperatera = loginHelper.LogInUserAndGetPodaciOperatera(txtPwd, txtUser);


                    /**
                     Introduction to cookies
                     https://www.c-sharpcorner.com/UploadFile/225740/cookies/
                     */

                    /**
                     * ASP.NET MVC 404 Error Handling 
                     https://stackoverflow.com/questions/717628/asp-net-mvc-404-error-handling
                     */

                    if (podaciOperatera != null)
                    {
                        DPViewModel.PodaciOperatera = podaciOperatera;



                        // set authentication cookie
                        FormsAuthentication.SetAuthCookie(txtUser, false);

                        string userData = txtUser + ";" + txtPwd;

                        // set authorization ticket
                        var authTicket = new FormsAuthenticationTicket(1, txtUser, DateTime.Now, DateTime.Now.AddDays(2), true, userData);

                        // encrypt ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(authCookie);

                        // check authentication
                        bool isAuth = HttpContext.User.Identity.IsAuthenticated;

                        if (isAuth)
                        {
                            return RedirectToAction("Dnevni", "Promet");
                        } else
                        {
                            return RedirectToAction("Loader");
                        }

                    }
                    else
                    {
                        return RedirectToAction("Loader");
                    }
                }
            }
            return RedirectToAction("Loader");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(".ASPXAUTH");

            return RedirectToAction("Loader");
        }

    }
}