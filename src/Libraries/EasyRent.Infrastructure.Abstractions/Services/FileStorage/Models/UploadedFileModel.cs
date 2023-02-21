namespace EasyRent.Infrastructure.Abstractions.Services.FileStorage.Models;

public record UploadedFileModel
(
    string PublicIdentifier,
    string Url
);