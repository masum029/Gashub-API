using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.ProdReturnFeatures.Handlers.QueryHandlers
{
    public class GetProdReturnByIdQuery : IRequest<ProdReturnDTO>
    {
        public GetProdReturnByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetProdReturnByIdHandler : IRequestHandler<GetProdReturnByIdQuery, ProdReturnDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetProdReturnByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<ProdReturnDTO> Handle(GetProdReturnByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var prodReturn = await _unitOfWorkDb.prodReturnQueryRepository.GetByIdAsync(request.Id);
                var newProdReturn = _mapper.Map<ProdReturnDTO>(prodReturn);
                return newProdReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
