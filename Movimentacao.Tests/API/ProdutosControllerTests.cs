using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movimentacao.API.Controllers;
using Movimentacao.Application.Querys;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Movimentacao.Tests.API.Controllers
{
    public class ProdutosControllerTests
    {
        private readonly IMediator _mediator;
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new ProdutosController(_mediator);
        }

        [Fact]
        public async Task GetAllAsync_QuandoExecutadoComSucesso_RetornaOkComListaDeProdutos()
        {
            // Arrange
            var response = new List<GetProdutoResponse>
            {
                new GetProdutoResponse { CodProduto = "0001", DesProduto = "Produto A" }
            };

            _mediator.Send(Arg.Any<GetProdutoRequest>())
                     .Returns(Task.FromResult((IEnumerable<GetProdutoResponse>)response));

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var produtos = Assert.IsAssignableFrom<IEnumerable<GetProdutoResponse>>(okResult.Value);
            Assert.Single(produtos);
        }

        [Fact]
        public async Task GetAllAsync_QuandoOcorreErro_Retorna500()
        {
            // Arrange
            var exceptionMessage = "Erro inesperado";
            _mediator.Send(Arg.Any<GetProdutoRequest>())
                     .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal(exceptionMessage, objectResult.Value);
        }

        [Fact]
        public async Task GetAllCosifByProdutoAsync_QuandoExecutadoComSucesso_RetornaOkComListaDeCosif()
        {
            // Arrange
            var codProduto = "0001";
            var response = new List<GetProdutoCosifResponse>
            {
                new GetProdutoCosifResponse { CodCosif = "1.1.1.01.00", CodProduto = codProduto }
            };

            _mediator.Send(Arg.Any<GetProdutoCosifRequest>())
                     .Returns(Task.FromResult((IEnumerable<GetProdutoCosifResponse>)response));

            // Act
            var result = await _controller.GetAllCosifByProdutoAsync(codProduto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var cosifs = Assert.IsAssignableFrom<IEnumerable<GetProdutoCosifResponse>>(okResult.Value);
            Assert.Single(cosifs);
        }

        [Fact]
        public async Task GetAllCosifByProdutoAsync_QuandoOcorreErro_Retorna500()
        {
            // Arrange
            var codProduto = "0001";
            var exceptionMessage = "Erro inesperado";

            _mediator.Send(Arg.Any<GetProdutoCosifRequest>())
                     .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAllCosifByProdutoAsync(codProduto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal(exceptionMessage, objectResult.Value);
        }
    }
}
