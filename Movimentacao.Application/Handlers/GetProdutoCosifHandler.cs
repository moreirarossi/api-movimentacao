using AutoMapper;
using MediatR;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Enum;
using Movimentacao.Domain.Interfaces;

public class GetProdutoCosifHandler : IRequestHandler<GetProdutoCosifRequest, IEnumerable<GetProdutoCosifResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoCosifRepository _produtoCosifRepository;

    public GetProdutoCosifHandler(IProdutoRepository produtoRepository, IProdutoCosifRepository produtoCosifRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _produtoCosifRepository = produtoCosifRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProdutoCosifResponse>> Handle(GetProdutoCosifRequest request, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.GetByCodProduto(request.CodProduto);

        if (produto == null || produto.StaStatus == (char)Status.Inativo)
            return Enumerable.Empty<GetProdutoCosifResponse>();

        var result = await _produtoCosifRepository.GetByProdutoAsync(request.CodProduto);

        var response = _mapper.Map<IEnumerable<GetProdutoCosifResponse>>(result);

        return response;
    }
}