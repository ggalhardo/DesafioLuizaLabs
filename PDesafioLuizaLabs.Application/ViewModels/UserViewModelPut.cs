using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Application.ViewModels
{
    public class UserViewModelPut
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public DateTime? DateUpdated { get; set; }

    }
}
