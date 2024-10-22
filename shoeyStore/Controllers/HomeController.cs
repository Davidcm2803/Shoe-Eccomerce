using shoeyStore.Models.ViewModels;
using shoeyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoeyStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Carousel() 
        {
            using (var db = new ShoeyDatabaseEntities())
            {
                // Start with all products
                IQueryable<ProductTableViewModel> query = db.Producto
                    .Select(p => new ProductTableViewModel
                    {
                        IDProductoes = p.IDProducto,
                        IDVendedors = p.IDVendedor,
                        Nombres = p.Nombre,
                        Categorias = p.Categoria,
                        Generos = p.Genero,
                        Marcas = p.Marca,
                        Modelos = p.Modelo,
                        Imagens = p.Imagen,
                        InventoryEntries = db.Inventario.Where(i => i.IDProducto == p.IDProducto).Select(i => new InventoryViewModel
                        {
                            IDInventario = i.IDInventario,
                            TallaUS = i.TallaUS,
                            Cantidad = i.Cantidad,
                            Precio = i.Precio,
                        }).ToList(),
                        Calificaciones = p.Calificacions,
                        DetallesOrdens = p.DetallesOrdens,
                        Vendedor = p.Vendedors
                    });


                // Now 'query' contains the filtered products
                var products = query.ToList();
                Random rng = new Random();
                products.Sort((a, b) => rng.Next(3) - 1);

                // You can pass the 'products' to the view
                return View(products);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}