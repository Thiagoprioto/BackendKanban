using BackendKanban.DTO.Tarefa;

namespace BackendKanban.Service.Tarefa
{
    public interface ITarefaService
    {
            Task<IEnumerable<TarefaReadDTO>> ListarTarefas();
            Task<TarefaReadDTO> CriarTarefa(TarefaCreateDTO tarefaDto);
            Task<TarefaReadDTO> AtualizarTarefa(int id, TarefaUpdateDTO tarefaUpdateDto);
            Task<TarefaReadDTO> ObterTarefaPorId(int id);
            Task DeletarTarefa(int id);
    }
}
