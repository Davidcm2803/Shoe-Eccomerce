﻿using shoeyStore.Models;
using shoeyStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            //List of Orders will be populated with the database request
            List<CartViewModel> CartItems = null;

            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    //Fills the list with cart items 
                    CartItems = (from c in db.Carritoes
                                 where c.IDCliente == user.IDCliente
                                 select new CartViewModel
                                 {
                                     IDCarrito = c.IDCarrito,
                                     IDCliente = c.IDCliente,
                                     IDProducto = c.IDProducto,
                                     IDInventario = c.IDInventario,
                                     Cantidad = c.Cantidad,
                                 }).ToList();
                    //Then populates the object reference of the CartViewModel
                    foreach(var cartItem in CartItems) 
                    {
                        cartItem.Cliente = db.Clientes.Find(user.IDCliente);
                        cartItem.Inventario = db.Inventarios.Find(cartItem.IDInventario);
                        cartItem.Producto = db.Productoes.Find(cartItem.IDProducto);
                    }
                }
            }
            CartItems?.Reverse();
            return PartialView(CartItems);//Returns a partial view to be rendered when the cart icon is pressed
        }
        //Same function as above but this one returns a View
        [HttpGet]
        public ActionResult Checkout()
        {
            //List of Orders will be populated with the database request
            List<CartViewModel> CartItems = null;

            //Check on user session to see if it's logged
            var user = (Cliente)Session["Logged"];

            using (ShoeyDatabaseEntities db = new ShoeyDatabaseEntities())
            {
                if (user != null)
                {
                    user = db.Clientes.Include("Tarjetas").Include("Direccions").FirstOrDefault(u => u.IDCliente == user.IDCliente);

                    foreach (var card in user.Tarjetas)
                    {
                        card.Cliente = user;
                    }
                    foreach (var address in user.Direccions)
                    {
                        address.Cliente = user;
                    }
                    Session["Logged"] = user;
                    ViewBag.User = user;

                    CartItems = (from c in db.Carritoes
                                 where c.IDCliente == user.IDCliente
                                 select new CartViewModel
                                 {
                                     IDCarrito = c.IDCarrito,
                                     IDCliente = c.IDCliente,
                                     IDProducto = c.IDProducto,
                                     IDInventario = c.IDInventario,
                                     Cantidad = c.Cantidad,
                                 }).ToList();
                    foreach (var cartItem in CartItems)
                    {
                        cartItem.Cliente = db.Clientes.Find(user.IDCliente);
                        cartItem.Inventario = db.Inventarios.Find(cartItem.IDInventario);
                        cartItem.Producto = db.Productoes.Find(cartItem.IDProducto);
                    }
                }
            }
            CartItems?.Reverse();
            return View(CartItems);
        }
        //Function to add an item to the cart
        [HttpPost]
        public ActionResult Add(CartViewModel model)
        {
            if (!ModelState.IsValid) return Content("The item could not be added.");

            using (var db = new ShoeyDatabaseEntities())
            {
                //First we get the Inventory to confirm there are existing items
                Inventario inventory = db.Inventarios.Find(model.IDInventario);
                if (inventory != null)
                {
                    if (inventory.Cantidad >= model.Cantidad) //Checks if the quantity in Inventory is higher or equal than the one from the Model
                    {
                        Carrito cart = new Carrito();
                        cart.IDCliente = model.IDCliente;
                        cart.IDProducto = model.IDProducto;
                        cart.IDInventario = model.IDInventario;
                        cart.Cantidad = model.Cantidad;

                        db.Carritoes.Add(cart);
                        db.SaveChanges();

                        return Content("Nice pick!");
                    }
                }
            }
            return Content("We're sorry, but it looks like we don't have enough in stock");
        }
        //Function to delete an item from the cart
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                try 
                {
                    var cartTO = db.Carritoes.Find(id);

                    db.Entry(cartTO).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    return Content("200");
                } catch (Exception ex) {
                    return Content("Error:" + ex);
                }
            }
        }
    }
}