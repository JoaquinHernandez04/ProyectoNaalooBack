using Supabase;
using Supabase.Postgrest;
using static Supabase.Postgrest.Constants; 
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientesAPI.Models;

namespace ClientesAPI.Repositories
{
    public class ClienteRepository
    {
        private readonly Supabase.Client _supabaseClient;

        // Constructor que recibe el cliente de Supabase
        public ClienteRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        // Método para obtener todos los clientes desde Supabase
        public async Task<List<Cliente>> ObtenerClientesDeSupabase()
        {
            var response = await _supabaseClient
                .From<Cliente>()               // Usamos la tabla cliente
                .Get();                        // Ejecuta la consulta

            return response.Models;            // Devuelve la lista de clientes
        }

        // Método para obtener un cliente por ID desde Supabase
        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            var response = await _supabaseClient
                .From<Cliente>()
                .Filter("id", Operator.Equals, id)  // Filtra por ID
                .Get();                 // Retorna un solo resultado o null si no existe

            return response.Models.FirstOrDefault();;
        }

        // Método para guardar un nuevo cliente en Supabase
        public async Task GuardarClienteEnSupabase(Cliente cliente)
        {
            await _supabaseClient
                .From<Cliente>()
                .Insert(cliente);  // Inserta el nuevo cliente
        }

        // Método para actualizar un cliente en Supabase
        public async Task ActualizarClienteEnSupabase(Cliente cliente)
        {
            await _supabaseClient
                .From<Cliente>()
                .Match(new Dictionary<string, string> { { "id", cliente.Id.ToString() } }) 
                .Update(cliente);  // Actualiza los campos del cliente
        }

        // Método para eliminar un cliente en Supabase
        public async Task EliminarClienteDeSupabase(int id)
        {
            await _supabaseClient
                .From<Cliente>()
                .Match(new Dictionary<string, string> { { "id", id.ToString() } })  // Filtra por ID del cliente a eliminar
                .Delete();
        }
    }
}