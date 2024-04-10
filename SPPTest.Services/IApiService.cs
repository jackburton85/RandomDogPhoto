using SPPTest.Shared.Models;

namespace SPPTest.Services{
    
    public interface IApiService
    {
        Task<T> GetDataAsync<T, TData>(TData data) where TData : class;
    }
}
