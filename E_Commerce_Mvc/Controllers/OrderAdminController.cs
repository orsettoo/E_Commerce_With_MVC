using E_Commerce_Mvc.Areas.Identity.Data;
using E_Commerce_Mvc.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Mvc.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderAdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly E_Commerce_MvcContext _context;


        public OrderAdminController(IOrderService orderService, E_Commerce_MvcContext context)
        {
            _orderService = orderService;
            _context = context;
        }
        [HttpGet("OrderSettings")]
        public async Task<IActionResult> Index()
        {
            var result = await _orderService.ListOrders();
            if (result != null)
            {
                return View(result.Data);
            }
            return View(result);
        }


        [HttpPost("ChangeStatus/{orderId}")]
        public async Task<IActionResult> StatusCreate([FromRoute] int orderId, string orderStatus)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order != null)
            {
                order.Data.Status = orderStatus;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "OrderAdmin");

            }
            return BadRequest(order.Message);
        }
    }
}
