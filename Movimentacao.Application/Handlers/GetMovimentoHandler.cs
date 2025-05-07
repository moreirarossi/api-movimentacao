using AutoMapper;
using MediatR;
using Movimentacao.Application.Querys;
using Movimentacao.Domain.Interfaces;

public class GetMovimentoHandler : IRequestHandler<GetMovimentoRequest, IEnumerable<GetMovimentoResponse>>
{
    private readonly IMapper _mapper;
    private readonly IMovimentoManualRepository _movimentoManualRepository;

    public GetMovimentoHandler(
        IMovimentoManualRepository movimentoManualRepository,
        IMapper mapper)
    {
        _movimentoManualRepository = movimentoManualRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetMovimentoResponse>> Handle(GetMovimentoRequest request, CancellationToken cancellationToken)
    {
        var result = await _movimentoManualRepository.GetAllBySPAsync();
        return _mapper.Map<IEnumerable<GetMovimentoResponse>>(result);
    }
}