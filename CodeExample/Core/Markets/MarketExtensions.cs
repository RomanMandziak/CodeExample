using CodeExample.Models;

namespace CodeExample.Core.Markets
{
    public static class MarketExtensions
    {
        public static IQueryable<Market> FilterBy(this IQueryable<Market> markets, IMarketFilter filter)
        {
            return markets.Where(m =>
                (filter.Classification == null || m.Classification == filter.Classification)
                    && (filter.SearchText == null
                        || m.Name.Contains(filter.SearchText)
                        || (m.Description != null && m.Description.Contains(filter.SearchText))));
        }

        public static IQueryable<Market> SortBy(this IQueryable<Market> scenarios, IMarketFilter filter)
        {
            switch (filter.SortBy)
            {
                case MarketFieldIdentifier.Classification:
                    return scenarios.OrderBy(m => m.Classification, filter.SortOrder);

                case MarketFieldIdentifier.Description:
                    return scenarios.OrderBy(m => m.Description, filter.SortOrder);

                case MarketFieldIdentifier.Id:
                    return scenarios.OrderBy(m => m.Id, filter.SortOrder);

                case MarketFieldIdentifier.IsReserveMarket:
                    return scenarios.OrderBy(m => m.IsReserveMarket, filter.SortOrder);

                case MarketFieldIdentifier.Name:
                    return scenarios.OrderBy(m => m.Name, filter.SortOrder);

                default:
                    throw new ArgumentException(string.Format(Resource.NotSupportedSortField, filter.SortBy));
            }
        }
    }
}
