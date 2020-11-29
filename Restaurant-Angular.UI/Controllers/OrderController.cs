using Microsoft.AspNetCore.Mvc;
using Restaurant_Angular.Business.Constracts;
using Restaurant_Angular.Common.DTOs;


namespace Restaurant_Angular.UI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost("SaveOrder")]
        public bool SaveOrder([FromBody]OrderDto orderDto)
        {
            var result = _orderBusiness.SaveOrder(orderDto);
            return result.IsSuccess;
        }
    }
}
