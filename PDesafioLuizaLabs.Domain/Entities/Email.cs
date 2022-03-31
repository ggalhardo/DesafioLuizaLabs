using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDesafioLuizaLabs.Domain.Entities
{
    public class Email
    {
        
        public string Servidor { get; set; }
        public string Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Variavel { get; set; }

    }
}
