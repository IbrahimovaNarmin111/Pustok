using Pustok.Models;

namespace Pustok.Areas.Admin.ViewModels
{
    public class UpdateBookVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; }

        public List<Author>? Authors { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public IFormFile? MainPhoto { get; set; }

        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<BookImageVm>? BookImagesVm { get; set; }   
        public List<int>? ImageIds { get; set; } 


    }
    public class BookImageVm
    {
        public int Id { get; set; }
        public bool? IsPrimary { get; set; }
        public string Image { get; set; }
    }
}
