using MediatR;
using Proji.Domain.Entities;
using Proji.Domain.Enums;
using Proji.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Application.Commands.CommandHandlers
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, int>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public AddVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<int> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                BasePrice = request.BasePrice,
                CarType = Enum.Parse<CarType>(request.CarType, true)
            };

            await _vehicleRepository.AddVehicleAsync(vehicle);
            return vehicle.Id;
        }
    }

    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Unit>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {request.Id} not found.");
            }

            vehicle.BasePrice = request.BasePrice;
            vehicle.CarType = Enum.Parse<CarType>(request.CarType, true);

            await _vehicleRepository.UpdateVehicleAsync(vehicle);
            return Unit.Value;
        }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand,Unit>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            await _vehicleRepository.DeleteVehicleAsync(request.Id);
            return Unit.Value;
        }
    }
}
