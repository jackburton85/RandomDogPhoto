namespace SPPTest.Services
{
    public interface IApiClient<TData, TResult>
    {
        Task<TResult> GetDataAsync(TData data);
    }
}
