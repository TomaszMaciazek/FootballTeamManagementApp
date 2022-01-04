using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using App.UserMiddleware.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using App.DataAccess.Exceptions;

namespace App.ServiceLayer.Services
{
    public interface IUserService
    {
        Task<bool> Activate(Guid id);
        Task Add(User entity);
        Task<bool> Deactivate(Guid id);
        Task<IEnumerable<SelectUserDto>> GetAll();
        Task<User> GetByLogin(string login);
        Task<User> GetById(Guid id);
        Task Remove(Guid id);
        Task Update(UpdateUserCommandVM command);
        Task<PaginatedList<UserListItemDto>> GetUsers(UserQuery query);
        Task<UserAccountDto> GetUserAccount(Guid id);
        Task UpdatePassword(UpdatePasswordVM command);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IApplicationDbContext context, IMapper mapper)
        {
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Activate(Guid id)
        {
            var userToActivate = await _userRepository.GetById(id).SingleOrDefaultAsync();
            if (userToActivate == null)
            {
                return false;
            }

            userToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Add(User entity)
        {
            _userRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Deactivate(Guid id)
        {
            var objectToDeactivate = await _userRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByLogin(string login) => await _userRepository.GetByEmailOrUsername(login);

        public async Task<IEnumerable<SelectUserDto>> GetAll() => await _userRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.PlayerDetails).ThenInclude(x => x.Country)
            .Include(x => x.PlayerDetails).ThenInclude(x => x.Team)
            .Include(x => x.CoachDetails).ThenInclude(x => x.Country)
            .Include(x => x.Role)
            .ProjectTo<SelectUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<User> GetById(Guid id) => await _userRepository.GetById(id).SingleOrDefaultAsync();

        public async Task Remove(Guid id)
        {
            _userRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UpdateUserCommandVM command)
        {
            var user = await _userRepository.GetById(command.Id).SingleOrDefaultAsync();
            if(user != null)
            {
                user.Email = !string.IsNullOrEmpty(command.Email) ? command.Email : user.Email;
                user.Username = !string.IsNullOrEmpty(command.Email) ? command.Email.ToLower() : user.Username;
                user.PasswordHash = !string.IsNullOrEmpty(command.Password) ? PasswordHashHelper.HashPassword(command.Password) : user.PasswordHash;
                user.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : user.Name;
                user.MiddleName = !string.IsNullOrEmpty(command.MiddleName) ? command.MiddleName : user.MiddleName;
                user.Surname = !string.IsNullOrEmpty(command.Surname) ? command.Surname : user.Surname;
                user.BadLogonCount = command.BadLogonCount ?? user.BadLogonCount;
                user.AccountLockoutTime = command.AccountLockoutTime ?? user.AccountLockoutTime;
                user.LastLogon = command.LastLogon ?? user.LastLogon;
                user.LastPasswordSet = command.LastPasswordSet ?? user.LastPasswordSet;
                user.IsActive = command.IsActive ?? user.IsActive;
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("There is no user with designated Id");
            }
        }

        public async Task<PaginatedList<UserListItemDto>> GetUsers(UserQuery query)
        {
            var users = _userRepository.GetAll();
            users = query.IsActive.HasValue ? users.Where(x => x.IsActive == query.IsActive.Value) : users;

            if (!string.IsNullOrEmpty(query.OrderByColumnName))
            {
                users = users.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }
            else
            {
                users = users.OrderByProperty("Surname", query.OrderByDirection);
            }

            return await users
                .Select(x => new UserListItemDto
                { 
                    Id =  x.Id,
                    IsActive = x.IsActive,
                    Email = x.Email,
                    Surname = x.Surname,
                    Names = $"{x.Name} {x.MiddleName}",
                    UserName = x.Username,
                    Role = new RoleDto { Id = x.Role.Id, Name = x.Role.Name }
                    }
                )
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<UserAccountDto> GetUserAccount(Guid id)
        {
            return await _userRepository.GetById(id)
                .AsNoTracking()
                .Include(x => x.PlayerDetails).ThenInclude(x => x.Team)
                .Include(x => x.PlayerDetails).ThenInclude(x => x.Matches)
                .Include(x => x.CoachDetails).ThenInclude(x => x.Teams)
                .ProjectTo<UserAccountDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task UpdatePassword(UpdatePasswordVM command)
        {
            var user = await _userRepository.GetById(command.UserId).SingleOrDefaultAsync();
            if(user != null)
            {
                if(PasswordHashHelper.Verify(command.OldPassword, user.PasswordHash))
                {
                    user.PasswordHash = PasswordHashHelper.HashPassword(command.NewPassword);
                    _userRepository.Update(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new IncorrectPasswordException();
                }
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
