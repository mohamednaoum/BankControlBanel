using BankingControlPanel.Api.Helpers;
using BankingControlPanel.Application.Services;
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
        private readonly ISearchCriteriaService _searchCriteriaService;

        public ClientsController(IClientService clientService ,ISearchCriteriaService searchCriteriaService)
        {
            _clientService = clientService;
            _searchCriteriaService = searchCriteriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] string filter, [FromQuery] string sort, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var clients = await _clientService.GetClients(filter, sort, page, pageSize, User.GetUserId()!);
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
        public async Task<IActionResult> GetLastSearchParameters([FromQuery] int count = 3)
        {
            var parameters = await _searchCriteriaService.GetLastSearchCriteriasAsync(count,User.GetUserId()!);
            return Ok(parameters);
        }
    }
}
