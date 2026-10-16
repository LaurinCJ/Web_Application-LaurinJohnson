using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Franchises;

public class EditModel : PageModel
{
    private readonly IFranchiseRepository _franchiseRepository;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Franchise Franchise { get; set; } = new();

    public EditModel(IFranchiseRepository franchiseRepository)
    {
        _franchiseRepository = franchiseRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Franchise? franchise = await _franchiseRepository.GetByIdAsync(Id);

        if (franchise == null)
        {
            return NotFound();
        }

        Franchise = franchise;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        bool wasUpdated = await _franchiseRepository.UpdateAsync(Franchise);

        if (!wasUpdated)
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}