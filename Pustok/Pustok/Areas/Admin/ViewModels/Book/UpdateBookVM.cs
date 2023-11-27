using Pustok.Models;

namespace Pustok.Areas.Admin.ViewModels.Book
{
    public class UpdateBookVM
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public List<Author>? Authors { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
