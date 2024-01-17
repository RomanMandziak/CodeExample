using CodeExample.Models;
using System.Transactions;

namespace CodeExample.Core.Markets
{
    public class MarketManager : IMarketManager
    {
        private readonly MarketParkStore _marketParkStore;
        private readonly MarketStore _marketStore;
        private readonly PartnerDGOStore _partnerDGOStore;
        private readonly PartnerOperatorStore _partnerOperatorStore;
        private readonly PartnerTSOStore _partnerTSOStore;
        private readonly PriceStore _priceStore;
        private readonly ProductStore _productStore;

        public MarketManager(
            MarketParkStore marketParkStore,
            MarketStore marketStore,
            PartnerDGOStore partnerDGOStore,
            PartnerOperatorStore partnerOperatorStore,
            PartnerTSOStore partnerTSOStore,
            PriceStore priceStore,
            ProductStore productStore)
        {
            _marketParkStore = marketParkStore;
            _marketStore = marketStore;
            _partnerDGOStore = partnerDGOStore;
            _partnerOperatorStore = partnerOperatorStore;
            _partnerTSOStore = partnerTSOStore;
            _priceStore = priceStore;
            _productStore = productStore;
        }

        public async Task<Market> Create(Market market)
        {
            await _marketStore.Create(market);

            return market;
        }

        public async Task<PartnerDGO> Create(PartnerDGO dgo)
        {
            await _partnerDGOStore.Create(dgo);

            return dgo;
        }

        public async Task<PartnerOperator> Create(PartnerOperator @operator)
        {
            await _partnerOperatorStore.Create(@operator);

            return @operator;
        }

        public async Task<PartnerTSO> Create(PartnerTSO tso)
        {
            await _partnerTSOStore.Create(tso);

            return tso;
        }

        public async Task<Product> Create(Product product)
        {
            await _productStore.Create(product);

            return product;
        }

        public async Task<Market?> DeleteMarket(long id)
        {
            Market? market = await _marketStore.Get(id);
            if (market != null)
            {
                await _marketStore.Delete(market);
            }

            return market;
        }

        public async Task<PartnerDGO?> DeletePartnerDGO(Guid id)
        {
            PartnerDGO? dgo = await _partnerDGOStore.Get(id);
            if (dgo != null)
            {
                await _partnerDGOStore.Delete(dgo);
            }

            return dgo;
        }

        public async Task<PartnerOperator?> DeletePartnerOperator(Guid id)
        {
            PartnerOperator? @operator = await _partnerOperatorStore.Get(id);
            if (@operator != null)
            {
                await _partnerOperatorStore.Delete(@operator);
            }

            return @operator;
        }

        public async Task<PartnerTSO?> DeletePartnerTSO(Guid id)
        {
            PartnerTSO? tso = await _partnerTSOStore.Get(id);
            if (tso != null)
            {
                await _partnerTSOStore.Delete(tso);
            }

            return tso;
        }

        public async Task<Price?> DeletePrice(PriceId id)
        {
            Price? price = await _priceStore.Get(id);
            if (price != null)
            {
                await _priceStore.Delete(price);
            }

            return price;
        }

        public async Task<Product?> DeleteProduct(long id)
        {
            Product? product = await _productStore.Get(id);
            if (product != null)
            {
                await _productStore.Delete(product);
            }

            return product;
        }

        public async Task<IPage<Market>> Find(IMarketFilter filter)
        {
            IReadOnlyCollection<Market> markets = await _marketStore.Find(filter);
            int totalCount = await _marketStore.GetTotalCount(filter);

            return new PagedList<Market>(markets, totalCount);
        }

        public async Task<IPage<PartnerDGO>> Find(IPartnerDGOFilter filter)
        {
            IReadOnlyCollection<PartnerDGO> dgos = await _partnerDGOStore.Find(filter);
            int totalCount = await _partnerDGOStore.GetTotalCount(filter);

            return new PagedList<PartnerDGO>(dgos, totalCount);
        }

        public async Task<IPage<PartnerOperator>> Find(IPartnerOperatorFilter filter)
        {
            IReadOnlyCollection<PartnerOperator> operators = await _partnerOperatorStore.Find(filter);
            int totalCount = await _partnerOperatorStore.GetTotalCount(filter);

            return new PagedList<PartnerOperator>(operators, totalCount);
        }

        public async Task<IPage<PartnerTSO>> Find(IPartnerTSOFilter filter)
        {
            IReadOnlyCollection<PartnerTSO> tsos = await _partnerTSOStore.Find(filter);
            int totalCount = await _partnerTSOStore.GetTotalCount(filter);

            return new PagedList<PartnerTSO>(tsos, totalCount);
        }

        public async Task<IPage<Product>> Find(IProductFilter filter)
        {
            IReadOnlyCollection<Product> products = await _productStore.Find(filter);
            int totalCount = await _productStore.GetTotalCount(filter);

            return new PagedList<Product>(products, totalCount);
        }

        public Task<List<Price>> Find(IPriceFilter filter)
        {
            return _priceStore.Find(filter);
        }

        public Task<Market?> GetMarket(long id)
        {
            return _marketStore.Get(id);
        }

        public Task<PartnerDGO?> GetPartnerDGO(Guid id)
        {
            return _partnerDGOStore.Get(id);
        }

        public Task<PartnerOperator?> GetPartnerOperator(Guid id)
        {
            return _partnerOperatorStore.Get(id);
        }

        public Task<PartnerTSO?> GetPartnerTSO(Guid id)
        {
            return _partnerTSOStore.Get(id);
        }

        public Task<Product?> GetProduct(long id)
        {
            return _productStore.Get(id);
        }

        public async Task<Market> Update(Market market)
        {
            using (TransactionScope scope = CreateTransactionScope())
            {
                if (market.MarketParks != null)
                {
                    IReadOnlyCollection<MarketPark> existingMarketParks = await _marketParkStore.FindByMarketId(market.Id, asNoTracking: true);

                    // Remove deleted
                    IReadOnlyCollection<MarketPark> marketParksToDelete =
                        existingMarketParks
                            .Except(market.MarketParks, MarketParkComparers.Full)
                            .ToList();
                    await _marketParkStore.Delete(marketParksToDelete);

                    // Create added
                    IReadOnlyCollection<MarketPark> marketParksToCreate =
                        market.MarketParks
                            .Except(existingMarketParks, MarketParkComparers.Full)
                            .ToList();
                    await _marketParkStore.Create(marketParksToCreate);
                }

                await _marketStore.Update(market);

                scope.Complete();
            }

            return market;
        }

        public async Task<PartnerDGO> Update(PartnerDGO dgo)
        {
            await _partnerDGOStore.Update(dgo);

            return dgo;
        }

        public async Task<PartnerOperator> Update(PartnerOperator @operator)
        {
            await _partnerOperatorStore.Update(@operator);

            return @operator;
        }

        public async Task<PartnerTSO> Update(PartnerTSO tso)
        {
            await _partnerTSOStore.Update(tso);

            return tso;
        }

        public async Task<Price> Update(Price price)
        {
            await _priceStore.Update(price);
            Price? updated = await _priceStore.Get(price.Id);

            return updated!;
        }

        public async Task<Product> Update(Product product)
        {
            product.Asset = null;
            product.TargetAsset = null;
            await _productStore.Update(product);
            Product? updated = await _productStore.Get(product.Id);

            return updated!;
        }

        private static TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                },
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
