using AutoMapper;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Interfaces;
using Movimentacao.Domain.Model;
using NSubstitute;
using Xunit;

namespace Movimentacao.Tests.Application.Handlers
{
    public class GetMovimentoHandlerTests
    {
        private readonly IMovimentoManualRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetMovimentoHandler _handler;

        public GetMovimentoHandlerTests()
        {
            _repository = Substitute.For<IMovimentoManualRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetMovimentoHandler(_repository, _mapper);
        }

        [Fact]
        public async Task Handle_QuandoExecutado_RetornaListaDeMovimentos()
        {
            // Arrange
            var entidades = new List<MovimentoManualSPModel>
            {
                new MovimentoManualSPModel
                {
                    DatMes = 1,
                    DatAno = 2025,
                    CodProduto = "0001",
                    DesProduto = "Produto A",
                    DesDescricao = "Teste",
                    ValValor = 100.0m,
                    NumLancamento = 1
                }
            };

            var mappedResult = new List<GetMovimentoResponse>
            {
                new GetMovimentoResponse
                {
                    DatMes = 1,
                    DatAno = 2025,
                    CodProduto = "0001",
                    DesDescricao = "Teste",
                    ValValor = 100.0m,
                    NumLancamento = 1
                }
            };

            _repository.GetAllBySPAsync().Returns(entidades);
            _mapper.Map<IEnumerable<GetMovimentoResponse>>(entidades).Returns(mappedResult);

            // Act
            var result = await _handler.Handle(new GetMovimentoRequest(), default);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("0001", result.First().CodProduto);
            await _repository.Received(1).GetAllBySPAsync();
        }
    }
}
