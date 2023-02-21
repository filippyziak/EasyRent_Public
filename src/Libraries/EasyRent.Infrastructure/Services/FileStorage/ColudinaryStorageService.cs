using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage.Models;
using EasyRent.Infrastructure.Services.FileStorage.Configs;
using EasyRent.Infrastructure.Services.FileStorage.Factories;
using EasyRent.Shared;
using EasyRent.Shared.Exceptions;
using Microsoft.Extensions.Options;

namespace EasyRent.Infrastructure.Services.FileStorage;

public class ColudinaryStorageService : IFileStorageService
{
    private readonly ILogger _logger;
    private readonly Cloudinary _cloudinaryClient;
    
    public ColudinaryStorageService(ILogger logger,
        IOptionsMonitor<CloudinaryFileStorageProviderConfiguration> fileStorageProviderConfigurationOptions)
    {
        _logger = logger;
        _cloudinaryClient = ColudinaryStorageServiceFactory.CreateCloudinaryClient(fileStorageProviderConfigurationOptions.CurrentValue);
    }
    
    public async Task<UploadedFileModel> UploadFileAsync(string publicId, Stream fileStream, string[] allowedFormats = null, CancellationToken cancellationToken = default)
    {
        Validator.Text.NotNullOrEmpty(publicId);

        var uploadParams = new AutoUploadParams
        {
            File = new FileDescription(publicId, fileStream),
            PublicId = publicId,
            UseFilename = false,
            UniqueFilename = true,
            Overwrite = true,
            AllowedFormats = allowedFormats
        };

        var uploadResult = await _cloudinaryClient.UploadAsync(uploadParams, cancellationToken);

        if (uploadResult.Error is not null)
            throw new FileStorageException(uploadResult.Error.Message);

        var uploadedFileModel = new UploadedFileModel(uploadResult.PublicId,
            uploadResult.Url.ToString());

        _logger.Trace("File with public id: {PublicId} uploaded to the cloud file storage. Resource URL: {Url}",
            uploadedFileModel.PublicIdentifier,
            uploadedFileModel.Url);

        return uploadedFileModel;
    }

    public async Task<bool> DeleteFileAsync(string publicId, CancellationToken cancellationToken = default)
    {
        Validator.Text.NotNullOrEmpty(publicId);

        var deleteParams = new DelResParams
        {
            PublicIds = new List<string> { publicId },
            KeepOriginal = false
        };

        var deleteResult = await _cloudinaryClient.DeleteResourcesAsync(deleteParams, cancellationToken);

        if (deleteResult.Error is not null)
            throw new FileStorageException(deleteResult.Error.Message);

        _logger.Trace("File with public id: {PublicId} deleted from the cloud file storage", publicId);

        return true;
    }
}