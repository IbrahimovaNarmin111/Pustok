using Microsoft.EntityFrameworkCore;
using Pustok.DAL;

namespace Pustok.Services
{
    public class LayoutService
    {
        private AppDbContext _db;

        public LayoutService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Dictionary<string,string>> GetSetting()
        {
            var setting = await _db.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return setting;
        }
    }
}
