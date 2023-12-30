using E_Commerce_Mvc.Areas.Identity.Data;
using E_Commerce_Mvc.Services.Abstract;
using E_Commerce_Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Mvc.Controllers
{
    public class ProductClientController : Controller
    {
        private readonly IProductService _productService;
        private E_Commerce_MvcContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStarService _starService;

        public ProductClientController(IProductService productService, IStarService starService ,E_Commerce_MvcContext context, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _context = context;
            _userManager = userManager;
            _starService = starService;
        }

       public async Task<IActionResult> Index()
        {
            var result = await _productService.ListProduct();
            if(result != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }

        [HttpGet("productById/{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute]int productId)
        {
            var result = await _productService.GetProduct(productId);
            //var resultPro=await _context.Products.Include( x=>x.ProductImages).Include(x=>x.Features).FirstOrDefaultAsync(x => x.Id==productId);
            if(result.Data != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }
        [HttpGet("GetAll/{page}")]
        public async Task<IActionResult> GetAll ([FromRoute]int page)
        {
            var result = await _productService.GetProducts(page);
            if(result.Data != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }
        
        [HttpGet("GetProductsByCategory/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory([FromRoute] int categoryId)
        {
            var result = await _productService.GetProductsByCategory(categoryId);
            if (result != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }

        [HttpPost("SendRate/{productId}")]
        public async Task<IActionResult> GiveStar (string star, [FromRoute]int productId)
        {
            var userEmail = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);
            StarDTO _star = new StarDTO();
            double starValue = 0; 
            _star.RateStar = double.Parse(star);
            _star.ProductId=productId;
            _star.UserId = user.Id;
            var IsSuccess = await _starService.GiveStarProduct(_star);
            return View(IsSuccess.Message);
        }

    }
}
