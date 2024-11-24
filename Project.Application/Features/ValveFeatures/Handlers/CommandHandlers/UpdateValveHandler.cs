using AutoMapper;
using MediatR;
using Project.Application.ApiResponse;
using Project.Application.Exceptions;
using Project.Domail.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ValveFeatures.Handlers.CommandHandlers
{
    public class UpdateValveCommand : IRequest< ApiResponse<string>>
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Unit { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
    }
    public class UpdateValveHandler : IRequestHandler<UpdateValveCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateValveHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

 
        public async Task<ApiResponse<string>> Handle(UpdateValveCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            var valve = await _unitOfWorkDb.valverQueryRepository.GetByIdAsync(request.Id);

            if (valve == null || valve.Id != request.Id)
            {
                throw new NotFoundException($"valve with id = {request.Id} not found");
            }

            try
            {
                // Update company properties
                valve.Name = request.Name;
                valve.Unit = request.Unit;
                valve.UpdatedBy = request.UpdatedBy;

                // Perform update operation
                await _unitOfWorkDb.valveCommandRepository.UpdateAsync(valve);
                await _unitOfWorkDb.SaveAsync();

                // Map the updated company to your DTO model if needed
                response.Success = true;
                response.Data = $"valve with id = {valve.Id} updated successfully";
                response.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                response.Success = false;
                response.Data = "An error occurred while updating the trader";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
