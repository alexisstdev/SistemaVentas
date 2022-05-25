using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sistema_de_Ventas
{
    [Serializable]
    public class Compra
    {
        public List<Compra> misCompras = new List<Compra>();
        public List<Producto> misProductosCompra = new List<Producto>();

        public DateTime FechaCompra { get; set; }
        public string IDCompra { get; set; }
        public double TotalCompra { get; set; }

        public double CalcularTotal()
        {
            TotalCompra = 0;
            foreach (Producto miProducto in misProductosCompra)
            {
                TotalCompra += miProducto.PrecioCompra * miProducto.StockProducto;
            }
            return TotalCompra;
        }

        public void FinalizarCompra()
        {
            List<Producto> productosEnMemoria = Serializador.Deserializar<List<Producto>>("misProductos.json");

            foreach (Producto miProducto in misProductosCompra)
            {
                int i = productosEnMemoria.FindIndex(x => x.IDProducto == miProducto.IDProducto);
                if (i == -1)
                {
                    productosEnMemoria.Add(miProducto);
                }
                else
                {
                    productosEnMemoria[i].StockProducto += miProducto.StockProducto;
                    productosEnMemoria[i].PrecioCompra = miProducto.PrecioCompra;
                    productosEnMemoria[i].PrecioVenta = miProducto.PrecioVenta;
                }
            }

            Serializador.Serializar("misProductos.json", productosEnMemoria);
        }

        public void SerializarListaCompra()
        {
            Serializador.Serializar("misCompras.json", misCompras);
        }

        public List<Compra> DeserializarListaCompra()
        {
            if (Serializador.Deserializar<List<Compra>>("misCompras.json") == default) return misCompras;
            return misCompras = Serializador.Deserializar<List<Compra>>("misCompras.json");
        }
    }

    [Serializable]
    public class Producto
    {
        public List<Producto> misProductos = new List<Producto>();

        public string IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Proveedor { get; set; }
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }
        public int StockProducto { get; set; }

        public void SerializarLista()
        {
            Serializador.Serializar("misProductos.json", misProductos);
        }

        public List<Producto> DeserializarLista()
        {
            if (Serializador.Deserializar<List<Producto>>("misProductos.json") == default) return misProductos;
            return misProductos = Serializador.Deserializar<List<Producto>>("misProductos.json");
        }
    }

    [Serializable]
    public class Cliente
    {
        public List<Cliente> misClientes = new List<Cliente>();

        public string Correo { get; set; }
        public string IDCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }

        public void SerializarLista()
        {
            Serializador.Serializar("misClientes.json", misClientes);
        }

        public void DeserializarLista()
        {
            if (Serializador.Deserializar<List<Cliente>>("misClientes.json") == default) return;
            misClientes = Serializador.Deserializar<List<Cliente>>("misClientes.json");
        }
    }

    [Serializable]
    public class Usuario
    {
        public List<Usuario> misUsuarios = new List<Usuario>();
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public string IDUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }

        public void SerializarLista()
        {
            Serializador.Serializar("misUsuarios.json", misUsuarios);
        }

        public void DeserializarLista()
        {
            if (Serializador.Deserializar<List<Usuario>>("misUsuarios.json") == default) return;
            misUsuarios = Serializador.Deserializar<List<Usuario>>("misUsuarios.json");
        }
    }

    public class Venta
    {
        public int Cantidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public string IDVenta { get; set; }
        public double Montocambio { get; set; }
        public double MontoPago { get; set; }
        public double Precio { get; set; }
        public double TotalCompra { get; set; }
    }

    public static class Serializador
    {
        private static string carpeta = @"..\..\..\DatosJson\";

        public static void Serializar(string nombreArchivo, object miObjeto)
        {
            Directory.CreateDirectory(carpeta);

            using (Stream stream = File.Open(carpeta + nombreArchivo, FileMode.Create))
            {
                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    JsonSerializer.Serialize(stream, miObjeto, options);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static T Deserializar<T>(string nombreArchivo)
        {
            Directory.CreateDirectory(carpeta);

            T objeto;
            using (Stream stream = File.Open(carpeta + nombreArchivo, FileMode.OpenOrCreate))
            {
                if (stream.Length == 0) return default;
                try
                {
                    objeto = JsonSerializer.Deserialize<T>(stream);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return objeto;
        }
    }
}