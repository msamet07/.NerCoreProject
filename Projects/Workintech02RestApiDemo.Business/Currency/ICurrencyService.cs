using Workintech02RestApiDemo.Domain;
using Workintech02RestApiDemo.Domain.ApiLayer;

namespace Workintech02RestApiDemo.Business.Currency
{
    public interface ICurrencyService : IBaseSingletonService
    {
        Task<string> GetCurrencySymbol(string currencyCode);
        Task<CurrencyResponse> GetCurrency();
        Task<ApiLayerResponse> PostCurrencyToApiLayer(string startDate, string endDate);
    }
}