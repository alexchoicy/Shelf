using Shelf.Core.Models;
using Shelf.Core.Services;

namespace Shelf.Infrastructure.Services.Work;

public class WorkService : IWorkService
{
    public async Task<WorkCreationResponse> CreateWorkAsync(WorkCreationRequest request, string userId)
    {
        throw new NotImplementedException();
    }
}
