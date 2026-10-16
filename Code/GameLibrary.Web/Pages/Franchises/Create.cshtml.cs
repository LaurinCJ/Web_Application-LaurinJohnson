using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Franchises;

public class CreateModel : PageModel
{
    private readonly IFranchiseRepository _franchiseRepository;

    [BindProperty]
    public Franchise Franchise { get; set; } = new();

    public CreateModel(IFranchiseRepository franchiseRepository)
    {
        _franchiseRepository = franchiseRepository;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _franchiseRepository.AddAsync(Franchise);

        return RedirectToPage("./Index");
    }
}