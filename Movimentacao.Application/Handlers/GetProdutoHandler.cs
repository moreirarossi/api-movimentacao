using AutoMapper;
using MediatR;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Interfaces;

public class GetProdutoHandler : IRequestHandler<GetProdutoRequest, IEnumerable<GetProdutoResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProdutoRepository _produtoRepository;

    public GetProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProdutoResponse>> Handle(GetProdutoRequest request, CancellationToken cancellationToken)
    {
        var result = await _produtoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProdutoResponse>>(result);
    }
}