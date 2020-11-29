using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Business.Constracts
{
    public interface IOrderBusiness
    {
        Result<bool> SaveOrder(OrderDto orderDto);
        Result<List<GetOrderDto>> GetOrders();
        Result<bool> DeleteOrder(int orderId);
    }
}
