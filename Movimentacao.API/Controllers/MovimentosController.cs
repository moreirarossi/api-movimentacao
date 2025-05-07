using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movimentacao.Application.Model;
using Movimentacao.Application.Querys;

namespace Movimentacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo movimento manual
        /// </summary>
        /// <param name="request">Dados da movimentação</param>
        /// <response code="201">Movimentacao criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] PostMovimentoRequest request)
        {
            try
            {
                var id = await _mediator.Send(request);

                return StatusCode(StatusCodes.Status201Created, id);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém vários movimentos manuais
        /// </summary>
        /// <response code="200">Retorna a lista de movimentos manuais</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetMovimentoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetMovimentoRequest());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
