using MediatR;
using Proji.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Application.Queries
{
    public record GetVehicleByIdQuery(int Id) : IRequest<VehicleDTO>;
    public record GetAllVehiclesQuery : IRequest<IEnumerable<VehicleDTO>>;
}
