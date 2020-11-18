using Microsoft.AspNetCore.Identity;
using Restaurant_Angular.Business.Constants;
using Restaurant_Angular.Common.DTOs;
using Restaurant_Angular.Common.Result_Constant;
using Restaurant_Angular.Common.ResultConstant;
using Restaurant_Angular.Data.DbModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Angular.Business.Implementaion
{
    public class ApplicationUserBusiness : IApplicationUserBusiness
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserBusiness(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<object>> CreateAppUser(ApplicationUserDto model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Fullname = model.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);

                if (result.Errors.Count() > 0)
                {
                    return new Result<object>(false, ResultConstant.RecordNotCreated);
                }
                return new Result<object>(true, ResultConstant.RecordCreated, result);
            }
            catch (Exception)
            {

                return new Result<object>(false, ResultConstant.RecordNotCreated);
            }
        }
    }
}
