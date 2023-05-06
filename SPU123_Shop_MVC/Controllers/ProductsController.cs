using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace SPU123_Shop_MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private ShopDbContext context;
        public ProductsController(ShopDbContext context)
        {
            this.context = context;
        }

        private void LoadCategories()
        {
            // transfer data to view
            // 1 - using TempData["key"] = value
            //TempData["categoryList"] = context.Categories.ToList();
            // 2 - using ViewBag.PropertyName = value;
            ViewBag.CategoryList = new SelectList(context.Categories.ToList(), "Id", "Name");
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
            LoadCategories();
            return View();
        }
        // приймає створений об'єкт та додає його в БД
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // model validation
            if (!ModelState.IsValid) // using model metadata
            {
                LoadCategories();
                return View("Create");
            }

            // add to db
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            LoadCategories();
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Edit");
            }
            
            context.Products.Update(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [AllowAnonymous] // allow access to unauthorized users
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
