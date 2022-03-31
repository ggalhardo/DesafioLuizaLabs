using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Application.ViewModels
{
    public class UserAuthenticateResponseViewModel
    {
        public UserAuthenticateResponseViewModel(UserViewModelGet user, string token)
        {
            this.User = user;
            this.Token = token;
        }

        public UserViewModelGet User { get; set; }
        public string Token { get; set; }
    }
}
