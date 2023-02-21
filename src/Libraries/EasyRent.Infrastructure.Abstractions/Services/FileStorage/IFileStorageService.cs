using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Services.FileStorage.Models;

namespace EasyRent.Infrastructure.Abstractions.Services.FileStorage;

public interface IFileStorageService
{
    Task<UploadedFileModel> UploadFileAsync(string publicId,
        Stream fileStream,
        string[] allowedFormats = null,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteFileAsync(string publicId, CancellationToken cancellationToken = default);
}