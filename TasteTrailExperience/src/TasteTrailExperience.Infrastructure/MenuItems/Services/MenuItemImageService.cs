using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using TasteTrailExperience.Core.MenuItems.Repositories;
using TasteTrailExperience.Core.Menus.Services;

namespace TasteTrailExperience.Infrastructure.MenuItems.Services;

public class MenuItemImageService : IMenuImageService
{
    private readonly BlobServiceClient _blobServiceClient;

    private readonly IMenuItemRepository _menuItemRepository;

    private readonly string _defaultLogoUrl;

    private readonly string _containerName = "menuitem-images";

    public MenuItemImageService(BlobServiceClient blobServiceClient, IMenuItemRepository menuItemRepository)
    {
        _blobServiceClient = blobServiceClient;
        _menuItemRepository = menuItemRepository;
        _defaultLogoUrl = GetDefaultImageUrl();
    }

    public async Task<string> SetImageAsync(int menuItemId, IFormFile? logo)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId) ?? throw new ArgumentException($"Menu with ID {menuItemId} not found.");

        if (logo == null || logo.Length == 0)
        {
            menuItem.ImageUrlPath = _defaultLogoUrl;
            await _menuItemRepository.PutAsync(menuItem);

            return _defaultLogoUrl;
        }

        // Create blob container if it doesn't exist
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = $"{menuItem.Id}{Path.GetExtension(logo.FileName)}";
        var blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = logo.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = logo.ContentType });
        }

        var logoUrl = blobClient.Uri.ToString();

        menuItem.ImageUrlPath = logoUrl;
        await _menuItemRepository.PutAsync(menuItem);

        return menuItem.ImageUrlPath;
    }

    public async Task<string> DeleteImageAsync(int menuItemId)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId) ?? throw new ArgumentException($"Menu with ID {menuItemId} not found.");

        // If logo isn't already default
        if (!string.IsNullOrEmpty(menuItem.ImageUrlPath) && !menuItem.ImageUrlPath.Equals(_defaultLogoUrl, StringComparison.OrdinalIgnoreCase))
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobUri = new Uri(menuItem.ImageUrlPath).AbsolutePath.TrimStart('/');
            var blobName = Path.GetFileName(blobUri);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }


        menuItem.ImageUrlPath = _defaultLogoUrl;
        await _menuItemRepository.PutAsync(mmenuItemenu);

        return menuItem.ImageUrlPath;
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
