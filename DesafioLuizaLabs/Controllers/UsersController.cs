using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDesafioLuizaLabs.Application.Interfaces;
using PDesafioLuizaLabs.Application.ViewModels;
using System;

namespace DesafioLuizaLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetConsultaUsuarios()
        {
            try
            {
                return Ok(this.userService.GetConsultaUsuarios());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PostCadastro(UserViewModelPost userViewModelPost)
        {
            try
            {
                return Ok(this.userService.PostCadastroUsuario(userViewModelPost));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetUsuarioById(string id)
        {
            try 
            {
                return Ok(this.userService.GetUsuarioById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PutAlterarSenhaUsuarioById(UserViewModelPut userViewModelPut)
        {
            try 
            {
                return Ok(this.userService.PutAlterarSenhaUsuarioById(userViewModelPut));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("forgot_password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PutEsqueciMinhaSenhaByEmail(UserViewModelPut userViewModelPut)
        {
            try
            {
                return Ok(this.userService.PutEsqueciMinhaSenhaByEmail(userViewModelPut));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PostAutenticacao(UserAuthenticateRequestViewModel user)
        {
            try
            {
                return Ok(this.userService.PostAutenticacao(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
