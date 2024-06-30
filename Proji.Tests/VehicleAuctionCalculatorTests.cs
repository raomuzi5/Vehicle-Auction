using Xunit;
using Proji.Domain.Entities;
using Proji.Domain.Enums;
using Proji.Domain.Services;

namespace Proji.Application.Services.Tests
{
    public class FeeCalculationServiceTests
    {
        private readonly FeeCalculationService _service;

        public FeeCalculationServiceTests()
        {
            _service = new FeeCalculationService();
        }

        [Theory]
        [InlineData(398.00, CarType.Common, 39.80, 7.96, 5.00, 100.00, 550.76)]
        [InlineData(501.00, CarType.Common, 50.00, 10.02, 10.00, 100.00, 671.02)]
        [InlineData(57.00, CarType.Common, 10.00, 1.14, 5.00, 100.00, 173.14)]
        [InlineData(1800.00, CarType.Luxury, 180.00, 72.00, 15.00, 100.00, 2167.00)]
        [InlineData(1100.00, CarType.Common, 50.00, 22.00, 15.00, 100.00, 1287.00)]
        [InlineData(1000000.00, CarType.Luxury, 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
        public void CalculateTotalCost_ShouldCalculateCorrectly(
            decimal basePrice, CarType carType,
            decimal expectedBasicFee, decimal expectedSpecialFee,
            decimal expectedAssociationFee, decimal expectedStorageFee,
            decimal expectedTotalCost)
        {
            // Arrange
            var vehicle = new Vehicle
            {
                BasePrice = basePrice,
                CarType = carType
            };

            // Act
            var result = _service.CalculateTotalCost(vehicle);

            // Assert
            Assert.Equal(expectedTotalCost, result.TotalCost);
            Assert.Equal(expectedBasicFee, result.BasicFee);
            Assert.Equal(expectedSpecialFee, result.SpecialFee);
            Assert.Equal(expectedAssociationFee, result.AssociationFee);
            Assert.Equal(expectedStorageFee, result.StorageFee);
        }
    }
}
