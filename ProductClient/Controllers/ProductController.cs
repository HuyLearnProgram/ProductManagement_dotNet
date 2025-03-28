using Microsoft.AspNetCore.Mvc;
using ProductManagementModule;
using ProductClient.ViewModels;
public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    public ActionResult Index()
    {
        var products = _productService.GetAllProducts();
        return View(products);
    }

    public ActionResult Details(long id)
    {
        var product = _productService.GetProductById(id);
        if (product == null) return HttpNotFound();
        return View(product);
    }

    private const int PageSize = 18;
    public ActionResult ProductView(int page = 1)
    {

        // Lấy tổng số sản phẩm
        
        var productCount = _productService.GetAllProducts().Count();

        // Lấy danh sách sản phẩm cho trang hiện tại
        var products = _productService.GetAllProducts()
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();
            ;

        // Tính toán tổng số trang
        var totalPages = (int)Math.Ceiling((double)productCount / PageSize);

        // Truyền dữ liệu vào View
        var model = new ProductViewModel
        {
            Products = products,
            CurrentPage = page,
            TotalPages = totalPages
        };

        return View("ProductView", model);
    }

    //public ActionResult AddProduct(long id)
    //{

    //}

    public ActionResult ProductDetailView(int id = 1)
    {

        var product = _productService.GetProductById(id);
            //.SingleOrDefault(p => p.Id == id);

        // Truyền dữ liệu vào View
        var model = new ProductViewModel
        {
            ProductDetail = product,
        };

        return View("ProductDetailView", model);
    }

    private ActionResult HttpNotFound()
    {
        throw new NotImplementedException();
    }
}
