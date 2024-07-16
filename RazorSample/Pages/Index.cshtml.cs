using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorSample.Model;

namespace RazorSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AdventureWorksDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AdventureWorksDbContext dbContext
            , IServiceProvider serviceProvider,
            ILogger<IndexModel> logger)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
           var items = await _dbContext.Departments.ToListAsync();

        }
    }
}
