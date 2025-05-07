using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Movimentacao.Application.Model;
using Movimentacao.Domain.Configs;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Interfaces;

public class PostMovimentoHandler : IRequestHandler<PostMovimentoRequest, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProdutoCosifRepository _produtoCosifRepository;
    private readonly IMovimentoManualRepository _movimentoManualRepository;
    private readonly Configs _configs;

    public PostMovimentoHandler(
        IMovimentoManualRepository movimentoManualRepository,
        IProdutoCosifRepository produtoCosifRepository,
        IMapper mapper,
        IOptions<Configs> configs)
    {
        _produtoCosifRepository = produtoCosifRepository;
        _movimentoManualRepository = movimentoManualRepository;
        _mapper = mapper;
        _configs = configs.Value;
    }

    public async Task<Unit> Handle(PostMovimentoRequest request, CancellationToken cancellationToken)
    {
        var produtoCosif = await _produtoCosifRepository.GetByProdutoCosif(request.CodProduto, request.CodCosif);

        if (produtoCosif == null)
        {
            throw new Exception($"Produto {request.CodProduto} / Cosif {request.CodCosif} não encontrado.");
        }

        var movimentoManual = _mapper.Map<MovimentoManual>(request);
        movimentoManual.CodUsuario = _configs.CodUsuario;
        await _movimentoManualRepository.AddAsync(movimentoManual);
        return Unit.Value;
    }
}