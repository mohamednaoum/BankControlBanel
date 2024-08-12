using AutoMapper;
using BankingControlPanel.Application.Services;
using BankingControlPanel.Domain.Enums;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Domain.ValueObjects;
using BankingControlPanel.Infrastructure.Repositories;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;
using Moq;

namespace BankingControlPanel.Application.UnitTests.ServicesUnitTests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _mockClientRepository= new();
        private readonly Mock<IMapper> _mapper =new();
        private readonly IClientService _clientService;
        

        public ClientServiceTests()
        {
            _clientService = new ClientService(_mockClientRepository.Object,_mapper.Object);
        }

        [Fact]
        public async Task AddClient_ShouldAddClient_WhenClientIsValid()
        {
            // Arrange
            var client = ValidClient();
            
            // Act
            await _clientService.AddClientAsync(WithClientDto(client));

            // Assert
            VerifyAddClientInRepoCalledOnce(client);
        }


        [Fact]
        public void GetAllClients_ShouldReturnClients_WhenClientsExist()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { FirstName = "John", LastName = "Doe" },
                new Client { FirstName = "Jane", LastName = "Smith" }
            };

            _mockClientRepository.Setup(repo => repo.GetClients(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<int>(),It.IsAny<int>())).Returns(clients);

            // Act
            var result =  _clientService.GetClients("dummeyFilter","dummySort",0,10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
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
                     c.Email == client.Email &&
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
