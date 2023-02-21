using AutoMapper;
using EasyRent.Management.Core.Handlers.Accounts.Command.ActivateAccount;
using EasyRent.Management.Core.Handlers.Accounts.Command.SuspendAccount;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;
using EasyRent.Management.ReadModels.ReadModels;
using EasyRent.Management.Requests;

namespace EasyRent.Management.Mappers;

public class ManagementMapperProfile : Profile
{
    public ManagementMapperProfile()
    {
        MapDocuments();
        CreateMap<ActivateAccountRequest, ActivateAccountCommand>();
        CreateMap<SuspendAccountRequest, SuspendAccountCommand>();
    }


    private void MapDocuments()
    {
        CreateMap<PlaceFeatureReadModel, PlaceFeatureDocument>()
            .ReverseMap();
    }
}