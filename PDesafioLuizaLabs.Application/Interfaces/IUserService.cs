using PDesafioLuizaLabs.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Application.Interfaces
{
    public interface IUserService
    {
        List<UserViewModelGet> GetConsultaUsuarios()
        {
            return null;
        }
        bool PostCadastroUsuario(UserViewModelPost userViewModelPost)
        {
            return true;
        }
        UserViewModelGet GetUsuarioById(string id)
        {
            return null;
        }
        bool PutAlterarSenhaUsuarioById(UserViewModelPut userViewModelPut)
        {
            return true;
        }
        bool PutEsqueciMinhaSenhaByEmail(UserViewModelPut userViewModelPut)
        {
            return true;
        }
        UserAuthenticateResponseViewModel PostAutenticacao(UserAuthenticateRequestViewModel user)
        {
            return null;
        }
    }
}
