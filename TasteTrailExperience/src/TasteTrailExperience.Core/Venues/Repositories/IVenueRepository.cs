using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Venues.Models;

namespace TasteTrailExperience.Core.Venues.Repositories;

public interface IVenueRepository : IGetFilteredAsync<Venue>, IGetCountBySpecificationAsync<Venue>, IGetAsNoTrackingAsync<Venue?, int>, 
    IGetCountAsync, IGetByIdAsync<Venue?, int>,
    ICreateAsync<Venue, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Venue, int?>
{
    public Task PatchLogoUrlPathAsync(Venue venue, string logoUrlPath);
}
