using AutoMapper;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Domain.ValueObjects;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Client to ClientDto mapping
        CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));

        // ClientDto to Client mapping
        CreateMap<ClientDto, Client>()
            .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));

        // Account to AccountDto mapping
        CreateMap<Account, AccountDto>();

        // AccountDto to Account mapping
        CreateMap<AccountDto, Account>();

        // Address to AddressDto mapping
        CreateMap<Address, AddressDto>();

        // AddressDto to Address mapping
        CreateMap<AddressDto, Address>();
    }
}