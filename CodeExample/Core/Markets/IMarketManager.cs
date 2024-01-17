using CodeExample.Models;

namespace CodeExample.Core.Markets
{
    public interface IMarketManager
    {
        Task<Market> Create(Market market);
        Task<PartnerDGO> Create(PartnerDGO dgo);
        Task<PartnerOperator> Create(PartnerOperator @operator);
        Task<PartnerTSO> Create(PartnerTSO tso);
        Task<Product> Create(Product product);
        Task<Market?> DeleteMarket(long id);
        Task<PartnerDGO?> DeletePartnerDGO(Guid id);
        Task<PartnerOperator?> DeletePartnerOperator(Guid id);
        Task<PartnerTSO?> DeletePartnerTSO(Guid id);
        Task<Price?> DeletePrice(PriceId id);
        Task<Product?> DeleteProduct(long id);
        Task<IPage<Market>> Find(IMarketFilter filter);
        Task<IPage<PartnerDGO>> Find(IPartnerDGOFilter filter);
        Task<IPage<PartnerOperator>> Find(IPartnerOperatorFilter filter);
        Task<IPage<PartnerTSO>> Find(IPartnerTSOFilter filter);
        Task<IPage<Product>> Find(IProductFilter filter);
        Task<List<Price>> Find(IPriceFilter filter);
        Task<Market?> GetMarket(long id);
        Task<PartnerDGO?> GetPartnerDGO(Guid id);
        Task<PartnerOperator?> GetPartnerOperator(Guid id);
        Task<PartnerTSO?> GetPartnerTSO(Guid id);
        Task<Product?> GetProduct(long id);
        Task<Market> Update(Market market);
        Task<PartnerDGO> Update(PartnerDGO dgo);
        Task<PartnerOperator> Update(PartnerOperator @operator);
        Task<PartnerTSO> Update(PartnerTSO tso);
        Task<Product> Update(Product product);
        Task<Price> Update(Price price);
    }
}
