using TasteTrailData.Core.Common.Repositories.Base;
using TasteTrailData.Core.Venues.Models;

namespace TasteTrailExperience.Core.Venues.Repositories;

public interface IVenueRepository : IGetByCountAsync<Venue>, IGetByIdAsync<Venue?, int>,
ICreateAsync<Venue, int>, IDeleteByIdAsync<int, int?>, IPutAsync<Venue, int?>
{
    
}
