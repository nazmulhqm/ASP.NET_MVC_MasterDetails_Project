CREATE PROCEDURE [dbo].[sp_InsertProduct]
    @ProductName NVARCHAR(100),
    @ProductDescription NVARCHAR(100),
    @CategoryId INT
AS
BEGIN
    INSERT INTO Products (ProductName, ProductDescription, CategoryId)
    VALUES (@ProductName, @ProductDescription, @CategoryId);
END
GO

CREATE PROCEDURE [dbo].[sp_UpdateProduct]
    @ProductId INT,
    @ProductName NVARCHAR(100),
    @ProductDescription NVARCHAR(100),
    @CategoryId INT
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        ProductDescription = @ProductDescription,
        CategoryId = @CategoryId
    WHERE ProductId = @ProductId;
END
GO

CREATE PROCEDURE [dbo].[sp_DeleteProduct]
    @ProductId INT
AS
BEGIN
    DELETE FROM Products
    WHERE ProductId = @ProductId;
END
GO