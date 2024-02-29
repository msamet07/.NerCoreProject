using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Workintech02RestApiDemo.Domain.Dto;
using Workintech02RestApiDemo.Infrastructure;

namespace Workintech02RestApiDemo.Business.City
{
    public class CityService : BaseService, ICityService
    {
        private readonly IDistributedCache distributedCache;
        private readonly Workintech02CodeFirstContext context;
        private readonly IMapper mapper;
        private const string cacheKey = "CITY_LIST";

        public CityService(Workintech02CodeFirstContext _context,IMapper _mapper, IDistributedCache _distributedCache)
        {
            context = _context;
            mapper = _mapper;
            distributedCache = _distributedCache;
        }

        public CityService(Workintech02CodeFirstContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        private async Task<List<CityDto>> SetCacheCityList()
        {
            List<Domain.Entities.City> cityFromDbList = context.Cities.Include(x => x.Towns).ToList();
            List<CityDto> cities = mapper.Map<List<CityDto>>(cityFromDbList);
            

            if (cities.Count > 0)
            {
                string jsonValues = JsonSerializer.Serialize(cities);
                await distributedCache.SetStringAsync(cacheKey, jsonValues, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }
            return cities;
        }

        public async Task<List<CityDto>> GetCities()
        {
            var fromCacheCityList = distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(fromCacheCityList))
            {
                var cityList = JsonSerializer.Deserialize<List<CityDto>>(fromCacheCityList);
                return cityList;
            }
            return await SetCacheCityList();
        }

        public CityDto GetById(int id) 
        {
            var fromCacheCityList = distributedCache.GetString(cacheKey);
            var responseCity = new CityDto();
            if (!string.IsNullOrEmpty(fromCacheCityList))
            {
                var cityList = JsonSerializer.Deserialize<List<CityDto>>(fromCacheCityList);
                responseCity = cityList.FirstOrDefault(x => x.CityId == id);
            }
            else
            {
                SetCacheCityList();
            }

            if(responseCity!=null && responseCity.CityId > 0) 
            { 
                return responseCity;
            }

            var city = context.Cities.Include(x=>x.Towns).FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("City not found");
            var cityDto = mapper.Map<CityDto>(city);
            distributedCache.Remove(cacheKey);

            return cityDto;
        }

        public CityDto AddCity(string name)
        {
            var cityEntity = new Workintech02RestApiDemo.Domain.Entities.City()
            {
                Name = name,
                CreatedDate = DateTime.Now,
            };

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Domain.Entities.City> insertedCity = context.Cities.Add(cityEntity);
            context.SaveChanges();

            var city = mapper.Map<CityDto>(insertedCity.Entity);

            distributedCache.Remove(cacheKey);
            return city;
        }

        public CityDto  UpdateCity(int id, string name)
        {
            var city = context.Cities.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("City not found");
            city.Name = name;
            context.SaveChanges();

            var cityDto = mapper.Map<CityDto>(city);

            distributedCache.Remove(cacheKey);
            return cityDto;
        }

        public void DeleteCity(int id)
        {
            var city = context.Cities.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("City not found");
            context.Cities.Remove(city);
            context.SaveChanges();

            distributedCache.Remove(cacheKey);
        }
    }

}
