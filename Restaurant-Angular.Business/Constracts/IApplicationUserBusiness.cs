using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using System.Threading.Tasks;

namespace Restaurant_Angular.Business.Constracts
{
    public interface IApplicationUserBusiness
    {
    public Task<Result<object>> CreateAppUser(ApplicationUserDto model);
        
    }
}
