Go
-- Stored Procedure: GetAllVehicles
CREATE PROCEDURE GetAllVehicles
AS
BEGIN
    SELECT * FROM Vehicles;
END
GO

-- Stored Procedure: GetVehicleByIdUpdateVehicle
CREATE PROCEDURE GetVehicleById
    @Id INT
AS
BEGIN
    SELECT * FROM Vehicles WHERE Id = @Id;
END
GO

-- Stored Procedure: InsertVehicle
CREATE PROCEDURE AddVehicle
    @BasePrice DECIMAL(18, 2),
    @CarType NVARCHAR(50)
AS
BEGIN
    INSERT INTO Vehicles (BasePrice, CarType)
    VALUES (@BasePrice, @CarType);
END
GO

-- Stored Procedure: UpdateVehicle
CREATE PROCEDURE UpdateVehicle
    @Id INT,
    @BasePrice DECIMAL(18, 2),
    @CarType VARCHAR(20)
AS
BEGIN
    UPDATE Vehicles
    SET BasePrice = @BasePrice,
        CarType = @CarType
    WHERE Id = @Id;
END
GO

-- Stored Procedure: DeleteVehicle
CREATE PROCEDURE DeleteVehicle
    @Id INT
AS
BEGIN
    DELETE FROM Vehicles WHERE Id = @Id;
END
GO
