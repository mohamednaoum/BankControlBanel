using AutoMapper;
using BankingControlPanel.Application.Mapper;
using BankingControlPanel.Domain.Enums;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Domain.ValueObjects;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.UnitTests.MapperTests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Should_Map_Client_To_ClientDto()
        {
            // Arrange
            var client = new Client
            {
                Email = new Email("client@example.com"),
                FirstName = "John",
                LastName = "Doe",
                PersonalId = new PersonalId("12345678901"),
                ProfilePhoto = "photo.png",
                MobileNumber = "+1234567890",
                Gender = Gender.Male,
                Address = new Address
                (
                    "USA", 
                    "New York",
                    "5th Avenue",
                    "10001"
                ),
                Accounts = new List<Account>
                {
                    new Account { AccountNumber = "123456789", Balance = 1000 }
                }
            };

            // Act
            var clientDto = _mapper.Map<ClientDto>(client);

            // Assert
            Assert.Equal(client.Email.Value, clientDto.Email);
            // Assert.Equal(client.FirstName, clientDto.FirstName);
            // Assert.Equal(client.LastName, clientDto.LastName);
            // Assert.Equal(client.PersonalId.Value, clientDto.PersonalId);
            // Assert.Equal(client.ProfilePhoto, clientDto.ProfilePhoto);
            // Assert.Equal(client.MobileNumber, clientDto.MobileNumber);
            // Assert.Equal(client.Gender.ToString(), clientDto.Gender);
            // Assert.Equal(client.Address.Country, clientDto.Address.Country);
            // Assert.Equal(client.Address.City, clientDto.Address.City);
            // Assert.Equal(client.Address.Street, clientDto.Address.Street);
            // Assert.Equal(client.Address.ZipCode, clientDto.Address.ZipCode);
            // Assert.Single(clientDto.Accounts);
            // Assert.Equal(client.Accounts[0].AccountNumber, clientDto.Accounts[0].AccountNumber);
            // Assert.Equal(client.Accounts[0].Balance, clientDto.Accounts[0].Balance);
        }

        [Fact]
        public void Should_Map_ClientDto_To_Client()
        {
            // Arrange
            var clientDto = new ClientDto
            {
                Email = "client@example.com",
                FirstName = "John",
                LastName = "Doe",
                PersonalId = "12345678901",
                ProfilePhoto = "photo.png",
                MobileNumber = "+1234567890",
                Gender = "Male",
                Address = new AddressDto
                {
                    Country = "USA",
                    City = "New York",
                    Street = "5th Avenue",
                    ZipCode = "10001"
                },
                Accounts = new List<AccountDto>
                {
                    new AccountDto { AccountNumber = "123456789", Balance = 1000 }
                }
            };

            // Act
            var client = _mapper.Map<Client>(clientDto);

            // Assert
            Assert.Equal(clientDto.Email, client.Email.Value);
            Assert.Equal(clientDto.FirstName, client.FirstName);
            Assert.Equal(clientDto.LastName, client.LastName);
            Assert.Equal(clientDto.PersonalId, client.PersonalId.Value);
            Assert.Equal(clientDto.ProfilePhoto, client.ProfilePhoto);
            Assert.Equal(clientDto.MobileNumber, client.MobileNumber);
            Assert.Equal(clientDto.Gender, client.Gender.ToString());
            Assert.Equal(clientDto.Address.Country, client.Address.Country);
            Assert.Equal(clientDto.Address.City, client.Address.City);
            Assert.Equal(clientDto.Address.Street, client.Address.Street);
            Assert.Equal(clientDto.Address.ZipCode, client.Address.ZipCode);
            Assert.Single(client.Accounts);
            Assert.Equal(clientDto.Accounts[0].AccountNumber, client.Accounts[0].AccountNumber);
            Assert.Equal(clientDto.Accounts[0].Balance, client.Accounts[0].Balance);
        }

        [Fact]
        public void Should_Map_Account_To_AccountDto()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = "123456789",
                Balance = 1000
            };

            // Act
            var accountDto = _mapper.Map<AccountDto>(account);

            // Assert
            Assert.Equal(account.AccountNumber, accountDto.AccountNumber);
            Assert.Equal(account.Balance, accountDto.Balance);
        }

        [Fact]
        public void Should_Map_AccountDto_To_Account()
        {
            // Arrange
            var accountDto = new AccountDto
            {
                AccountNumber = "123456789",
                Balance = 1000
            };

            // Act
            var account = _mapper.Map<Account>(accountDto);

            // Assert
            Assert.Equal(accountDto.AccountNumber, account.AccountNumber);
            Assert.Equal(accountDto.Balance, account.Balance);
        }

        [Fact]
        public void Should_Map_Address_To_AddressDto()
        {
            // Arrange
            var address = new Address
            (
                 "USA",
                 "New York",
                 "5th Avenue",
                 "10001"
            );

            // Act
            var addressDto = _mapper.Map<AddressDto>(address);

            // Assert
            Assert.Equal(address.Country, addressDto.Country);
            Assert.Equal(address.City, addressDto.City);
            Assert.Equal(address.Street, addressDto.Street);
            Assert.Equal(address.ZipCode, addressDto.ZipCode);
        }

        [Fact]
        public void Should_Map_AddressDto_To_Address()
        {
            // Arrange
            var addressDto = new AddressDto
            {
                Country = "USA",
                City = "New York",
                Street = "5th Avenue",
                ZipCode = "10001"
            };

            // Act
            var address = _mapper.Map<Address>(addressDto);

            // Assert
            Assert.Equal(addressDto.Country, address.Country);
            Assert.Equal(addressDto.City, address.City);
            Assert.Equal(addressDto.Street, address.Street);
            Assert.Equal(addressDto.ZipCode, address.ZipCode);
        }
    }
}
