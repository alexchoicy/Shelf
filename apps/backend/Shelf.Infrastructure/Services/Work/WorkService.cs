using Shelf.Core.Models;
using Shelf.Core.Services;
using Shelf.Infrastructure.Data;

namespace Shelf.Infrastructure.Services.Work;

public class WorkService(AppDbContext _db) : IWorkService
{

    public async Task<WorkCreationResponse> CreateWorkAsync(WorkCreationRequest request, string userId)
    {
        throw new NotImplementedException();
    }
}
