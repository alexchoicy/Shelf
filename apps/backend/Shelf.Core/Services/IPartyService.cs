namespace Shelf.Core.Services;

public interface IPartyService
{
    Task<IReadOnlyList<Models.PartyListModel>> GetAllForListAsync();
}
