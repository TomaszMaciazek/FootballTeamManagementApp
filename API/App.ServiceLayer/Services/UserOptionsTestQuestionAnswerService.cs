using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities.AnswersResults;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IUserOptionsTestQuestionAnswerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(UserOptionsTestQuestionAnswer entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<UserOptionsTestQuestionAnswer>> GetAllAsync();
        Task<List<UserOptionsTestQuestionAnswer>> GetAnswers(TestAnswerQuery query);
        Task<UserOptionsTestQuestionAnswer> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UserOptionsTestQuestionAnswer entity);
    }

    public class UserOptionsTestQuestionAnswerService : IService<UserOptionsTestQuestionAnswer>, IUserOptionsTestQuestionAnswerService
    {
        private readonly IUserOptionsTestQuestionAnswerRepository _answersTemplateRepository;
        private readonly IApplicationDbContext _context;

        public UserOptionsTestQuestionAnswerService(IUserOptionsTestQuestionAnswerRepository answersTemplateRepository, IApplicationDbContext context)
        {
            _answersTemplateRepository = answersTemplateRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _answersTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(UserOptionsTestQuestionAnswer entity)
        {
            _answersTemplateRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _answersTemplateRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserOptionsTestQuestionAnswer>> GetAllAsync() => await _answersTemplateRepository.GetAll().ToListAsync();

        public async Task<UserOptionsTestQuestionAnswer> GetByIdAsync(Guid id) => await _answersTemplateRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _answersTemplateRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserOptionsTestQuestionAnswer entity)
        {
            _answersTemplateRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserOptionsTestQuestionAnswer>> GetAnswers(TestAnswerQuery query)
        {
            var answers = _answersTemplateRepository.GetAllEager().Where(x => x.IsActive);

            answers = answers.WhereGuidPropertyEquals(x => x.User.Id, query.UserId);

            answers = answers.WhereCollectionContainsPropertyValue(x => x.Question.Id, query.QuestionsIds);

            return await answers.ToListAsync();
        }
    }
}
