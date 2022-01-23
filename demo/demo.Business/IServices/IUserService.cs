using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using demo.Model.ViewModel;

namespace demo.Business.IServices
{
    public interface IUserService
    {
        Task<UserViewModel> AuthorizeUserAsync(UserViewModel model);
    }
}
