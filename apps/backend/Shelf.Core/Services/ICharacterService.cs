namespace Shelf.Core.Services;

public interface ICharacterService
{
    Task<IReadOnlyList<Models.CharacterListModel>> GetAllForListAsync();
    Task CreateCharacterAsync(Models.CharacterCreationRequest request, string userId);
}
