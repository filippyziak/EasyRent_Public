using AutoMapper;
using EasyRent.Identity.Core.Handlers.Command.LoginAccount;
using EasyRent.Identity.Core.Handlers.Command.RegisterAccount;
using EasyRent.Identity.Core.Handlers.Command.UpdateAccountEmailAddress;
using EasyRent.Identity.Core.Handlers.Command.UpdateAccountPassword;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Infrastructure.DocumentStore.Documents;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Identity.Requests;

namespace EasyRent.Identity.Mappers;

public class IdentityMapperProfile : Profile
{
    public IdentityMapperProfile()
    {
        CreateMap<RegisterAccountRequest, RegisterAccountCommand>();
        CreateMap<LoginAccountRequest, LoginAccountCommand>();
        CreateMap<UpdateAccountEmailAddressRequest, UpdateAccountEmailAddressCommand>();
        CreateMap<UpdateAccountPasswordRequest, UpdateAccountPasswordCommand>();
        MapDocuments();
    }


    private void MapDocuments()
    {
        CreateMap<AccountReadModel, AccountDocument>()
            .ReverseMap();
        CreateMap<Account, AccountReadModel>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(x => x.Id.Value))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress.Value))
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(x => x.Type.Value));
    }
}