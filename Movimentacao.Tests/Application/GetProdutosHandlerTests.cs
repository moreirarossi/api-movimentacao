using AutoMapper;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace Movimentacao.Tests.Application.Handlers
{
    public class GetProdutoHandlerTests
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly GetProdutoHandler _handler;

        public GetProdutoHandlerTests()
        {
            _produtoRepository = Substitute.For<IProdutoRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetProdutoHandler(_produtoRepository, _mapper);
        }

        [Fact]
        public async Task Handle_QuandoExecutadoComSucesso_RetornaListaDeProdutosMapeada()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { CodProduto = "0001", DesProduto = "Produto A" },
                new Produto { CodProduto = "0002", DesProduto = "Produto B" }
            };

            var responseEsperado = new List<GetProdutoResponse>
            {
                new GetProdutoResponse { CodProduto = "0001", DesProduto = "Produto A" },
                new GetProdutoResponse { CodProduto = "0002", DesProduto = "Produto B" }
            };

            _produtoRepository.GetAllAsync().Returns(produtos);
            _mapper.Map<IEnumerable<GetProdutoResponse>>(produtos).Returns(responseEsperado);

            var request = new GetProdutoRequest();

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal("0001", item.CodProduto),
                item => Assert.Equal("0002", item.CodProduto));
            await _produtoRepository.Received(1).GetAllAsync();
        }
    }
}
