using Emporium.Domain.SeedWork;

namespace Emporium.Domain.Stocks;
public record StockId(Guid Value) : TypedIdValueBase(Value);