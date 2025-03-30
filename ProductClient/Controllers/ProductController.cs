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

    [HttpPost]
    [Route("/product/Edit")]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        
        if (ModelState.IsValid)
        {
            var existingProduct = _productService.GetProductById(model.ProductDetail.Id);
            if (existingProduct == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy sản phẩm
            }

            // Cập nhật các thuộc tính mới
            existingProduct.ProductName = model.ProductDetail.ProductName;
            existingProduct.Price = model.ProductDetail.Price;
            existingProduct.Quantity = model.ProductDetail.Quantity;
            existingProduct.Unit = model.ProductDetail.Unit;
            existingProduct.Description = model.ProductDetail.Description;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadDir, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                existingProduct.ImageUrl = "/images/" + uniqueFileName;
            }

            _productService.UpdateProduct(existingProduct);
            //return RedirectToAction(nameof(Edit), new { id = existingProduct.Id });
            return RedirectToAction("ProductDetailView", new { id = model.ProductDetail.Id });
        }
        return RedirectToAction("ProductDetailView", new { id = model.ProductDetail.Id });
        //return View(model);
    }

    [HttpGet]
    [Route("/product/delete/{id}")]
    public IActionResult Delete(long id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(id);

        // Sau khi xóa, tính toán lại số trang hợp lệ
        var totalProducts = _productService.GetAllProducts().Count();
        var totalPages = (int)Math.Ceiling((double)totalProducts / PageSize);

        // Kiểm tra nếu trang hiện tại không còn sản phẩm nào, giảm số trang xuống
        var currentPage = HttpContext.Request.Query["page"].ToString();
        int page = string.IsNullOrEmpty(currentPage) ? 1 : int.Parse(currentPage);

        if (page > totalPages)
        {
            page = totalPages > 0 ? totalPages : 1;
        }

        return RedirectToAction("ProductView", new { page });
    }

}
