using Data;
using Microsoft.AspNetCore.Mvc;
using SPU123_Shop_MVC.Helpers;
using SPU123_Shop_MVC.Services;

namespace SPU123_Shop_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(cartService.GetAll());
        }

        public IActionResult Add(int id)
        {
            cartService.Add(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
