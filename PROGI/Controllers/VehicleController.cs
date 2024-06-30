using MediatR;
using Microsoft.AspNetCore.Mvc;
using Proji.Application.Commands;
using Proji.Application.Queries;
using Proji.Domain.Dtos;
using Proji.Domain.Entities;
using Proji.Domain.Enums;
using Proji.Domain.Services;

namespace PROGI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFeeCalculationService _feeCalculationService;

        public VehicleController(IMediator mediator, IFeeCalculationService feeCalculationService)
        {
            _mediator = mediator;
            _feeCalculationService = feeCalculationService;
        }
        [HttpPost("calculate")]
        public ActionResult<ApiResponse<CalculationResult>> CalculateTotalCost(AddVehicleCommand command)
        {
            if (!Enum.TryParse(command.CarType, true, out CarType carType))
            {
                return BadRequest(ApiResponse<CalculationResult>.FailureResponse("Invalid car type."));
            }

            var vehicle = new Vehicle
            {
                BasePrice = command.BasePrice,
                CarType = carType
            };

            var calculationResult = _feeCalculationService.CalculateTotalCost(vehicle);

            return Ok(ApiResponse<CalculationResult>.SuccessResponse(calculationResult));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<VehicleDTO>>> GetVehicleById(int id)
        {
            var query = new GetVehicleByIdQuery(id);
            var vehicle = await _mediator.Send(query);
            if (vehicle == null)
            {
                return NotFound(ApiResponse<VehicleDTO>.FailureResponse($"Vehicle with ID {id} not found."));
            }

            return Ok(ApiResponse<VehicleDTO>.SuccessResponse(vehicle));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<VehicleDTO>>>> GetAllVehicles()
        {
            var query = new GetAllVehiclesQuery();
            var vehicles = await _mediator.Send(query);
            return Ok(ApiResponse<IEnumerable<VehicleDTO>>.SuccessResponse(vehicles));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> AddVehicle(AddVehicleCommand command)
        {
            try
            {
                var vehicleId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetVehicleById), new { id = vehicleId }, ApiResponse<int>.SuccessResponse(vehicleId, "Vehicle added successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<int>.FailureResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Unit>>> UpdateVehicle(int id, UpdateVehicleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<Unit>.FailureResponse("Vehicle ID mismatch."));
            }

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<Unit>.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<Unit>.FailureResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Unit>>> DeleteVehicle(int id)
        {
            try
            {
                var command = new DeleteVehicleCommand(id);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<Unit>.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<Unit>.FailureResponse(ex.Message));
            }
        }
    }
}
