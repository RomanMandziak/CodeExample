using CodeExample.Models;

namespace CodeExample.Core.Markets
{
    public class MarketStore
    {
        private readonly PriceDbContext _dbContext;

        public MarketStore(PriceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> Create(Market market)
        {
            _dbContext.Markets.Add(market);

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> Delete(Market market)
        {
            _dbContext.Markets.Remove(market);

            return _dbContext.SaveChangesAsync();
        }

        public Task<List<Market>> Find(IMarketFilter filter)
        {
            return _dbContext.Markets
                .FilterBy(filter)
                .SortBy(filter)
                .TakePage(filter)
                .ToListAsync();
        }

        public Task<Market?> Get(long id)
        {
            return _dbContext.Markets
                .Include(m => m.MarketParks)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<int> GetTotalCount(IMarketFilter filter)
        {
            return _dbContext.Markets
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(Market market)
        {
            _dbContext.Markets.Update(market);

            return _dbContext.SaveChangesAsync();
        }
    }
}
