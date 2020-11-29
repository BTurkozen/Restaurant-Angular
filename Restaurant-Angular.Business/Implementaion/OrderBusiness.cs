using Restaurant_Angular.Business.Constracts;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using Restaurant_Angular.Common.ResultConstant;
using Restaurant_Angular.Data.DataContracts;
using Restaurant_Angular.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Business.Implementaion
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result<List<GetOrderDto>> GetOrders()
        {
            var data = _unitOfWork.orderRepository.GetAll(null, null, "Customer");
            if (data != null)
            {
                List<GetOrderDto> returnModel = new List<GetOrderDto>();
                foreach (var item in data)
                {
                    returnModel.Add(new GetOrderDto()
                    {
                        
                            GrandTotal = item.GrandTotal,
                            OrderNo = item.OrderNo,
                            OrderId = item.OrderId,
                            PaymentMethod = item.PaymentMethod,
                            CustomerName = item.Customer.Name
                        
                    });
                }
                return new Result<List<GetOrderDto>>(true, ResultConstant.RecordFound, returnModel);
            }
            return new Result<List<GetOrderDto>>(false, ResultConstant.RecordNotFound);

        }

        public Result<bool> SaveOrder(OrderDto orderDto)
        {
            try
            {
                Order orderModel = new Order();
                orderModel.CustomerId = Convert.ToInt32(orderDto.OrderSubDto.CustomerId);
                orderModel.GrandTotal = Convert.ToInt32(orderDto.OrderSubDto.GrandTotal);
                orderModel.OrderNo = orderDto.OrderSubDto.OrderNo;
                orderModel.PaymentMethod = orderDto.OrderSubDto.PaymentMethod;

                _unitOfWork.orderRepository.Add(orderModel);

                foreach (var item in orderDto.orderItemModelDtos)
                {
                    OrderItem oItem = new OrderItem();
                    oItem.OrderId = orderModel.OrderId;
                    oItem.Quantity = item.Quantity;
                    oItem.ItemId = Convert.ToInt32(item.ItemId);
                    _unitOfWork.orderItemRepository.Add(oItem);
                }
                _unitOfWork.save();
                return new Result<bool>(true, ResultConstant.RecordCreated);
            }
            catch (Exception ex)
            {

                return new Result<bool>(false, ex.Message.ToString());
            }
        }
    }
}
