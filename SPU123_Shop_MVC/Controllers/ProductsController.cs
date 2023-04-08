using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using System.Xml.Linq;

namespace SPU123_Shop_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext context;
        public ProductsController(ShopDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            // read products from db

            // .Include() - LEFT JOIN in SQL
            var products = context.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        // відкриття сторінки для створення нового продукта
        public IActionResult Create()
        {
            return View();
        }
        // приймає створений об'єкт та додає його в БД
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // model validation
            if (!ModelState.IsValid) // using model metadata
            {
                return View("Create");
            }

            // add to db
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var item = context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        public IActionResult Delete(int id)
        {
            // delete product
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            context.Products.Remove(item);
            context.SaveChanges(); // submit changes to db

            return RedirectToAction("Index");
        }
    }
}
