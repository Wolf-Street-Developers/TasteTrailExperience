using Microsoft.AspNetCore.Mvc;

namespace TasteTrailExperience.Api.Common.Extensions.Controller;

public static class InternalServerErrorMethod
{
    public static IActionResult InternalServerError(this ControllerBase controller, string message)
    {
        return controller.StatusCode(500, new { message });
    }
}
