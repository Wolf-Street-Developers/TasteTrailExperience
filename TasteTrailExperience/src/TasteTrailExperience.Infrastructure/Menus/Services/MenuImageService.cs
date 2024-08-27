using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using TasteTrailData.Core.Common.Managers.ImageManagers;
using TasteTrailExperience.Core.Menus.Repositories;

public class MenuImageService : IImageManager<int>
{
    private readonly BlobServiceClient _blobServiceClient;

    private readonly IMenuRepository _menuRepository;

    private readonly string _defaultLogoUrl;

    private readonly string _containerName = "menu-images";

    public MenuImageService(BlobServiceClient blobServiceClient, IMenuRepository menuRepository)
    {
        _blobServiceClient = blobServiceClient;
        _menuRepository = menuRepository;
        _defaultLogoUrl = GetDefaultImageUrl();
    }

    public async Task<string> SetImageAsync(int menuId, IFormFile? logo)
    {
        var menu = await _menuRepository.GetByIdAsync(menuId) ?? throw new ArgumentException($"Menu with ID {menuId} not found.");

        if (logo == null || logo.Length == 0)
        {
            menu.ImageUrlPath = _defaultLogoUrl;
            await _menuRepository.PutAsync(menu);

            return _defaultLogoUrl;
        }

        // Create blob container if it doesn't exist
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = $"{menu.Id}{Path.GetExtension(logo.FileName)}";
        var blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = logo.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = logo.ContentType });
        }

        var logoUrl = blobClient.Uri.ToString();

        menu.ImageUrlPath = logoUrl;
        await _menuRepository.PutAsync(menu);

        return menu.ImageUrlPath;
    }

    public async Task<string> DeleteImageAsync(int menuId)
    {
        var menu = await _menuRepository.GetByIdAsync(menuId) ?? throw new ArgumentException($"Menu with ID {menuId} not found.");

        // If logo isn't already default
        if (!string.IsNullOrEmpty(menu.ImageUrlPath) && !menu.ImageUrlPath.Equals(_defaultLogoUrl, StringComparison.OrdinalIgnoreCase))
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobUri = new Uri(menu.ImageUrlPath).AbsolutePath.TrimStart('/');
            var blobName = Path.GetFileName(blobUri);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }


        menu.ImageUrlPath = _defaultLogoUrl;
        await _menuRepository.PutAsync(menu);

        return menu.ImageUrlPath;
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
