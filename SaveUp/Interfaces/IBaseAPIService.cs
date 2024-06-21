namespace SaveUp.Interfaces
{

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