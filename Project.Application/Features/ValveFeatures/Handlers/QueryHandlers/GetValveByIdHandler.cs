using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.ValveFeatures.Handlers.QueryHandlers
{
    public class GetValveByIdQuery : IRequest<ValveDTO>
    {
        public GetValveByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetValveByIdHandler : IRequestHandler<GetValveByIdQuery, ValveDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetValveByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<ValveDTO> Handle(GetValveByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var valve = await _unitOfWorkDb.valverQueryRepository.GetByIdAsync(request.Id);
                var newvalve = _mapper.Map<ValveDTO>(valve);
                return newvalve;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
