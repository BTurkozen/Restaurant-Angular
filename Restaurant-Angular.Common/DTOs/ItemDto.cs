using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Common.DTOs
{
   public class ItemDto
    {

        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}
