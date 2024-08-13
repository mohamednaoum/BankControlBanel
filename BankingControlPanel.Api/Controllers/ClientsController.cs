using BankingControlPanel.Api.Helpers;
using BankingControlPanel.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(IClientService clientService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] string filter, [FromQuery] string sort, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var clients = await clientService.GetClients(filter, sort, page, pageSize, User.GetUserId()!);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] ClientDto clientDto)
        {
            clientService.AddClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = clientDto.PersonalId }, clientDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            clientService.UpdateClient(id, clientDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            clientService.DeleteClient(id);
            return NoContent();
        }

        [HttpGet("search-parameters")]
        public IActionResult GetLastSearchParameters([FromQuery] int count = 3)
        {
            var parameters = clientService.GetLastSearchParameters(count);
            return Ok(parameters);
        }
    }
}
