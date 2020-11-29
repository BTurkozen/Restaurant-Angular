﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Common.DTOs
{
   public class GetOrderDto
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public decimal GrandTotal { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}