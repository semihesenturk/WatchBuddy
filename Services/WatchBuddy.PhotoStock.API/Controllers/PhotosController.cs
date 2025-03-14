using Microsoft.AspNetCore.Mvc;
using WatchBuddy.PhotoStock.API.Dtos;
using WatchBuddy.Shared.ControllerBases;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.PhotoStock.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/", file.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            var returnPath = $"/photos/{file.FileName}";
            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResultInstance(BaseServiceResponse<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(BaseServiceResponse<PhotoDto>.Fail("File is empty", 400));
    }
}