using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //Obtains parameters for Login and Password
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                using (var db = new ShoeyDatabaseEntities())
                {
                    var list = from user in db.Cliente
                              where user.CorreoElectronico == Email && user.Contrasenna == Password
                              select user;
                    //If the list count is more than 0 it means the list was filled and the user was found 
                    if (list.Count() > 0)
                    {
                        Cliente user = list.First();

                        Session["Logged"] = user;

                        //Return '200' will be read by the View through JS
                        return Content("200");
                    }
                    else
                    {
                        //If list empty, return error 
                        return Content("Invalid username or password");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("The following error has occurred: " + ex.Message);
            }
        }

        public ActionResult Logout()
        {
            //Sets the current session Logged to be null and redirect to Home Screen
            Session["Logged"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}