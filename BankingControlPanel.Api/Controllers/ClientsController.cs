using BankingControlPanel.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClients([FromQuery] string filter, [FromQuery] string sort, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var clients = _clientService.GetClients(filter, sort, page, pageSize);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] ClientDto clientDto)
        {
            _clientService.AddClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = clientDto.PersonalId }, clientDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            _clientService.UpdateClient(id, clientDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            _clientService.DeleteClient(id);
            return NoContent();
        }

        [HttpGet("search-parameters")]
        public IActionResult GetLastSearchParameters([FromQuery] int count = 3)
        {
            var parameters = _clientService.GetLastSearchParameters(count);
            return Ok(parameters);
        }
    }
}
