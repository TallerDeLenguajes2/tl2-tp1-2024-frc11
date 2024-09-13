2)  a- ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
    R: Cliente tiene una relacion de composicion con pedido, cadete tiene una relacion de agregacion con pedidos y con cadeteria(puesto que los cadetes siguen existiendo sin la cadeteria)
    b-¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
    R:Los metodos que deberia tener la cadeteria serian: agregarCadete,borrarCadete,AsignarPedidoACadete,mostrarCadetes,mostrarPedidosPendientes,mostrarPedidosCadete.
    Y cadete agregarPedido,removerPedido,obtenerPedido,mostrarPedidos.
    c- Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
    Públicos:
    Cliente: Nombre, Direccion, Telefono, ReferenciaDireccion para acceder desde otras clases.
    Pedido: Numero, Estado, Cliente, Obs para facilitar la gestión de pedidos.
    Cadete: Id, Nombre, Direccion, Telefono, Pedidos para manipular los datos del cadete
    d-¿Cómo diseñaría los constructores de cada una de las clases?
    R:public Pedido(Cliente cliente, string? obs, int numero, EstadoPedido estado)
    {
        Cliente = cliente;
        Obs = obs;
        Numero = numero;
        Estado = estado;
    }
    public Cadete(int id, string nombre, string direccion, string? telefono = null)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        Pedidos = new List<Pedido>(); 
    }
    // Constructor
    public Cliente(string nombre, string direccion, string telefono, string referenciaDireccion)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ReferenciaDireccion = referenciaDireccion;
    }
    e-¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
    R:Se podria haber implementado un gestor de pedidos para facilitar un poco todo 