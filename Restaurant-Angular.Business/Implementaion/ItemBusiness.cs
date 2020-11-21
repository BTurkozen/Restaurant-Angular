using Restaurant_Angular.Business.Constracts;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using Restaurant_Angular.Common.ResultConstant;
using Restaurant_Angular.Data.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_Angular.Business.Implementaion
{
    public class ItemBusiness : IItemBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result<List<ItemDto>> GetItems()
        {
            List<ItemDto> items = new List<ItemDto>();

            var data = _unitOfWork.itemRepository.GetAll().ToList();

            if (data != null)
            {
                foreach (var item in items)
                {
                    items.Add(new ItemDto() {

                        ItemId = item.ItemId,
                        Name = item.Name,
                        Price = item.Price

                    });
                }
                return new Result<List<ItemDto>>(true, ResultConstant.RecordFound, items);
            }

            return new Result<List<ItemDto>>(false, ResultConstant.RecordNotFound);

        }

    }
}
