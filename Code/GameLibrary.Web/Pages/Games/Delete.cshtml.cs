using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Games;

public class DeleteModel : PageModel
{
    private readonly IGameRepository _gameRepository;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Game Game { get; set; } = new();

    public DeleteModel(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Game? game = await _gameRepository.GetByIdAsync(Id);

        if (game == null)
        {
            return NotFound();
        }

        Game = game;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        bool wasDeleted = await _gameRepository.DeleteAsync(Id);

        if (!wasDeleted)
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}