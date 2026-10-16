using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Franchises;

public class IndexModel : PageModel
{
    private readonly IFranchiseRepository _franchiseRepository;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<Franchise> Franchises { get; private set; } = [];

    public IndexModel(IFranchiseRepository franchiseRepository)
    {
        _franchiseRepository = franchiseRepository;
    }

    public async Task OnGetAsync()
    {
        Franchises = await _franchiseRepository.GetAllAsync(Search);
    }
}