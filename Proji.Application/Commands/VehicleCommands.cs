using MediatR;
using Proji.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Proji.Application.Commands
{
    public record AddVehicleCommand(
        [Required(ErrorMessage = "BasePrice is required")]
        [Range(1, double.MaxValue, ErrorMessage = "BasePrice must be greater than 0")]
        decimal BasePrice,

        [Required(ErrorMessage = "CarType is required")]
        [EnumDataType(typeof(CarType), ErrorMessage = "Invalid CarType value")]
        string CarType
        ) : IRequest<int>;


    public record UpdateVehicleCommand(
        [Required(ErrorMessage = "Id is required")]
        int Id,
        [Required(ErrorMessage = "BasePrice is required")]
        [Range(1, double.MaxValue, ErrorMessage = "BasePrice must be greater than 0")]
        decimal BasePrice,

        [Required(ErrorMessage = "CarType is required")]
        [EnumDataType(typeof(CarType), ErrorMessage = "Invalid CarType value")]
        string CarType
    ) : IRequest<Unit>;


    public record DeleteVehicleCommand(
        [Required(ErrorMessage = "Id is required")]
        int Id
    ) : IRequest<Unit>;
}
