using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movimentacao.Application.Querys;

namespace Movimentacao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém Produtos 
        /// </summary>
        /// <response code="200">Retorna a lista de Produtos manuais</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetProdutoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _mediator.Send(new GetProdutoRequest());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém Cosifs por Produto
        /// </summary>
        /// <response code="200">Retorna a lista de Produtos manuais</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{codProduto}/cosif")]
        [ProducesResponseType(typeof(IEnumerable<GetProdutoCosifResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCosifByProdutoAsync([FromRoute] string codProduto)
        {
            try
            {
                var response = await _mediator.Send(new GetProdutoCosifRequest() { CodProduto = codProduto });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
