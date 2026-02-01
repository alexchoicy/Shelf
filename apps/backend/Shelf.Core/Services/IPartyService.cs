namespace Shelf.Core.Services;

public interface IPartyService
{
    Task<IReadOnlyList<Models.PartyListModel>> GetAllForListAsync();
    Task<Models.PartyCreationResponse> CreatePartyAsync(Models.PartyCreationRequest request, string userId);
    Task<Models.PartyDetailModel?> GetByIdAsync(Guid id);
}
