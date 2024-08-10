using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Core.Menus.Models;

namespace TasteTrailExperience.Core.Menus.Dtos;

public class MenuDto
{
    public required Menu Menu { get; set; }

    public required List<MenuItem> MenuItems { get; set; }
}
