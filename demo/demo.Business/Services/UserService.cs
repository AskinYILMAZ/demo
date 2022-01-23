using demo.Business.IServices;
using demo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace demo.Business.Services
{
    public class UserService : IUserService
    {
        //user işlemleri bu servis ile yapılabilir.
        public async Task<UserViewModel> AuthorizeUserAsync(UserViewModel model)
        {
            // user kontrolü burada yapılabilir
            return model;
        }
    }
}
