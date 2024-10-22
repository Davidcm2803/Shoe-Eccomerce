using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Access()
        {
            List<AccessTableViewModel> AccessList = new List<AccessTableViewModel>();
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    AccessList = (from e in db.Cliente
                                  where e.IDCliente == user.IDCliente
                                  orderby e.IDCliente
                                  select new AccessTableViewModel
                                  {
                                      CorreoElectronico = e.CorreoElectronico,
                                      Contrasenna = e.Contrasenna,
                                  }).ToList();
                }
            }
            return View(AccessList);
        }
        [HttpGet]
        public ActionResult AccessEdit()
        {
            var user = (Cliente)Session["Logged"];

            AccessViewModel accessData = new AccessViewModel();

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    var userData = db.Cliente.FirstOrDefault(e => e.IDCliente == user.IDCliente);

                    if (userData != null)
                    {
                        accessData.CorreoElectronico = userData.CorreoElectronico;
                        accessData.Contrasenna = userData.Contrasenna;
                    }
                }
            }

            return View(accessData);
        }

        [HttpPost]
        public ActionResult AccessEdit(AccessViewModel accessData)
        {
            var user = (Cliente)Session["Logged"];

            if (!ModelState.IsValid)
            {
                return View(accessData);
            }

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    var userData = db.Cliente.FirstOrDefault(e => e.IDCliente == user.IDCliente);

                    if (userData != null)
                    {
                        userData.CorreoElectronico = accessData.CorreoElectronico;
                        userData.Contrasenna = accessData.Contrasenna;

                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Access");
        }




        [HttpGet]
        public ActionResult Shipping()
        {
            List<AddressTableViewModel> AddressList = new List<AddressTableViewModel>();
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    AddressList = (from e in db.Direccion
                                   where e.IDCliente == user.IDCliente
                                   orderby e.Nombre
                                   select new AddressTableViewModel
                                   {
                                       IDDireccion = e.IDDireccion,
                                       Nombre = e.Nombre,
                                       Apellido = e.Apellido,
                                       Linea = e.Linea,
                                       Ciudad = e.Ciudad,
                                       Estado = e.Estado,
                                       ZIP = e.ZIP,
                                       Telefono = e.Telefono,
                                   }).ToList();
                }
            }

            return View(AddressList);
        }


        [HttpGet]
        public ActionResult ShippingAdd()
        {
            AddressViewModel model = new AddressViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult ShippingAdd(AddressViewModel model)
        {
            var user = (Cliente)Session["Logged"];

            if (!ModelState.IsValid) return View(model);

            using (var db = new ShoeyDatabaseEntities())
            {
                Direccion direccionTO = new Direccion();
                direccionTO.IDCliente = user.IDCliente;
                direccionTO.Nombre = model.Nombre;
                direccionTO.Apellido = model.Apellido;
                direccionTO.Linea = model.Linea;
                direccionTO.Ciudad = model.Ciudad;
                direccionTO.Estado = model.Estado;
                direccionTO.ZIP = model.ZIP;
                direccionTO.Telefono = model.Telefono;

                db.Direccion.Add(direccionTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/Settings/Shipping"));
            }
        }

        [HttpGet]
        public ActionResult ShippingEdit(int id)
        {
            AddressViewModel model = new AddressViewModel();
            var user = (Cliente)Session["Logged"];

            using (var db = new ShoeyDatabaseEntities())
            {
                var shipTO = db.Direccion.Find(id);

                if (shipTO != null)
                {
                    model.IDDireccion = shipTO.IDDireccion;
                    model.Nombre = shipTO.Nombre;
                    model.Apellido = shipTO.Apellido;
                    model.Linea = shipTO.Linea;
                    model.Ciudad = shipTO.Ciudad;
                    model.Estado = shipTO.Estado;
                    model.ZIP = shipTO.ZIP;
                    model.Telefono = shipTO.Telefono;
                    model.IDDireccion = shipTO.IDDireccion;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ShippingEdit(AddressViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            using (var db = new ShoeyDatabaseEntities())
            {
                var shipTO = db.Direccion.Find(model.IDDireccion);
                if (shipTO != null)
                {
                    shipTO.IDDireccion = model.IDDireccion;
                    shipTO.Nombre = model.Nombre;
                    shipTO.Apellido = model.Apellido;
                    shipTO.Linea = model.Linea;
                    shipTO.Ciudad = model.Ciudad;
                    shipTO.Estado = model.Estado;
                    shipTO.ZIP = model.ZIP;
                    shipTO.Telefono = model.Telefono;

                    db.Entry(shipTO).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Shipping", "Settings");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            //List of Orders will be populated with the database request
            List<CardViewModel> cardsList = new List<CardViewModel>();
            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    user = db.Cliente.Include("Tarjetas").FirstOrDefault(u => u.IDCliente == user.IDCliente);
                    foreach (var card in user.Tarjetas)
                    {
                        card.Clientes = user;
                    }
                    Session["Logged"] = user;

                    foreach (var card in user.Tarjetas)
                    {
                        CardViewModel cardTO = new CardViewModel
                        {
                            IDTarjeta = card.IDTarjeta,
                            IDCliente = card.IDCliente,
                            Numero = card.Numero,
                            Expiracion = card.Expiracion,
                            CVV = card.CVV,
                        };
                        cardsList.Add(cardTO);
                    }

                    return View(cardsList);
                }
            }


            return View();
        }

        [HttpGet]
        public ActionResult PaymentAdd()
        {
            CardViewModel model = new CardViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PaymentAdd(CardViewModel model)
        {
            var user = (Cliente)Session["Logged"];

            if (!ModelState.IsValid) return View(model);

            using (var db = new ShoeyDatabaseEntities())
            {
                Tarjeta accountTO = new Tarjeta();
                accountTO.IDCliente = user.IDCliente;
                accountTO.Numero = model.Numero;
                accountTO.Expiracion = model.Expiracion;
                accountTO.CVV = model.CVV;

                db.Tarjeta.Add(accountTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/Settings/Payment"));
            }
        }

        [HttpPost]
        public ActionResult DeleteCard(int IDTarjetas)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                var cardTO = db.Tarjeta.Find(IDTarjetas);

                if (cardTO != null)
                {
                    db.Entry(cardTO).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return Content("200");
                }
                else
                {
                    return Content("Error: Card not found");
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteShipping(int IDDireccion)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                var shippingTO = db.Direccion.Find(IDDireccion);

                if (shippingTO != null)
                {
                    db.Direccion.Remove(shippingTO);
                    db.SaveChanges();
                    return Content("200");
                }
                else
                {
                    return Content("Error: Card not found");
                }
            }
        }
    }
}