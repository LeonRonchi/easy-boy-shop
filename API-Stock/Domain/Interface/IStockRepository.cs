using Domain.Model;

namespace Domain.Interface;

public interface IStockRepository
{
    Task<Stock> SaveAsync(Stock stock);
    Task<IEnumerable<Stock>> GetStockAsync();
    Task<Stock?> GetStockByIdAsync(Guid id);
}
