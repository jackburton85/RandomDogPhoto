﻿namespace SPPTest.Services
{
    public interface IApiService<TResult, TData> where TData : class
    {
        Task<TResult> GetDataAsync(TData data);
    }
}
