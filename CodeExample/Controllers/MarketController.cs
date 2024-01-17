using CodeExample.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeExample.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Administrator)]
    public class MarketsController : ControllerBase
    {
        private readonly FileExporter _fileExporter;
        private readonly IMarketManager _manager;

        public MarketsController(FileExporter fileExporter, IMarketManager manager)
        {
            _fileExporter = fileExporter;
            _manager = manager;
        }

        [HttpPost("api/markets")]
        public async Task<IActionResult> Create([FromBody] Market market)
        {
            Market created = await _manager.Create(market);

            return Ok(created);
        }

        [HttpPost("api/markets/dgos")]
        public async Task<IActionResult> Create([FromBody] PartnerDGO dgo)
        {
            PartnerDGO created = await _manager.Create(dgo);

            return Ok(created);
        }

        [HttpPost("api/markets/operators")]
        public async Task<IActionResult> Create([FromBody] PartnerOperator @operator)
        {
            PartnerOperator created = await _manager.Create(@operator);

            return Ok(created);
        }

        [HttpPost("api/markets/tsos")]
        public async Task<IActionResult> Create([FromBody] PartnerTSO tso)
        {
            PartnerTSO created = await _manager.Create(tso);

            return Ok(created);
        }

        [HttpPost("api/markets/{marketId:long}/products")]
        public async Task<IActionResult> Create([FromRoute] long marketId, [FromBody] Product product)
        {
            product.MarketId = marketId;
            Product created = await _manager.Create(product);

            return Ok(created);
        }

        [HttpDelete("api/markets/{id:long}")]
        public async Task<IActionResult> DeleteMarket([FromRoute] long id)
        {
            Market? deleted = await _manager.DeleteMarket(id);

            return Ok(deleted);
        }

        [HttpDelete("api/markets/dgos/{id:guid}")]
        public async Task<IActionResult> DeletePartnerDGO(Guid id)
        {
            PartnerDGO? deleted = await _manager.DeletePartnerDGO(id);

            return Ok(deleted);
        }

        [HttpDelete("api/markets/operators/{id:guid}")]
        public async Task<IActionResult> DeletePartnerOperator([FromRoute] Guid id)
        {
            PartnerOperator? deleted = await _manager.DeletePartnerOperator(id);

            return Ok(deleted);
        }

        [HttpDelete("api/markets/tsos/{id:guid}")]
        public async Task<IActionResult> DeletePartnerTso(Guid id)
        {
            PartnerTSO? deleted = await _manager.DeletePartnerTSO(id);

            return Ok(deleted);
        }

        [HttpDelete("api/markets/prices/{priceId}")]
        public async Task<IActionResult> DeletePrice([FromRoute] string priceId)
        {
            Price? deleted = null;
            if (PriceId.TryParse(priceId, out PriceId? id))
            {
                deleted = await _manager.DeletePrice(id!);
            }

            return Ok(deleted);
        }

        [HttpDelete("api/markets/{marketId:long}/products/{id:long}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long marketId, [FromRoute] long id)
        {
            Product? deleted = await _manager.DeleteProduct(id);

            return Ok(deleted);
        }

        [HttpPost("api/markets/export")]
        public async Task<IActionResult> Export([FromBody] FileExportRequest<Market, MarketFilter> request)
        {
            IPage<Market> markets = await _manager.Find(request.Filter);
            FileBytes file = await _fileExporter.Export(markets, request.Configuration);

            return Ok(file);
        }

        [HttpPost("api/markets/dgos/export")]
        public async Task<IActionResult> Export([FromBody] FileExportRequest<PartnerDGO, PartnerDGOFilter> request)
        {
            IPage<PartnerDGO> dgos = await _manager.Find(request.Filter);
            FileBytes file = await _fileExporter.Export(dgos, request.Configuration);

            return Ok(file);
        }

        [HttpPost("api/markets/operators/export")]
        public async Task<IActionResult> Export([FromBody] FileExportRequest<PartnerOperator, PartnerOperatorFilter> request)
        {
            IPage<PartnerOperator> operators = await _manager.Find(request.Filter);
            FileBytes file = await _fileExporter.Export(operators, request.Configuration);

            return Ok(file);
        }

        [HttpPost("api/markets/tsos/export")]
        public async Task<IActionResult> Export([FromBody] FileExportRequest<PartnerTSO, PartnerTSOFilter> request)
        {
            IPage<PartnerTSO> tsos = await _manager.Find(request.Filter);
            FileBytes file = await _fileExporter.Export(tsos, request.Configuration);

            return Ok(file);
        }

        [HttpPost("api/markets/{marketId:long}/products/export")]
        public async Task<IActionResult> Export([FromBody] FileExportRequest<Product, ProductFilter> request)
        {
            IPage<Product> products = await _manager.Find(request.Filter);
            FileBytes file = await _fileExporter.Export(products, request.Configuration);

            return Ok(file);
        }

        [HttpPost("api/markets/find")]
        public async Task<IActionResult> Find([FromBody] MarketFilter filter)
        {
            IPage<Market> markets = await _manager.Find(filter);

            return Ok(markets);
        }

        [HttpPost("api/markets/dgos/find")]
        public async Task<IActionResult> Find([FromBody] PartnerDGOFilter filter)
        {
            IPage<PartnerDGO> dgos = await _manager.Find(filter);

            return Ok(dgos);
        }

        [HttpPost("api/markets/operators/find")]
        public async Task<IActionResult> Find([FromBody] PartnerOperatorFilter filter)
        {
            IPage<PartnerOperator> operators = await _manager.Find(filter);

            return Ok(operators);
        }

        [HttpPost("api/markets/tsos/find")]
        public async Task<IActionResult> Find([FromBody] PartnerTSOFilter filter)
        {
            IPage<PartnerTSO> tsos = await _manager.Find(filter);

            return Ok(tsos);
        }

        [HttpPost("api/markets/{marketId:long}/products/find")]
        public async Task<IActionResult> Find([FromRoute] long marketId, [FromBody] ProductFilter filter)
        {
            filter.MarketId = marketId;
            IPage<Product> products = await _manager.Find(filter);

            return Ok(products);
        }

        [HttpPost("api/markets/{marketId:long}/products/{productId:long}/prices/find")]
        public async Task<IActionResult> Find([FromRoute] long marketId, [FromRoute] long productId, [FromBody] PriceFilter filter)
        {
            List<Price> analytics = await _manager.Find(filter);

            return Ok(analytics);
        }

        [HttpGet("api/markets/{id:long}")]
        public async Task<IActionResult> GetMarket([FromRoute] long id)
        {
            Market? market = await _manager.GetMarket(id);

            return Ok(market);
        }

        [HttpGet("api/markets/dgos/{id:guid}")]
        public async Task<IActionResult> GetPartnerDGO(Guid id)
        {
            PartnerDGO? dgo = await _manager.GetPartnerDGO(id);

            return Ok(dgo);
        }

        [HttpGet("api/markets/operators/{id:guid}")]
        public async Task<IActionResult> GetPartnerOperator([FromRoute] Guid id)
        {
            PartnerOperator? @operator = await _manager.GetPartnerOperator(id);

            return Ok(@operator);
        }

        [HttpGet("api/markets/tsos/{id:guid}")]
        public async Task<IActionResult> GetPartnerTSO(Guid id)
        {
            PartnerTSO? tso = await _manager.GetPartnerTSO(id);

            return Ok(tso);
        }

        [HttpGet("api/markets/{marketId:long}/products/{id:long}")]
        public async Task<IActionResult> GetProduct([FromRoute] long marketId, [FromRoute] long id)
        {
            Product? products = await _manager.GetProduct(id);

            return Ok(products);
        }

        [HttpPut("api/markets/{id}")]
        public async Task<IActionResult> Update([FromBody] Market market)
        {
            Market updated = await _manager.Update(market);

            return Ok(updated);
        }

        [HttpPut("api/markets/dgos")]
        public async Task<IActionResult> Update([FromBody] PartnerDGO dgo)
        {
            PartnerDGO? updated = await _manager.Update(dgo);

            return Ok(updated);
        }

        [HttpPut("api/markets/operators/{id:guid}")]
        public async Task<IActionResult> Update([FromBody] PartnerOperator @operator)
        {
            PartnerOperator updated = await _manager.Update(@operator);

            return Ok(updated);
        }

        [HttpPut("api/markets/tsos")]
        public async Task<IActionResult> Update([FromBody] PartnerTSO tso)
        {
            PartnerTSO? updated = await _manager.Update(tso);

            return Ok(updated);
        }

        [HttpPut("api/markets/prices")]
        public async Task<IActionResult> Update([FromBody] Price price)
        {
            Price updated = await _manager.Update(price);

            return Ok(updated);
        }

        [HttpPut("api/markets/{marketId:long}/products/{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long marketId, [FromBody] Product product)
        {
            product.MarketId = marketId;
            Product updated = await _manager.Update(product);

            return Ok(updated);
        }
    }
}