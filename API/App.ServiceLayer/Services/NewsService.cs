using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface INewsService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(News entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<News>> GetAllAsync();
        Task<News> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateNewsCommand command);
        Task<PaginatedList<News>> GetNews(NewsQuery query);
    }

    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IApplicationDbContext _context;

        public NewsService(INewsRepository newsRepository, IApplicationDbContext context)
        {
            _newsRepository = newsRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _newsRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(News entity)
        {
            _newsRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _newsRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<News>> GetAllAsync() => await _newsRepository.GetAll().ToListAsync();

        public async Task<News> GetByIdAsync(Guid id) => await _newsRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _newsRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateNewsCommand command)
        {
            var news = await _newsRepository.GetById(command.Id).SingleOrDefaultAsync();
            if (news != null)
            {
                news.Content = !string.IsNullOrEmpty(command.Content) ? command.Content : news.Content;
                news.Title = !string.IsNullOrEmpty(command.Title) ? command.Title : news.Title;
                news.IsImportant = command.IsImportant ?? news.IsImportant;
                _newsRepository.Update(news);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("There are no news with designated Id");
            }
        }

        public async Task<PaginatedList<News>> GetNews(NewsQuery query)
        {
            var news = _newsRepository.GetAll().AsNoTracking();

            news = news.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await news.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
