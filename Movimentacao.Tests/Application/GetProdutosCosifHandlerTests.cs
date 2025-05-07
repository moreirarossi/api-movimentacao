using AutoMapper;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Enum;
using Movimentacao.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace Movimentacao.Tests.Application.Handlers
{
    public class GetProdutoCosifHandlerTests
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoCosifRepository _produtoCosifRepository;
        private readonly IMapper _mapper;
        private readonly GetProdutoCosifHandler _handler;

        public GetProdutoCosifHandlerTests()
        {
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _produtoCosifRepository = Substitute.For<IProdutoCosifRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetProdutoCosifHandler(_produtoRepository, _produtoCosifRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ProdutoValidoAtivo_RetornaCosifsMapeados()
        {
            // Arrange
            var codProduto = "0001";
            var request = new GetProdutoCosifRequest { CodProduto = codProduto };

            var produto = new Produto { CodProduto = codProduto, StaStatus = (char)Status.Ativo };

            var cosifs = new List<ProdutoCosif>
            {
                new ProdutoCosif { CodProduto = codProduto, CodCosif = "1.1.1.01.00" }
            };

            var mappedResponse = new List<GetProdutoCosifResponse>
            {
                new GetProdutoCosifResponse { CodProduto = codProduto, CodCosif = "1.1.1.01.00" }
            };

            _produtoRepository.GetByCodProduto(codProduto).Returns(produto);
            _produtoCosifRepository.GetByProdutoAsync(codProduto).Returns(cosifs);
            _mapper.Map<IEnumerable<GetProdutoCosifResponse>>(cosifs).Returns(mappedResponse);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("1.1.1.01.00", result.First().CodCosif);
        }

        [Fact]
        public async Task Handle_ProdutoNaoExiste_RetornaListaVazia()
        {
            // Arrange
            var request = new GetProdutoCosifRequest { CodProduto = "0002" };
            _produtoRepository.GetByCodProduto(request.CodProduto).Returns((Produto)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Empty(result);
            await _produtoCosifRepository.DidNotReceive().GetByProdutoAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task Handle_ProdutoInativo_RetornaListaVazia()
        {
            // Arrange
            var codProduto = "0003";
            var produto = new Produto { CodProduto = codProduto, StaStatus = (char)Status.Inativo };
            var request = new GetProdutoCosifRequest { CodProduto = codProduto };

            _produtoRepository.GetByCodProduto(codProduto).Returns(produto);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.Empty(result);
            await _produtoCosifRepository.DidNotReceive().GetByProdutoAsync(Arg.Any<string>());
        }
    }
}
