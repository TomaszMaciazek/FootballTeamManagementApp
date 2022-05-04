using App.Model.Dtos;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
        Task<CountryDto> GetCountryByIdAsync(int id);
    }

    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync() => await _countryRepository.GetAll()
            .Select(x => new CountryDto { Id = x.Id, Code = x.Code }).ToListAsync();

        public async Task<CountryDto> GetCountryByIdAsync(int id) => await _countryRepository.GetById(id)
            .Select(x => new CountryDto { Id = x.Id, Code = x.Code }).FirstOrDefaultAsync();
    }
}
