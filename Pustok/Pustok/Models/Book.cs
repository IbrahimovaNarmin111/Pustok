﻿namespace Pustok.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<BookImage> BookImages { get; set; }
        public List<BookTag> BookTags { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        
    }
}
