namespace Emporium.Domain.Stocks;
public interface IStockRepository
{
    Task Add(Stock stock);
    void Remove(Stock stock);
    void Update(Stock stock);
}
