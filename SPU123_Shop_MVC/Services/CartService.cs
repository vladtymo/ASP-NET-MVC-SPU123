﻿using Data;
using Data.Entities;
using SPU123_Shop_MVC.Helpers;
using SPU123_Shop_MVC.Interfaces;

namespace SPU123_Shop_MVC.Services
{
    public class CartService : ICartService
    {
        private readonly ShopDbContext context;
        private readonly HttpContext httpContext;

        public CartService(ShopDbContext context, IHttpContextAccessor accessor)
        {
            this.context = context;
            this.httpContext = accessor.HttpContext;
        }

        public void Add(int id)
        {
            List<int>? ids = httpContext.Session.Get<List<int>>("cartData");
            if (ids == null)
                ids = new List<int>();

            ids.Add(id);

            // save
            httpContext.Session.Set("cartData", ids);
        }

        public IEnumerable<Product> GetAll()
        {
            List<int>? ids = httpContext.Session.Get<List<int>>("cartData");
            if (ids == null) return new List<Product>();

            // get products by id collection
            var products = ids.Select(id => context.Products.Find(id)).ToList();
            return products;
        }

        public int GetCount()
        {
            List<int>? ids = httpContext.Session.Get<List<int>>("cartData");
            if (ids == null) return 0;

            return ids.Count;
        }

        public bool IsInCart(int id)
        {
            List<int>? ids = httpContext.Session.Get<List<int>>("cartData");
            if (ids == null) return false;

            return ids.Contains(id);
        }

        public void Remove(int id)
        {
            List<int>? ids = httpContext.Session.Get<List<int>>("cartData");
            if (ids == null) return;

            ids.Remove(id);

            // save
            httpContext.Session.Set("cartData", ids);
        }
    }
}
