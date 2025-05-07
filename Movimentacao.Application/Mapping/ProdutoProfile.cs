using AutoMapper;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Movimentacao.Application.Mapping
{
    [ExcludeFromCodeCoverage]
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<GetProdutoResponse, Produto>().ReverseMap();
        }
    }
}
