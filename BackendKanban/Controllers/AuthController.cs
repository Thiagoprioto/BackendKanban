using BackendKanban.DTO.Auth;
using BackendKanban.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BackendKanban.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginUsuario([FromBody] AuthLoginDTO authLoginDto)
        {
            var token = await _authService.LoginUsuario(authLoginDto.Nome, authLoginDto.Senha, authLoginDto);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthDTO>> CriarUsuario(AuthCreateDTO authCreateDto)
        {
            var usuario = await _authService.CriarUsuario(authCreateDto);
            if (usuario == null)
            {
                return BadRequest();
            }
            return Ok(usuario);
        }
    }
}
