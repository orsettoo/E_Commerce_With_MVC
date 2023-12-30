using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_Mvc.Services.Abstract
{
    public interface IOrderService
    {
        Task<ServiceResponse<Order>> CreateOrder(OrderDTO model,List<Product> _product);
        Task<ServiceResponse<List<Order>>> ListOrders();
        Task<ServiceResponse<Order>> GetOrderById(int Id);

        Task<ServiceResponse<List<Order>>> MyOrders(string userId);

        Task<bool> IsMyProduct(string userId,int productId);
    }
}
