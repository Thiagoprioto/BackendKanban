using BackendKanban.Data;
using BackendKanban.DTO;

namespace BackendKanban.Service.Tarefa
{
    public class TarefaService : ITarefaService
    {
        private readonly KanbanDbContext _context;
        public TarefaService(KanbanDbContext context)
        {
            _context = context;
        }

        public Task<TarefaReadDTO> AtualizarTarefa(int id, TarefaUpdateDTO tarefaUpdateDto)
        {
            try
            {
                var tarefa = _context.Tarefa.FirstOrDefault(t => t.Id == id);
                if (tarefa == null)
                {
                    throw new Exception("Tarefa não encontrada.");
                }

                tarefa.Titulo = tarefaUpdateDto.Titulo;
                tarefa.Descricao = tarefaUpdateDto.Descricao;
                tarefa.Status = tarefaUpdateDto.Status;

                _context.SaveChanges();

                var tarefaReadDto = new TarefaReadDTO
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Status = tarefa.Status,
                    DataCriacao = tarefa.DataCriacao
                };

                return Task.FromResult(tarefaReadDto);
            }
            catch (Exception ex)
            {
                // Tratamento de erro caso ocorra algum problema ao acessar o banco de dados ou mapear os dados
                throw new Exception("Ocorreu um erro ao atualizar a tarefa.", ex);
            }

        }

        public Task<TarefaReadDTO> CriarTarefa(TarefaCreateDTO tarefaDto)
        {
            try
            {
                var tarefa = new Models.TarefaModel
                {
                    Titulo = tarefaDto.Titulo,
                    Descricao = tarefaDto.Descricao,
                    Status = tarefaDto.Status,
                    DataCriacao = DateTime.UtcNow
                };
                _context.Tarefa.Add(tarefa);
                _context.SaveChanges();
                var tarefaReadDto = new TarefaReadDTO
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Status = tarefa.Status,
                    DataCriacao = tarefa.DataCriacao
                };
                return Task.FromResult(tarefaReadDto);
            }
            catch (Exception ex)
            {
                // Tratamento de erro caso ocorra algum problema ao acessar o banco de dados ou mapear os dados
                throw new Exception("Ocorreu um erro ao criar a tarefa.", ex);
            }
        }

        public Task<IEnumerable<TarefaReadDTO>> ListarTarefas()
        {
            try
            {
                var tarefas = _context.Tarefa.Select(t => new TarefaReadDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    Status = t.Status,
                    DataCriacao = t.DataCriacao
                }).ToList();
                return Task.FromResult<IEnumerable<TarefaReadDTO>>(tarefas);

            }
            catch (Exception ex)
            {
                // Tratamento de erro caso ocorra algum problema ao acessar o banco de dados ou mapear os dados
                throw new Exception("Ocorreu um erro ao listar as tarefas.", ex);
            }
        }
        public async Task DeletarTarefa(int id)
        {
            try
            {
                var tarefa = await _context.Tarefa.FindAsync(id);
                if (tarefa == null)
                {
                    throw new Exception("Tarefa não encontrada.");
                }
                _context.Tarefa.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Tratamento de erro caso ocorra algum problema ao acessar o banco de dados ou mapear os dados
                throw new Exception("Ocorreu um erro ao deletar a tarefa.", ex);
            }
        }
        public Task<TarefaReadDTO> ObterTarefaPorId(int id)
        {
            try
            {
                var tarefa = _context.Tarefa.FirstOrDefault(t => t.Id == id);
                if (tarefa == null)
                {
                    throw new Exception("Tarefa não encontrada.");
                }
                var tarefaReadDto = new TarefaReadDTO
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Status = tarefa.Status,
                    DataCriacao = tarefa.DataCriacao
                };
                return Task.FromResult(tarefaReadDto);
            }
            catch (Exception ex)
            {
                // Tratamento de erro caso ocorra algum problema ao acessar o banco de dados ou mapear os dados
                throw new Exception("Ocorreu um erro ao obter a tarefa por ID.", ex);
            }
        }
    }
}
