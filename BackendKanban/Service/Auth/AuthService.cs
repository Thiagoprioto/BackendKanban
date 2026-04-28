using BackendKanban.Data;
using BackendKanban.DTO.Auth;

namespace BackendKanban.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly KanbanDbContext _context;
        public AuthService(KanbanDbContext context)
        {
            _context = context;
        }
        public Task<AuthDTO> CriarUsuario(AuthCreateDTO authDto)
        {
            try 
            {
                var usuario = new Models.AuthModels
                {
                    Nome = authDto.Nome,
                    Email = authDto.Email,
                    Senha = authDto.Senha
                };
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                var authDtoResult = new AuthDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                };
                return Task.FromResult(authDtoResult);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o usuário.", ex);
            }
        }

        public Task<AuthDTO> AtualizarUsuario(int id, AuthUpdateDTO authUpdateDto)
        {
            try 
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                usuario.Nome = authUpdateDto.Nome;
                usuario.Email = authUpdateDto.Email;
                usuario.Senha = authUpdateDto.Senha;
                _context.SaveChanges();
                var authDtoResult = new AuthDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                };
                return Task.FromResult(authDtoResult);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o usuário.", ex);
            }
        }

        public Task<AuthDTO> LoginUsuario(string nome, string senha, AuthLoginDTO authLoginDto)
        {
            try 
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
                if (usuario == null)
                {
                    throw new Exception("Credenciais inválidas.");
                }
                var authDtoResult = new AuthDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                };
                return Task.FromResult(authDtoResult);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao realizar o login.", ex);
            }
        }

        public Task<AuthDTO> ObterUsuarioPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletarUsuario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
