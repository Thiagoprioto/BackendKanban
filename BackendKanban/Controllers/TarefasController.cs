using BackendKanban.Data;
using BackendKanban.DTO;
using BackendKanban.Models;
using BackendKanban.Service.Tarefa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendKanban.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefasController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }


        [HttpGet("ListarTarefas")]
        public async Task<ActionResult<IEnumerable<TarefaModel>>> GetTarefas()
        {
            var tarefas = await _tarefaService.ListarTarefas();
            return Ok(tarefas);
        }

        [HttpPost]
        [Route("novaTarefa")]
        public async Task<ActionResult<TarefaModel>> CriarTarefa(TarefaCreateDTO tarefaDto)
        {
            var novaTarefa = await _tarefaService.CriarTarefa(tarefaDto);
            return CreatedAtAction(nameof(GetTarefas), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpPut]
        [Route("atualizarTarefa/{id}")]
        public async Task<ActionResult<TarefaModel>> AtualizarTarefa(int id, TarefaUpdateDTO tarefaUpdateDto)
        {
            var tarefaAtualizada = await _tarefaService.AtualizarTarefa(id, tarefaUpdateDto);
            if (tarefaAtualizada == null)
            {
                return NotFound();
            }
            return Ok(tarefaAtualizada);
        }

        [HttpDelete]
        [Route("deletarTarefa/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var tarefa = await _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            await _tarefaService.DeletarTarefa(id);
            return NoContent();
        }
    }
}