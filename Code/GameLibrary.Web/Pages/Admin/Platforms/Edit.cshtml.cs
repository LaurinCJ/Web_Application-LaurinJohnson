using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Admin.Platforms;

public class EditModel : PageModel
{
    private readonly IPlatformRepository _platformRepository;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Platform Platform { get; set; } = new();

    public EditModel(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Platform? platform = await _platformRepository.GetByIdAsync(Id);

        if (platform == null)
        {
            return NotFound();
        }

        Platform = platform;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        bool wasUpdated = await _platformRepository.UpdateAsync(Platform);

        if (!wasUpdated)
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}