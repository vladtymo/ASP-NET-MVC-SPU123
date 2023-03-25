using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPU123_Shop_MVC.Data;
using SPU123_Shop_MVC.Entities;
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
