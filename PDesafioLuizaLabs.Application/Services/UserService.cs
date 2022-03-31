using DesafioLuizaLabs.Domain.Entities;
using PDesafioLuizaLabs.Application.Interfaces;
using PDesafioLuizaLabs.Application.ViewModels;
using PDesafioLuizaLabs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography;
using DesafioLuizaLabs.Auth.Services;
using DesafioLuizaLabs.Auth.Models;
using PDesafioLuizaLabs.Domain.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PDesafioLuizaLabs.Application.Services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly Settings _settings;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<Settings> settings)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this._settings = settings.Value;
        }

        public List<UserViewModelGet> GetConsultaUsuarios()
        {
            List<UserViewModelGet> _userViewModelGet = new List<UserViewModelGet>();

            IEnumerable<User> _users = this.userRepository.GetAll();

            _userViewModelGet = mapper.Map<List<UserViewModelGet>>(_users);

            return _userViewModelGet;
        }

        public bool PostCadastroUsuario(UserViewModelPost userViewModelPost)
        {

            User _user = this.userRepository.Find(x => x.Email == userViewModelPost.Email);

            if (_user != null)
                throw new Exception("Usuário já cadastrado com o e-mail enviado.");

            if (userViewModelPost.Senha != userViewModelPost.ConfirmacaoSenha)
                throw new Exception("Senha e confirmação não conferem.");

            _user = mapper.Map<User>(userViewModelPost);

            _user.Senha = EncryptPassword(userViewModelPost.Senha);
            _user.ConfirmacaoSenha = EncryptPassword(userViewModelPost.ConfirmacaoSenha);

            this.userRepository.Create(_user);

            DispararEmail.DispararCadastro(
                new Email
                {
                    De = _settings.EmailDe,
                    Para = userViewModelPost.Email,
                    Porta = _settings.EmailPorta,
                    Senha = _settings.EmailSenha,
                    Servidor = _settings.EmailServidor,
                    Usuario = _settings.EmailUsuario,
                    Variavel = userViewModelPost.Name
                });

            return true;
        }

        public UserViewModelGet GetUsuarioById(string id)
        {

            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("O ID do usuário não é válido");

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);

            if (_user == null)
                throw new Exception("Usuário não encontrado");

            return mapper.Map<UserViewModelGet>(_user);
        }

        public bool PutAlterarSenhaUsuarioById(UserViewModelPut userViewModelPut)
        {
            if (!Guid.TryParse(userViewModelPut.Id, out Guid userId))
                throw new Exception("O ID do usuário não é válido");

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);

            _user.Senha = EncryptPassword(userViewModelPut.Senha);
            _user.DateUpdated = DateTime.Now;

            if (_user == null)
                throw new Exception("Usuário não encontrado ou inativo.");

            DispararEmail.DispararSenhaAlterada(
               new Email
               {
                   De = _settings.EmailDe,
                   Para = userViewModelPut.Email,
                   Porta = _settings.EmailPorta,
                   Senha = _settings.EmailSenha,
                   Servidor = _settings.EmailServidor,
                   Usuario = _settings.EmailUsuario,
                   Variavel = _user.Name
               });

            this.userRepository.Update(_user);

            return true;
        }

        public bool PutEsqueciMinhaSenhaByEmail(UserViewModelPut userViewModelPut)
        {
            if (userViewModelPut.Email.Trim()=="")
                throw new Exception("O e-mail informado está vazio.");

            User _user = this.userRepository.Find(x => x.Email == userViewModelPut.Email.Trim() && !x.IsDeleted);

            if (_user == null)
                throw new Exception("Usuário não encontrado ou inativo");

            string senhaAleatoria = "";
            string chars = "abcdefghjkmnpqrstuvwxyz023456789";
            Random random = new Random();
            for (int f = 0; f < 6; f++)
            {
                senhaAleatoria += chars.Substring(random.Next(0, chars.Length - 1), 1);
            }

            _user.Senha = EncryptPassword(senhaAleatoria);
            _user.ConfirmacaoSenha = EncryptPassword(senhaAleatoria);
            _user.DateUpdated = DateTime.Now;

            DispararEmail.DispararRecuperacaoSenha(
                new Email
                {
                    De = _settings.EmailDe,
                    Para = userViewModelPut.Email,
                    Porta = _settings.EmailPorta,
                    Senha = _settings.EmailSenha,
                    Servidor = _settings.EmailServidor,
                    Usuario = _settings.EmailUsuario,
                    Variavel = senhaAleatoria
                });

            this.userRepository.Update(_user);

            return true;
        }

        public UserAuthenticateResponseViewModel PostAutenticacao(UserAuthenticateRequestViewModel user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                throw new Exception("Email/Senha são obrigatporios");

            user.Senha = EncryptPassword(user.Senha);

            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == user.Email.ToLower()
                                                    && x.Senha.ToLower() == user.Senha.ToLower());
            if (_user == null)
                throw new Exception("Usuário não encontrado");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Name),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            //return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModelGet>(_user), TokenService.GenerateToken(_user));
            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModelGet>(_user), tokenHandler.WriteToken(token));
        }

        private string EncryptPassword(string password)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            byte[] encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

    }
}
