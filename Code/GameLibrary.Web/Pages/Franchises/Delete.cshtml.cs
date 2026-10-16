using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Franchises;

public class DeleteModel : PageModel
{
    private readonly IFranchiseRepository _franchiseRepository;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Franchise Franchise { get; set; } = new();

    public DeleteModel(IFranchiseRepository franchiseRepository)
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
        bool wasDeleted = await _franchiseRepository.DeleteAsync(Id);

        if (!wasDeleted)
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}