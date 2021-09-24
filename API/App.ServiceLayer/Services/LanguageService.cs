using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ILanguageService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Language entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Language>> GetAllAsync();
        Task<Language> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Language entity);
    }

    public class LanguageService : IService<Language>, ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IApplicationDbContext _context;

        public LanguageService(ILanguageRepository languageRepository, IApplicationDbContext context)
        {
            _languageRepository = languageRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _languageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Language entity)
        {
            _languageRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _languageRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Language>> GetAllAsync() => await _languageRepository.GetAll().ToListAsync();

        public async Task<Language> GetByIdAsync(Guid id) => await _languageRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _languageRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language entity)
        {
            _languageRepository.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
