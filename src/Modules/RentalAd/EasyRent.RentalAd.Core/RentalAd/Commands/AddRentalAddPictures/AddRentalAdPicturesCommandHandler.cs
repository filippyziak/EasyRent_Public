using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAddPictures;

public class AddRentalAdPicturesCommandHandler : IRequestHandler<AddRentalAdPicturesCommand, AddRentalAdPicturesResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IFileStorageService _fileStorageService;

    public AddRentalAdPicturesCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IFileStorageService fileStorageService)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<AddRentalAdPicturesResponse> Handle(AddRentalAdPicturesCommand request, CancellationToken cancellationToken)
    {
        string[] allowedFormats = { "JPG", "PNG" };

        if (!await _aggregateRepository.ExistsAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId)))
        {
            throw new EntityNotFoundException(request.RentalAdId, typeof(Domain.RentalAd.RentalAd));
        }

        var rentalAd = await _aggregateRepository.LoadAsync<Domain.RentalAd.RentalAd, RentalAdId>(new RentalAdId(request.RentalAdId));

        if (rentalAd.PlaceOwner.PlaceOwnerId != request.CurrentOwnerId)
        {
            throw new PermissionException(typeof(Domain.RentalAd.RentalAd));
        }
        
        var urlsToReturn = new List<string>();
        
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
                
                urlsToReturn.Add(uploadedFileModel.Url);
            }
            catch (Exception)
            {
                await _fileStorageService.DeleteFileAsync(pictureId.ToString(), cancellationToken);
                throw;
            }
        }

        await _aggregateRepository.SaveAsync<Domain.RentalAd.RentalAd, RentalAdId>(rentalAd);

        _logger.Info("Rental ad with ID: {RentalAdId} updated in the event stream",
            rentalAd.Id.Value);

        return new AddRentalAdPicturesResponse
        {
            PicturesUrls = urlsToReturn
        };
    }
}