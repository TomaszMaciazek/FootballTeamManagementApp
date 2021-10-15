using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAllAsync();
        Task<Language> GetByIdAsync(Guid id);
    }

    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<List<Language>> GetAllAsync() => await _languageRepository.GetAll().ToListAsync();

        public async Task<Language> GetByIdAsync(Guid id) => await _languageRepository.GetById(id).FirstOrDefaultAsync();
    }
}
