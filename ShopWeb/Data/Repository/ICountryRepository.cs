﻿namespace ShopWeb.Data
{
    using ShopWeb.Data.Entities;
    using ShopWeb.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();

        Task<Country> GetCountryWithCitiesAsync(int id);

        Task<City> GetCityAsync(int id);

        Task AddCityAsync(CityViewModel model);

        Task<int> UpdateCityAsync(City city);

        Task<int> DeleteCityAsync(City city);

    }
}
