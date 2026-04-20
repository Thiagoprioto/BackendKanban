using BackendKanban.Data;
using BackendKanban.DTO;
using BackendKanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendKanban.Controllers
{
    [Route("v1")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly KanbanDbContext _context;

        public TarefasController(KanbanDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaReadDTO>>> GetTarefas()
        {
            return await _context.Tarefa.ToListAsync();
        }

        [HttpPost]
        [Route("novaTarefa")]
        public async Task<ActionResult<TarefaReadDTO>> CriarTarefa(TarefaCreateDTO tarefaDto)
        {
            var novaTarefa = new Models.Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                Status = tarefaDto.Status,
                DataCriacao = DateTime.UtcNow
            };

            // Salva no banco
            _context.Tarefa.Add(novaTarefa);
            await _context.SaveChangesAsync();

            var respostaDto = new TarefaReadDTO
            {
                Id = novaTarefa.Id,
                Titulo = novaTarefa.Titulo,
                Descricao = novaTarefa.Descricao,
                Status = novaTarefa.Status,
                DataCriacao = novaTarefa.DataCriacao
            };

            return CreatedAtAction(nameof(GetTarefas), new { id = respostaDto.Id }, respostaDto);
        }

        [HttpPut]
        [Route("atualizarTarefa/{id}")]
        public async Task<ActionResult<TarefaReadDTO>> AtualizarTarefa(int id, TarefaUpdateDTO tarefaUpdateDto)
        {
            var tarefaExistente = await _context.Tarefa.FindAsync(id);
            if (tarefaExistente == null)
            {
                return NotFound();
            }
            tarefaExistente.Titulo = tarefaUpdateDto.Titulo;
            tarefaExistente.Descricao = tarefaUpdateDto.Descricao;
            tarefaExistente.Status = tarefaUpdateDto.Status;
            _context.Entry(tarefaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var respostaDto = new TarefaReadDTO
            {
                Id = tarefaExistente.Id,
                Titulo = tarefaExistente.Titulo,
                Descricao = tarefaExistente.Descricao,
                Status = tarefaExistente.Status,
                DataCriacao = tarefaExistente.DataCriacao
            };
            return Ok(respostaDto);
        }

        [HttpDelete]
        [Route("deletarTarefa/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Tarefa.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Tarefa.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}