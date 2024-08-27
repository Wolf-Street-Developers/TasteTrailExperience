using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Common.Managers.ImageManagers;

namespace TasteTrailExperience.Core.Venues.Services;

public interface IVenueImageService : IImageManager<int>
{
    
}
