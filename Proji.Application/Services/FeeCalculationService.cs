using Proji.Domain.Dtos;
using Proji.Domain.Entities;
using Proji.Domain.Enums;
using Proji.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proji.Application.Services
{
    public class FeeCalculationService : IFeeCalculationService
    {
        private const decimal StorageFee = 100;

        private const decimal CommonCarMinBasicFee = 10;
        private const decimal CommonCarMaxBasicFee = 50;
        private const decimal LuxuryCarMinBasicFee = 25;
        private const decimal LuxuryCarMaxBasicFee = 200;

        private const decimal FeeUpTo500 = 5;
        private const decimal FeeUpTo1000 = 10;
        private const decimal FeeUpTo3000 = 15;
        private const decimal FeeAbove3000 = 20;

        public CalculationResult CalculateTotalCost(Vehicle vehicle)
        {
            var basePrice = vehicle.BasePrice;
            var basicFee = CalculateBasicFee(vehicle);
            var specialFee = CalculateSpecialFee(vehicle);
            var associationFee = CalculateAssociationFee(basePrice);

            var totalCost = basePrice + basicFee + specialFee + associationFee + StorageFee;

            return new CalculationResult
            {
                TotalCost = totalCost,
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = StorageFee
            };
        }

        public decimal CalculateBasicFee(Vehicle vehicle)
        {
            var basePrice = vehicle.BasePrice;
            decimal basicFee = basePrice * 0.1m;

            if (vehicle.CarType == CarType.Common)
            {
                basicFee = Math.Clamp(basicFee, CommonCarMinBasicFee, CommonCarMaxBasicFee);
            }
            else if (vehicle.CarType == CarType.Luxury)
            {
                basicFee = Math.Clamp(basicFee, LuxuryCarMinBasicFee, LuxuryCarMaxBasicFee);
            }

            return basicFee;
        }

        public decimal CalculateSpecialFee(Vehicle vehicle)
        {
            var basePrice = vehicle.BasePrice;
            decimal specialFee = vehicle.CarType == CarType.Common ? basePrice * 0.02m : basePrice * 0.04m;
            return specialFee;
        }

        public decimal CalculateAssociationFee(decimal basePrice)
        {
            if (basePrice <= 500) return FeeUpTo500;
            if (basePrice <= 1000) return FeeUpTo1000;
            if (basePrice <= 3000) return FeeUpTo3000;
            return FeeAbove3000;
        }

    }
}
