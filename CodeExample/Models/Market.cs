namespace CodeExample.Models
{
    public class Market
    {
        public const int DescriptionMaxLength = 500;
        public const int NameMaxLength = 50;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? IsReserveMarket { get; set; }
        public string? Description { get; set; }
        public MarketClassification? Classification { get; set; }

        public List<MarketPark>? MarketParks { get; set; }
        public List<MarketScenario>? MarketScenarios { get; set; }
        public List<OutputPowerFlow>? OutputPowerFlows { get; set; }
        public List<Product>? Products { get; set; }
        public List<TransmissionLimit>? TransmissionLimits { get; set; }
    }
}
