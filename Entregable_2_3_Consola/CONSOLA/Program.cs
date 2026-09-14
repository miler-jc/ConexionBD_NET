// See https://aka.ms/new-console-template for more information
using CONSOLA.Data;
using CONSOLA.Models;
using Microsoft.Data.SqlClient;

namespace BD_NET.CONSOLA;

internal static class Program
{
    private static readonly ConsultaSQLdirecto SqlDirecto = new();
    private static readonly ConsultaSP Sql_StoredProcedure = new();

    private static async Task Main()
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine("                PEDIDOS               ");
        Console.WriteLine("======================================");
        Console.WriteLine("Modo de consulta a datos:");
        Console.WriteLine("Digite 1 para SQL directo");
        Console.WriteLine("Digite 2 para Stored Procedure");
        Console.Write("\nSeleccione una opción: ");

        var tipo = Console.ReadLine();

        if (tipo == "1" || tipo == "2")
        {
            await IniciarCrud(tipo);
        }
        else 
        {
            Console.WriteLine("La opción no es valida");
            return;
        }
    }

    private static async Task IniciarCrud(string tipo)
    {
        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine($" Pedido - {(tipo=="2" ? "Stored Procedure" : "SQL directo")}");
        Console.WriteLine("======================================");
        Console.WriteLine("1. Listar Pedidos");
        Console.WriteLine("2. Insertar Pedido");
        Console.WriteLine("3. Actualizar Pedido");
        Console.WriteLine("4. Eliminar Pedido");
        Console.WriteLine("0. Finalizar");
        Console.Write("\nSeleccione una opción: ");

        var opcion = Console.ReadLine();

        switch (opcion)
        {
            case "0":
                return;
            case "1":
                await ListarAsync(tipo);
                break;
            case "2":
                await InsertarAsync(tipo);
                break;
            case "3":
                await ActualizarAsync(tipo);
                break;
            case "4":
                await EliminarAsync(tipo);
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

    }

    private static async Task ListarAsync(string tipo)
    {
        var Pedido = (tipo=="2") 
            ? await Sql_StoredProcedure.PedidoDetalleAsync()
            : await SqlDirecto.PedidoDetalleAsync();

        Console.Clear();
        Console.WriteLine($"{"OrdenID",-10} {"Cliente",-25} {"NombreProducto",-25} {"Cantidad",-15} {"PrecioUnitario",15} {"Total",15}")  ;
        Console.WriteLine(new string('-', 120));

        foreach (var pedido in Pedido)
        {
            Console.WriteLine(
                $"{pedido.OrdenID,-10} " +
                $"{Recortar(pedido.Cliente, 25),-25} " +
                $"{Recortar(pedido.NombreProducto, 25),-25} " +
                $"{Recortar(pedido.Cantidad.ToString(), 15),-15} " +
                $"{Recortar(pedido.PrecioUnitario.ToString(),15),-15}" +
                $"{pedido.Total,15}");
        }

        Console.WriteLine($"\nTotal: {Pedido.Count} Pedidos(s).");
    }

    private static async Task InsertarAsync(string tipo)
    {
        Console.Clear();
        Console.WriteLine("Nuevo Pedido\n");
        var pedido = LeerPedido();

        var NuevoPedido = (tipo == "2")
            ? await Sql_StoredProcedure.InsertarAsync(pedido)
            : await SqlDirecto.InsertarAsync(pedido);

        Console.WriteLine($"Pedido insertado correctamente. Nueva OrdenID: {NuevoPedido}");
    }

    private static async Task ActualizarAsync(string tipo)
    {
        int ordenId;
        Console.Clear();
        Console.WriteLine("Ingrese el ID del Pedido a actualizar:\n");
        ordenId = int.Parse(Console.ReadLine());
        var actualizarpedido = LeerPedidoActualizado();

        var PedidoActualizado = (tipo == "2")
            ? await Sql_StoredProcedure.ActualizarAsync(ordenId, actualizarpedido)
            : await SqlDirecto.ActualizarAsync(ordenId, actualizarpedido);

        Console.WriteLine($"Pedido Actualizado correctamente. OrdenID Actualizado: {ordenId}");
    }

    private static async Task EliminarAsync(string tipo)
    {
        int ordenId;
        Console.Clear();
        Console.WriteLine("Ingrese el ID del Pedido a eliminar:\n");
        ordenId = int.Parse(Console.ReadLine());

        var PedidoEliminado = (tipo == "2")
            ? await Sql_StoredProcedure.EliminarAsync(ordenId)
            : await SqlDirecto.EliminarAsync(ordenId);

        Console.WriteLine(PedidoEliminado > 0 ? $"Pedido con OrdenID:{ordenId} eliminado correctamente" : 
            $"No se encontro el OrdenID:{ordenId}");
    }

    private static PedidoInput LeerPedido()
    {
        var pedido = new PedidoInput();

        Console.Write("Cliente: ");
        pedido.Cliente = Console.ReadLine();

        Console.Write("Nombre del producto: ");
        pedido.NombreProducto = Console.ReadLine();

        Console.Write("Cantidad: ");
        pedido.Cantidad = int.Parse(Console.ReadLine());

        Console.Write("Precio unitario: ");
        pedido.PrecioUnitario = decimal.Parse(Console.ReadLine());

        //pedido.Total = 0;

        return pedido;
    }

    private static PedidoInput LeerPedidoActualizado()
    {
        var pedidoactualizado = new PedidoInput();

        Console.Write("Cliente: ");
        pedidoactualizado.Cliente = Console.ReadLine();

        Console.Write("Nombre del producto: ");
        pedidoactualizado.NombreProducto = Console.ReadLine();

        Console.Write("Cantidad: ");
        pedidoactualizado.Cantidad = int.Parse(Console.ReadLine());

        Console.Write("Precio unitario: ");
        pedidoactualizado.PrecioUnitario = decimal.Parse(Console.ReadLine());

        //pedido.Total = 0;

        return pedidoactualizado;
    }


    private static string Recortar(string valor, int maximo)
    {
        if (valor.Length <= maximo)
            return valor;

        return valor[..(maximo - 3)] + "...";
    }


}
