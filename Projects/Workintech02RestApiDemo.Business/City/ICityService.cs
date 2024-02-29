
using Workintech02RestApiDemo.Domain.Dto;

namespace Workintech02RestApiDemo.Business.City
{
    public interface ICityService:IBaseService
    {
        Task<List<CityDto>> GetCities();
        CityDto GetById(int id);
        CityDto AddCity(string name);
        CityDto UpdateCity(int id, string name);
        void DeleteCity(int id);
    }
}