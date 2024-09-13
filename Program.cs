using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using clases;
public static class CsvReader
{
    public static List<T> LeerCsv<T>(string rutaArchivo, Func<string[], T> mapa)
    {
        var resultados = new List<T>();

        using (var reader = new StreamReader(rutaArchivo))
        {
            var encabezado = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var linea = reader.ReadLine();
                if (linea != null)
                {
                    var valores = linea.Split(',');

                    var objeto = mapa(valores);
                    resultados.Add(objeto);
                }
            }
        }

        return resultados;
    }
}


class Program
{
    static void Main()
    {
        string rutaCadetes = "cadetes.csv";
        string rutaPedidos = "pedidos.csv";

        var cadetes = CsvReader.LeerCsv(rutaCadetes, MapeoCadete);

        var pedidos = CsvReader.LeerCsv(rutaPedidos, MapeoPedido);

        AsignarPedidosACadetes(pedidos, cadetes);

 
        foreach (var cadete in cadetes)
        {
            cadete.MostrarPedidos();
        }
        //Interfaz
        int condicion=0;
        while(condicion==0){
            Console.WriteLine("Seleccione la opcion que desea ejecutar: \n 1-Dar de alta pedido \n 2-Asignar Cadete \n 3-Cambiar estado de pedido \n 4-Reasignar Cadete\n 5-Nada ");
            int respuesta = int.Parse(Console.ReadLine());
            switch (respuesta){
                //PedidoNumero,Estado,ClienteNombre,ClienteDireccion,ClienteTelefono,ClienteReferencia,Obs,CadeteId
                case 1:
                    Console.WriteLine("Ingrese el numero del pedido");
                    int pedidoNumero = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el estado del pedido(1-Procesado 2-Enviado 3-Entregado 4-Cancelado)");
                    int valorestado = int.Parse(Console.ReadLine());
                    EstadoPedido estado = (EstadoPedido)valorestado;
                    Console.WriteLine("Ingrese el nombre del cliente");
                    string clienteNombre = Console.ReadLine();
                    Console.WriteLine("Ingrese la direccion del cliente");
                    string clienteDireccion = Console.ReadLine();
                    Console.WriteLine("Ingrese el telefono del cliente");
                    string clienteTelefono = Console.ReadLine();
                    Console.WriteLine("Ingrese la referencia del cliente");
                    string clienteReferencia = Console.ReadLine();
                    Console.WriteLine("Ingrese la observacion del pedido");
                    string obs = Console.ReadLine();
                    Console.WriteLine("Ingrese el id del cadete");
                    int cadeteId = int.Parse(Console.ReadLine());
                    Cliente cliente = new Cliente(clienteNombre,clienteDireccion,clienteTelefono,clienteReferencia);
                    Pedido pedido = new Pedido(cliente,obs,pedidoNumero,estado);
                break;

                case 2:
                    Console.WriteLine("Ingrese el numero del pedido a asignar cadete");
                    int pedidoAsignar = int.Parse(Console.ReadLine());
                    Pedido pedidoEncontrado = null;
                    foreach(var (pedido1,_) in pedidos){
                        if(pedido1.Numero==pedidoAsignar){
                            pedidoEncontrado = pedido1;
                            break;}
                            if(pedidoEncontrado!=null){
                                Console.WriteLine("Ingrese el id del cadete a asignar");
                                int cadeteIdAsignar = int.Parse(Console.ReadLine());
                                foreach(Cadete cadete in cadetes){
                                if(cadete.Id==cadeteIdAsignar){
                                    cadete.AgregarPedido(pedidoEncontrado);
                                    break;
                                }}}}
                break;

                case 3:
                    Console.WriteLine("Ingrese el numero del pedido a cambiar de estado");
                    int pedidoCambio = int.Parse(Console.ReadLine());
                    Pedido pedidoEncontrado2 = null;
                    foreach(var (pedido2, _) in pedidos){
                        if(pedido2.Numero==pedidoCambio){
                            pedidoEncontrado2 = pedido2;
                            break;}}
                            if(pedidoEncontrado2!=null){
                                Console.WriteLine("Ingrese el nuevo estado del pedido(1-Procesado 2-Enviado 3-Entregado 4-Cancelado)");
                                int valorestado2 = int.Parse(Console.ReadLine());
                                EstadoPedido estado2 = (EstadoPedido)valorestado2;
                                pedidoEncontrado2.Estado=estado2;
                            }  
                break;

                case 4: 
                    Console.WriteLine("Ingrese el numero de pedido a cambiar de cadete");
                    int pedidoCambioCadete = int.Parse(Console.ReadLine());
                    Pedido pedidoEncontrado3 = null;
                    foreach(var (pedido3,_) in pedidos){
                        if(pedido3.Numero==pedidoCambioCadete){
                            pedidoEncontrado3 = pedido3;
                            break;}
                        if(pedidoEncontrado3!=null){
                            Console.WriteLine("Ingrese el id del nuevo cadete");
                            int cadeteIdCambio = int.Parse(Console.ReadLine());
                            foreach(Cadete cadete in cadetes){
                                if(cadete.TienePedido(pedidoCambioCadete)){
                                    cadete.RemoverPedido(pedidoEncontrado3);
                                }
                                if(cadete.Id==cadeteIdCambio){
                                    cadete.AgregarPedido(pedidoEncontrado3);
                                    break;}}}}
                break;
                case 5: condicion=1;
                        Console.WriteLine("Saliendo del menu \n");
                break;
                default: Console.WriteLine("Opcion no valida, vuelva a seleccionar \n");
                break;
                }}
                //Informe
                MostrarInformeFinal(cadetes);
                }

           


private static Cadete MapeoCadete(string[] valores)
{
    int id = int.Parse(valores[0]);
    string nombre = valores[1];
    string direccion = valores[2];
    string? telefono = valores.Length > 3 ? valores[3] : null;
    return new Cadete(id, nombre, direccion, telefono);
}
private static (Pedido, int) MapeoPedido(string[] valores) 
{
    int numero = int.Parse(valores[0]);
    EstadoPedido estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), valores[1]);
    var cliente = new Cliente(valores[2], valores[3], valores[4], valores[5]);
    string? obs = valores.Length > 6 ? valores[6] : null;
    int cadeteId = int.Parse(valores[7]); 
    Pedido pedido = new Pedido(cliente, obs, numero, estado);
    return (pedido, cadeteId); 
}

