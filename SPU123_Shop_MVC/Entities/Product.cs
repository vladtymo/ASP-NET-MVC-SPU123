﻿namespace SPU123_Shop_MVC.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public decimal? Discout { get; set; }
        public string? Description { get; set; }

        // ---------- navigation properties
        public Category Category { get; set; }
    }
}