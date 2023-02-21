using AutoMapper;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAddPictures;
using EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;
using EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPicture;
using EasyRent.RentalAd.Core.RentalAd.Commands.SetRentalAdMainPicture;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdDescription;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;
using EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdTitle;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.RentalAd.ReadModels.Dtos;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.RentalAd.Requests.V1.RentalAd;

namespace EasyRent.RentalAd.Mappers;

public class RentalAdMapperProfile : Profile
{
    public RentalAdMapperProfile()
    {
        CreateMap<CreateRentalAdRequest, CreateRentalAdCommand>();
        CreateMap<AddRentalAddPicturesRequest, AddRentalAdPicturesCommand>();
        CreateMap<SetRentalAdMainPictureRequest, SetRentalAdMainPictureCommand>();
        CreateMap<UpdateRentalAdDescriptionRequest, UpdateRentalAdDescriptionCommand>();
        CreateMap<UpdateRentalAdPricePerDayRequest, UpdateRentalAdPricePerDayCommand>();
        CreateMap<UpdateRentalAdTitleRequest, UpdateRentalAdTitleCommand>();
        CreateMap<RemoveRentalAdPictureRequest, RemoveRentalAdPictureCommand>();
        CreateMap<UpdateRentalAdAddressRequest, UpdateRentalAdAddressCommand>();
        
        MapDocuments();
        MapExternalResources();
    }
    
    private void MapDocuments()
    {
        CreateMap<RentalAdDocument, RentalAdReadModel>();
        CreateMap<PlaceAddressDto, PlaceAddressReadModel>()
            .ReverseMap();
        CreateMap<PlaceReservationDocumentDto, PlaceReservationDto>()
            .ReverseMap();
    }

    private void MapExternalResources()
    {
        CreateMap<AccountReadModel, PlaceOwnerReadModel>()
            .ForMember(dest => dest.PlaceOwnerId, opt => opt.MapFrom(x => x.AccountId))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress));
        CreateMap<PlaceFeatureReadModel, Management.ReadModels.ReadModels.PlaceFeatureReadModel>()
            .ReverseMap();
    }
}