using Proji.Domain.Dtos;
using Proji.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Domain.Services
{
    public interface IFeeCalculationService
    {
        CalculationResult CalculateTotalCost(Vehicle vehicle);
        decimal CalculateBasicFee(Vehicle vehicle);
        decimal CalculateSpecialFee(Vehicle vehicle);
        decimal CalculateAssociationFee(decimal basePrice);
    }
}
