namespace SPPTest.Services
{
    public interface IApiAddDataService<TResult, TData>
    {
        Task AddDataAsync(TData data);
    }
}
