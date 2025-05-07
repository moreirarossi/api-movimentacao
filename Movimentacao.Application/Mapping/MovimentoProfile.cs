using AutoMapper;
using Movimentacao.Application.Model;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Movimentacao.Application.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MovimentoProfile : Profile
    {
        public MovimentoProfile()
        {
            CreateMap<PostMovimentoRequest, MovimentoManual>().ReverseMap();
            CreateMap<GetMovimentoResponse, MovimentoManualSPModel>().ReverseMap();
        }
    }
}
