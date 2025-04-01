using Application.Core;
using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Photos;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error is not null) throw new Exception(result.Error.Message);

        return result.Result;
    }

    public async Task<PhotoUploadResult?> UploadPhotoAsync(IFormFile file)
    {
        if (file.Length <= 0) return null;

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "MovieBooker"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error is not null) throw new Exception(uploadResult.Error.Message);

        var hiddenFolderPublicId = RemoveFolderFromPublicId(uploadResult.PublicId, uploadParams.Folder);

        return new PhotoUploadResult
        {
            PublicId = hiddenFolderPublicId,
            Url = uploadResult.SecureUrl.AbsoluteUri
        };
    }

    private static string RemoveFolderFromPublicId(string publicId, string folderName)
    {
        var folderPrefix = folderName + "/";

        if (publicId.StartsWith(folderPrefix)) return publicId.Substring(folderPrefix.Length);

        return publicId;
    }
}
