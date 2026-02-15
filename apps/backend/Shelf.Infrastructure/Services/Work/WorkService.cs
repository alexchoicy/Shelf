using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shelf.Core.Entity;
using Shelf.Core.Enum;
using Shelf.Core.Exceptions;
using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Data;

namespace Shelf.Infrastructure.Services.Work;

public class WorkService(AppDbContext _db, IStorageService _storageService) : IWorkService
{

    public async Task<WorkCreationResponse> CreateWorkAsync(WorkCreationRequest request, string userId, CancellationToken cancellationToken)
    {
        bool isTitleExisting = await _db.Works.AnyAsync(w => w.Title == request.Title, cancellationToken: cancellationToken);
        if (isTitleExisting)
        {
            throw new DuplicateEntityException($"A work with the title '{request.Title}' already exists.");
        }

        await using IDbContextTransaction transaction = await _db.Database
            .BeginTransactionAsync(cancellationToken);


        try
        {
            WorkCreationResponse response = await CreateSingleWorkAsync(request, userId, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

    }

    private async Task<WorkCreationResponse> CreateSingleWorkAsync(WorkCreationRequest request, string userId, CancellationToken cancellationToken)
    {
        Core.Entity.Work newWork = new()
        {
            UploaderId = userId,
            Title = request.Title,
            Description = request.Description,
            IsAI = request.IsAI,
            Medium = request.Medium,
            Type = request.Type,
            Visibility = request.Visibility,
            Rating = request.Rating,
            ReleasedAt = request.ReleasedAt
        };

        _db.Works.Add(newWork);

        foreach (WorkCreationCredit creditRequest in request.Credits)
        {
            WorkCredit newCredit = new()
            {
                WorkId = newWork.Id,
                PartyId = creditRequest.PartyId,
                Role = creditRequest.Role
            };

            _db.WorkCredits.Add(newCredit);
        }

        foreach (Guid characterId in request.CharacterIds)
        {
            WorkCharacter newWorkCharacter = new()
            {
                WorkId = newWork.Id,
                CharacterId = characterId
            };

            _db.WorkCharacters.Add(newWorkCharacter);
        }

        WorkCreationResponse workCreationResponse = new()
        {
            WorkId = newWork.Id,
            Title = newWork.Title,
            MediaItems = new List<MediaItemUploadInfo>()
        };

        foreach (MediaItemCreationRequest mediaItemRequest in request.MediaItems)
        {
            switch (mediaItemRequest.MediaType)
            {
                case MediaItemType.Video:
                case MediaItemType.Image:
                    if (string.IsNullOrEmpty(mediaItemRequest.SimpleBlake3) || string.IsNullOrEmpty(mediaItemRequest.MimeType) ||
                        mediaItemRequest.FileSize == null || string.IsNullOrEmpty(mediaItemRequest.OriginalFileName))
                    {
                        throw new ArgumentException("Media item of type Video or Image must have SimpleBlake3, MimeType, FileSize, and OriginalFileName.");
                    }
                    Guid id = Guid.CreateVersion7();
                    string storagePath = _storageService.GetStoragePath(MediaVariantPurpose.Original, id, mediaItemRequest.MimeType);

                    Core.Entity.File mediaFile = new()
                    {
                        Id = id,
                        SimpleBlake3Hash = mediaItemRequest.SimpleBlake3,
                        MimeType = mediaItemRequest.MimeType!,
                        OriginalFileName = mediaItemRequest.OriginalFileName,
                        SizeInBytes = mediaItemRequest.FileSize.Value,
                        FileObjectVariant = FileObjectVariant.Original,
                        StoragePath = storagePath,
                    };
                    _db.Files.Add(mediaFile);

                    MediaAsset newMediaAsset = new()
                    {
                        WorkId = newWork.Id,
                        Description = mediaItemRequest.Description,
                        Order = mediaItemRequest.Order,
                        Kind = mediaItemRequest.Kind,
                        MediaType = mediaItemRequest.MediaType,
                    };
                    _db.MediaAssets.Add(newMediaAsset);

                    MediaVariant newMediaVariant = new()
                    {
                        MediaAssetId = newMediaAsset.Id,
                        FileId = mediaFile.Id,
                        Purpose = MediaVariantPurpose.Original,

                    };
                    _db.MediaVariants.Add(newMediaVariant);

                    workCreationResponse.MediaItems.Add(new MediaItemUploadInfo
                    {
                        SimpleBlake3Id = mediaItemRequest.SimpleBlake3!,
                        FileName = mediaItemRequest.OriginalFileName!,
                        MultipartUploadInfo = await _storageService.CreateMultipartUploadAsync(storagePath, mediaItemRequest.MimeType!, mediaItemRequest.FileSize.Value, cancellationToken)
                    });

                    break;
                case Core.Enum.MediaItemType.Text:
                    MediaAsset newTextMediaAsset = new()
                    {
                        WorkId = newWork.Id,
                        TextContent = mediaItemRequest.TextContent,
                        Description = mediaItemRequest.Description,
                        Order = mediaItemRequest.Order,
                        Kind = mediaItemRequest.Kind,
                        MediaType = mediaItemRequest.MediaType,
                    };
                    _db.MediaAssets.Add(newTextMediaAsset);
                    break;
            }

        }
        return workCreationResponse;
    }

}
