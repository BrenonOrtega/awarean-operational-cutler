using Awarean.Operational.Cutler.Domain;
using Awarean.Sdk.Result;

namespace Awarean.Operational.Cutler.Application.Repositories
{
    public interface IFoodRepository
    {
        Task<Result<string>> SaveAsync(Food food);
        Task<Result> SaveBatchAsync(IEnumerable<Food> foods);
        Task<Result<IEnumerable<object>>> GetFoods<T>(T paginator);
        Task<Result<Food>> GetFoodById(string id);
    }
}