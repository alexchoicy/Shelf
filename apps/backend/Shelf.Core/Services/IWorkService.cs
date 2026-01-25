using Shelf.Core.Models;

namespace Shelf.Core.Services;

public interface IWorkService
{
    Task<WorkCreationResponse> CreateWorkAsync(WorkCreationRequest request, string userId);
}
