using Pustok.Models;

namespace Pustok.ViewModels
{
    public class BookDetailVM
    {
        public Book CurrentBook { get; set; }
        public List<Book> RelatedBooks { get; set;}
    }
}
