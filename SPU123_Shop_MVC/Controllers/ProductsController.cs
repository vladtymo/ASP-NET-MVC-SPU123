using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using SPU123_Shop_MVC.Models;
using SPU123_Shop_MVC.Interfaces;

namespace SPU123_Shop_MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly ShopDbContext context;
        private readonly IFileService fileService;

        public ProductsController(ShopDbContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
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
        public async Task<IActionResult> Create(CreateProductModel product)
        {
            // model validation
            if (!ModelState.IsValid) // using model metadata
            {
                LoadCategories();
                return View("Create");
            }

            // save product image
            string path = await fileService.UploadImage(product.ImageFile);

            Product entity = new()
            {
                Name = product.Name,
                Discout = product.Discout,
                CategoryId = product.CategoryId,
                Description = product.Description,
                InStock = product.InStock,
                Price = product.Price,
                ImageUrl = path
            };

            // add to db
            context.Products.Add(entity);
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
