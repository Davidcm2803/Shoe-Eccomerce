using shoeyStore.Models.ViewModels;
using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class ItemDescriptionController : Controller
    {
        // GET: ItemDescription
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                // Retrieve the product from the database based on its id
                var product = db.Producto.Find(id);
                // Fills a list with the Inventory items where the IDProducto matches the same as in the product to edit 
                List<InventoryViewModel> inventoryList = db.Inventario.Where(i => i.IDProducto == product.IDProducto).Select(i => new InventoryViewModel
                {
                    IDInventario = i.IDInventario,
                    TallaUS = i.TallaUS,
                    Cantidad = i.Cantidad,
                    Precio = i.Precio,
                }).ToList();
                //If no product found, we redirect 
                if (product == null)
                {
                    // Handle the case where the product is not found
                    return RedirectToAction("Products", "SellerAdmin");
                }
                // Map the product
                var viewModel = new ProductViewModel
                {
                    IDProducto = product.IDProducto,
                    IDVendedor = product.IDVendedor,
                    Nombre = product.Nombre,
                    Categoria = product.Categoria,
                    Genero = product.Genero,
                    Marca = product.Marca,
                    Modelo = product.Modelo,
                    ImagenBase64 = Convert.ToBase64String(product.Imagen),
                    InventoryEntries = inventoryList,
                };
                return View(viewModel);
            }
        }
    }
}