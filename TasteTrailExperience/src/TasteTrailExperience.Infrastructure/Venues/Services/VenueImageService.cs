using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Core.Venues.Services;

namespace TasteTrailExperience.Infrastructure.Venues.Services;

public class VenueImageService : IVenueImageService
{
    private readonly BlobServiceClient _blobServiceClient;

    private readonly IVenueRepository _venueRepository;

    private readonly string _defaultLogoUrl;

    private readonly string _containerName = "venue-images";

    public VenueImageService(BlobServiceClient blobServiceClient, IVenueRepository venueRepository)
    {
        _blobServiceClient = blobServiceClient;
        _venueRepository = venueRepository;
        _defaultLogoUrl = GetDefaultImageUrl();
    }

    public async Task<string> SetImageAsync(int venueId, IFormFile? logo)
    {
        var venue = await _venueRepository.GetByIdAsync(venueId) ?? throw new ArgumentException($"Venue with ID {venueId} not found.");

        if (logo == null || logo.Length == 0)
        {
            venue.LogoUrlPath = _defaultLogoUrl;
            await _venueRepository.PutAsync(venue);

            return _defaultLogoUrl;
        }

        // Create blob container if it doesn't exist
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = $"{venue.Id}{Path.GetExtension(logo.FileName)}";
        var blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = logo.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = logo.ContentType });
        }

        var logoUrl = blobClient.Uri.ToString();

        venue.LogoUrlPath = logoUrl;
        await _venueRepository.PutAsync(venue);

        return logoUrl;
    }

    public async Task<string> DeleteImageAsync(int venueId)
    {
        var venue = await _venueRepository.GetByIdAsync(venueId) ?? throw new ArgumentException($"Venue with ID {venueId} not found.");

        // If logo isn't already default
        if (!string.IsNullOrEmpty(venue.LogoUrlPath) && !venue.LogoUrlPath.Equals(_defaultLogoUrl, StringComparison.OrdinalIgnoreCase))
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobUri = new Uri(venue.LogoUrlPath).AbsolutePath.TrimStart('/');
            var blobName = Path.GetFileName(blobUri);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        venue.LogoUrlPath = _defaultLogoUrl;
        await _venueRepository.PutAsync(venue);

        return venue.LogoUrlPath;
    }

    public string GetDefaultImageUrl()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var defaultLogoBlobName = "default-image.png";

        var blobClient = containerClient.GetBlobClient(defaultLogoBlobName);
        
        if (!blobClient.Exists())
            throw new InvalidOperationException("Default logo does not exist in Blob Storage.");

        return blobClient.Uri.ToString();
    }
}
