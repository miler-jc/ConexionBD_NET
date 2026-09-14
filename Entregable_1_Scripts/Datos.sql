USE BD_NET;
GO

INSERT INTO BD_NET.dbo.PEDIDOS (Cliente, NombreProducto, Cantidad, PrecioUnitario, Total)
VALUES ('Miler Javier', 'Laptop Gamer', 2, 1500.00, 3000.00);


CREATE OR ALTER PROCEDURE  dbo.SP_ConsultarPedido
AS
BEGIN
SELECT * FROM [BD_NET].[dbo].[PEDIDOS] WITH(NOLOCK)
END


CREATE OR ALTER PROCEDURE  dbo.SP_Insertar 
    @Cliente NVARCHAR(100),
    @NombreProducto NVARCHAR(100),
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2)
AS
BEGIN
   DECLARE @Total DECIMAL(10, 2) = @Cantidad * @PrecioUnitario;
   INSERT INTO BD_NET.dbo.PEDIDOS (Cliente, NombreProducto, Cantidad, PrecioUnitario, Total)
   VALUES (@Cliente, @NombreProducto, @Cantidad, @PrecioUnitario, @Total);

   SELECT CAST(SCOPE_IDENTITY() AS INT) AS OrdenID;
END


CREATE OR ALTER PROCEDURE  dbo.SP_Actualizar
    @OrdenID INT,
	@Cliente NVARCHAR(100),
    @NombreProducto NVARCHAR(100),
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2)
AS
BEGIN
    UPDATE BD_NET.dbo.PEDIDOS
    SET Cliente = @Cliente,
        NombreProducto = @NombreProducto,
        Cantidad = @Cantidad,
        PrecioUnitario = @PrecioUnitario,
        Total = @Cantidad * @PrecioUnitario
    WHERE OrdenID = @OrdenID;
END


CREATE OR ALTER PROCEDURE  dbo.SP_Eliminar
@OrdenID INT
AS
BEGIN
   DELETE FROM BD_NET.dbo.PEDIDOS
   WHERE OrdenID = @OrdenID;
END
