using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Business.Constracts
{
   public interface ICustomerBusiness
    {
        Result<List<CustomerDto>> GetCustomers();
    }
}
