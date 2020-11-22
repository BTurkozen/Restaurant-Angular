using Restaurant_Angular.Business.Constracts;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using Restaurant_Angular.Common.ResultConstant;
using Restaurant_Angular.Data.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant_Angular.Business.Implementaion
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result<List<CustomerDto>> GetCustomers()
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            var data = _unitOfWork.customerRepository.GetAll().ToList();
            if (data != null)
            {
                foreach (var customer in data)
                {
                    customers.Add(new CustomerDto()
                    {
                        CustomerId = customer.CustomerId,
                        Name = customer.Name

                    });
                }
                return new Result<List<CustomerDto>>(true, ResultConstant.RecordFound, customers);
            }
            return new Result<List<CustomerDto>>(false, ResultConstant.RecordNotFound);
        }
    }
}
