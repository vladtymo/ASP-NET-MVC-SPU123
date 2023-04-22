using Data;
using Microsoft.AspNetCore.Mvc;
using SPU123_Shop_MVC.Helpers;

namespace SPU123_Shop_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext context;

        public CartController(ShopDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<int>? ids = HttpContext.Session.Get<List<int>>("cartData");
            if (ids == null)
                ids = new List<int>();

            // get products by id collection
            var products = ids.Select(id => context.Products.Find(id)).ToList();

            return View(products);
        }

        public IActionResult Add(int id)
        {
            // add product to the cart
            List<int>? ids = HttpContext.Session.Get<List<int>>("cartData");
            if (ids == null)
                ids = new List<int>();

            ids.Add(id);

            HttpContext.Session.Set("cartData", ids);

            return RedirectToAction("Index", "Home");
        }
    }
}
