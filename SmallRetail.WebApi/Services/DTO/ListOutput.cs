
using SmallRetail.WebApi.Data;

namespace SmallRetail.WebApi.Services.DTO;

public class ListOutput<T>
{
    public required List<T> Items { get; set; }
    public required int Total { get; set; }
}