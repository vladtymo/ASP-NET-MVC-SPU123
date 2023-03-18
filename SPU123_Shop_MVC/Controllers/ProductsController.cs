using Microsoft.AspNetCore.Mvc;
using SPU123_Shop_MVC.Models;
using System.Xml.Linq;

namespace SPU123_Shop_MVC.Controllers
{
    public class ProductsController : Controller
    {
        static List<Product> products = new()
        {
            new Product() { Id = 1, Name = "iPhone X", Category = "Electronics", Price = 650 },
            new Product() { Id = 2, Name = "PowerBall", Category = "Sport", Price = 45.5M },
            new Product() { Id = 3, Name = "Nike T-Shirt", Category = "Clothes", Price = 189 },
            new Product() { Id = 4, Name = "Samsung S23", Category = "Electronics", Price = 1200 }
        };

        public ProductsController()
        {
        }

        public IActionResult Index()
        {
            // read products from db

            return View(products);
        }

        public IActionResult Delete(int id)
        {
            // delete product
            var item = products.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            products.Remove(item);

            return RedirectToAction("Index");
        }
    }
}
