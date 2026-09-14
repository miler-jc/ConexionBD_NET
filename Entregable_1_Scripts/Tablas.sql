/*
CREATE DATABASE BD_NET;
GO
*/

/*=======================================================
Entregable 1 - Modelado
=======================================================*/ 
USE BD_NET;
GO

IF OBJECT_ID('BD_NET.dbo.Productos', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.Productos;
GO

CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    NombreProducto NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    CategoriaID INT NOT NULL
)

IF OBJECT_ID('BD_NET.dbo.Categorias', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.Categorias;
GO

CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200)
)

IF OBJECT_ID('BD_NET.dbo.Clientes', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.Clientes;
GO

CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Telefono NVARCHAR(20)
)

IF OBJECT_ID('BD_NET.dbo.Ordenes', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.Ordenes;
GO

CREATE TABLE Ordenes (
    OrdenID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    FechaOrden DATETIME NOT NULL,
    Total DECIMAL(10,2),
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
)

IF OBJECT_ID('BD_NET.dbo.OrdenDetalle', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.OrdenDetalle;
GO

CREATE TABLE OrdenDetalle (
    OrdenID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrdenID) REFERENCES Ordenes(OrdenID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
)
/*===========================================================
  Entregable 2,3,4 
===========================================================*/


IF OBJECT_ID('BD_NET.dbo.PEDIDOS', 'U') IS NOT NULL
    DROP TABLE BD_NET.dbo.PEDIDOS;
GO


CREATE TABLE BD_NET.dbo.PEDIDOS (
    OrdenID INT IDENTITY(1,1) PRIMARY KEY,
    Cliente NVARCHAR(100) NOT NULL,
    NombreProducto NVARCHAR(100) NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Total DECIMAL(10,2) NULL
);
