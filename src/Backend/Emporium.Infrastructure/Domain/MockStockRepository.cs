using Emporium.Domain.Stocks;
using System.Collections.Concurrent;

namespace Emporium.Infrastructure.Domain;

public class MockStockRepository : IStockRepository
{
    private readonly ConcurrentDictionary<Guid, Stock> _stocks = new();

    public Task Add(Stock stock)
    {
        _stocks[stock.Id.Value] = stock;
        return Task.CompletedTask;
    }

    public void Remove(Stock stock)
    {
        _stocks.TryRemove(stock.Id.Value, out _);
    }

    public void Update(Stock stock)
    {
        _stocks[stock.Id.Value] = stock;
    }
}