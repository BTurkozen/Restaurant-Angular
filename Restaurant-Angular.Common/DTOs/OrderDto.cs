using System.Collections.Generic;

namespace Restaurant_Angular.Common.DTOs
{
    public class OrderDto
    {

        public OrderSubDto OrderSubDto  { get; set; }

        public List<OrderItemModelDto> orderItemModelDtos { get; set; }

    }
}
