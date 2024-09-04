using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Venues.Models;
using TasteTrailExperience.Core.Common.Repositories;

namespace TasteTrailExperience.Core.Venues.Repositories;

public interface IVenueRepository : IGetFilteredAsync<Venue>, IGetCountFilteredAsync<Venue>, IGetAsNoTrackingAsync<Venue?, int>, 
    IGetByIdAsync<Venue?, int>, ICreateAsync<Venue, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Venue, int?>
{

    public Task PatchLogoUrlPathAsync(Venue venue, string logoUrlPath);
}
