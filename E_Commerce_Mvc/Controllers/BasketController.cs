using E_Commerce_Mvc.Areas.Identity.Data;
using E_Commerce_Mvc.Models;
using E_Commerce_Mvc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Mvc.Constans;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Mvc.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductService _productService;
        private readonly E_Commerce_MvcContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasketController(UserManager<ApplicationUser> userManager, E_Commerce_MvcContext context, IProductService productService)
        {
            _userManager = userManager;
            _productService = productService;
            _context = context;
        }

        [HttpPost("AddProductToBasket/{productId}")]
        public async Task<IActionResult> AddBasketItem([FromRoute] int productId)
        {
            List<ProductBasketModel> _products = new List<ProductBasketModel>();
            ProductBasketModel _model = new ProductBasketModel();
            var userEmail = HttpContext.User.Identity.Name;
            var product = await _productService.GetProduct(productId);
            var category = await _context.Categories.FindAsync(product.Data.CategoryId);
            _model.ProductDescription = product.Data.ProductDescription;
            _model.ProductName = product.Data.ProductName;
            _model.Price = product.Data.Price;
            _model.ImageUrl = product.Data.ImageUrl;
            _model.CategoryName = category.CategoryName;
            _model.Id = product.Data.Id;
            var basket = HttpContext.Session.GetString(Key.Basket_Items);
            if (basket != null)
            {
                var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
                foreach (var item in basket_items)
                {
                    _products.Add(item);
                }
                _products.Add(_model);
            }
            else
            {
                _products.Add(_model);
            }

            var user = await _userManager.FindByNameAsync(userEmail);
            string serializeProduct = JsonConvert.SerializeObject(_products);
            string serializeUser = JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString(Key.Basket_Items, serializeProduct);
            HttpContext.Session.SetString(Key.Users, serializeUser);
            return RedirectToAction("Index", "ProductClient");

        }

        [HttpGet("GetMyBasketItems")]
        public async Task<IActionResult> Index()
        {
            var basket = HttpContext.Session.GetString(Key.Basket_Items);



            if (string.IsNullOrEmpty(basket))
            {
                return View(new List<ProductBasketModel>());
            }
            else
            {
                var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
                return View(basket_items);
            }


        }
        
        
        [HttpPost("DeleteItem/{productId}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int productId)
        {
            var basket = HttpContext.Session.GetString(Key.Basket_Items);
            var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
            foreach (var item in basket_items)
            {
                if (item.Id == productId)
                {
                    basket_items.Remove(item);
                    string serializeProduct = JsonConvert.SerializeObject(basket_items);
                    HttpContext.Session.SetString(Key.Basket_Items, serializeProduct);
                    return RedirectToAction("Index", "Basket");
                }

            }
            return RedirectToAction("Index", "Basket");
        }


    
}
}
