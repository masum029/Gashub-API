using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;

namespace Project.Application.Features.CompanyFeatures.Handlers.QueryHandlers
{
    public class GetCompanyByIdQuery : IRequest<CompanyDTO>
    {
        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetCompanyByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<CompanyDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _unitOfWorkDb.companyrQueryRepository.GetByIdAsync(request.Id);

                if (company == null)
                {
                    throw new NotFoundException($"Company with id = {request.Id} not found");
                }

                var companyDto = _mapper.Map<CompanyDTO>(company);
                return companyDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception("An error occurred while retrieving company by id", ex);
            }
        }

    }
}
