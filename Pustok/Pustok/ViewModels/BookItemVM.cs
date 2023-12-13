namespace Pustok.ViewModels
{
    public class BookItemVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Image { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double SubTotal { get; set; }
    }
}
