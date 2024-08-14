using AutoMapper;
using BankingControlPanel.Application.Interfaces.Repositories;
using BankingControlPanel.Application.Mapper;
using BankingControlPanel.Application.Services;
using BankingControlPanel.Domain.Enums;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Domain.ValueObjects;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;
using Moq;

namespace BankingControlPanel.Application.UnitTests.ServicesUnitTests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _mockClientRepository= new();
        private readonly Mock<ISearchCriteriaService> _mockSearchCriteriaService= new();
        private readonly Mock<ICacheService> _mockCacheService= new();
        private readonly Mock<IMapper> _mapper =new();
        private readonly IClientService _clientService;
        

        public ClientServiceTests()
        {
            _clientService = new ClientService(_mockClientRepository.Object,_mockSearchCriteriaService.Object, _mockCacheService.Object,GetMapper());
        }

        private static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = configuration.CreateMapper();
            return mapper;
        }

        [Fact]
        public async Task AddClient_ShouldAddClient_WhenClientIsValid()
        {
            // Arrange
            var client = ValidClient();
            
            // Act
            _clientService.AddClient(WithClientDto(client));

            // Assert
            VerifyAddClientInRepoCalledOnce(client);
        }
        
        [Fact]
        public async Task GetClientsAsync_ShouldReturnCachedClients_WhenCacheHasData()
        {
            // Arrange
            var cacheKey = "Clients_123_John_IdAsc_1_10";
            var cachedClients = new List<ClientDto>
            {
                new ClientDto { FirstName = "John", LastName = "Doe" }
            };

            _mockCacheService.Setup(cache => cache.GetFromCacheAsync<IEnumerable<ClientDto>>(cacheKey))
                .ReturnsAsync(cachedClients);

            // Act
            var result = await _clientService.GetClientsAsync("John", "IdAsc", 1, 10, "123");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Value);
            Assert.Equal("John", result.Value.First().FirstName);
            _mockClientRepository.Verify(repo => repo.GetClients(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mockCacheService.Verify(cache => cache.Set(It.IsAny<string>(), It.IsAny<object>()), Times.Never);
            _mockSearchCriteriaService.Verify(service => service.SaveSearchCriteriaAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetClientsAsync_ShouldFetchAndCacheClients_WhenCacheHasNoData()
        {
            // Arrange
            var cacheKey = "Clients_123_John_IdAsc_1_10";
            var clientsFromRepo = new List<Client>
            {
                new Client { FirstName = "John", LastName = "Doe" }
            };
            var clientDtos = new List<ClientDto>
            {
                new ClientDto { FirstName = "John", LastName = "Doe" }
            };

            _mockCacheService.Setup(cache => cache.GetFromCacheAsync<IEnumerable<ClientDto>>(cacheKey))
                .ReturnsAsync((IEnumerable<ClientDto>)null);

            _mockClientRepository.Setup(repo => repo.GetClients("John", "IdAsc", 1, 10))
                .Returns(clientsFromRepo);

            _mapper.Setup(m => m.Map<IEnumerable<ClientDto>>(clientsFromRepo))
                .Returns(clientDtos);

            // Act
            var result = await _clientService.GetClientsAsync("John", "IdAsc", 1, 10, "123");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Value);
            Assert.Equal("John", result.Value.First().FirstName);
            _mockClientRepository.Verify(repo => repo.GetClients("John", "IdAsc", 1, 10), Times.Once);
            _mockCacheService.Verify(cache => cache.Set(cacheKey, It.IsAny<IEnumerable<ClientDto>>()), Times.Once);
            _mockSearchCriteriaService.Verify(service => service.SaveSearchCriteriaAsync("John_IdAsc_1_10", "123"), Times.Once);
        }
        
        private static ClientDto WithClientDto(Client client)
        {
            return new ClientDto()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email.Value,
                PersonalId = client.PersonalId.Value,
                MobileNumber = client.MobileNumber,
                Gender = client.Gender.ToString(),
                Address = new AddressDto()
                {
                    City = client.Address.City,
                    Country = client.Address.Country,
                    Street = client.Address.Street,
                    ZipCode = client.Address.ZipCode
                }
            };
        }

        private static Client ValidClient()
        {
            var client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                Email = new Email("john.doe@example.com"),
                PersonalId = new PersonalId("12345678901"),
                MobileNumber = "+1234567890",
                Gender = Gender.Male,
                Address = new Address("USA", "New York", "Main St", "12345")
            };
            return client;
        }
        private void VerifyAddClientInRepoCalledOnce(Client client)
        {
            _mockClientRepository.Verify(repo => repo.AddClient(It.Is<Client>(
                c => c.FirstName == client.FirstName &&
                     c.LastName == client.LastName &&
                     c.Email.Value == client.Email.Value &&
                     c.PersonalId.Value == client.PersonalId.Value &&
                     c.MobileNumber == client.MobileNumber &&
                     c.Gender == client.Gender &&
                     c.Address.Country == client.Address.Country &&
                     c.Address.City == client.Address.City &&
                     c.Address.Street == client.Address.Street &&
                     c.Address.ZipCode == client.Address.ZipCode
            )), Times.Once);
        }
    }
}
