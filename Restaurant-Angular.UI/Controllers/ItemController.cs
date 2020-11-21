using Microsoft.AspNetCore.Mvc;
using Restaurant_Angular.Business.Constracts;
using Restaurant_Angular.Common.DTOs;
using System.Collections.Generic;

namespace Restaurant_Angular.UI.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemBusiness _itemBusiness;

        public ItemController(IItemBusiness itemBusiness)
        {
           _itemBusiness = itemBusiness;
        }

        [HttpGet("GetItems")]
        public List<ItemDto> GetItems() {
            return _itemBusiness.GetItems().Data;
        }
    }
}
