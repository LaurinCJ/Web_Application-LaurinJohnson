using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Games;

public class IndexModel : PageModel
{
    private readonly IGameRepository _gameRepository;

    [BindProperty(SupportsGet = true)]
    public string? TitleSearch { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? FranchiseSearch { get; set; }

    public List<Game> Games { get; private set; } = [];

    public IndexModel(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task OnGetAsync()
    {
        Games = await _gameRepository.GetAllAsync(TitleSearch, FranchiseSearch);
    }
}