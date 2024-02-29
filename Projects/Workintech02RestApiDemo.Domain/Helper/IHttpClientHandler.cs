
namespace Workintech02RestApiDemo.Domain.Helper
{
    public interface IHttpClientHandler
    {
        Task<string> GetStringAsync(string url);
    }
}