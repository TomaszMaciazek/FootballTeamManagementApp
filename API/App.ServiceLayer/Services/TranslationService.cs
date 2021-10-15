using App.Model.Entities;
using App.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ITranslationService
    {
        Task<IEnumerable<Translation>> GetAllAsync();
        Task<IEnumerable<Translation>> GetAllFromLanguageAsync(Guid languageId);
        Task<Translation> GetByIdAsync(Guid id);
    }

    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<IEnumerable<Translation>> GetAllAsync() => await _translationRepository.GetAll().ToListAsync();

        public async Task<IEnumerable<Translation>> GetAllFromLanguageAsync(Guid languageId) => await _translationRepository.GetAll()
            .Include(x => x.Language)
            .Where(x => x.Language.Id == languageId)
            .ToListAsync();

        public async Task<Translation> GetByIdAsync(Guid id) => await _translationRepository.GetById(id).FirstOrDefaultAsync();
    }
}
