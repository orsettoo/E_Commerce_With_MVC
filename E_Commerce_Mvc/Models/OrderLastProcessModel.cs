using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_Mvc.Models
{
    public class OrderLastProcessModel
    {
        public OrderDTO _orderDTO { get;set;}

        public List<Product> _product { get; set;} = new List<Product>();
    }
}
