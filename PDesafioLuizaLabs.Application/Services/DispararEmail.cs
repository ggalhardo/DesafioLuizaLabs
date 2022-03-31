using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using PDesafioLuizaLabs.Domain.Entities;

namespace PDesafioLuizaLabs.Application.Services
{
    public static class DispararEmail
    {

        public static void Disparar(Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(email.Servidor);
                SmtpServer.Port = (!string.IsNullOrEmpty(email.Porta)) ? Convert.ToInt32(email.Porta) : 0;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email.Usuario, email.Senha);
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress(email.De);
                mail.To.Add(email.Para);
                mail.Subject = email.Assunto;
                mail.Body = email.Mensagem;
                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);
            }
            catch { }
        }

        public static void DispararRecuperacaoSenha(Email email)
        {
            try
            {
                var variaveis = new Dictionary<string, string>();
                variaveis.Add("senha", email.Variavel);

                var body = GeraEmailByArquivo("recuperacao_senha.html", variaveis);

                email.Assunto = "Recuperação de senha DesafioLuizaLabs";
                email.Mensagem = body;

                Disparar(email);
            }
            catch { }
        }

        public static void DispararCadastro(Email email)
        {
            try
            {
                var variaveis = new Dictionary<string, string>();
                variaveis.Add("nome", email.Variavel);

                var body = GeraEmailByArquivo("cadastro.html", variaveis);

                email.Assunto = "Cadastro realizado DesafioLuizaLabs";
                email.Mensagem = body;

                Disparar(email);
            }
            catch { }
        }

        public static void DispararSenhaAlterada(Email email)
        {
            try
            {

                var variaveis = new Dictionary<string, string>();
                variaveis.Add("nome", email.Variavel);

                var body = GeraEmailByArquivo("senha_alterada.html", variaveis);

                email.Assunto = "Senha alterada DesafioLuizaLabs";
                email.Mensagem = body;

                Disparar(email);
            }
            catch { }
        }

        private static string GeraEmailByArquivo(string arquivo, Dictionary<string, string> variaveis)
        {
            var appPath = Path.Combine("wwwroot\\Templates", $"{arquivo}");
            var body = File.ReadAllText($"{appPath}");

            foreach (var variavel in variaveis)
                body = body.Replace($"##{variavel.Key.ToUpper()}##", variavel.Value);

            return body;
        }

    }
}
