using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Core.StorageServices;
using Shelf.Infrastructure.Data;

namespace Shelf.Infrastructure.Services.Work;

public class WorkService(AppDbContext _db, IStorageProvider _storageProvider) : IWorkService
{

    public async Task<WorkCreationResponse> CreateWorkAsync(WorkCreationRequest request, string userId)
    {

        Entity.Work newWork = new()
        {
            UploaderId = userId,
            Title = request.Title,
            Description = request.Description,
            IsAI = request.IsAI,
            Medium = request.Medium,
            Type = request.Type,
            Visibility = request.Visibility,
            Rating = request.Rating,
            ReleasedAt = request.ReleasedAt,
        };

        _db.Works.Add(newWork);

        foreach (WorkCreationCredit credit in request.Credits)
        {
            Entity.WorkCredit workCredit = new()
            {
                WorkId = newWork.Id,
                PartyId = credit.PartyId,
                Role = credit.Role,
            };
            _db.WorkCredits.Add(workCredit);
        }

        WorkCreationResponse response = new()
        {
            WorkId = newWork.Id,
            MediaItems = [],
        };


        if (request.CoverHash != null)
        {
            Entity.File coverFile = new()
            {
                Hash = request.CoverHash,
                MimeType = request.CoverMimeType!,
                FileSize = request.CoverFileSize ?? 0,
                Width = request.CoverWidth ?? 0,
                Height = request.CoverHeight ?? 0,
            };
            _db.Files.Add(coverFile);
            Entity.WorkCover workCover = new()
            {
                WorkId = newWork.Id,
                FileId = coverFile.Id,
                SetByUserId = userId,
            };
            _db.WorkCovers.Add(workCover);

            response.CoverUploadURL = await _storageProvider.CreatePresignedCoverUploadAsync(coverFile.Hash);
        }

        //TODO: Handle Source later

        foreach (MediaItemCreationRequest mediaItem in request.MediaItems)
        {
            switch (mediaItem.MediaType)
            {
                case Core.Enum.MediaItemType.IMAGE:
                case Core.Enum.MediaItemType.VIDEO:
                    Entity.File mediaFile = new()
                    {
                        Hash = mediaItem.FileHash!,
                        MimeType = mediaItem.MimeType,
                        FileSize = mediaItem.FileSize,
                        Width = mediaItem.Width ?? 0,
                        Height = mediaItem.Height ?? 0,
                    };
                    _db.Files.Add(mediaFile);

                    Entity.MediaAsset mediaAsset = new()
                    {
                        WorkId = newWork.Id,
                        Description = mediaItem.Description,
                        MediaType = mediaItem.MediaType,
                        Kind = mediaItem.Kind,
                        Order = mediaItem.Order,
                    };
                    _db.MediaAssets.Add(mediaAsset);

                    Entity.MediaVariant mediaVariant = new()
                    {
                        FileId = mediaFile.Id,
                        MediaAssetId = mediaAsset.Id,
                        IsDefault = true,
                        Purpose = Core.Enum.MediaVariantPurpose.ORIGINAL,
                    };
                    _db.MediaVariants.Add(mediaVariant);

                    string uploadUrl = await _storageProvider.CreatePresignedSourceUploadAsync(mediaFile.Hash);
                    response.MediaItems.Add(new MediaItemUploadInfo
                    {
                        MediaItemId = mediaAsset.Id,
                        FileHash = mediaFile.Hash,
                        UploadURL = uploadUrl,
                    });

                    break;
                case Core.Enum.MediaItemType.TEXT:
                    Entity.MediaAsset novelAsset = new()
                    {
                        WorkId = newWork.Id,
                        Description = mediaItem.Description,
                        MediaType = mediaItem.MediaType,
                        State = Core.Enum.MediaItemState.COMPLETED,
                        Kind = mediaItem.Kind,
                        Order = mediaItem.Order,
                        TextContent = mediaItem.TextContent,
                    };
                    _db.MediaAssets.Add(novelAsset);
                    break;
            }
        }
        await _db.SaveChangesAsync();
        return response;
    }
}
