using MediatR;
using Proji.Domain.Dtos;
using Proji.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Application.Queries.QueryHandlers
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDTO>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleDTO> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.Id);
            if (vehicle == null)
            {
                return null;
            }

            return new VehicleDTO
            (
                Id: vehicle.Id,
                BasePrice: vehicle.BasePrice,
                CarType: vehicle.CarType.ToString()
            );
        }
    }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, IEnumerable<VehicleDTO>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleDTO>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();

            return vehicles.Select(vehicle => new VehicleDTO
            (
                Id : vehicle.Id,
                BasePrice : vehicle.BasePrice,
                CarType : vehicle.CarType.ToString()
            ));
        }
    }
}
