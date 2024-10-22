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
    public class SellerLoginController : Controller
    {
        // GET: SellerLogin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string Email, string Password)
        {
            try
            {
                using (var db = new ShoeyDatabaseEntities())
                {
                    var list = from seller in db.Vendedor
                               where seller.CorreoElectronico == Email && seller.Contrasenna == Password
                               select seller;

                    if (list.Count() > 0)
                    {
                        Vendedor user = list.First();
                        Session["SellerLogged"] = user;
                        return Content("200");
                    }
                    else
                    {
                        return Content("Invalid username or password.");
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
            Session["SellerLogged"] = null;
            return RedirectToAction("Index", "SellerLogin");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SellerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ShoeyDatabaseEntities())
            {
                Vendedor sellerTO = new Vendedor();
                sellerTO.NombreVendedor = model.NombreVendedor;
                sellerTO.CorreoElectronico = model.CorreoElectronico;
                sellerTO.Contrasenna = model.Contrasenna;

                db.Vendedor.Add(sellerTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/SellerLogin/Index?showToast=registrationSuccessful"));
            }
        }
    }
}
