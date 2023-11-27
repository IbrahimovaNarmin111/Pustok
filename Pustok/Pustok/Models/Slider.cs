using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public IFormFile? ImageFile {  get; set; }
    }
}
