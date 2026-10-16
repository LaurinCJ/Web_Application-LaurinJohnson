using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameLibrary.Web.Pages.Games;

public class CreateModel : PageModel
{
    private readonly IGameRepository _gameRepository;
    private readonly IFranchiseRepository _franchiseRepository;
    private readonly IPlatformRepository _platformRepository;

    [BindProperty]
    public Game Game { get; set; } = new();

    public List<SelectListItem> Franchises { get; private set; } = [];
    public List<SelectListItem> Platforms { get; private set; } = [];

    public CreateModel(IGameRepository gameRepository, IFranchiseRepository franchiseRepository, IPlatformRepository platformRepository)
    {
        _gameRepository = gameRepository;
        _franchiseRepository = franchiseRepository;
        _platformRepository = platformRepository;
    }

    public async Task OnGetAsync()
    {
        await LoadDropdownListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownListsAsync();
            return Page();
        }

        await _gameRepository.AddAsync(Game);

        return RedirectToPage("./Index");
    }

    private async Task LoadDropdownListsAsync()
    {
        Franchises = (await _franchiseRepository.GetAllAsync()).Select(franchise => new SelectListItem
            {
                Value = franchise.FranchiseId.ToString(),
                Text = franchise.FranchiseName
            }).ToList();

        Platforms = (await _platformRepository.GetAllAsync()).Select(platform => new SelectListItem
            {
                Value = platform.PlatformId.ToString(),
                Text = platform.PlatformName
            }).ToList();
    }
}