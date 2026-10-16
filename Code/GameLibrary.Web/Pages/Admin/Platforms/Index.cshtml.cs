using GameLibrary.Data.Models;
using GameLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Web.Pages.Admin.Platforms;

public class IndexModel : PageModel
{
    private readonly IPlatformRepository _platformRepository;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<Platform> Platforms { get; private set; } = [];

    public IndexModel(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }

    public async Task OnGetAsync()
    {
        Platforms = await _platformRepository.GetAllAsync(Search);
    }
}