using BackendKanban.DTO.Auth;

namespace BackendKanban.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthDTO> CriarUsuario(AuthCreateDTO authDto);
        Task<AuthDTO> AtualizarUsuario(int id, AuthUpdateDTO authUpdateDto);
        Task<AuthDTO> LoginUsuario(string usename, string senha, AuthLoginDTO authLoginDto);
        Task<AuthDTO> ObterUsuarioPorId(int id);
        Task DeletarUsuario(int id);
    }
}
