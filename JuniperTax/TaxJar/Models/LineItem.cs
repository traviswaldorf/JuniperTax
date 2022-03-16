using System.Text.Json.Serialization;

namespace JuniperTax.TaxJar.Models
{
    public class LineItem
    {
        // Request Values

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string? ProductTaxCode { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal? UnitPrice { get; set; }

        [JsonPropertyName("discount")]
        public decimal? Discount { get; set; }


        // Response Values

        [JsonPropertyName("taxable_amount")]
        public decimal TaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public decimal TaxCollectable { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public decimal CombinedTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public decimal StateTaxableAmount { get; set; }

        [JsonPropertyName("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonPropertyName("state_amount")]
        public double StateAmount { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public decimal CountyTaxableAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_amount")]
        public double CountyAmount { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public decimal CityTaxableAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public decimal CityTaxRate { get; set; }

        [JsonPropertyName("city_amount")]
        public decimal CityAmount { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public decimal SpecialTaxRate { get; set; }

        [JsonPropertyName("special_district_amount")]
        public decimal SpecialDistrictAmount { get; set; }
    }
}
