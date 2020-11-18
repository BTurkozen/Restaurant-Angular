using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using Restaurant_Angular.Data.DbModels;
using System.Threading.Tasks;

namespace Restaurant_Angular.Business.Constants
{
    public interface IApplicationUserBusiness
    {
    public Task<Result<object>> CreateAppUser(ApplicationUserDto model);
        
    }
}
