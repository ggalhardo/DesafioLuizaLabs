using PDesafioLuizaLabs.Domain.Models;
using System;

namespace DesafioLuizaLabs.Domain.Entities
{
    public class User: Entity
    {
        public string Name { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Email { get; set; }
    }
}
