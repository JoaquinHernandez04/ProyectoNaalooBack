using Microsoft.AspNetCore.Mvc;
using ClientesAPI.Models;
using ClientesAPI.Repositories;
using System.Threading.Tasks;

namespace ClientesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _clienteRepository;

        public ClientesController(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // Obtener todos los clientes
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteRepository.ObtenerClientesDeSupabase();
            return Ok(clientes);
        }

        // Obtener cliente por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var clientes = await _clienteRepository.ObtenerClientesDeSupabase();
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDTO clienteDto)
        {
            var nuevoCliente = new Cliente
            {
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Direccion = clienteDto.Direccion
            };

            await _clienteRepository.GuardarClienteEnSupabase(nuevoCliente);

            return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        // Actualizar cliente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente clienteActualizado)
        {
            var clientes = await _clienteRepository.ObtenerClientesDeSupabase();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.Nombre = clienteActualizado.Nombre;
            clienteExistente.Apellido = clienteActualizado.Apellido;
            clienteExistente.Direccion = clienteActualizado.Direccion;

            await _clienteRepository.ActualizarClienteEnSupabase(clienteExistente);

            return NoContent();
        }

        // Eliminar cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var clientes = await _clienteRepository.ObtenerClientesDeSupabase();
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteRepository.EliminarClienteDeSupabase(id);

            return NoContent();
        }
    }
}