using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PrettyCash.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace PrettyCash.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["UserDefaultCurrency"] == null)
            {
                var db = new ApplicationDbContext();
                var userId = User.Identity.GetUserId();
                var userCurrency = db.UserCurrency.Where(u => u.ApplicationUser.Id == userId);

                if(userCurrency.Any())
                {
                    Session.Add("UserDefaultCurrency", userCurrency.First().Currency.ISO);
                }
            }

            return View();
        }
    }
}