private static void AsignarPedidosACadetes(List<(Pedido, int)> pedidosConCadeteId, List<Cadete> cadetes)
{
    foreach (var (pedido, cadeteId) in pedidosConCadeteId)
    {
        var cadete = cadetes.FirstOrDefault(c => c.Id == cadeteId); 
        if (cadete != null)
        {
            cadete.AgregarPedido(pedido); 
        }
    }
}

    
    public static void MostrarInformeFinal(List<Cadete> cadetes)
{
    int totalPedidosEntregados = 0;
    int totalMontoGanado = 0;
    int pagoPorPedido = 500;

    Console.WriteLine("Informe de pedidos al finalizar la jornada:");
    Console.WriteLine("--------------------------------------------");

    foreach (var cadete in cadetes)
    {
        int cantidadPedidosEntregados = 0;

        foreach (var pedido in cadete.Pedidos)
        {
            if (pedido.Estado == EstadoPedido.Entregado)
            {
                cantidadPedidosEntregados++;
            }
        }
        int montoGanado = cantidadPedidosEntregados * pagoPorPedido;
        totalPedidosEntregados += cantidadPedidosEntregados;
        totalMontoGanado += montoGanado;

        Console.WriteLine($"Cadete: {cadete.Nombre}");
        Console.WriteLine($"Cantidad de pedidos entregados: {cantidadPedidosEntregados}");
        Console.WriteLine($"Monto ganado: {montoGanado:C}");
        Console.WriteLine("--------------------------------------------");
    }
    int cantidadCadetes = cadetes.Count;
    decimal promedioEnviosPorCadete = (decimal)totalPedidosEntregados / cantidadCadetes;

    Console.WriteLine("Resumen general:");
    Console.WriteLine($"Total de pedidos entregados: {totalPedidosEntregados}");
    Console.WriteLine($"Monto total ganado: {totalMontoGanado:C}");
    Console.WriteLine($"Promedio de envíos por cadete: {promedioEnviosPorCadete:F2}");
}

}