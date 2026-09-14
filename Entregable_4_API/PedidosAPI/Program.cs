using Microsoft.Data.SqlClient;
using PedidosAPI.Data.Dapper;
using PedidosAPI.Domain.Models;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

builder.Services.AddScoped<ISQLDapperBD, SQLDapperBD>();
builder.Services.AddScoped<PedidoDetalleDapperRepository>();

var app = builder.Build();


app.MapGet("/api/pedidodetalle", async (PedidoDetalleDapperRepository repo) =>
{
    var detalles = await repo.ListarPedidosAsync();
    return Results.Ok(detalles);
});

app.MapPost("/api/insertarpedido", async (PedidoDetalle detalle, PedidoDetalleDapperRepository repo) =>
{
    var id = await repo.InsertAsync(detalle);
    return Results.Ok(new { PedidoId = id });
});

app.MapPut("/api/actualizarpedido", async (PedidoDetalle detalle, PedidoDetalleDapperRepository repo) =>
{
    var id = await repo.ActualizarPedido(detalle);
    return Results.Ok(new { IdActualizado = id });
});

app.MapDelete("/api/eliminarpedido/{id}", async (int id, PedidoDetalleDapperRepository repo) =>
{
    var filas = await repo.EliminarPedido(new PedidoDetalle { OrdenID = id });
    return filas > 0
        ? Results.Ok(new { Eliminado = true, OrdenID = id })
        : Results.NotFound(new { Eliminado = false, OrdenID = id });
});



app.Run();
