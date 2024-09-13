namespace clases{


public enum EstadoPedido
{
    Procesando = 1,
    Enviado,
    Entregado,
    Cancelado
}

public class Cliente
{
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? ReferenciaDireccion { get; set; }

    // Constructor
    public Cliente(string nombre, string direccion, string telefono, string referenciaDireccion)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ReferenciaDireccion = referenciaDireccion;
    }
}

public class Pedido
{
    public int Numero { get; set; }
    public EstadoPedido Estado { get; set; }
    public Cliente Cliente { get; set; }
    public string? Obs { get; set; }
    public int CadeteID{get;set;}

    // Constructor
    public Pedido(Cliente cliente, string? obs, int numero, EstadoPedido estado,int cadeteID)
    {
        Cliente = cliente;
        Obs = obs;
        Numero = numero;
        Estado = estado;
        CadeteID=cadeteID;
    }

    // Funciones
    public string VerDireccionCliente()
    {
        return Cliente.Direccion;
    }

    public string VerDatosCliente()
    {
        return $"Nombre: {Cliente.Nombre}\nDireccion: {Cliente.Direccion}\nTelefono: {Cliente.Telefono}\nReferencia Direccion: {Cliente.ReferenciaDireccion}";
    }
}
public class Cadete
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public List<Pedido> Pedidos { get; set; }  //lista de pedidos

    //Constructora
    public Cadete(int id, string nombre, string direccion, string? telefono = null)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Pedidos = new List<Pedido>(); 
    }

    public void AgregarPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
    }
    public void RemoverPedido(Pedido pedido){
        Pedidos.Remove(pedido);
    }
    public bool TienePedido(int numeroPedido)
    {
        return Pedidos.Any(p => p.Numero == numeroPedido);
    }
}

public class Cadeteria{
    public string? Nombre {get;set;}
    public string? Telefono {get;set;}
    public List<Cadete>? ListadoCadetes {get;set;}

    //constructora
    public Cadeteria(string nombre, string telefono){
        Nombre = nombre;
        Telefono = telefono;
        ListadoCadetes = new List<Cadete>();
    }

    //metodos adicionales
    public void AgregarCadete(Cadete CadeteNuevo){
        ListadoCadetes.Add(CadeteNuevo);
    }

    public void EliminarCadete(Cadete CadeteABorrar){
        ListadoCadetes.Remove(CadeteABorrar);
    }

    public void AsignarPedido(Cadete Cadete,Pedido pedido){
        Cadete.AgregarPedido(pedido);
        pedido.Estado = EstadoPedido.Procesando;
    }

    public static void MostrarPedidos(List<Cadete> ListadoCadetes){
        foreach (Cadete cadete in ListadoCadetes){
            Console.WriteLine($"Cadete:{cadete.Nombre}");
            foreach(Pedido pedido in cadete.Pedidos){
                Console.WriteLine($"Pedido Número: {pedido.Numero}, Estado: {pedido.Estado}");
            }

        }
    }
    public int JornalACobrar(int CadeteID){
        int jornal = 0;
        int monto = 500;
        foreach(Cadete cadete in ListadoCadetes){
            if(cadete.Id == CadeteID){
                foreach(Pedido pedido in cadete.Pedidos){
                    if(pedido.Estado==EstadoPedido.Entregado){
                        jornal += monto;
                    }
                }
        }
    }
    return jornal;
    }   
    public void AsignarCadeteAPedido(int CadeteID,int PedidoID){
        Cadete CadeteEncontrado=null;
        Pedido PedidoEncontrado=null;
        foreach(Cadete cadete in ListadoCadetes){
            if(cadete.Id == CadeteID){
                CadeteEncontrado=cadete;
                break;
        }
        if(CadeteEncontrado!=null){
            foreach(Pedido pedido in CadeteEncontrado.Pedidos){
                if(pedido.Numero == PedidoID){
                    PedidoEncontrado=pedido;
                    CadeteEncontrado.AgregarPedido(PedidoEncontrado);
                    break;
            }else{
                Console.WriteLine("No se encontró el pedido");
            }}
        }else{
            Console.WriteLine("No se encontró el cadete");
        }
}

}}}