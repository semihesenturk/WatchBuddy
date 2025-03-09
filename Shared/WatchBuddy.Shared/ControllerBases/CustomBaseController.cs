using Microsoft.AspNetCore.Mvc;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Shared.ControllerBases;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(BaseServiceResponse<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}