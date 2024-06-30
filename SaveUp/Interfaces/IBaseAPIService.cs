namespace SaveUp.Interfaces
{
    /// <summary>
    /// Interface for the BaseAPIService
    /// </summary>
    public interface IBaseAPIServiceBase
    {
        HttpClient Client { get; }
    }

    public interface IBaseAPIService<TCreateRequest, TUpdateRequest, TResponse>
        where TCreateRequest : class
        where TUpdateRequest : class
        where TResponse : class
    { }
}