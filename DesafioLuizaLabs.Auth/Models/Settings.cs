using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioLuizaLabs.Auth.Models
{
    public class Settings
    {
        public string Secret { get; set; }
        public string EmailServidor { get; set; }
        public string EmailPorta { get; set; }
        public string EmailUsuario { get; set; }
        public string EmailSenha { get; set; }
        public string EmailDe { get; set; }
    }
}
