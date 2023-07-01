using Microsoft.AspNetCore.Mvc;

namespace SmallRetail.WebApi.Controllers.Resources;

public class ProductFilter
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    [FromQuery(Name = "price")]
    public decimal? Price { get; set; }

    [FromQuery(Name = "barcode")]
    public string? Barcode { get; set; }

    [FromQuery(Name = "user_id")]
    public string? UserId { get; set; }

    [FromQuery(Name = "shop_id")]
    public long? ShopId { get; set; }
}
