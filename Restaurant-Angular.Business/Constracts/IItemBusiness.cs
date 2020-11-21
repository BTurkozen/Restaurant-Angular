using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using System.Collections.Generic;

namespace Restaurant_Angular.Business.Constracts
{
    public interface IItemBusiness
    {
        Result<List<ItemDto>> GetItems();
    }
}
