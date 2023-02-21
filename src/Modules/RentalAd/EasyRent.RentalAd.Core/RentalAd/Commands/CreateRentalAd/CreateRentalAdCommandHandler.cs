using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;

public class CreateRentalAdCommandHandler : IRequestHandler<CreateRentalAdCommand, CreateRentalAdResponse>
{
    private readonly ILogger _logger;
    private readonly IPlaceOwnerReadOnlyRepository _placeOwnerReadOnlyRepository;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IFileStorageService _fileStorageService;

    public CreateRentalAdCommandHandler(ILogger logger,
        IPlaceOwnerReadOnlyRepository placeOwnerReadOnlyRepository,
        IAggregateRepository aggregateRepository,
        IFileStorageService fileStorageService)
    {
        _logger = logger;
        _placeOwnerReadOnlyRepository = placeOwnerReadOnlyRepository;
        _aggregateRepository = aggregateRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<CreateRentalAdResponse> Handle(CreateRentalAdCommand request, CancellationToken cancellationToken)
    {
        var rentalAdId = Guid.NewGuid();

        var placeOwner = await _placeOwnerReadOnlyRepository.GetPlaceOwnerByIdAsync(request.PlaceOwnerId)
                         ?? throw new EntityNotFoundException(request.PlaceOwnerId, typeof(PlaceOwnerReadModel));

        var rentalAd = new Domain.RentalAd.RentalAd(new RentalAdId(rentalAdId),
            new PlaceOwnerId(placeOwner.PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(placeOwner.EmailAddress),
            RentalAdTitle.FromString(request.Title),
            RentalAdDescription.FromString(request.Description),
            new PlaceAddressId(Guid.NewGuid()),
            PlaceAddressCountry.FromString(request.Country),
            PlaceAddressCity.FromString(request.City),
            PlaceAddressStreet.FromString(request.Street),
            RentalAdPricePerDay.FromDecimal(request.PricePerDay)
        );

        string[] allowedFormats = { "JPG", "PNG" };
        
        foreach (var picture in request.PictureFiles)
        {
            var pictureId = new PlacePictureId(Guid.NewGuid());

            try
            {
                var uploadedFileModel = await _fileStorageService.UploadFileAsync(pictureId.Value.ToString(),
                    picture.OpenReadStream(),
                    allowedFormats,
                    cancellationToken);

                rentalAd.AddPicture(pictureId,
                    PlacePictureUrl.FromString(uploadedFileModel.Url));
            }
            catch (Exception)
            {
                await _fileStorageService.DeleteFileAsync(pictureId.ToString(), cancellationToken);
                throw;
            }
        }
        
        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental Ad with Id: {RentalAdId} stored", rentalAd.Id);

        return new CreateRentalAdResponse();
    }
}