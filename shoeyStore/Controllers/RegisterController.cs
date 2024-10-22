using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ClientViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            //If model is valid it will create a new Client and send it to the database
            using (var db = new ShoeyDatabaseEntities())
            {
                    Cliente clientTO = new Cliente();
                    clientTO.NombreCliente = model.NombreCliente;
                    clientTO.CorreoElectronico = model.CorreoElectronico;
                    clientTO.Contrasenna = model.Contrasenna;

                    db.Cliente.Add(clientTO);
                    db.SaveChanges();
                    //Returns to Home Screen and enables the Toast to confirm registration
                    return Redirect(Url.Content("~/Home/Index?showToast=registrationSuccessful"));
            }
        }
    }
}