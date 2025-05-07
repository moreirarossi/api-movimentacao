using AutoMapper;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Movimentacao.Application.Mapping
{
    [ExcludeFromCodeCoverage]
    public class ProdutoCosifProfile : Profile
    {
        public ProdutoCosifProfile()
        {
            CreateMap<GetProdutoCosifResponse, ProdutoCosif>().ReverseMap();
        }
    }
}
