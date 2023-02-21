using AutoMapper;
using EasyRent.Reservation.Core.Reservation.Commands.CreatePlaceReservation;
using EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;
using EasyRent.Reservation.Infrastructure.DocumentStore.Documents;
using EasyRent.Reservation.ReadModels.ReadModels;
using EasyRent.Reservation.Requests;

namespace EasyRent.Reservation.Mappers;

public class ReservationMapperProfile : Profile
{
    public ReservationMapperProfile()
    {
        MapDocuments();
        CreateMap<CreatePlaceReservationRequest, CreatePlaceReservationCommand>();
        CreateMap<ReviewPlaceReservationRequest, ReviewPlaceReservationCommand>();
    }


    private void MapDocuments()
    {
        CreateMap<PlaceReservationReadModel, PlaceReservationDocument>()
            .ReverseMap();
    }
}