using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Movimentacao.Application.Model;
using Movimentacao.Domain.Configs;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace Movimentacao.Tests.Application.Handlers
{
    public class PostMovimentoHandlerTests
    {
        private readonly IMovimentoManualRepository _movimentoManualRepository;
        private readonly IProdutoCosifRepository _produtoCosifRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<Configs> _configs;
        private readonly PostMovimentoHandler _handler;

        public PostMovimentoHandlerTests()
        {
            _movimentoManualRepository = Substitute.For<IMovimentoManualRepository>();
            _produtoCosifRepository = Substitute.For<IProdutoCosifRepository>();
            _mapper = Substitute.For<IMapper>();
            _configs = Options.Create(new Configs { CodUsuario = "USR_TESTE" });

            _handler = new PostMovimentoHandler(
                _movimentoManualRepository,
                _produtoCosifRepository,
                _mapper,
                _configs
            );
        }

        [Fact]
        public async Task Handle_QuandoProdutoCosifExiste_AdicionaMovimentoRetornaUnit()
        {
            // Arrange
            var request = new PostMovimentoRequest
            {
                DatMes = 1,
                DatAno = 2025,
                CodProduto = "0001",
                CodCosif = "1.1.1.01.00",
                ValValor = 100.0m,
                DesDescricao = "Teste de inserção"
            };

            var produtoCosif = new ProdutoCosif();
            var movimentoManual = new MovimentoManual();

            _produtoCosifRepository.GetByProdutoCosif(request.CodProduto, request.CodCosif)
                .Returns(produtoCosif);

            _mapper.Map<MovimentoManual>(request).Returns(movimentoManual);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.Equal("USR_TESTE", movimentoManual.CodUsuario);
            await _movimentoManualRepository.Received(1).AddAsync(movimentoManual);
        }

        [Fact]
        public async Task Handle_QuandoProdutoCosifNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var request = new PostMovimentoRequest
            {
                DatMes = 1,
                DatAno = 2025,
                CodProduto = "0001",
                CodCosif = "1.1.1.01.00",
                ValValor = 100.0m,
                DesDescricao = "Teste de falha"
            };

            _produtoCosifRepository.GetByProdutoCosif(request.CodProduto, request.CodCosif)
                .Returns((ProdutoCosif?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, default));
            Assert.Equal("Produto 0001 / Cosif 1.1.1.01.00 não encontrado.", ex.Message);
            await _movimentoManualRepository.DidNotReceive().AddAsync(Arg.Any<MovimentoManual>());
        }
    }
}
