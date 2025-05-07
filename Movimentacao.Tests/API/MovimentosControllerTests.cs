using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movimentacao.API.Controllers;
using Movimentacao.Application.Model;
using Movimentacao.Application.Querys;
using Movimentacao.Application.Validations;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Movimentacao.Tests.API.Controllers
{
    public class MovimentosControllerTests
    {
        private readonly IMediator _mediator;
        private readonly MovimentosController _controller;

        public MovimentosControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new MovimentosController(_mediator);
        }

        [Fact]
        public async Task CreateAsync_ComDadosValidos_RetornaOkComMensagem()
        {
            // Arrange
            var request = new PostMovimentoRequest
            {
                DatMes = 1,
                DatAno = 2025,
                CodProduto = "0001",
                CodCosif = "1.1.1.01.00",
                ValValor = 2025.01m,
                DesDescricao = "Teste de unidade"
            };

            _mediator.Send(Arg.Any<PostMovimentoRequest>()).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
            await _mediator.Received(1).Send(Arg.Any<PostMovimentoRequest>());
        }

        [Theory]
        [InlineData(0, 2025, "0001", "1.1.1.01.00", 100.00, "Teste unitário", "Mês é obrigatório.")]
        [InlineData(13, 2025, "0001", "1.1.1.01.00", 100.00, "Teste unitário", "Mês deve ser um número entre 1 e 12.")]
        [InlineData(12, 0, "0001", "1.1.1.01.00", 100.00, "Teste unitário", "Ano é obrigatório.")]
        [InlineData(12, 2025, "", "1.1.1.01.00", 100.00, "Teste unitário", "Produto é obrigatório.")]
        [InlineData(12, 2025, "0001", "", 100.00, "Teste unitário", "Cosif é obrigatório.")]
        [InlineData(12, 2025, "0001", "1.1.1.01.00", 0, "Teste unitário", "Valor deve ser maior que zero.")]
        [InlineData(12, 2025, "0001", "1.1.1.01.00", 100.00, "", "A descrição é obrigatória e não pode conter apenas espaços.")]
        public async Task CreateAsync_ComDadosInvalidos_DeveFalharNaValidacao(
            decimal datMes, decimal datAno, string codProduto, string codCosif, decimal valValor, string desDescricao, string message)
        {
            // Arrange
            var validator = new MovimentoValidator();
            var request = new PostMovimentoRequest
            {
                DatMes = datMes,
                DatAno = datAno,
                CodProduto = codProduto,
                CodCosif = codCosif,
                ValValor = valValor,
                DesDescricao = desDescricao
            };

            // Act
            var result = await validator.ValidateAsync(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == message);
        }

        [Fact]
        public async Task CreateAsync_ComExcecaoDeBanco_Retorna409Conflict()
        {
            // Arrange
            var request = new PostMovimentoRequest
            {
                DatMes = 1,
                DatAno = 2025,
                CodProduto = "0001",
                CodCosif = "1.1.1.01.00",
                ValValor = 2025.01m,
                DesDescricao = "Teste de unidade"
            };
            var exception = new DbUpdateException("Erro de banco", new Exception("Violação de chave única"));

            _mediator.Send(Arg.Any<PostMovimentoRequest>())
                .ThrowsAsync(exception);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var failedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(409, failedResult.StatusCode);
        }

        [Fact]
        public async Task CreateAsync_ComExcecaoGenerica_Retorna500InternalServerError()
        {
            // Arrange
            var request = new PostMovimentoRequest
            {
                DatMes = 1,
                DatAno = 2025,
                CodProduto = "0001",
                CodCosif = "1.1.1.01.00",
                ValValor = 2025.01m,
                DesDescricao = "Teste de unidade"
            };
            var exceptionMessage = "Erro inesperado";
            var exception = new Exception(exceptionMessage);

            _mediator.Send(Arg.Any<PostMovimentoRequest>())
                .ThrowsAsync(exception);

            // Act
            var result = await _controller.CreateAsync(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllAsync_QuandoExecutadoComSucesso_RetornaOkComListaDeMovimentos()
        {
            // Arrange
            var response = new List<GetMovimentoResponse>
            {
                new GetMovimentoResponse
                {
                    DatMes = 1,
                    DatAno = 2025,
                    CodProduto = "0001",
                    DesProduto = "Produto A",
                    NumLancamento = 1,
                    DesDescricao = "Teste",
                    ValValor = 100.0m
                }
            };

            _mediator.Send(Arg.Any<GetMovimentoRequest>())
                     .Returns(Task.FromResult((IEnumerable<GetMovimentoResponse>)response));

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var retorno = Assert.IsAssignableFrom<IEnumerable<GetMovimentoResponse>>(okResult.Value);
            Assert.Single(retorno);
        }

        [Fact]
        public async Task GetAllAsync_QuandoOcorreExcecao_RetornaStatus500ComMensagemDeErro()
        {
            // Arrange
            var exceptionMessage = "Erro inesperado";
            _mediator.Send(Arg.Any<GetMovimentoRequest>())
                     .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal(exceptionMessage, objectResult.Value);
        }

    }
}