using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Admin.Platforms;

public class CreateModel : PageModel
{
    private readonly IPlatformRepository _platformRepository;

    [BindProperty]
    public Platform Platform { get; set; } = new();

    public CreateModel(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _platformRepository.AddAsync(Platform);

        return RedirectToPage("./Index");
    }
}