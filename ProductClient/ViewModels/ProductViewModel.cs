using ProductManagementModule;
namespace ProductClient.ViewModels
{
    public class ProductViewModel
    {
        public List<Product>? Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public Product? ProductDetail { get; set; }
    }
}
